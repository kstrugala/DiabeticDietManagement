using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.EF
{
    public class DiabeticDietContext : DbContext
    {
        private readonly SqlSettings _settings;

        public DbSet<User> Users { get; set; }

        public DiabeticDietContext(DbContextOptions<DiabeticDietContext> options, SqlSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.HasKey(x => x.Id);
        }
    }
}
