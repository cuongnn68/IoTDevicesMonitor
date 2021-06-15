using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Entities {
    public class AdminAccountEntity {
        [Key]
        public string Admin { get; set; }
        [Required]
        public string HPassword { get; set; }
    }
}