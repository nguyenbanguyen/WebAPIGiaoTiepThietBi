using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestJWTIn.NetCoreApi.Models;
using TestJWTIn.NetCoreApi.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestJWTIn.NetCoreApi.Options;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace TestJWTIn.NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
 


        //Dependency Injection for Account Controller

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PeopleDbContext _Peopledb;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        //private static bool _databaseChecked;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            PeopleDbContext Peopledb,
            IOptions<JwtIssuerOptions> JwtOptions,
            //IEmailSender emailSender,
            //ISmsSender smsSender,
            ILoggerFactory loggerFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Peopledb = Peopledb;
            _jwtOptions = JwtOptions.Value;
            //_emailSender = emailSender;
            //_smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        [HttpPost]
        public async Task<IActionResult> RegisterTemplate([FromBody] RegisterViewModel model)
        {
            //var _User = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var user = new ApplicationUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            var result = await _userManager.CreateAsync(user,  model.Password);

            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }

            return Ok();
        }
        [HttpPost("post")]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        public async Task<bool> Register(string UserName, string Email, String Password)
        {
            int n = 9;
            int[] d = new int[n - 1];
            var user = new ApplicationUser { UserName = UserName, Email = Email };
            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            var result = await _userManager.CheckPasswordAsync(user,model.Password);
            if (result)
            {
                var Princial = await _signInManager.CreateUserPrincipalAsync(user);
                var claims=new Claim("Role", "Superman");
                 
                 
                // add lẻ claim vào claims của princial, chưa hoạt động
                // IEnumerable doen't support add. but why we can use append?
                // tạo mới princial claim và khai báo bằng claim.append :V this is how append work for! stupid at all
                var addedprincial= Princial.Claims.Append<Claim>(claims);
               
                //tạo mới list claim và add claim từ princial và claim mới vào
                //List<Claim> testclaim = new List<Claim>();
                //testclaim.AddRange(Princial.Claims.ToList<Claim>());
                //testclaim.Add(claims);


                
                //Create JWT and encode
                var Jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: addedprincial,
                    notBefore: _jwtOptions.NotBefore,
                    expires:_jwtOptions.Expiration,
                    signingCredentials:_jwtOptions.SigningCredentials
                    );
                
                var EncodedJwt = new JwtSecurityTokenHandler().WriteToken(Jwt);
                var response = new
                {
                    access_token = EncodedJwt,
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };
                return new JsonResult(response);
            }
            throw new NotImplementedException();
        }
        [HttpGet("TestJson")]
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGeneration == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGeneration));
            }
        }
            
        //private readonly PeopleDbContext _context;

        //public AccountController(PeopleDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Contact
        [HttpGet("test")]
        public SigningCredentials testSecretKey()
        {
             string SecretKey = "ThisStringToMakeUniqueKeyIsn'tIt?";
          SymmetricSecurityKey _SignInKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(SecretKey));
           var sign=  new SigningCredentials(_SignInKey, SecurityAlgorithms.HmacSha256);

            return sign;
        }
        [HttpGet("get")]
        public UserManager<ApplicationUser> GetAccount()
        {
            var user = _userManager;
            //return _userManager.Users.FirstOrDefault();
            return user;
        }

        [HttpPost("GaSieuPham")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromForm] string applicationUser)
        {
            var identity = await GetClaimsIdentity(applicationUser);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({applicationUser }) ");
                return BadRequest("Invalid credentials");
            }

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, applicationUser),
               new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGeneration()),
               new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
               identity.FindFirst("DisneyCharacter")
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
        private static long ToUnixEpochDate(DateTime date)
     => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static   Task<ClaimsIdentity> GetClaimsIdentity(string user)
        {
            if (user == "GaSieuPham")
            {
                var genericiden = new GenericIdentity(user, "Token");
                return Task.FromResult(new ClaimsIdentity(genericiden,
                    new[]
                    {
                        new Claim("DisneyCharacter","IAmMickey")
                    }
                    ));
            }
            if (user == "NotGaSieuPham")
            {
                var genericiden = new GenericIdentity(user, "Token");
                return Task.FromResult(new ClaimsIdentity(genericiden, new Claim[] { }));
            }
            //cere invalid or doen't find account
            return Task.FromResult<ClaimsIdentity>(null);
        }
        [HttpGet("AuthenGaSieuPham")]
        [Authorize("guest")]
        public IActionResult Get() {
            var response = new
            {
                made_it = "Welcome GaSieuPham !"
            };
            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpGet("AuthenSuperMan")]
        [Authorize("SuperMan")]
        public IActionResult GetSuperMan()
        {
            var response = new
            {
                made_it = "Welcome SuperMan !"
            };
            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

    }
}