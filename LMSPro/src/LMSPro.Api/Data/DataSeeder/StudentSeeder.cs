using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class StudentSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Students.Any()) return;

        var students = new List<Student>
        {
            new() { FirstName="Ali", LastName="Valiyev", Email="ali1@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Bekzod", LastName="Karimov", Email="bekzod@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Sardor", LastName="Toshmatov", Email="sardor@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Jasur", LastName="Aliyev", Email="jasur@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Aziza", LastName="Nazarova", Email="aziza@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Madina", LastName="Ergasheva", Email="madina@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Dilshod", LastName="Rasulov", Email="dilshod@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Umid", LastName="Xolmatov", Email="umid@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Shahnoza", LastName="Qodirova", Email="shahnoza@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Malika", LastName="Saidova", Email="malika@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Bobur", LastName="Yuldashev", Email="bobur@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Jamshid", LastName="Akbarov", Email="jamshid@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Nodira", LastName="Olimova", Email="nodira@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Gulnoza", LastName="Toirova", Email="gulnoza@gmail.com", RegisteredAt=DateTime.Now },
            new() { FirstName="Zafar", LastName="Mamatov", Email="zafar@gmail.com", RegisteredAt=DateTime.Now },
        };

        await context.Students.AddRangeAsync(students);
        await context.SaveChangesAsync();
    }
}