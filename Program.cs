using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IoTDevicesMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    if(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true") {
                        var url = "http://*:" + Environment.GetEnvironmentVariable("PORT").Trim('/');
                        Console.WriteLine(url);
                        webBuilder.UseUrls(url);
                    }
                });
    }
}
