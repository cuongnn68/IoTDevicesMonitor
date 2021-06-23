using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class AlertEntity {
        public int DeviceId { get; set; }
        public DateTime TimeAlert { get; set; }
        public string Content { get; set; }
        
        [ForeignKey("DeviceId")]
        public DeviceEntity Device { get; set; }
    }
}