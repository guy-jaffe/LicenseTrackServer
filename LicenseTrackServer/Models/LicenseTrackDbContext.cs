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
            entity.HasKey(e => e.Id).HasName("PK__lessons__3214EC072E96BE75");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Lessons).HasConstraintName("FK__lessons__Instruc__300424B4");

            entity.HasOne(d => d.Student).WithMany(p => p.Lessons).HasConstraintName("FK__lessons__Student__2F10007B");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__students__3214EC07960CB9A6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LicenseStatus).HasDefaultValue(0);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__students__Id__2B3F6F97");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teachers__3214EC071E516612");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConfirmationStatus).HasDefaultValue(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Teacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__teachers__Id__276EDEB3");
        });

        modelBuilder.Entity<TeacherWorkHour>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.DayDate, e.StartTime }).HasName("PK__teacher___D6D95F489FE35B46");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherWorkHours)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__teacher_w__Teach__32E0915F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07E263FD8C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
