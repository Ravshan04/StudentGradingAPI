using Microsoft.EntityFrameworkCore;

using StudentGradingAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StudentGradingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student va Grade o'rtasidagi bog'lanish
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            // Ba'zi test ma'lumotlarini qo'shish
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "Aziz Karimov", Group = "AT-1", EnrollmentDate = DateTime.Parse("2023-09-01") },
                new Student { Id = 2, FullName = "Dilnoza Aliyeva", Group = "AT-1", EnrollmentDate = DateTime.Parse("2023-09-01") },
                new Student { Id = 3, FullName = "Jasur Toshmatov", Group = "AT-2", EnrollmentDate = DateTime.Parse("2023-09-01") }
            );

            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 1, Subject = "Matematika", Score = 85, GradeDate = DateTime.Parse("2023-12-15"), StudentId = 1 },
                new Grade { Id = 2, Subject = "Fizika", Score = 78, GradeDate = DateTime.Parse("2023-12-16"), StudentId = 1 },
                new Grade { Id = 3, Subject = "Matematika", Score = 92, GradeDate = DateTime.Parse("2023-12-15"), StudentId = 2 },
                new Grade { Id = 4, Subject = "Fizika", Score = 88, GradeDate = DateTime.Parse("2023-12-16"), StudentId = 2 },
                new Grade { Id = 5, Subject = "Matematika", Score = 76, GradeDate = DateTime.Parse("2023-12-15"), StudentId = 3 }
            );
        }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}