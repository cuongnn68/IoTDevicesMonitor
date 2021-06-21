using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class HumiModuleEntity {
        [ForeignKey("Device")]
        [Key]
        public int DeviceId { get; set; }
        public int Upperbound { get; set; }
        public int Lowerbound { get; set; }
        public bool Auto { get; set; }
        public int Value { get; set; }

        public DeviceEntity Device { get; set; }
    }
}