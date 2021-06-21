namespace IoTDevicesMonitor.Models.Respones {
    public class DeviceInfoModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasLightModule { get; set; }
        public bool HasTempModule { get; set; }
        public bool HasHumiModule { get; set; }
        public bool? LightState { get; set; }
        public int? TempValue { get; set; }
        public int? HumiValue { get; set; }
        
    }
}