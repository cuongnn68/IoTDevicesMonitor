using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Services {
    public interface IFileManager {
        bool FileExist(string folder, string fileName);
        bool FolderExist(string folderName); // RM can change  param name of method in sub class 
        (bool, string) CreateNewFile(string folder, string fileName, IFormFile file);
        FileStream GetFile(string folder, string fileName);

        IEnumerable<string> GetFolders();
        IEnumerable<string> GetImagesInFolder(string folderName);
    }
}