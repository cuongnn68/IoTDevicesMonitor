using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTDevicesMonitor.Models.Entities {
    public class DeviceEntity {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public bool HaveLightModule { get; set; } = false;
        public bool HaveTempModule { get; set; } = false;
        public bool HaveHumidityModule { get; set; } = false;
        public bool HavePHModule { get; set; } = false;

        [Required]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public UserEntity User { get; set; }
    }
}