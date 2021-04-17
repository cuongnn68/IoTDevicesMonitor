﻿// <auto-generated />
using IoTDevicesMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IoTDevicesMonitor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("IoTDevicesMonitor.Model.Base64FileEntity", b =>
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
#pragma warning restore 612, 618
        }
    }
}
