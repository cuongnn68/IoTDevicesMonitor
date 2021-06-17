using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Requests {
    public class UserLoginModel {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}