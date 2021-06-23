using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IoTDevicesMonitor.Models.Entities;
using IoTDevicesMonitor.Models.Requests;
using IoTDevicesMonitor.Models.Respones;

namespace IoTDevicesMonitor.Utils {
    public static class Mapping {
        static public UserEntity ToUserEntity(this NewUserModel newUser) {
            return new UserEntity {
                Username = newUser.Username,
                HPassword = newUser.Password,
                FullName = newUser.FullName,
                Phone = newUser.PhoneNumber,
                Email = newUser.Email,
            };
        }

        static public ICollection<DeviceEntity> ToDeviceEntity(this NewUserModel newUser) {
            if(newUser.Devices == null) return new List<DeviceEntity>();
            return newUser?.Devices.Select(d => new DeviceEntity {
                DeviceName = d.Name,
                Username = newUser.Username,
                HaveLightModule = d.HasLight,
                HaveTempModule = d.HasTemp,
                HaveHumidityModule = d.HasHumi,
                HavePHModule = d.HasPH,
                LightModule = d.HasLight ? new LightModuleEntity{
                    Auto = false,
                    State = false,
                } : null,
                TempModule = d.HasTemp ? new TemperatureModuleEntity {
                    Value = 99,
                    UpperAlertOption = false,
                    LowerAlertOption = false,
                } : null,
                HumiModule = d.HasHumi ? new HumiModuleEntity {
                    Auto = false,
                    Value = 99,
                } : null,
            }).ToList();
        }

        static public UserDeviceModel ToUserDeviceRespone(this UserEntity user) {
            if(user == null) return null;
            return new UserDeviceModel {
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Devices = (user.Devices == null) ? null : user.Devices.Select(d => new IoTDevicesMonitor.Models.Respones.DeviceModel {
                    Id = d.DeviceId,
                    Name = d.DeviceName,
                    HaveLightModule = d.HaveLightModule,
                    HaveTempModule = d.HaveTempModule,
                    HaveHumidityModule = d.HaveHumidityModule,
                    HavePHModule = d.HavePHModule,
                }).ToList(),
            };
        }

        static public DeviceEntity ToDeviceEntity(this NewDeviceModel newDevice, string username) {
            if(newDevice == null) return null;
            return new DeviceEntity {
                Username = username,
                DeviceName = newDevice.Name,
                HaveLightModule = newDevice.HasLight,
                HaveTempModule = newDevice.HasTemp,
                HaveHumidityModule = newDevice.HasHumi,
                HavePHModule = newDevice.HasPH,
                LightModule = newDevice.HasLight ? new LightModuleEntity{
                    Auto = false,
                    State = false,
                    TimeOnOption = false,
                    TimeOffOption = false,
                } : null,
                TempModule = newDevice.HasTemp ? new TemperatureModuleEntity {
                    Value = 99,
                    UpperAlertOption = false,
                    LowerAlertOption = false,
                } : null,
                HumiModule = newDevice.HasHumi ? new HumiModuleEntity {
                    Auto = false,
                    Value = 99,
                } : null,
            };
        }

        static public DeviceInfoModel ToDeviceInfoResponse(this DeviceEntity device) {
            if(device == null) return null;
            return new DeviceInfoModel {
                Id = device.DeviceId,
                Name = device.DeviceName,
                HasLightModule = device.HaveLightModule,
                HasHumiModule = device.HaveHumidityModule,
                HasTempModule = device.HaveTempModule,
                LightState = (device.HaveLightModule ? device.LightModule.State : false),
                TempValue = device.TempModule?.Value,
                HumiValue = device.HumiModule?.Value,
            };
        }

        static public TempRecordEntity ToTempRecordEntity(this NewRecordModel newRecord, int deviceId) {
            if(newRecord == null) return null;
            return new TempRecordEntity {
                DeviceId = deviceId,
                Value = newRecord.Value,
                Time = (newRecord.Time == "auto" ? DateTime.Now : DateTime.ParseExact(newRecord.Time, "HH:mm", null)),
            };
        }
        static public HumiRecordEntity ToHumiRecordEntity(this NewRecordModel newRecord, int deviceId) {
            if(newRecord == null) return null;
            return new HumiRecordEntity {
                DeviceId = deviceId,
                Value = newRecord.Value,
                Time = (newRecord.Time == "auto" ? DateTime.Now : DateTime.ParseExact(newRecord.Time, "HH:mm", null)),
            };
        }
        
    }
}