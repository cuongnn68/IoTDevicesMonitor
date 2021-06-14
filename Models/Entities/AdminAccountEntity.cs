using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Data.Entities {
    public class AdminAccount {
        [Key]
        public string Admin { get; set; }
        [Required]
        public string HPassword { get; set; }
    }
}