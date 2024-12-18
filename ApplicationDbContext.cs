using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Controllers;

namespace SchoolManagementSystem.Data
    .Migrations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<StudentCourseSelection> StudentCourseSelections { get; set; } = null!;
        public DbSet<Advisor> Advisors { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users tablosu için birincil anahtar
            modelBuilder.Entity<User>().HasKey(u => u.UserID);

            // Advisors tablosu için birincil anahtar
            modelBuilder.Entity<Advisor>().HasKey(a => a.AdvisorID);

            // Students tablosu
            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Advisor) // Bir öğrenci bir danışmana bağlı
                .WithMany(a => a.Student) // Bir danışmanın birden fazla öğrencisi olabilir
                .HasForeignKey(s => s.AdvisorID)
                .OnDelete(DeleteBehavior.Restrict); // Danışman silindiğinde öğrenciler korunur

            // Courses tablosu için birincil anahtar
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);

            // StudentCourseSelections tablosu
            modelBuilder.Entity<StudentCourseSelection>()
                .HasKey(scs => scs.SelectionID);

            modelBuilder.Entity<StudentCourseSelection>()
                .HasOne(scs => scs.Student) // Ders seçimi bir öğrenciye bağlı
                .WithMany(s => s.StudentCourseSelections) // Bir öğrencinin birden fazla ders seçimi olabilir
                .HasForeignKey(scs => scs.StudentID)
                .OnDelete(DeleteBehavior.Cascade); // Öğrenci silinirse ilgili seçimler de silinir

            modelBuilder.Entity<StudentCourseSelection>()
                .HasOne(scs => scs.Course) // Ders seçimi bir derse bağlı
                .WithMany(c => c.StudentCourseSelections) // Bir ders birden fazla seçime sahip olabilir
                .HasForeignKey(scs => scs.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Students tablosu için zorunlu alanlar
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired();

            // Courses tablosu için zorunlu alanlar
            modelBuilder.Entity<Course>()
                .Property(c => c.CourseName)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.CourseCode)
                .IsRequired();

            base.OnModelCreating(modelBuilder);


            // Student ve Advisor'ı User olarak tanımladım. 
            modelBuilder.Entity<Student>().HasBaseType<User>();
            modelBuilder.Entity<Advisor>().HasBaseType<User>();
        }
    }
}
