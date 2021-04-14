using System.Linq;
using IoTDevicesMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IoTDevicesMonitor.Controllers {
    [ApiController]
    [Route("devices-api")]
    public class DeviceController : Controller {
        private IHubContext<SignalrHub> hubContext;
        private DeviceState deviceState;

        public DeviceController(
            IHubContext<SignalrHub> hubContext,
            DeviceState deviceState
        ) {
            this.hubContext = hubContext;
            this.deviceState = deviceState;
        }
        
        [HttpGet("string-state")]
        public IActionResult GetStringState() {
            return Ok(deviceState.StringStates);
        }

        [HttpGet("bool-state")]
        public IActionResult GetBoolState() {
            return Ok(deviceState.BoolStates);
        }

        [HttpPut("string-state/{name}/state/{state}")]
        public IActionResult ChangeStateString(string name, string state) {
            if(!deviceState.StringStates.ContainsKey(name)) 
                return NotFound(new ErrorModel{Error = "State not exist"});
            deviceState.StringStates[name] = state;
            return Ok();
        }

        [HttpPut("bool-state/{name}/state/{state}")]
        public IActionResult ChangeStateBool(string name, bool state) {
            if(!deviceState.BoolStates.ContainsKey(name)) 
                return NotFound(new ErrorModel{Error = "State not exist"});
            deviceState.BoolStates[name] = state;
            return Ok();
        }
        
    }
}