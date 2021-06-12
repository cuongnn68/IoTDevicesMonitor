using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IoTDevicesMonitor.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : Controller {
        [HttpGet("test1")]
        public IActionResult Test1 () {
            return Ok(new {mess="test1"});
        }
        
        [HttpGet("test2")]
        [Authorize]
        public IActionResult Test2 () {
            return Ok(new {mess="passed authentication"});
        }

        [HttpGet("test3")]
        [Authorize(Policy = "Yomama")]
        public IActionResult Test3() {
            return Ok(new {mess="passed authorization"});
        }

        [HttpGet("token-test")]
        public IActionResult GetTokenTest () {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConstains.TestKey));
            var descriptor = new SecurityTokenDescriptor {
                IssuedAt = DateTime.Now,                
                Expires = DateTime.Now.AddMinutes(5),
                // TokenType = "JWT",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };
            var handle = new JwtSecurityTokenHandler();
            var token = handle.CreateJwtSecurityToken(descriptor);
            return Ok(new {token = handle.WriteToken(token)});
        }
        [HttpGet("token-test-with-claim")]
        public IActionResult GetTokenTestWithClaim () {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConstains.TestKey));
            var descriptor = new SecurityTokenDescriptor {
                IssuedAt = DateTime.Now,                
                Expires = DateTime.Now.AddMinutes(5),
                TokenType = "JWT",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Claims = new Dictionary<string, object> {
                    {"Mama", "Just kill a man"},
                }
            };
            var handle = new JwtSecurityTokenHandler();
            var token = handle.CreateJwtSecurityToken(descriptor);
            return Ok(new {token = handle.WriteToken(token)});
        }

        [HttpPost("token")] // TODO this
        public IActionResult Token(string username, string password) {
            return Ok(new {yo="mama"});
        }

        [HttpPost("validate")] // TODO this
        [Authorize]
        public IActionResult Validate(string token) {
            return Ok(new {uhm="ok"});
        }
    }

}