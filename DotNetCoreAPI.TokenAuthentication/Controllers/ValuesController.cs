using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAPI.TokenAuthentication.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAPI.TokenAuthentication.Controllers
{
    [TokenAuthenticationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IActionResult GetValues()
        {
            return Ok("List of values ");
        }
    }
}
