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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IoTDevicesMonitor.Utils;

namespace IoTDevicesMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTDevicesMonitor", Version = "v1" });
            });

            
            var uesedDatabase = Configuration.GetValue<bool>("UsedDatabase",false);
            if(uesedDatabase) {
                services.AddDbContext<AppDbContext>(options => {
                    // options.UseNpgsql(Configuration.GetConnectionString("HerokuPsql"));
                    options.UseNpgsql(GetConnectionString());
                });
                services.AddScoped<IFileManager, FileManagerDatabase>();
            } else {
                services.AddSingleton<IFileManager, FileManagerFileSystem>();
            }
            services.AddSingleton<DeviceState>();
            services.AddSignalR();
            services.AddScoped<JwtServices>();
            services.AddScoped<UserManager>();
            
            services.AddSpaStaticFiles(options => {
                options.RootPath = "./admin-app/dist";
            });
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    SecurityKey key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(GlobalConstains.TestKey)
                    );
                    options.TokenValidationParameters = new TokenValidationParameters{
                        // TODO ????
                        ClockSkew = new TimeSpan(0, 0, 1),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        AuthenticationType = "Bearer"
                    };
                });
            services.AddAuthorization(option => {
                option.AddPolicy("Admin", builder => {
                    builder.RequireClaim("admin");
                });
                option.AddPolicy("User", builder => {
                    builder.RequireClaim("username");
                });
            });

            services.AddCors(options => {
                options.AddPolicy("My CORS", builder => {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        // .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTDevicesMonitor v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("My CORS");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalrHub>("/realtime");
            });
            app.UseSpaStaticFiles();
            app.UseSpa(config => {
                config.Options.SourcePath = "admin-app/dist";
            });
        }

        private string GetConnectionString() {
            var stringDbUrl = env.IsProduction() 
                ? Environment.GetEnvironmentVariable("DATABASE_URL") 
                : Environment.GetEnvironmentVariable("HEROKU_DATABASE_URL");
            Console.WriteLine(stringDbUrl);
            var dbUrl = new Uri(stringDbUrl);
            return $"Host={dbUrl.Host};"
                + $"Port={dbUrl.Port};"
                + $"Database={dbUrl.AbsolutePath.Remove(0,1)};"
                + $"User Id={dbUrl.UserInfo.Split(":")[0]};"
                + $"Password={dbUrl.UserInfo.Split(":")[1]};"
                + "Trust Server Certificate=true;SSL Mode=Require;";
        }
    }
}
