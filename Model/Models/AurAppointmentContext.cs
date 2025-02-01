using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class AurAppointmentContext : DbContext
{
    public AurAppointmentContext()
    {
    }

    public AurAppointmentContext(DbContextOptions<AurAppointmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Taluka> Talukas { get; set; }

    public virtual DbSet<Village> Villages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8NFMH31\\ATIF;Initial Catalog=AurAppointment;User Id=sa;Password=Kaleem@123;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK_dbo.District");

            entity.ToTable("District");

            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Login");

            entity.ToTable("Login");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(250);
        });

        modelBuilder.Entity<Taluka>(entity =>
        {
            entity.HasKey(e => e.TalukaId).HasName("PK_dbo.Taluka");

            entity.ToTable("Taluka");

            entity.Property(e => e.TalukaId).HasColumnName("Taluka_ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");

            entity.HasOne(d => d.District).WithMany(p => p.Talukas)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District");
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.HasKey(e => e.VillageId).HasName("PK_dbo.Village");

            entity.ToTable("Village");

            entity.Property(e => e.VillageId).HasColumnName("Village_ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TalukaId).HasColumnName("Taluka_ID");

            entity.HasOne(d => d.Taluka).WithMany(p => p.Villages)
                .HasForeignKey(d => d.TalukaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Taluka");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
