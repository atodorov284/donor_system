using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DonorSystem.Models;

namespace DonorSystem
{
    /// <summary>
    /// Main class that creates database connection
    /// </summary>
    public partial class DonorDBContext : DbContext
    {
        public DonorDBContext() //empty constructor
        {
        }

        public DonorDBContext(DbContextOptions<DonorDBContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// List of donors.
        /// </summary>
        public virtual DbSet<Donor> Donors { get; set; }

        /// <summary>
        /// List of patients.
        /// </summary>
        public virtual DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// List of websites.
        /// </summary>
        public virtual DbSet<Website> Websites { get; set; }

        /// <summary>
        /// Connects to the localhost database donor_system with uID admin and password password.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=127.0.0.1;Database=donor_system;uID=admin;pwd=password;persistsecurityInfo=True");
            }
        }

        /// <summary>
        /// Creates new model on creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor>(entity =>
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

            modelBuilder.Entity<Patient>(entity =>
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

            modelBuilder.Entity<Website>(entity =>
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
