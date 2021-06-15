using System;

namespace IoTDevicesMonitor.Models.Entities {
    public class AlertEntity {
        public string Username { get; set; }
        public int DeviceId { get; set; }
        public DateTime TimeAlert { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}