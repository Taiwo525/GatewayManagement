using GatewayManagement.Models;
using GatewayManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GatewayRepository : IGatewayRepo
{
    private readonly GatewayDbContext _context;

    public GatewayRepository(GatewayDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Gateway>> GetGatewaysAsync()
    {
        return await _context.Gateway.ToListAsync();
    }

    public async Task<Gateway> GetGatewayBySerialNumberAsync(string serialNumber)
    {
        return await _context.Gateway.FirstOrDefaultAsync(g => g.SerialNumber == serialNumber);
    }

    public async Task AddGatewayAsync(Gateway gateway)
    {
        _context.Gateway.Add(gateway);
        await _context.SaveChangesAsync();
    }

    public async Task AddPeripheralDeviceAsync(string serialNumber, PeripheralDevices device)
    {
        var gateway = await _context.Gateway.Include(g => g.PeripheralDevices)
                                             .FirstOrDefaultAsync(g => g.SerialNumber == serialNumber);

        if (gateway == null)
        {
            throw new InvalidOperationException("Gateway not found");
        }

        if (gateway.PeripheralDevices.Count >= 10)
        {
            throw new InvalidOperationException("A gateway can have at most 10 peripheral devices.");
        }

        gateway.PeripheralDevices.Add(device);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePeripheralDeviceAsync(string serialNumber, int deviceUid)
    {
        var gateway = await _context.Gateway.Include(g => g.PeripheralDevices)
                                            .FirstOrDefaultAsync(g => g.SerialNumber == serialNumber);

        if (gateway == null)
        {
            throw new InvalidOperationException("Gateway not found");
        }

        var deviceToRemove = gateway.PeripheralDevices.FirstOrDefault(d => d.UID == deviceUid);

        if (deviceToRemove != null)
        {
            gateway.PeripheralDevices.Remove(deviceToRemove);
            await _context.SaveChangesAsync();
        }
    }
}



//        private readonly SqlConnection connection;
//        private readonly GatewayDbContext dbContext;

//        public GatewayRepo(SqlConnection connection, GatewayDbContext dbContext)
//        {
//            this.connection = connection;
//            this.dbContext = dbContext;
//        }

//        public Gateway CreateGateway(Gateway gateway)
//        {
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "INSERT INTO Gateways (Name, IPAddress, Location) VALUES (@Name, @IPAddress, @Location)";
//                command.Parameters.AddWithValue("@Name", gateway.Name);
//                command.Parameters.AddWithValue("@IPAddress", gateway.IPAddress);
//                command.Parameters.AddWithValue("@Location", gateway.Location);

//                connection.Open();
//                command.ExecuteNonQuery();

//                gateway.Id = (int)command.ExecuteScalar();
//            }

//            return gateway;
//        }

//        public Gateway GetGatewayById(int id)
//        {
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "SELECT * FROM Gateways WHERE Id = @Id";
//                command.Parameters.AddWithValue("@Id", id);

//                connection.Open();
//                var reader = command.ExecuteReader();

//                if (reader.Read())
//                {
//                    var gateway = new Gateway
//                    {
//                        Id = reader.GetInt32(0),
//                        Name = reader.GetString(1),
//                        IPAddress = reader.GetString(2),
//                        Location = reader.GetString(3)
//                    };
//                    reader.Close();
//                    return gateway;
//                }
//                else
//                {
//                    reader.Close();
//                    return null; // Return null if no gateway found
//                }
//            }


//        }

//        public Gateway UpdateGateway(Gateway gateway)
//        {
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "UPDATE Gateways SET Name = @Name, IPAddress = @IPAddress, Location = @Location WHERE Id = @Id";
//                command.Parameters.AddWithValue("@Id", gateway.Id);
//                command.Parameters.AddWithValue("@Name", gateway.Name);
//                command.Parameters.AddWithValue("@IPAddress", gateway.IPAddress);
//                command.Parameters.AddWithValue("@Location", gateway.Location);

//                connection.Open();
//                command.ExecuteNonQuery();
//            }

//            return gateway;
//        }

//        public void DeleteGateway(int id)
//        {
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "DELETE FROM Gateways WHERE Id = @Id";
//                command.Parameters.AddWithValue("@Id", id);

//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }

//        public List<Gateway> GetAllGateways()
//        {
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "SELECT * FROM Gateways";

//                connection.Open();
//                var reader = command.ExecuteReader();

//                var gateways = new List<Gateway>();
//                while (reader.Read())
//                {
//                    var gateway = new Gateway
//                    {
//                        Id = reader.GetInt32(0),
//                        Name = reader.GetString(1),
//                        IPAddress = reader.GetString(2),
//                        Location = reader.GetString(3)
//                    };

//                    gateways.Add(gateway);
//                }

//                reader.Close();
//                return gateways;
//            }
//        }



