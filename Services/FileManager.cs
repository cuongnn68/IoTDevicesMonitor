using System.Collections.Generic;
using System.IO;
using IoTDevicesMonitor.Model;
using Microsoft.AspNetCore.Hosting;

namespace IoTDevicesMonitor.Services {
    public class FileManager {
        public Dictionary<string, HashSet<string>> FoldersFiles { get; private set; }
        private IWebHostEnvironment env;
        public string FilePath { get; private set; }
        public string GetPathFile(string folder, string fileName = "") => Path.Combine();

        public FileManager (IWebHostEnvironment env) {
            this.env = env;
            FilePath = Path.Combine(env.WebRootPath, "file");
            if(!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);

            // TODO save object to file, and check if file exist to load before create new
            FoldersFiles = new Dictionary<string, HashSet<string>>();
        }






    }
}