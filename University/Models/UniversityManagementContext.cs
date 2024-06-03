using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Models;

public partial class UniversityManagementContext : DbContext
{
    public UniversityManagementContext()
    {
    }

    public UniversityManagementContext(DbContextOptions<UniversityManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementCategory> AnnouncementCategories { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<HolidaySchedule> HolidaySchedules { get; set; }

    public virtual DbSet<MeetingSchedule> MeetingSchedules { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TeachingSchedule> TeachingSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-TN56ENE\\SQLEXPRESS;Database=UniversityManagement;Integrated Security=true;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE44554403A8C7D");

            entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Announcem__Autho__4222D4EF");

            entity.HasOne(d => d.Category).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Announcem__Categ__4316F928");
        });

        modelBuilder.Entity<AnnouncementCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Announce__19093A2B3C69FB99");

            entity.ToTable("AnnouncementCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A009DE7BCC");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HolidaySchedule>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PK__HolidayS__2D35D59A2A4B4B5E");

            entity.ToTable("HolidaySchedule");

            entity.Property(e => e.HolidayId).HasColumnName("HolidayID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MeetingSchedule>(entity =>
        {
            entity.HasKey(e => e.MeetingId).HasName("PK__MeetingS__E9F9E9AC1FCDBCEB");

            entity.ToTable("MeetingSchedule");

            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA12603815502E78");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.PostImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Posts__AuthorID__173876EA");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Posts__CategoryI__182C9B23");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__PostCate__19093A2B0DAF0CB0");

            entity.ToTable("PostCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388060DEAE8");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeachingSchedule>(entity =>
        {
            entity.HasKey(e => e.TeachingId).HasName("PK__Teaching__69B6BA5E239E4DCF");

            entity.ToTable("TeachingSchedule");

            entity.Property(e => e.TeachingId).HasColumnName("TeachingID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Class).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__TeachingS__Class__276EDEB3");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__TeachingS__Subje__267ABA7A");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__TeachingS__Teach__25869641");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC7F60ED59");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4023D5A04").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
