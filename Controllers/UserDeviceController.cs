using System;
using System.Linq;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Models.Entities;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;
using IoTDevicesMonitor.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("/api/device")]
    [Authorize]
    public class UserDeviceController : Controller {
        private AppDbContext dbContext;

        public UserDeviceController(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet("{deviceId}/light")]
        public IActionResult GetLightModule(string username, int deviceId) {
            var lightModule = dbContext.LightModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(lightModule == null) return BadRequest(new {error = "device not exist or dont have light module"});
            return Ok(new {
                lightState = lightModule.State,
                lightOnOption = lightModule.TimeOnOption,
                lightOffOption = lightModule.TimeOffOption,
                timeOn = lightModule?.TimeOn.ToString("HH:mm"),
                timeOff = lightModule?.TimeOff.ToString("HH:mm"),
                auto = lightModule.Auto,
            });
        }

        [HttpPost("{deviceId}/light")]
        public IActionResult CreateLightState([FromRoute] int deviceId, [FromBody] NewLightStateModel state) {
            // TODO realtime
            var lightModule = dbContext.LightModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(lightModule == null) return BadRequest(new {error = "device not exist or dont have light module"});
            lightModule.State = state.State;
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{deviceId}/light")]
        public IActionResult UpdateLightModule(int deviceId, UpdateLightModuleModel lightModule) {
            var lightModuleEntity = dbContext.LightModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(lightModule == null) return BadRequest(new {error = "device not exist or dont have light module"});
            lightModuleEntity.TimeOn = DateTime.ParseExact(lightModule.TimeOn, "HH:mm", null);
            lightModuleEntity.TimeOff = DateTime.ParseExact(lightModule.TimeOff, "HH:mm", null);
            lightModuleEntity.TimeOnOption = lightModule.TimeOnOption;
            lightModuleEntity.TimeOffOption = lightModule.TimeOffOption;
            lightModuleEntity.Auto = lightModule.Auto;
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{deviceId}/temp")]
        public IActionResult GetTempModule(int deviceId) {
            var tempModule = dbContext.TempModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(tempModule == null) return BadRequest(new {error = "device not exist or dont have temperature module"});
            return Ok(new {
                Value = tempModule.Value,
                UpperAlertOptions = tempModule.UpperAlertOption,
                LowerAlertOptions = tempModule.LowerAlertOption,
                Upperbound = tempModule.Upperbound,
                Lowerbound = tempModule.Lowerbound,
            });
        }

        [HttpPut("{deviceId}/temp")]
        public IActionResult UpdateTempModule(int deviceId, UpdateTempModuleModel updateTemp) {
            var tempModule = dbContext.TempModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(tempModule == null) return BadRequest(new {error = "device not exist or dont have temperature module"});
            tempModule.UpperAlertOption = updateTemp.UpperAlertOptions;
            tempModule.Upperbound = updateTemp.Upperbound;
            tempModule.LowerAlertOption = updateTemp.LowerAlertOptions;
            tempModule.Lowerbound = updateTemp.Lowerbound;
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{deviceId}/temp/day")]
        public IActionResult GetTempOfDay(int deviceId) {
            // TODO get real data
            return Ok(new {data = new [] {
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-1).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 31,
                    Time = DateTime.Now.AddHours(-2).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-3).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-4).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 31,
                    Time = DateTime.Now.AddHours(-5).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 31,
                    Time = DateTime.Now.AddHours(-6).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 31,
                    Time = DateTime.Now.AddHours(-7).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-8).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-9).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 30,
                    Time = DateTime.Now.AddHours(-10).ToString("HH") + "h",
                },
            }});
        }

        [HttpGet("{deviceId}/temp/week")]
        public IActionResult GetTempOfWeek(int deviceId) {
            // TODO get real data
            // TODO get day
            return Ok(new {data = new [] {
                new RecordModel{
                    Value = 30,
                    Time = "Mon",
                },
                new RecordModel{
                    Value = 31,
                    Time = "Tue",
                },
                new RecordModel{
                    Value = 32,
                    Time = "Wed",
                },
                new RecordModel{
                    Value = 35,
                    Time = "Thu",
                },
                new RecordModel{
                    Value = 39,
                    Time = "Fri",
                },
                new RecordModel{
                    Value = 31,
                    Time = "Sat",
                },
                new RecordModel{
                    Value = 30,
                    Time = "Sun",
                },
            }});
        }

        [HttpPost("{deviceId}/temp")] // TODO test
        public IActionResult CreateTempRecord([FromRoute] int deviceId, [FromBody] NewRecordModel newTemp) {
            // TODO realtime
            var tempModule = dbContext.TempModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(tempModule == null) return BadRequest(new {error = "device not exist or dont have temperature module"});
            tempModule.Value = newTemp.Value;
            dbContext.Add(newTemp.ToTempRecordEntity(deviceId));
            dbContext.SaveChanges();
            return Ok();
        }







        [HttpGet("{deviceId}/humi")]
        public IActionResult GetHumiModule(int deviceId) {
            var humiModule = dbContext.HumiModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(humiModule == null) return BadRequest(new {error = "device not exist or dont have temperature module"});
            return Ok(new {
                Value = humiModule.Value,
                Upperbound = humiModule.Upperbound,
                Lowerbound = humiModule.Lowerbound,
                Auto = humiModule.Auto,
            });
        }

        [HttpPut("{deviceId}/humi")]
        public IActionResult UpdateHumiModule(int deviceId, UpdateHumiModuleModel updateTemp) {
            var humiModule = dbContext.HumiModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(humiModule == null) return BadRequest(new {error = "device not exist or dont have temperature module"});
            humiModule.Auto = updateTemp.Auto;
            humiModule.Upperbound = updateTemp.Upperbound;
            humiModule.Lowerbound = updateTemp.Lowerbound;
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{deviceId}/humi/day")]
        public IActionResult GetHumiOfDay(int deviceId) {
            // TODO get real data
            return Ok(new {data = new [] {
                new RecordModel{
                    Value = 70,
                    Time = DateTime.Now.AddHours(-1).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 71,
                    Time = DateTime.Now.AddHours(-2).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 70,
                    Time = DateTime.Now.AddHours(-3).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 70,
                    Time = DateTime.Now.AddHours(-4).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 68,
                    Time = DateTime.Now.AddHours(-5).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 68,
                    Time = DateTime.Now.AddHours(-6).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 68,
                    Time = DateTime.Now.AddHours(-7).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 66,
                    Time = DateTime.Now.AddHours(-8).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 65,
                    Time = DateTime.Now.AddHours(-9).ToString("HH") + "h",
                },
                new RecordModel{
                    Value = 64,
                    Time = DateTime.Now.AddHours(-10).ToString("HH") + "h",
                },
            }});
        }

        [HttpGet("{deviceId}/humi/week")]
        public IActionResult GetHumiOfWeek(int deviceId) {
            // TODO get real data
            // TODO get day
            return Ok(new {data = new [] {
                new RecordModel{
                    Value = 70,
                    Time = "Mon",
                },
                new RecordModel{
                    Value = 71,
                    Time = "Tue",
                },
                new RecordModel{
                    Value = 72,
                    Time = "Wed",
                },
                new RecordModel{
                    Value = 75,
                    Time = "Thu",
                },
                new RecordModel{
                    Value = 79,
                    Time = "Fri",
                },
                new RecordModel{
                    Value = 71,
                    Time = "Sat",
                },
                new RecordModel{
                    Value = 70,
                    Time = "Sun",
                },
            }});
        }
        

        [HttpPost("{deviceId}/humi")] // TODO test
        public IActionResult CreateHumiRecord([FromRoute] int deviceId, [FromBody] NewRecordModel newHumi) {
            // TODO realtime
            var humiModule = dbContext.HumiModules.FirstOrDefault(m => m.DeviceId == deviceId);
            if(humiModule == null) return BadRequest(new {error = "device not exist or dont have humidity module"});
            humiModule.Value = newHumi.Value;
            dbContext.Add(newHumi.ToHumiRecordEntity(deviceId));
            dbContext.SaveChanges();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{deviceId}/state")]
        public IActionResult NewRecord([FromRoute]int deviceId, [FromBody] NewStateModel newState) {
            var header = HttpContext.Request.Headers;
            if(header.ContainsKey("Authorization"))
                Console.WriteLine($@"Authorization: {header["Authorization"]}");
            if(header.ContainsKey("Content-Type"))
                Console.WriteLine($@"Content-Type: {header["Content-type"]}");
            if(header.ContainsKey("Accept"))
                Console.WriteLine($@"Accept: {header["Accept"]}");
            Console.WriteLine("requtested ------------------");
            Console.WriteLine($"temp: {newState.Temp}");
            Console.WriteLine($"time temp: {newState.TimeTemp}");
            Console.WriteLine($"humi: {newState.Humi}");
            Console.WriteLine($"time humi: {newState.TimeHumi}");
            var device = dbContext.Devices
                            .Include(d => d.LightModule)
                            .Include(d => d.TempModule)
                            .Include(d => d.HumiModule)
                            .FirstOrDefault(d => d.DeviceId == deviceId);
            if(device == null)
                return Ok(new {
                    autoLight = false,
                    lightState = false,
                    autoPump = false,
                    lowerBoundSoilHumi = 100,
                    upperBoundSoilHumi = 100,
                });
            if(device.HaveTempModule) {

                var tempModule = dbContext.TempModules.FirstOrDefault(m => m.DeviceId == deviceId);
                if(tempModule.UpperAlertOption 
                && tempModule.Value < tempModule.Upperbound 
                && newState.Temp > tempModule.Upperbound) {
                    dbContext.Alerts.Add(new AlertEntity{
                        DeviceId = deviceId,
                        Content = "Nhiệt độ vướt quá mức cảnh báo!",
                        TimeAlert = DateTime.Now,
                    });
                }
                if(tempModule.LowerAlertOption 
                && tempModule.Value > tempModule.Lowerbound 
                && newState.Temp < tempModule.Lowerbound) {
                    dbContext.Alerts.Add(new AlertEntity{
                        DeviceId = deviceId,
                        Content = "Nhiệt độ xuống dưới mức cảnh báo!",
                        TimeAlert = DateTime.Now,
                    });
                }
                tempModule.Value = newState.Temp;
                dbContext.TempRecords.Add(new TempRecordEntity{
                    DeviceId = deviceId,
                    Value = newState.Temp,
                    Time = (newState.TimeTemp == "auto" ? DateTime.Now : DateTime.Parse(newState.TimeTemp)) 
                });
            }
            if(device.HaveHumidityModule) {
                dbContext.HumiModules.FirstOrDefault(m => m.DeviceId == deviceId).Value = newState.Humi;
                dbContext.HumiRecords.Add(new HumiRecordEntity{
                    DeviceId = deviceId,
                    Value = newState.Humi,
                    Time = (newState.TimeHumi == "auto" ? DateTime.Now : DateTime.Parse(newState.TimeHumi)),
                });
            }
            dbContext.SaveChanges();
            return Ok(new {
                autoLight = (device.HaveLightModule) ? device.LightModule.Auto : false,
                lightState = (device.HaveLightModule) ? device.LightModule.State : false,
                autoPump = (device.HaveHumidityModule) ? device.HumiModule.Auto : false,
                lowerBoundSoilHumi = (device.HaveHumidityModule) ? device.HumiModule.Lowerbound : 100,
                upperBoundSoilHumi = (device.HaveHumidityModule) ? device.HumiModule.Upperbound : 100,
            });
        }
    }
}