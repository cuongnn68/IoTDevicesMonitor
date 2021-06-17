using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Services;
using IoTDevicesMonitor.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : Controller {
        AppDbContext dbContext;
        JwtServices jwtServices;

        public AuthorizationController(
            AppDbContext dbContext,
            JwtServices jwtServices
        ) {
            this.dbContext = dbContext;
            this.jwtServices = jwtServices;
        }

        [HttpPost("admin-token")]
        public IActionResult CreateAdminToken(AdminModel account) {
            var adminAccount = dbContext.AdminAccounts.Where(e => e.Admin == account.Admin).FirstOrDefault();
            if(adminAccount == null || adminAccount.HPassword != account.Password) {
                return BadRequest(new {error = "Wrong account or password"});
            }
            return Ok(new {adminToken = jwtServices.CreateAdminToken(adminAccount)});    
        }

        [HttpGet("admin-token")]
        [Authorize(Policy = "Admin")]
        public IActionResult CheckAdminToken() {
            return Ok(new {token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last()});
        }

        [HttpPost("user-token")]
        public IActionResult CreateUserToken(UserLoginModel userLogin) {
            var user = dbContext.Users.FirstOrDefault(u => u.Username == userLogin.Username);
            if(user == null || (user.HPassword != userLogin.Password)) 
                return BadRequest(new {error = "Wrong username or password"});
            return Ok(new {userToken = jwtServices.CreateUserToken(user)});
        }

        [HttpGet("user-token")]
        [Authorize(Policy = "User")]
        public IActionResult CheckUsertoken() {
            return Ok(new {userToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last()});
        }

    }

}