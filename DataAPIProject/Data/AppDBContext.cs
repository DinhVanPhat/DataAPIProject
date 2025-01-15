using Microsoft.EntityFrameworkCore;
using DataAPIProject.Model;

namespace DataAPIProject.AppDbContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        // DbSet cho các bảng
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map cho bảng Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");  // Map với bảng Student

                entity.HasKey(e => e.ID);  // Khóa chính

                entity.Property(e => e.ID)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();  // SERIAL

                entity.Property(e => e.LastName)
                      .HasColumnName("lastname")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.FirstMidName)
                      .HasColumnName("firstmidname")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.EnrollmentDate)
                      .HasColumnName("enrollmentdate")
                      .IsRequired();

                // Quan hệ 1-n với Enrollment
                entity.HasMany(e => e.Enrollments)
                      .WithOne(e => e.Student)
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Map cho bảng Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");  // Map với bảng Course

                entity.HasKey(e => e.CourseID);  // Khóa chính

                entity.Property(e => e.CourseID)
                      .HasColumnName("courseid")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                      .HasColumnName("title")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Credits)
                      .HasColumnName("credits")
                      .IsRequired();

                // Quan hệ 1-n với Enrollment
                entity.HasMany(e => e.Enrollments)
                      .WithOne(e => e.Course)
                      .HasForeignKey(e => e.CourseID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Map cho bảng Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("enrollment");  // Map với bảng Enrollment

                entity.HasKey(e => e.EnrollmentID);  // Khóa chính

                entity.Property(e => e.EnrollmentID)
                      .HasColumnName("enrollmentid")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CourseID)
                      .HasColumnName("courseid")
                      .IsRequired();

                entity.Property(e => e.StudentID)
                      .HasColumnName("studentid")
                      .IsRequired();

                entity.Property(e => e.Grade)
                      .HasColumnName("grade")
                      .HasMaxLength(2);

                // Quan hệ n-1 với Course
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Quan hệ n-1 với Student
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
