using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IoTDevicesMonitor.Data.Entities;
using IoTDevicesMonitor.Utils;
using Microsoft.IdentityModel.Tokens;

namespace IoTDevicesMonitor.Services {
    public class JwtServices {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConstains.TestKey));

        public string CreateAdminToken(AdminAccount adminAccount) {
            var descriptor = new SecurityTokenDescriptor {
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(30),
                TokenType = "JWT",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Claims = new Dictionary<string, object> {
                    {"admin", adminAccount.Admin}
                }
            };
            return CreateToken(descriptor);
        }
        
        private string CreateToken(SecurityTokenDescriptor descriptor) {
            var handle = new JwtSecurityTokenHandler();
            var token = handle.CreateJwtSecurityToken(descriptor);
            return handle.WriteToken(token);
        }
    }
}