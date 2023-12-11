using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCWebAppEFTest.DAL.Entities;

public partial class MVCWebAppEFTestContext : DbContext
{
    public MVCWebAppEFTestContext()
    {
    }

    public MVCWebAppEFTestContext(DbContextOptions<MVCWebAppEFTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<VisitorLog> VisitorLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.ToTable("Visitor");

            entity.Property(e => e.Address)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.CheckInDateTime).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDateTime).HasColumnType("datetime");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientFin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PatientFIN");
            entity.Property(e => e.PatientLocation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientMrn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PatientMRN");
        });

        modelBuilder.Entity<VisitorLog>(entity =>
        {
            entity.HasKey(e => e.VisitorLogsId);

            entity.HasOne(d => d.Patient).WithMany(p => p.VisitorLogs)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitorLogs_Patient");

            entity.HasOne(d => d.Visitor).WithMany(p => p.VisitorLogs)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitorLogs_Visitor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
