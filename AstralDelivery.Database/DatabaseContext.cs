﻿using AstralDelivery.Domain.Entities;
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
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(a => a.Email).IsUnique();
        }
    }

}
