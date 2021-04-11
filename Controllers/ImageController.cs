using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Model;
using IoTDevicesMonitor.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IoTDevicesMonitor.Controllers
{
    [ApiController]
    [Route("images-api")]
    public class ImageController : ControllerBase {
        private IWebHostEnvironment env;
        private string path;
        private FileManager fileManager;
        private Dictionary<string, HashSet<string>> setFiles;

        public ImageController(
            IWebHostEnvironment env,
            FileManager fileManager) 
        {
            this.fileManager = fileManager;
            this.env = env;
            path = fileManager.DataPath;
            setFiles = fileManager.FoldersFiles;
        }

        [HttpGet]
        [Route("folder")]
        public IActionResult GetAllImageFolder() {
            Console.WriteLine(env.WebRootPath);
            Console.WriteLine(env.ContentRootPath);
            IEnumerable<string> folders = fileManager.FoldersFiles.Keys;
            return Ok(folders);
        }

        [HttpPost("image")]
        public IActionResult UploadImage([FromForm] NewFileModel newFile) {
            var (created, error) = fileManager.CreateNewFile(newFile.Folder, newFile.Name, newFile.File);
            if(created) return Created($"folders/{newFile.Folder}/image/{newFile.Name}", null);
            return Conflict(error);
        }

        [HttpGet("folder/{folderName}/image/{imageName}")]
        public IActionResult DonwloadImage(string folderName, string imageName) {
            if(!fileManager.FileExist(folderName, imageName)) {
                var error = "File not exist";
                return Conflict(error);
            }
            // var fileStream = fileManager.GetFile(folderName, imageName);
            // return File(fileStream, "image/*",imageName);
            return PhysicalFile(fileManager.GetPath(folderName, imageName), "image/*",imageName);
        }


        // TODO: dowload image with folder and name
        // TODO: signalR
        // TODO: admin add devices
        // TODOL athentication

        
        
    }
}
