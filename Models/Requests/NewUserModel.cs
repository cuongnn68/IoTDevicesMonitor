using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IoTDevicesMonitor.Models.Entities;

namespace IoTDevicesMonitor.Models.Requests {
    public class NewUserModel {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<NewDeviceModel> Devices { get; set; }
    }
}