using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class TeacherCourseSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.TeacherCourses.Any()) return;

        var teacherIds = context.Teachers.Select(t => t.TeacherId).ToList();
        var courseIds = context.Courses.Select(c => c.CourseId).ToList();

        var list = new List<TeacherCourse>();

        var rnd = new Random();

        foreach (var courseId in courseIds)
        {
            list.Add(new TeacherCourse
            {
                TeacherId = teacherIds[rnd.Next(teacherIds.Count)],
                CourseId = courseId
            });
        }

        await context.TeacherCourses.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}