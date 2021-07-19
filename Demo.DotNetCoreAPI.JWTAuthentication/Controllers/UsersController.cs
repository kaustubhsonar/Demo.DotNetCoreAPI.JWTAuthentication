using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DotNetCoreAPI.JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.DotNetCoreAPI.JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public UsersController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(UserCred userCred)        
        {
            var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);            
            if (token == null)
                return Unauthorized();
            return Ok(token);
            
        }

    }
}
