using AstralDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AstralDelivery.Database
{
    /// <summary>
    /// EF Core контекст базы данных менеджмента
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary />
        public DbSet<User> Users { get; set; }
        /// <summary />
        public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }
        /// <summary />
        public DbSet<DeliveryPoint> DeliveryPoints { get; set; }
        /// <summary />
        public DbSet<WorkTime> WorkTimes { get; set; }
        /// <summary />
        public DbSet<Product> Products { get; set; }

        /// <summary />
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<WorkTime>().HasKey(w => new { w.DeliveryPointGuid, w.DayOfWeek });
        }
    }

}
