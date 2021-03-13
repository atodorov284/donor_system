using System;
using Microsoft.EntityFrameworkCore;
using DonorSystem.Models;

namespace DonorSystem
{
    public partial class DonorDBContext : DbContext
    {
        public DonorDBContext()
        {
        }

        public DonorDBContext(DbContextOptions<DonorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Donors> Donors { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Websites> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=127.0.0.1;Database=donor_system;uID=admin;pwd=password;persistsecurityInfo=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donors>(entity =>
            {
                entity.HasKey(e => e.DonorId)
                    .HasName("PRIMARY");

                entity.ToTable("donors");

                entity.Property(e => e.DonorId).HasColumnName("donorId");

                entity.Property(e => e.BloodGroup)
                    .HasColumnName("bloodGroup")
                    .HasMaxLength(2);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(45);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PRIMARY");

                entity.ToTable("patients");

                entity.Property(e => e.PatientId).HasColumnName("patientId");

                entity.Property(e => e.BloodGroup)
                    .HasColumnName("bloodGroup")
                    .HasMaxLength(2);

                entity.Property(e => e.Diagnose)
                    .HasColumnName("diagnose")
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(45);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Websites>(entity =>
            {
                entity.HasKey(e => e.WebsiteId)
                    .HasName("PRIMARY");

                entity.ToTable("websites");

                entity.Property(e => e.WebsiteId).HasColumnName("websiteId");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
