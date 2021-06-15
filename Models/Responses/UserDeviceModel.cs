using System.Collections;
using System.Collections.Generic;

namespace IoTDevicesMonitor.Models.Respones {
    public class UserDeviceModel {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<DeviceModel> Devices { get; set; }
    }
}