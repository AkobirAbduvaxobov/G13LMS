using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class TeacherSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Teachers.Any()) return;

        var teachers = new List<Teacher>
        {
            new() { FirstName="Olim", LastName="Karimov" },
            new() { FirstName="Rustam", LastName="Qodirov" },
            new() { FirstName="Diyor", LastName="Tursunov" },
            new() { FirstName="Shavkat", LastName="Aliyev" },
            new() { FirstName="Bekzod", LastName="Xolmatov" },
            new() { FirstName="Farhod", LastName="Rasulov" },
            new() { FirstName="Javohir", LastName="Nazarov" },
            new() { FirstName="Siroj", LastName="Ergashev" },
            new() { FirstName="Azamat", LastName="Yuldashev" },
            new() { FirstName="Islom", LastName="Saidov" },
        };

        await context.Teachers.AddRangeAsync(teachers);
        await context.SaveChangesAsync();
    }
}