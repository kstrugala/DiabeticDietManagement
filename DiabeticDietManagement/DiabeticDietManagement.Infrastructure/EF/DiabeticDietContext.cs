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
        public DbSet<Product> Products { get; set; }
        public DbSet<RecommendedMealPlan> RecommendedMealPlans { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Patient> Patients { get; set; }


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
            userBuilder.Property(x => x.Email)
                       .IsRequired()
                       .HasMaxLength(200);
            userBuilder.Property(x => x.Password)
                        .IsRequired()
                        .HasMaxLength(200);
            userBuilder.Property(x => x.Salt)
                       .IsRequired().HasMaxLength(200);
            userBuilder.Property(x => x.Username)
                       .IsRequired()
                       .HasMaxLength(200);
            userBuilder.Property(x => x.Role)
                       .IsRequired()
                       .HasMaxLength(100);

            var productBuilder = modelBuilder.Entity<Product>();
            productBuilder.HasKey(x => x.Id);

            var recommendedMealPlansBuilder = modelBuilder.Entity<RecommendedMealPlan>();
            recommendedMealPlansBuilder.HasKey(x => x.Id);

            var doctorBuilder = modelBuilder.Entity<Doctor>();
            doctorBuilder.HasKey(x => x.UserId);

            var receptionistBuilder = modelBuilder.Entity<Receptionist>();
            receptionistBuilder.HasKey(x => x.UserId);

            var patientBuilder = modelBuilder.Entity<Patient>();
            patientBuilder.HasKey(x => x.UserId);

        }
    }
}
