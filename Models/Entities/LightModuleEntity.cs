using System;

namespace IoTDevicesMonitor.Models.Entities {
    public class LightModuleEntity {
        public int IdDevice { get; set; }
        public bool IsOn { get; set; }
        public DateTime TimeOn { get; set; }
        public DateTime TimeOff { get; set; }
    }
}