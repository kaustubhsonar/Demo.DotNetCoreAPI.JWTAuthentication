using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAPI.TokenAuthentication.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAPI.TokenAuthentication.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;

        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public IActionResult Authenticate(UserCred userCred)
        {
            if (tokenManager.Authenticate(userCred.Username, userCred.Password))
            {
                return Ok(new { Token = tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized.");
                return Unauthorized(ModelState);
            }
        }
    }
    public class UserCred
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
