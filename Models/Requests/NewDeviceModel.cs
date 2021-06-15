using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Requests {
    public class NewDeviceModel {
        public string Name { get; set; }
        [Required]
        public bool HasLight { get; set; }
        [Required]
        public bool HasTemp { get; set; }
        [Required]
        public bool HasHumi { get; set; }
        [Required]
        public bool HasPH { get; set; }
    }
}