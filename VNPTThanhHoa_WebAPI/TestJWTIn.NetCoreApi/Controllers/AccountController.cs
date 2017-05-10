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
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        //private static bool _databaseChecked;
        //private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            PeopleDbContext Peopledb,
            IOptions<JwtIssuerOptions> JwtOptions
            //IEmailSender emailSender,
            //ISmsSender smsSender,
            //ILoggerFactory loggerFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Peopledb = Peopledb;
            _jwtOptions = JwtOptions.Value;
            //_emailSender = emailSender;
            //_smsSender = smsSender;
            //_logger = loggerFactory.CreateLogger<AccountController>();
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
                //Create JWT and encode
                var Jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: Princial.Claims,
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
        //private readonly PeopleDbContext _context;

        //public AccountController(PeopleDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Contact
        [HttpGet("get")]
        public IEnumerable<Contact> GetContact()
        {
            return _Peopledb.Contact;
        }
        [HttpGet("get")]
        public UserManager<ApplicationUser> GetAccount()
        {
            var user = _userManager;
            //return _userManager.Users.FirstOrDefault();
            return user;
        }
    }
}