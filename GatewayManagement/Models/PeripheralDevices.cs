using System.ComponentModel.DataAnnotations;

namespace GatewayManagement.Models
{
    public class PeripheralDevices
    {
        [Key]
        public int UID { get; set; }

        [Required(ErrorMessage = "Vendor is required")]
        public string Vendor { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        public DateTime DateCreated { get; set; }

        public bool IsOnline { get; set; }
    }
}
