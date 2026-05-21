using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Data.DataSeeder;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        await CourseSeeder.Seed(context);
        await StudentSeeder.Seed(context);
        await TeacherSeeder.Seed(context);

        await TeacherCourseSeeder.Seed(context);
        await LessonSeeder.Seed(context);
        await EnrollmentSeeder.Seed(context);

        await QuestionSeeder.Seed(context);
    }
}
