using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IoTDevicesMonitor.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace IoTDevicesMonitor.Services {
    public class FileManager {
        public Dictionary<string, HashSet<string>> FoldersFiles { get; private set; }
        private IWebHostEnvironment env;
        public string DataPath { get; private set; }
        public string GetPath(string folder, string fileName = "") => Path.Combine(DataPath, folder, fileName);

        public FileManager (IWebHostEnvironment env) {
            this.env = env;
            DataPath = Path.Combine(env.WebRootPath, "file");
            if(!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);



            // TODO save object to file, and check if file exist to load before create new
            // this working by read 1 folder deep
            FoldersFiles = new Dictionary<string, HashSet<string>>();
            var folders = Directory.GetDirectories(DataPath);
            foreach(var folder in folders) {
                var files = Directory.GetFiles(folder)
                                    .Select(e => e.Replace(folder, null).Replace("\\", null))
                                    .ToHashSet();
                FoldersFiles.Add(
                    folder.Replace(DataPath, null).Replace("\\", null), 
                    files);
            }
        }

        public bool FileExist(string folder, string fileName) {
            if (!Directory.Exists(GetPath(folder))) return false;
            if (!File.Exists(GetPath(folder, fileName))) return false;
            return true;
        }

        public (bool, string) CreateNewFile(string folder, string fileName, IFormFile file) {
            // var folderPath = GetPath(folder);
            // var filePath = GetPath(folder, fileName);
            if(!FoldersFiles.ContainsKey(folder)) {
                Directory.CreateDirectory(GetPath(folder));
                FoldersFiles.Add(folder, new HashSet<string>());
            }
            if(FoldersFiles[folder].Contains(fileName)) {
                return (false, "File already exist");
            }
            using var stream = new FileStream(
                GetPath(folder, fileName),
                FileMode.Create, 
                FileAccess.ReadWrite);
            file.CopyTo(stream);
            return (true, "");
        }

        public FileStream GetFile(string folder, string fileName) {
            return new FileStream(
                GetPath(folder, fileName),
                FileMode.Open,
                FileAccess.Read
            );
        }
    }
}