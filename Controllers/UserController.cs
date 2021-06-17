using System.Threading.Tasks;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;
using IoTDevicesMonitor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("/api/user")]
    [Authorize(Policy = "User")]
    public class UserController : Controller {
        private UserManager userManager;

        public UserController (UserManager userManager) {
            this.userManager = userManager;
        }

        // TODO check if username own what is querrying
        [HttpGet("{username}")]
        public IActionResult GetUserInfo([FromRoute] string username) {
            bool gotInfo = userManager.GetUser(username, out var user, out var error);
            if(gotInfo) return Ok(new {
                username = user.Username,
                fullName = user.FullName,
                phone = user.Phone,
                email = user.Email,
                // devices = user.Devices, // TODO value of module
            });
            return BadRequest(new {error});
        }

        [HttpPut("{username}")]
        public IActionResult UpdateUser(UpdatedUserModel updatedUser) {
            var isUpdated = userManager.UpdateUserInfo(updatedUser, out var error);
            if(isUpdated) return Ok();
            return BadRequest(new {error});
        }

        [HttpGet("{username}/device/")]
        public IActionResult GetDevices([FromQuery] string username) {
            bool gotInfo = userManager.GetUser(username, out var user, out var error);
            if(gotInfo) return Ok(new {
                user.Devices
            });
            return BadRequest(new {error});
        }



        
    }
}