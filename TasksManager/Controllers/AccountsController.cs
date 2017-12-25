using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TasksManager.DataAccess;
using TasksManager.ViewModel.Auth;

namespace TasksManager.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAuthService authorizationService;

        public AccountsController(IAuthService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthorizeRequest request)
        {
            var response = await authorizationService.LoginAsync(request.Username, request.Password);
            if (response!=null)
            {
                return Ok(response);
            }
            return NotFound("Login failed");
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await authorizationService.RegisterAsync(request.Username, request.Password);
            if (response!=null)
            {
                return Ok(response);
            }
            return BadRequest("Registration Failed.");
        }
    }
}
