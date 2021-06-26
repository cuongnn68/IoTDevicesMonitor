namespace IoTDevicesMonitor.Models.Requests {
    public class NewStateModel {
        public int Temp { get; set; }
        public string TimeTemp { get; set; }
        public int Humi { get; set; }
        public string TimeHumi { get; set; }
    }
}