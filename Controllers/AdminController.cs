using System;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Services;
using IoTDevicesMonitor.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("api/admin")]
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller {
        UserManager userManager;
        public AdminController(UserManager userManager) {
            this.userManager = userManager;
        }

        public class Test {
            public int Num { get; set; }
            public string Str { get; set; }
        }
        
        [HttpPost("user/test")]
        public IActionResult TestAction(Test test) {
            // Console.WriteLine(num + str);
            Console.WriteLine(test.Num);
            return Ok("TODO");
        }

        // ---------------------------------------
        [HttpGet("user")]
        public IActionResult GetUsers([FromQuery] UserSearchModel search) {
            Console.WriteLine(search?.Keyword + search.Page + search.RowPerPage);
            var userList = userManager.SearchUser(search, out int total);
            return Ok(new {
                userList,
                total,
                search.Page,
                search.RowPerPage,
                search.Keyword,
            });
        }

        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] NewUserModel newUser) {
            var isCreated = userManager.CreateUser(newUser, out string error);
            if(!isCreated) return BadRequest(new {error});
            return Ok();
        }

        [HttpGet("user/{username}")]
        public IActionResult GetUser(string username) {
            if(string.IsNullOrWhiteSpace(username)) return BadRequest(new {error = "Username not valid"});
            bool userFound = userManager.GetUser(username, out var user, out string error);
            if(!userFound) {
                return BadRequest(new {error});
            }
            return Ok(user);
        }

        [HttpPut("user/{username}")]
        public IActionResult UpdateUser(UpdatedUserModel user, string username) {
            if(string.IsNullOrWhiteSpace(username) || username != user.Username) return BadRequest();
            var isUpdated = userManager.UpdateUserInfo(user, out var error);
            if(isUpdated) {
                return Ok();
            }
            return BadRequest(new {error});
        }
        [HttpDelete("user/{username}")]
        public IActionResult DeleteUser(string username) {
            if(string.IsNullOrWhiteSpace(username)) return BadRequest();
            var isDeleted = userManager.DeleteUser(username, out string error);
            if(!isDeleted) return BadRequest(new {error});
            return Ok();
        }

        [HttpPost("user/{username}/device")]
        public IActionResult CreateDevice(NewDeviceModel newDevice, [FromRoute] string username) {
            Console.WriteLine($"test [ username: {username} ]");
            bool isCreated = userManager.AddDevice(username, newDevice, out string error);
            if(!isCreated) return BadRequest(new {error});
            return Ok();
        }

        [HttpDelete("user/{username}/device/{deviceId}")] // TODO test
        public IActionResult DeleteDevice(string username, int deviceId) {
            bool isDelete = userManager.DeleteDevice(username, deviceId, out string error);
            if(!isDelete) return BadRequest(new {error});
            return Ok();
        }
    }
}