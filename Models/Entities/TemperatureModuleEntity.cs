using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class TemperatureModuleEntity {
        [Key]
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public int Value { get; set; }
        public int Upperbound { get; set; }
        public int Lowerbound { get; set; }
        public bool UpperAlertOption { get; set; } = false;
        public bool LowerAlertOption { get; set; } = false;

        public DeviceEntity Device { get; set; }
    }
}