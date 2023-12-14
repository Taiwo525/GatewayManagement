using GatewayManagement.Models;

namespace GatewayManagement.Repository.Interface
{
    public interface IGatewayRepo
    {
        Task<IEnumerable<Gateway>> GetGatewaysAsync();
        Task<Gateway> GetGatewayBySerialNumberAsync(string serialNumber);
        Task AddGatewayAsync(Gateway gateway);
        Task AddPeripheralDeviceAsync(string serialNumber, PeripheralDevices device);
        Task RemovePeripheralDeviceAsync(string serialNumber, int deviceUid);
    }
}
