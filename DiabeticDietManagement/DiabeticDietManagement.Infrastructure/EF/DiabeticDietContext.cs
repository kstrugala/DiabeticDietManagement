using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.EF
{
    public class DiabeticDietContext : DbContext
    {
        private readonly SqlSettings _settings;

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DietaryCompliance> DietaryCompliances { get; set; }

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

            var doctorBuilder = modelBuilder.Entity<Doctor>();
            doctorBuilder.HasKey(x => x.UserId);

            var receptionistBuilder = modelBuilder.Entity<Receptionist>();
            receptionistBuilder.HasKey(x => x.UserId);

            var patientBuilder = modelBuilder.Entity<Patient>();
            patientBuilder.HasKey(x => x.UserId);

            var dietaryComplianceBuilder = modelBuilder.Entity<DietaryCompliance>();
            dietaryComplianceBuilder.HasKey(x => x.Id);

            //var mealPlanBuilder = modelBuilder.Entity<MealPlan>();
            //mealPlanBuilder.HasKey(x => x.Id);
            //mealPlanBuilder.Property(x => x.Name).IsRequired().HasMaxLength(500);

            //var dailyMealPlanBuilder = modelBuilder.Entity<DailyMealPlan>();
            //dailyMealPlanBuilder
            //    .HasOne(mp => mp.MealPlan)
            //    .WithMany(dp => dp.DailyMealPlans)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var breakfastBuilder = modelBuilder.Entity<Breakfast>();
            //breakfastBuilder
            //    .HasOne(dmp => dmp.DailyMealPlan)
            //    .WithOne(b => b.Breakfast)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var snapBuilder = modelBuilder.Entity<Snap>();
            //snapBuilder
            //    .HasOne(dmp => dmp.DailyMealPlan)
            //    .WithOne(s => s.Snap)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var lunchBuilder = modelBuilder.Entity<Lunch>();
            //lunchBuilder
            //    .HasOne(dmp => dmp.DailyMealPlan)
            //    .WithOne(l => l.Lunch)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var dinnerBuilder = modelBuilder.Entity<Dinner>();
            //dinnerBuilder
            //    .HasOne(dmp => dmp.DailyMealPlan)
            //    .WithOne(d => d.Dinner)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var supperBuilder = modelBuilder.Entity<Supper>();
            //supperBuilder
            //    .HasOne(dmp => dmp.DailyMealPlan)
            //    .WithOne(s => s.Supper)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var portionBuilder = modelBuilder.Entity<Portion>();
            //portionBuilder
            //    .HasKey(x => x.Id);

        }
    }
}
