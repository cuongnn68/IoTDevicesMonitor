using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace IoTDevicesMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTDevicesMonitor", Version = "v1" });
            });

            
            var uesedDatabase = Configuration.GetValue<bool>("UsedDatabase");
            if(uesedDatabase) {
                services.AddDbContext<AppDbContext>();
                services.AddScoped<IFileManager, FileManagerDatabase>();
            } else {
                services.AddSingleton<IFileManager, FileManagerFileSystem>();
            }
            services.AddSingleton<DeviceState>();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTDevicesMonitor v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalrHub>("/realtime");
            });
        }
    }
}
