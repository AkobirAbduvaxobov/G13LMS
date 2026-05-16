using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherCourse> TeacherCourses { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
