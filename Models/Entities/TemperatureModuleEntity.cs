namespace IoTDevicesMonitor.Models.Entities {
    public class TemperatureModuleEntity {
        public int DeviceId { get; set; }
        public int Temperature { get; set; }
        public int Upperbound { get; set; }
        public int Lowerbound { get; set; }
    }
}