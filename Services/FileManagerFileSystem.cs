using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Services {
    public class FileManagerFileSystem : IFileManager{
        private Dictionary<string, HashSet<string>> foldersFiles;
        private IWebHostEnvironment env;
        public string DataPath { get; private set; }
        public string GetPath(string folder, string fileName = "") => Path.Combine(DataPath, folder, fileName);

        public FileManagerFileSystem (IWebHostEnvironment env) {
            this.env = env;
            DataPath = Path.Combine(env.WebRootPath, "file");
            if(!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);



            // TODO save object to file, and check if file exist to load before create new
            // this working by read 1 folder deep
            foldersFiles = new Dictionary<string, HashSet<string>>();
            var folders = Directory.GetDirectories(DataPath);
            foreach(var folder in folders) {
                var files = Directory.GetFiles(folder)
                                    .Select(e => e.Replace(folder, null).Replace("\\", null))
                                    .ToHashSet();
                foldersFiles.Add(
                    folder.Replace(DataPath, null).Replace("\\", null), 
                    files);
            }
        }

        public Task<bool> FileExistAsync(string folder, string fileName) {
            if (!Directory.Exists(GetPath(folder))) return Task.FromResult(false);
            if (!File.Exists(GetPath(folder, fileName))) return Task.FromResult(false);
            return Task.FromResult(true);
        }
        // TODO compare 2 exist method see what better implement
        public Task<bool> FolderExistAsync(string folder) {
            return Task.FromResult(foldersFiles.ContainsKey(folder));
        }

        public Task<(bool, string)> CreateNewFileAsync(string folder, string fileName, IFormFile file) {
            if(!foldersFiles.ContainsKey(folder)) {
                Directory.CreateDirectory(GetPath(folder));
                foldersFiles.Add(folder, new HashSet<string>());
            }
            if(foldersFiles[folder].Contains(fileName)) {
                return Task.FromResult((false, "File already exist"));
            }
            using var stream = new FileStream(
                GetPath(folder, fileName),
                FileMode.Create, 
                FileAccess.ReadWrite);
            file.CopyTo(stream);
            foldersFiles[folder].Add(fileName);
            return Task.FromResult((true, ""));
        }

        public Task<Stream> GetFileAsync(string folder, string fileName) {
            return Task.FromResult<Stream>(
                new FileStream(
                GetPath(folder, fileName),
                FileMode.Open,
                FileAccess.Read
                )
            );
        }

        public Task<IEnumerable<string>> GetFoldersAsync() {
            return Task.FromResult<IEnumerable<string>>(foldersFiles.Keys);
        }

        public Task<IEnumerable<string>> GetImagesInFolderAsync(string folderName) {
            if(!foldersFiles.ContainsKey(folderName)) return Task.FromResult<IEnumerable<string>>(new List<string>());
            return Task.FromResult<IEnumerable<string>>(foldersFiles[folderName]);
        }


    }
}