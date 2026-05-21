using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class CourseSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Courses.Any()) return;

        var courses = new List<Course>
        {
            new() { Title="C# Asoslari", Description="C# boshlang'ich kurs", Price=120000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=30, AccessPeriodDays=60 },
            new() { Title="ASP.NET Core Web API", Description="API yaratish", Price=180000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=40, AccessPeriodDays=90 },
            new() { Title="SQL Server", Description="Database asoslari", Price=100000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=25, AccessPeriodDays=60 },
            new() { Title="Entity Framework Core", Description="ORM texnologiya", Price=130000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=20, AccessPeriodDays=50 },
            new() { Title="Frontend HTML CSS", Description="Web dizayn", Price=90000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=15, AccessPeriodDays=40 },
            new() { Title="JavaScript", Description="JS asoslari", Price=110000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=20, AccessPeriodDays=50 },
            new() { Title="React JS", Description="Frontend framework", Price=150000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=25, AccessPeriodDays=60 },
            new() { Title="Python", Description="Dasturlash asoslari", Price=120000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=30, AccessPeriodDays=60 },
            new() { Title="Django", Description="Backend framework", Price=160000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=35, AccessPeriodDays=70 },
            new() { Title="Docker Basics", Description="Container texnologiya", Price=140000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=15, AccessPeriodDays=40 },
            new() { Title="Microservices", Description="Arxitektura", Price=200000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=45, AccessPeriodDays=90 },
            new() { Title="Clean Architecture", Description="Kod struktura", Price=170000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=25, AccessPeriodDays=60 },
            new() { Title="Git & GitHub", Description="Version control", Price=80000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=10, AccessPeriodDays=30 },
            new() { Title="SignalR", Description="Real-time app", Price=130000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=20, AccessPeriodDays=50 },
            new() { Title="Unit Testing", Description="Test yozish", Price=100000, CreatedAt=DateTime.Now, IsActive=true, DurationDays=15, AccessPeriodDays=40 },
        };

        await context.Courses.AddRangeAsync(courses);
        await context.SaveChangesAsync();
    }
}