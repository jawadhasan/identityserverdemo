using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IDPDemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //todo: microsoft ns
            var claims1 =HttpContext.User.Claims.Select(claim => new {claim.Type, claim.Value}).ToList();
            var claims2 = from c in User.Claims select new {c.Type, c.Value};
            return Ok(claims1);
        }
    }
}
