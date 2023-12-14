using System.ComponentModel.DataAnnotations;

namespace GatewayManagement.Models
{
    public class GatewayValidator
    {
        public void ValidateGateway(Gateway gateway)
        {
            if (string.IsNullOrWhiteSpace(gateway.SerialNumber))
            {
                throw new InvalidOperationException("Serial number cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(gateway.Name))
            {
                throw new InvalidOperationException("Name cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(gateway.IPAddress) || !IsValidIPAddress(gateway.IPAddress))
            {
                throw new InvalidOperationException("Invalid IP address format.");
            }

            // Additional validations based on your requirements
            if (gateway.PeripheralDevices != null && gateway.PeripheralDevices.Count > 0)
            {
                // Ensure each peripheral device in the list is valid
                foreach (var device in gateway.PeripheralDevices)
                {
                    ValidatePeripheralDevice(device);
                }
            }

            // Add more validations as needed
        }

        public bool IsValidIPAddress(string ipAddress)
        {
            // Simple validation for IPv4 address format
            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }

        public void ValidatePeripheralDevice(PeripheralDevices device)
        {
            if (device.UID <= 0)
            {
                throw new InvalidOperationException("Peripheral device UID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(device.Vendor))
            {
                throw new InvalidOperationException("Peripheral device vendor cannot be empty or whitespace.");
            }

            // Add more validations for PeripheralDevice as needed
        }

    }
    public class EnsureMaximumPeripheralDevicesAttribute : ValidationAttribute
    {
        private const int MaxPeripheralDevices = 10;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var peripheralDevices = value as List<PeripheralDevices>;

            if (peripheralDevices != null && peripheralDevices.Count > MaxPeripheralDevices)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
