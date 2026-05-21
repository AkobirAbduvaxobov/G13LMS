using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class LessonSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Lessons.Any()) return;

        var courseIds = context.Courses.Select(x => x.CourseId).ToList();

        var lessons = new List<Lesson>();

        foreach (var courseId in courseIds)
        {
            for (int i = 1; i <= 3; i++)
            {
                lessons.Add(new Lesson
                {
                    Title = $"Dars {i}",
                    Content = "O'zbekcha dars materiali",
                    Order = i,
                    Duration = TimeSpan.FromMinutes(45),
                    CourseId = courseId
                });
            }
        }

        await context.Lessons.AddRangeAsync(lessons);
        await context.SaveChangesAsync();
    }
}