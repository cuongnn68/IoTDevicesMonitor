using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IoTDevicesMonitor.Model {
    public class Base64FileEntity {
        [Column("test name", TypeName = "varchar(420)", Order = 1), MaxLength(69), JsonPropertyName("json name")]
        [Required]
        public string Name { get; set; }
        public string Folder { get; set; }
        public string File { get; set; }
    }
}