﻿// <auto-generated />
using System;
using IoTDevicesMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IoTDevicesMonitor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210621121348_UpdateTempModule")]
    partial class UpdateTempModule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("IoTDevicesMonitor.Models.Base64FileEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(69)
                        .HasColumnType("varchar(420)")
                        .HasColumnName("test name");

                    b.Property<string>("Folder")
                        .HasColumnType("text");

                    b.Property<string>("File")
                        .HasColumnType("text");

                    b.HasKey("Name", "Folder");

                    b.ToTable("Base64Files");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.AdminAccountEntity", b =>
                {
                    b.Property<string>("Admin")
                        .HasColumnType("text");

                    b.Property<string>("HPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Admin");

                    b.ToTable("AdminAccounts");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.DeviceEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DeviceName")
                        .HasColumnType("text");

                    b.Property<bool>("HaveHumidityModule")
                        .HasColumnType("boolean");

                    b.Property<bool>("HaveLightModule")
                        .HasColumnType("boolean");

                    b.Property<bool>("HavePHModule")
                        .HasColumnType("boolean");

                    b.Property<bool>("HaveTempModule")
                        .HasColumnType("boolean");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DeviceId");

                    b.HasIndex("Username");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.HumiModuleEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<bool>("Auto")
                        .HasColumnType("boolean");

                    b.Property<int>("Lowerbound")
                        .HasColumnType("integer");

                    b.Property<int>("Upperbound")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("DeviceId");

                    b.ToTable("HumiModules");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.HumiRecordEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("DeviceId", "Time");

                    b.ToTable("HumiRecords");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.LightModuleEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<bool>("Auto")
                        .HasColumnType("boolean");

                    b.Property<bool>("State")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("TimeOff")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("TimeOffOption")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("TimeOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("TimeOnOption")
                        .HasColumnType("boolean");

                    b.HasKey("DeviceId");

                    b.ToTable("LightModules");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.TempRecordEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("DeviceId", "Time");

                    b.ToTable("TempRecords");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.TemperatureModuleEntity", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<bool>("LowerAlertOption")
                        .HasColumnType("boolean");

                    b.Property<int>("Lowerbound")
                        .HasColumnType("integer");

                    b.Property<bool>("UpperAlertOption")
                        .HasColumnType("boolean");

                    b.Property<int>("Upperbound")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("DeviceId");

                    b.ToTable("TempModules");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.UserEntity", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("HPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.DeviceEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.UserEntity", "User")
                        .WithMany("Devices")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.HumiModuleEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.DeviceEntity", "Device")
                        .WithOne("HumiModule")
                        .HasForeignKey("IoTDevicesMonitor.Models.Entities.HumiModuleEntity", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.HumiRecordEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.DeviceEntity", "Device")
                        .WithMany("HumiRecords")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.LightModuleEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.DeviceEntity", "Device")
                        .WithOne("LightModule")
                        .HasForeignKey("IoTDevicesMonitor.Models.Entities.LightModuleEntity", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.TempRecordEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.DeviceEntity", "Device")
                        .WithMany("TempRecords")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.TemperatureModuleEntity", b =>
                {
                    b.HasOne("IoTDevicesMonitor.Models.Entities.DeviceEntity", "Device")
                        .WithOne("TempModule")
                        .HasForeignKey("IoTDevicesMonitor.Models.Entities.TemperatureModuleEntity", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.DeviceEntity", b =>
                {
                    b.Navigation("HumiModule");

                    b.Navigation("HumiRecords");

                    b.Navigation("LightModule");

                    b.Navigation("TempModule");

                    b.Navigation("TempRecords");
                });

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.UserEntity", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
