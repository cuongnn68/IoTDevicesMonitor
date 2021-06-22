using System;

namespace IoTDevicesMonitor.Models.Requests {
    public class UpdateLightModuleModel {
        public String TimeOn { get; set; }
        public String TimeOff { get; set; }
        public bool TimeOnOption { get; set; } = false;
        public bool TimeOffOption { get; set; } = false;
        public bool Auto { get; set; } = false;
    }
}