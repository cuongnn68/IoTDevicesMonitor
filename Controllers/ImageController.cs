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
    [Route("api/image")]
    public class ImageController : ControllerBase {
        private IWebHostEnvironment env;
        private string dir;
        private FileManager fileManager;

        public ImageController(
            IWebHostEnvironment env,
            FileManager fileManager) 
        {
            this.fileManager = fileManager;
            this.env = env;
            dir = fileManager.GetPathFile;
        }

        [HttpGet]
        public IActionResult GetAllImageFolder() {
            // TODO
            IEnumerable<string> folders = fileManager.FoldersFiles.Keys;
            return Ok(folders);
        }

        [HttpPost]
        public IActionResult CreateImage(NewFileModel newFile) {
            if(!fileManager.FoldersFiles.Keys.Contains(newFile.Folder)) {
                Directory.CreateDirectory()
            }

            return Created($"/{newFile.Folder}/{newFile.File}", null);
        }

        
    }
}
