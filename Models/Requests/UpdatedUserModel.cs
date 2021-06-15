using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IoTDevicesMonitor.Models.Entities;

namespace IoTDevicesMonitor.Models.Requests {
    public class UpdatedUserModel {
        [Required]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}