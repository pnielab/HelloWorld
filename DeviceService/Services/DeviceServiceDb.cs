using System.ComponentModel.Composition;
using DeviceService.Models;
using HelloWorld.Dtos;

namespace HelloWorld.Services
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    // [Export(typeof(IDeviceService))]
    // [Export("deviceServiceDb", typeof(IDeviceService))]
    public class DeviceServiceDb : IDeviceService
    {
        public Device CreateDevice(Device device)
        {
            return device;
        }

        public Device GetDevice(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDevice(int id)
        {
            throw new System.NotImplementedException();
        }

        public Device UpdateDevice(Device device)
        {
            throw new System.NotImplementedException();
        }
    }
}