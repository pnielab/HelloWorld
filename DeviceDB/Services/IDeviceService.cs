using DeviceService.Models;

namespace HelloWorld.Services
{
    public interface IDeviceService
    {
        Device CreateDevice(Device device);
        Device GetDevice(int id);
        void DeleteDevice(int id);
        Device UpdateDevice(Device device);
    }
}