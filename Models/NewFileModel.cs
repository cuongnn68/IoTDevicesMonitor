using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Model {
    public class NewFileModel {
        public string Name { get; set; }
        public string Folder { get; set; }
        public IFormFile File { get; set; }
    }
}