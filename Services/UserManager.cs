using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IoTDevicesMonitor.Data;
using IoTDevicesMonitor.Models.Entities;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;
using IoTDevicesMonitor.Utils;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Services {
    public class UserManager {
        private AppDbContext dbContext;

        public UserManager(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public bool CreateUser(NewUserModel newUserModel, out string error) {
            var newUser = newUserModel.ToUserEntity();
            if(dbContext.Users.Where(u => u.Username == newUser.Username).FirstOrDefault() != null) {
                error = "User already exist";
                return false;
            }
            dbContext.Add(newUser);
            var devices = newUserModel.ToDeviceEntity();
            dbContext.AddRange(devices);
            dbContext.SaveChanges();
            error = null;
            return true;
        }

        public IEnumerable<UserModel> SearchUser(UserSearchModel search, out int count) {
            IQueryable<UserEntity> userMatched;
            if(string.IsNullOrEmpty(search.Keyword)) {
                userMatched = dbContext.Users;
            } else {
                userMatched = dbContext.Users.Where(
                    u => u.Username.ToLower().Contains(search.Keyword.ToLower())
                    || u.FullName.ToLower().Contains(search.Keyword.ToLower())
                    || u.Phone.ToLower().Contains(search.Keyword.ToLower())
                    || u.Email.ToLower().Contains(search.Keyword.ToLower())
                );
            }
            count = userMatched.Count();
            return userMatched.OrderBy(u => u.Username)
                            .Skip((search.Page - 1) * search.RowPerPage)
                            .Take(search.RowPerPage)
                            .Select(u => new UserModel {
                                Username = u.Username,
                                FullName = u.FullName,
                                Phone = u.Phone,
                                Email = u.Email,
                            });
        }

        public bool UpdateUserInfo(UpdatedUserModel updatedUser, out string error) {
            var user = dbContext.Users.Find(updatedUser.Username);
            if(user == null) {
                error = "User not exist";
                return false;
            }
            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.PhoneNumber;
            dbContext.SaveChanges();
            error = "";
            return true;
        }

        public bool DeleteUser(string username, out string error) {
            var user = dbContext.Users.Find(username);
            if(user == null) {
                error = "User not exist";
                return false;
            }
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            error = "";
            return true;
        }

        public bool AddDevice(string username, NewDeviceModel newDevice, out string error) {
            var user = dbContext.Users.Include(u => u.Devices).FirstOrDefault(u => u.Username == username);
            if(user == null) {
                error = "User not exist";
                return false;
            }
            dbContext.Devices.Add(newDevice.ToDeviceEntity(username));
            dbContext.SaveChanges();
            error = "";
            return true;
        }

        public bool GetUser(string username, out UserDeviceModel user, out string error) {
            user = dbContext.Users.Include(u => u.Devices).FirstOrDefault(e => e.Username == username)?.ToUserDeviceRespone();
            if(user == null) {
                error = "User not exist";
                return false;
            }
            error = "";
            return true;
        }

        public bool DeleteDevice(string username, int deviceId, out string error) {
            var user = dbContext.Users.Include(u => u.Devices).FirstOrDefault(u => u.Username == username);
            if(user == null) {
                error = "User not exist";
                return false;
            }
            var device = user.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
            if(device == null) {
                error = "Device not exist";
                return false;
            }
            dbContext.Remove(device);
            dbContext.SaveChanges();
            error = "";
            return true;
        }

        private bool IsMatched(UserEntity u, string keyword) { // ??? not work in linq.where(), dont know why
            return u.Username.ToLower().Contains(keyword.ToLower())
                || u.FullName.ToLower().Contains(keyword.ToLower())
                || u.Phone.ToLower().Contains(keyword.ToLower())
                || u.Email.ToLower().Contains(keyword.ToLower());
        }
    }
}