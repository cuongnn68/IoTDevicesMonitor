namespace IoTDevicesMonitor.Models.Requests {
    public class UpdateHumiModuleModel {
        public bool Auto { get; set; }
        public int Upperbound { get; set; }
        public int Lowerbound { get; set; }
    }
}