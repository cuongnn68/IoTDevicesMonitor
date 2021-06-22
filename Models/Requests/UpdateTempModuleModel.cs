namespace IoTDevicesMonitor.Models.Requests {
    public class UpdateTempModuleModel {
        public bool UpperAlertOptions { get; set; }
        public bool LowerAlertOptions { get; set; }
        public int Upperbound { get; set; }
        public int Lowerbound { get; set; }
    }
}