
using System.ComponentModel.DataAnnotations.Schema;
using IoTDevicesMonitor.Models;
using IoTDevicesMonitor.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Data {
    public class AppDbContext : DbContext {
        public DbSet<Base64FileEntity> Base64Files { get; set; }
        public DbSet<AdminAccountEntity> AdminAccounts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // optionsBuilder.UseInMemoryDatabase("Test-only-db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Base64FileEntity>().HasKey(e => new {e.Name, e.Folder});
            /*
            .HasAlternateKey(System.Linq.Expressions.Expression<System.Func<Base64FileEntity, object>> keyExpression) (+ 2 overloads)
            */
            // modelBuilder.Entity<UserEntity>().HasKey(e => e.Username);
            // modelBuilder.Entity<UserEntity>()
            //     .HasMany<DeviceEntity>(u => u.Devices)
            //     .WithOne(d => d.User)
            //     .HasForeignKey(d => d.Username);

            // modelBuilder.Entity<DeviceEntity>().HasKey(d => d.DeviceId);
            // modelBuilder.Entity<DeviceEntity>()
            //     .Property(d => d.Username)
            //     .ValueGeneratedOnAdd();
            //     // .UseIdentityAlwaysColumn();
            // modelBuilder.Entity<DeviceEntity>().Property(d => d.Username).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}