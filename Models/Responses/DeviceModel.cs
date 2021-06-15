namespace IoTDevicesMonitor.Models.Respones {
    public class DeviceModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HaveLightModule { get; set; }
        public bool HaveTempModule { get; set; }
        public bool HaveHumidityModule { get; set; }
        public bool HavePHModule { get; set; }
    }
}