using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HealthcarePortal.Models;

public partial class HealthcareContext : DbContext
{
    public HealthcareContext()
    {
    }

    public HealthcareContext(DbContextOptions<HealthcareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:HealthcareContext") ??
    //                               Configuration.GetConnectionString("HealthcareContext");
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AId).HasName("PK__Admin__71AC6D415117F576");

            entity.ToTable("Admin");

            entity.Property(e => e.AId)
                .ValueGeneratedNever()
                .HasColumnName("A_ID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2FDE9358B");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId)
                .ValueGeneratedNever()
                .HasColumnName("AppointmentID");
            entity.Property(e => e.DId).HasColumnName("D_ID");
            entity.Property(e => e.PId).HasColumnName("P_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.DIdNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Appointment_Doctor");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Appointment_Patient");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillNo).HasName("PK__Bill__11F28419494618A4");

            entity.ToTable("Bill");

            entity.Property(e => e.BillNo).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PId).HasColumnName("P_ID");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Bill_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DId).HasName("PK__Doctor__76B8FF7DC6A928E7");

            entity.ToTable("Doctor");

            entity.Property(e => e.DId)
                .ValueGeneratedNever()
                .HasColumnName("D_ID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Medical___FBDF78C98493E1A0");

            entity.ToTable("Medical_Records");

            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("RecordID");
            entity.Property(e => e.Attachment).IsUnicode(false);
            entity.Property(e => e.DId).HasColumnName("D_ID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.PId).HasColumnName("P_ID");
            entity.Property(e => e.Prescription).IsUnicode(false);

            entity.HasOne(d => d.DIdNavigation).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MedicalRecords_Doctor");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MedicalRecords_Patient");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Patient__A3420A7778C093FC");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.AdharNumber, "UQ__Patient__2D5561712CA3292F").IsUnique();

            entity.Property(e => e.PId)
                .ValueGeneratedNever()
                .HasColumnName("P_ID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.AdharNumber).HasColumnName("adhar_number");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EmailID");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
