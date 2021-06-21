using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class TempRecordEntity {
        public int DeviceId { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
        
        [ForeignKey("DeviceId")]
        public DeviceEntity Device { get; set; }
    }
}