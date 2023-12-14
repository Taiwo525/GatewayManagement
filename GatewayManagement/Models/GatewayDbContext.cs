using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace GatewayManagement.Models
{
    public class GatewayDbContext : DbContext
    {
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {
        }
        public DbSet<Gateway> Gateway { get; set; }
        public DbSet<PeripheralDevices> PeripheralDevices { get; set; }
        
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseInMemoryDatabase("MyInMemoryDB");
    //}
}
