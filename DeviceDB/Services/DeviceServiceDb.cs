using System.ComponentModel.Composition;
using System.Linq;
using DeviceDB.Repository;
using DeviceService.Models;

namespace HelloWorld.Services
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DeviceServiceDb : IDeviceService
    {
        private DeviceContext deviceContext;
        public Device CreateDevice(Device device)
        {
            deviceContext.Devices.Attach(device);
            deviceContext.Devices.Add(device);
            deviceContext.SaveChanges();
            return device;
        }

        public Device GetDevice(int id)
        {
            return deviceContext.Devices.Find(id);
        }

        public void DeleteDevice(int id)
        {
            Device device = new Device() {Id = id};
            deviceContext.Devices.Attach(device);
            deviceContext.Devices.Remove(device);
        }

        public Device UpdateDevice(Device device)
        {
            Device current = deviceContext.Devices.SingleOrDefault((d)=>d.Id == device.Id);
            current.Serial = device.Serial;
            current.Type = device.Type;
            deviceContext.SaveChanges();
            return device;
        }
    }
}