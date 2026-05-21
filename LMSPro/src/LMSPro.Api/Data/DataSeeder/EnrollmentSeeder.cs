using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class EnrollmentSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Enrollments.Any()) return;

        var studentIds = context.Students.Select(s => s.StudentId).ToList();
        var courseIds = context.Courses.Select(c => c.CourseId).ToList();

        var enrollments = new List<Enrollment>();
        var usedPairs = new HashSet<(long StudentId, long CourseId)>();
        var rnd = new Random();

        int targetCount = 100;

        while (enrollments.Count < targetCount)
        {
            var studentId = studentIds[rnd.Next(studentIds.Count)];
            var courseId = courseIds[rnd.Next(courseIds.Count)];

            if (!usedPairs.Add((studentId, courseId)))
                continue;

            enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrolledAt = DateTime.UtcNow.AddDays(-rnd.Next(1, 200))
            });
        }

        await context.Enrollments.AddRangeAsync(enrollments);
        await context.SaveChangesAsync();
    }
}