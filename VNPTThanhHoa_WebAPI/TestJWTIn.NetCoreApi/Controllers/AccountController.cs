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

namespace TestJWTIn.NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        // nạp dữ liệu từ db vào _userManager
        //private UserManager<ApplicationUser> _userManager;

        //private AccountController(UserManager<ApplicationUser> UserManager)
        //{
        //    _userManager = UserManager;
        //}


        //Dependency Injection for Account Controller

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PeopleDbContext _Peopledb;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        //private static bool _databaseChecked;
        //private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            PeopleDbContext Peopledb
            //IEmailSender emailSender,
            //ISmsSender smsSender,
            //ILoggerFactory loggerFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Peopledb = Peopledb;
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
            var user = new ApplicationUser { UserName = UserName, Email = Email };
            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                return true;
            }

            return false;
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