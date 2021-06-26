using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Models.Entities;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;
using IoTDevicesMonitor.Services;
using IoTDevicesMonitor.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("/api/user")]
    [Authorize(Policy = "User")]
    public class UserController : Controller {
        private UserManager userManager;
        private AppDbContext dbContext;

        public UserController (UserManager userManager, AppDbContext dbContext) {
            this.userManager = userManager;
            this.dbContext = dbContext;
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

        // [HttpGet("{username}/device/")]
        // public IActionResult GetDevices([FromQuery] string username) {
        //     bool gotInfo = userManager.GetUser(username, out var user, out var error);
        //     if(gotInfo) return Ok(new {
        //         user.Devices
        //     });
        //     return BadRequest(new {error});
        // }

        [HttpGet("{username}/notification/")]
        public IActionResult GetAlert(string username) {
            // TODO: get alert from database
            var user = dbContext.Users.Include(u => u.Devices)
                    .ThenInclude(d => d.Alerts)
                    .FirstOrDefault(u => u.Username == username);
            if(user == null)
                return Ok(new {
                    notification = new [] {
                        new AlertModel {
                            Content = "Test notification list", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                        new AlertModel {
                            Content = "Xuông dưới mức nhiệt độ quy định", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                        new AlertModel {
                            Content = "Xuông dưới mức độ ẩm quy định", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                        new AlertModel {
                            Content = "Xuông dưới mức độ ẩm quy định", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                        new AlertModel {
                            Content = "Xuông dưới mức độ ẩm quy định", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                        new AlertModel {
                            Content = "Vượt quá mức nhiệt độ quy định", 
                            Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                        },
                    }
                });
            return Ok(new {
                notification = user.Devices.SelectMany(d => d.Alerts).Select(a => new AlertModel{
                    Content = a.Content,
                    Time = a.TimeAlert.ToString("dd/MM/yyyy HH:mm"),
                }),
            });
        }

        [HttpGet("{username}/device/")]
        public IActionResult GetDevice(string username, int deviceId) {
            var user = dbContext.Users.FirstOrDefault(u => u.Username == username);
            if(user == null) return BadRequest(new {error = "User dont exist"});
            var devices = dbContext.Devices.Include(d => d.LightModule)
                                        .Include(d => d.TempModule)
                                        .Include(d => d.HumiModule)
                                        .Where(d => d.Username == username)
                                        .Select(d => d).ToList();
            // var deviceResponse = new List<DeviceInfoModel>();
            // foreach(var device in devices) {
            //     deviceResponse.Add(device.ToDeviceInfoResponse());
            // }
            return Ok(new {
                devices = devices.Select<DeviceEntity, DeviceInfoModel>(d => d.ToDeviceInfoResponse())
            });
        }

        // [HttpGet("{username}/device/{deviceId}/light-module")]
        // public IActionResult GetLightModule(string username, int deviceId) {
        //     var device = dbContext.Devices
        //                         .Include(d => d.LightModule)
        //                         .FirstOrDefault(d => d.Username == username && d.DeviceId == deviceId);
        //     if(device == null) return BadRequest(new {error = "user or device not exist"});
        //     return Ok(new {
        //         lightState = device.LightModule.State,
        //         lightOnOption = device.LightModule.TimeOnOption,
        //         lightOffOption = device.LightModule.TimeOffOption,
        //         timeOn = device.LightModule?.TimeOn.ToString("HH:mm"),
        //         timeOff = device.LightModule?.TimeOff.ToString("HH:mm"),
        //         auto = device.LightModule.Auto,
        //     });
        // }

        // [HttpGet("{username}/device/{deviceId}/temp")] 
        // public IActionResult GetTemp(string username, int deviceId) {
        //     // TODO
        //     return Ok(new {Temp = 31});
        // }

        // [HttpGet("{username}/device/{deviceId}/temp/day")]
        // public IActionResult GetTempDay(string username, int deviceId) {
        //     return Ok(new {
        //         tempWeek = new [] {
        //             new TimeValueModel{Time = "1h ago", Value = 30},
        //             new TimeValueModel{Time = "2h ago", Value = 31},
        //             new TimeValueModel{Time = "3h ago", Value = 32},
        //             new TimeValueModel{Time = "4h ago", Value = 33},
        //             new TimeValueModel{Time = "5h ago", Value = 30},
        //         }
        //     });
        // }
        // [HttpGet("{username}/device/{deviceId}/temp/week")]
        // public IActionResult GetTempWeek(string username, int deviceId) {
        //     return Ok(new {
        //         tempWeek = new [] {
        //             new TimeValueModel{Time = "MON", Value = 30},
        //             new TimeValueModel{Time = "TUE", Value = 31},
        //             new TimeValueModel{Time = "WED", Value = 32},
        //             new TimeValueModel{Time = "THU", Value = 33},
        //             new TimeValueModel{Time = "FRI", Value = 30},
        //             new TimeValueModel{Time = "SAT", Value = 29},
        //             new TimeValueModel{Time = "SUN", Value = 25},
        //         }
        //     });
        // }
        // [HttpGet("{username}/device/{deviceId}/temp/month")]
        // public IActionResult GetTempMonth(string username, int deviceId) {
        //     return Ok(new {
        //         tempWeek = new [] {
        //             new TimeValueModel{Time = "1 week ago", Value = 30},
        //             new TimeValueModel{Time = "2 week ago", Value = 31},
        //             new TimeValueModel{Time = "3 week ago", Value = 32},
        //             new TimeValueModel{Time = "4 week ago", Value = 33},
        //         }
        //     });
        // }
    }

    // public class TimeValueModel {
    //     public string Time { get; set; }
    //     public int Value { get; set; }
    // }
}