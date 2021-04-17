using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Services {
    public class FileManagerDatabase : IFileManager {
        private AppDbContext dbContext;

        public FileManagerDatabase([FromServices] AppDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> FolderExistAsync(string folder) 
            => await dbContext.Base64Files.AnyAsync(e => e.Folder == folder);
        
        public async Task<bool> FileExistAsync(string folder, string fileName) 
            => await dbContext.Base64Files.AnyAsync(e => e.Folder == folder && e.Name == fileName);
        
        public async Task<(bool, string)> CreateNewFileAsync(string folder, string fileName, IFormFile file) {
            if(await FileExistAsync(folder, fileName)) return (false, "File already exist");
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var newFile = new Base64FileEntity {
                Name = fileName,
                Folder = folder,
                File = Convert.ToBase64String(stream.ToArray()),
            };
            dbContext.Add(newFile);
            await dbContext.SaveChangesAsync();
            return (true, "File saved");
        }

        public async Task<Stream> GetFileAsync(string folder, string fileName) {
            var base64File = await dbContext.Base64Files.FirstOrDefaultAsync(e => e.Folder == folder && e.Name == fileName);
            if(base64File == null) return null;
            var bytes = Convert.FromBase64String(base64File.File);
            return new MemoryStream(bytes);
        }

        public async Task<IEnumerable<string>> GetFoldersAsync() 
            => await dbContext.Base64Files.Select(e => e.Folder).Distinct().ToListAsync();

        public async Task<IEnumerable<string>> GetImagesInFolderAsync(string folderName) 
            => await dbContext.Base64Files.Where(e => e.Folder == folderName).Select(e => e.Name).Distinct().ToListAsync();

        
    }
}