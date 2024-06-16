using HospitalManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Reception> Receptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reception>()
                .HasOne(r => r.Client)
                .WithMany(u => u.ClientsReceptions)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Reception>()
                .HasOne(r => r.Doctor)
                .WithMany(u => u.DoctorsReceptions)
                .HasForeignKey(r => r.DoctorId);
        }
    }
}
