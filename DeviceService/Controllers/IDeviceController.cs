using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Dtos;

namespace HelloWorld.Controllers
{
    public interface IDeviceController
    {
        DeviceDto GetDevice(int id, string dbType);
        SuccessDto DeleteDevice(int id, string dbType);
        Task<DeviceDto> UpdateDevice(DeviceDto device, string dbType);
        Task<DeviceDto> CreateDevice(DeviceDto deviceDto, string dbType);
    }
}