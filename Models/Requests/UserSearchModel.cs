using System.ComponentModel.DataAnnotations;

namespace IoTDevicesMonitor.Models.Requests {
    public class UserSearchModel {
        [Required]
        public int Page { get; set; }
        [Required]
        public int RowPerPage { get; set; }
        public string Keyword { get; set; }
    }
}