using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Requests {
    public class AdminModel {
        [Required]
        public string Admin { get; set; }
        [Required]
        public string Password { get; set; }
    }
}