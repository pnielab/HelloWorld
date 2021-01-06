using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DeviceService.Models;
using HelloWorld.Dtos;
using HelloWorld.Services;
using Microsoft.JScript;

namespace HelloWorld.Controllers
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IDeviceController))]
    [RoutePrefix("api")]
    public class DeviceController : ApiController, IDeviceController
    {
        private readonly IDeviceService deviceFileService;
        private readonly IDeviceService deviceServiceDb;

        [ImportingConstructor]
        public DeviceController(IDeviceService deviceFileService)
        {
            this.deviceFileService = deviceFileService;
        }

        [Route("devices/{id}")]
        [HttpGet]
        public DeviceDto GetDevice(int id, [FromUri] string dbType)
        {
            Device device =  deviceFileService.GetDevice(id);
            return ConvertDeviceToDeviceDto(device);
        }
        
        [Route("devices/{id}")]
        [HttpDelete]
        public SuccessDto DeleteDevice(int id, [FromUri] string dbType)
        {
            deviceFileService.DeleteDevice(id);
            return new SuccessDto();
        }


        [Route("devices/{id}")]
        [HttpPut]
        public async Task<DeviceDto> UpdateDevice(int id, [FromBody] DeviceDto deviceDto, [FromUri] string dbType)
        {
            Device device = ConvertDeviceDtoToDevice(deviceDto);
            device.Id = id;
            Device savedDevice =  deviceFileService.UpdateDevice(device);
            return ConvertDeviceToDeviceDto(savedDevice);
        }

        [Route("devices")]
        [HttpPost]
        public async Task<DeviceDto> CreateDevice(DeviceDto deviceDto, [FromUri] string dbType)
        {
            Device device = ConvertDeviceDtoToDevice(deviceDto);
            Device savedDevice =  deviceFileService.CreateDevice(device);
            return ConvertDeviceToDeviceDto(savedDevice);
        }

        private Device ConvertDeviceDtoToDevice(DeviceDto deviceDto)
        {
            Device device = new Device();
            device.Id = deviceDto.Id;
            device.Serial = deviceDto.Serial;
            device.Type = deviceDto.Type;
            return device;
        }
        
        private DeviceDto ConvertDeviceToDeviceDto(Device device)
        {
            DeviceDto deviceDto = new DeviceDto();
            deviceDto.Id = device.Id;
            deviceDto.Serial = device.Serial;
            deviceDto.Type = device.Type;
            return deviceDto;
        }

        // [Route("devices")]
        // [HttpGet]
        // public DeviceDto GetDeviceByFilter([FromUri] DeviceFilter filter, [FromUri] string dbType)
        // {
        //     return new DeviceDto();
        // }
    }
}