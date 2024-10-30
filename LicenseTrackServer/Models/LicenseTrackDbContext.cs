using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

public partial class LicenseTrackDbContext : DbContext
{
    public LicenseTrackDbContext()
    {
    }

    public LicenseTrackDbContext(DbContextOptions<LicenseTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherWorkHour> TeacherWorkHours { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=admin123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lessons__3214EC07D2E3D204");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Lessons).HasConstraintName("FK__lessons__Instruc__2C3393D0");

            entity.HasOne(d => d.Student).WithMany(p => p.Lessons).HasConstraintName("FK__lessons__Student__2B3F6F97");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__students__3214EC07B4764405");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teachers__3214EC072DC68A75");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherWorkHour>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.DayDate, e.StartTime }).HasName("PK__teacher___D6D95F484353F219");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherWorkHours)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__teacher_w__Teach__2F10007B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07DD59A349");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
