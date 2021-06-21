using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class LightModuleEntity {
        [Key]
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public bool State { get; set; }
        public DateTime TimeOn { get; set; }
        public DateTime TimeOff { get; set; }
        public bool Auto { get; set; }
        
        public DeviceEntity Device { get; set; }
    }
}