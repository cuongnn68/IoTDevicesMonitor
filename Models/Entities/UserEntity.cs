using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Entities {
    public class UserEntity {
        [Key]
        public string Username { get; set; }
        [Required]
        public string HPassword { get; set; } = "123456";
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<DeviceEntity> Devices { get; set; }
    }
}