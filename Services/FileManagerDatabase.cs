using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IoTDevicesMonitor.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Services {
    public class FileManagerDatabase : IFileManager {
        private AppDbContext dbContext;

        public FileManagerDatabase(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public (bool, string) CreateNewFile(string folder, string fileName, IFormFile file) {
            throw new NotImplementedException();
        }

        public bool FolderExist(string folder) 
            => dbContext.Base64Files.Select(e => e.Folder).Distinct().Contains(folder);
        
        public bool FileExist(string folder, string fileName) 
            => dbContext.Base64Files.Any(e => e.Folder == folder && e.Name == fileName);
        


        public FileStream GetFile(string folder, string fileName) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFolders() 
            => dbContext.Base64Files.Select(e => e.Folder).Distinct();

        public IEnumerable<string> GetImagesInFolder(string folderName) 
            => dbContext.Base64Files.Where(e => e.Folder == folderName).Select(e => e.Name).Distinct();
    }
}