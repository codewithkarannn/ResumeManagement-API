using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ResumeManagement_API.Models;

public partial class ResumeManagementContext : DbContext
{
    public ResumeManagementContext()
    {
    }

    public ResumeManagementContext(DbContextOptions<ResumeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateCvfile> CandidateCvfiles { get; set; }

    public virtual DbSet<CityMaster> CityMasters { get; set; }

    public virtual DbSet<CountryMaster> CountryMasters { get; set; }

    public virtual DbSet<StatusMaster> StatusMasters { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;username=root;password=Admin@123;database=ResumeManagement");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PRIMARY");

            entity.ToTable("Candidate", "ResumeManagement");

            entity.HasIndex(e => e.CityId, "CityID");

            entity.HasIndex(e => e.StatusId, "StatusID");

            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Ctc)
                .HasPrecision(10)
                .HasColumnName("CTC");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ExpectedCtc)
                .HasPrecision(10)
                .HasColumnName("ExpectedCTC");
            entity.Property(e => e.InterviewDate).HasColumnType("datetime");
            entity.Property(e => e.InterviewFeedback).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.City).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("candidate_ibfk_2");

            entity.HasOne(d => d.Status).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("candidate_ibfk_1");
        });

        modelBuilder.Entity<CandidateCvfile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PRIMARY");

            entity.ToTable("CandidateCVFiles", "ResumeManagement");

            entity.HasIndex(e => e.CandidateId, "CandidateID_idx");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(250);
            entity.Property(e => e.FileType).HasMaxLength(50);

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateCvfiles)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("CandidateID");
        });

        modelBuilder.Entity<CityMaster>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PRIMARY");

            entity.ToTable("CityMaster", "ResumeManagement");

            entity.HasIndex(e => e.CountryId, "CountryID");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");

            entity.HasOne(d => d.Country).WithMany(p => p.CityMasters)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("citymaster_ibfk_1");
        });

        modelBuilder.Entity<CountryMaster>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("CountryMaster", "ResumeManagement");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("StatusMaster", "ResumeManagement");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserMasterId).HasName("PRIMARY");

            entity.ToTable("UserMaster", "ResumeManagement");

            entity.Property(e => e.UserMasterId).HasColumnName("UserMasterID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasColumnType("enum('Admin','User')");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
