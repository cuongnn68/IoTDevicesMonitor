using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Model.Requests {
    public class AdminModel {
        [Required]
        public string Admin { get; set; }
        [Required]
        public string Password { get; set; }
    }
}