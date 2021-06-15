using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Models.Requests {
    public class NewFileModel {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Folder { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}