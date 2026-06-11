
using LMSPro.Api.Configurations;
using LMSPro.Api.Data.DataSeeder;
using LMSPro.Api.Data;
using Serilog;
using FluentValidation;
using LMSPro.Api.Dtos;

namespace LMSPro.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        builder.Logging.ClearProviders(); // Remove default logging providers
        builder.Logging.AddSerilog(dispose: true); // Add Serilog as the logging provider

        //builder.Services.AddValidatorsFromAssemblyContaining<CourseCreateDto>();
        builder.Services.AddValidatorsFromAssembly(typeof(CourseCreateDto).Assembly);
        

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.ConfigureDB();
        builder.ConfigureDI();

       
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
