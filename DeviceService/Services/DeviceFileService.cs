using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using DeviceService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HelloWorld.Services
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IDeviceService))]
   // [Export("deviceFileService", typeof(IDeviceService))]
    public class DeviceFileService : IDeviceService
   {
       public DeviceFileService()
       {
           CreateDb();
       }

       private string dbPath = "C:\\\\helloworld\\devices";
        public Device CreateDevice(Device device)
        {
            device.Id = GetFileCount() + 1;
            string filePath = ConstructDeviceFilePath(device.Id);
            var deviceLocation = constructDeviceFolderPath(device.Id);
            createFolder(deviceLocation);
            deviceLocation = deviceLocation + "\\device.json";
            JObject deviceAsJson = JObject.FromObject(device);
            File.WriteAllText(@deviceLocation, deviceAsJson.ToString());
// write JSON directly to a file
            using (StreamWriter file = File.CreateText(@deviceLocation))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                deviceAsJson.WriteTo(writer);
            }
            return device;
        }



        public Device GetDevice(int id)
        {
            string filePath = ConstructDeviceFilePath(id);
            bool exists = System.IO.File.Exists(filePath);
            if (!exists)
            {
                throw new Exception("not found");
            }
            using (StreamReader file = File.OpenText(@filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                Device device = o2.ToObject<Device>();
                return device;
            }
        }

        private string ConstructDeviceFilePath(int id)
        {
            return constructDeviceFolderPath(id) + "\\device.json";
        }

        public void DeleteDevice(int id)
        {
            string filePath = ConstructDeviceFilePath(id);
            bool exists = System.IO.File.Exists(filePath);
            if (!exists)
            {
                throw new Exception("not found");
            }
            System.IO.File.Delete(filePath);
        }

        public Device UpdateDevice(Device device)
        {
            throw new NotImplementedException();
        }
        
        private string constructDeviceFolderPath(int deviceId)
        {
            return String.Format("{0}\\{1}", dbPath, deviceId);
        }

        private void CreateDb()
        {
            createFolder(dbPath);
        }

        private void createFolder(string path)
        {
            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
        
        private bool fileExists(string path)
        {
            bool exists = System.IO.File.Exists("");
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }


        private int GetFileCount()
        {
            return System.IO.Directory.GetDirectories(dbPath).Length;
        }
   }
}