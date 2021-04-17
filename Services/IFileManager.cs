using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Services {
    public interface IFileManager {
        Task<bool> FileExistAsync(string folder, string fileName);
        Task<bool> FolderExistAsync(string folderName); // RM can change  param name of method in sub class 
        Task<(bool, string)> CreateNewFileAsync(string folder, string fileName, IFormFile file);
        Task<Stream> GetFileAsync(string folder, string fileName);

        Task<IEnumerable<string>> GetFoldersAsync();
        Task<IEnumerable<string>> GetImagesInFolderAsync(string folderName);
    }
}