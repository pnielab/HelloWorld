using System.Data.Entity;
using DeviceService.Models;

namespace DeviceDB.Repository
{
    public class DeviceContext: DbContext
    {
        public DbSet<Device> Devices { get; set; }
    }
}