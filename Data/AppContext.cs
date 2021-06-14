using IoTDevicesMonitor.Data.Entities;
using IoTDevicesMonitor.Model;
using Microsoft.EntityFrameworkCore;

namespace IoTDevicesMonitor.Data {
    public class AppDbContext : DbContext {
        public DbSet<Base64FileEntity> Base64Files { get; set; }
        public DbSet<AdminAccount> AdminAccount { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // optionsBuilder.UseInMemoryDatabase("Test-only-db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Base64FileEntity>().HasKey(e => new {e.Name, e.Folder});
            base.OnModelCreating(modelBuilder);
            /*
            .HasAlternateKey(System.Linq.Expressions.Expression<System.Func<Base64FileEntity, object>> keyExpression) (+ 2 overloads)
            */
        }
    }
}