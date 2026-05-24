
using LMSPro.Api.Configurations;
using LMSPro.Api.Data.DataSeeder;
using LMSPro.Api.Data;

namespace LMSPro.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.ConfigureDB();
        builder.ConfigureDI();

        // My test comment
        // My test comment
        // My test comment
        // My test comment
        // My test comment
        // My test comment
        // My coder
        // My coder
        // My coder
        // My test comment
        // My test comment
        // My test comment
        // salom
        // salom
        // salom
        // salom
        // salom
        // salom
        // salom
        // salom
        //Hi





        // hello
        // hello
        // hello
        // hello
        // hello
        // hello
        // hello
        // hello
        // hello

        // My test comment elshodni cometi
        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    await DbSeeder.SeedAsync(context);
        //}

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
