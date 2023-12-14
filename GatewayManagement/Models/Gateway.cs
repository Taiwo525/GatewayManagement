using GatewayManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace GatewayManagement.Models
{
    public class Gateway
    {
        [Key] 
        [Required(ErrorMessage = "SerialNumber is required")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "IPAddress is required")]
        public string IPAddress { get; set; }

        [EnsureMaximumPeripheralDevices(ErrorMessage = "No more than 10 peripheral devices are allowed.")]
        public List<PeripheralDevices> PeripheralDevices { get; set; } 
    
        public Gateway()
        {
            PeripheralDevices = new List<PeripheralDevices>();
        }
    }
}
