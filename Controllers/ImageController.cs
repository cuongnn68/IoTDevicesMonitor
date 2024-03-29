﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Models;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;
using IoTDevicesMonitor.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;

namespace IoTDevicesMonitor.Controllers
{
    [ApiController]
    [Route("images-api")]
    public class ImageController : Controller {
        private IWebHostEnvironment env;
        private string path;
        private IFileManager fileManager;
        private Dictionary<string, HashSet<string>> setFiles;

        public ImageController(
            IWebHostEnvironment env,
            IFileManager fileManager) 
        {
            this.fileManager = fileManager;
            this.env = env;
            // path = fileManager.DataPath;
            // setFiles = fileManager.FoldersFiles;
        }

        [HttpGet]
        [Route("folder")]
        public async Task<IActionResult> GetAllImageFolder() {
            var folders = new {Folders = await fileManager.GetFoldersAsync()};
            return Ok(folders);
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] NewFileModel newFile) {
            var (canCreated, error) = await fileManager.CreateNewFileAsync(newFile.Folder, newFile.Name, newFile.File);
            if(canCreated) return Created($"folders/{newFile.Folder}/image/{newFile.Name}", null);
            return Conflict(new {Error = error});
        }

        [HttpGet("folder/{folderName}/image")]
        public async Task<IActionResult> GetFolderContent(string folderName) {
            var exist = await fileManager.FolderExistAsync(folderName);
            if(!exist) return Conflict(new ErrorModel{Error = "Folder not exist"});
            return Ok(new {Files = await fileManager.GetImagesInFolderAsync(folderName)});
        }

        [HttpGet("folder/{folderName}/image/{imageName}")]
        public async Task<IActionResult> DonwloadImage(string folderName, string imageName) {
            if(! await fileManager.FileExistAsync(folderName, imageName)) {
                var error = "File not exist";
                return Conflict(error);
            }
            var fileStream = await fileManager.GetFileAsync(folderName, imageName);
            return File(fileStream, "image/*",imageName);
            // return PhysicalFile(fileManager.GetPath(folderName, imageName), "image/*",imageName);
        }

        // DONE: dowload image with folder and name
        // DONE: image list of folder
        // DONE: signalR
        // TODOL athentication
        // TODO: admin add devices
    }
}
