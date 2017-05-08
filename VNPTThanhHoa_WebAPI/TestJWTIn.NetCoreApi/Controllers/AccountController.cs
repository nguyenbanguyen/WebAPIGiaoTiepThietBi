using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestJWTIn.NetCoreApi.Models;
using TestJWTIn.NetCoreApi.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace TestJWTIn.NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        // nạp dữ liệu từ db vào _userManager
        private readonly UserManager<ApplicationUser> _userManager;
        private AccountController(UserManager<ApplicationUser> UserManager)
        {
            _userManager = UserManager;
        }
        [HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                }, model.Password
                );
            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }
            return Ok();
        }

    }
}