﻿// <auto-generated />
using IoTDevicesMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IoTDevicesMonitor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210614134733_UpdateDeviceInDBContext")]
    partial class UpdateDeviceInDBContext
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

            modelBuilder.Entity("IoTDevicesMonitor.Models.Entities.UserEntity", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
