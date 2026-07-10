
using FluentValidation;
using LMSPro.Api.Configurations;
using LMSPro.Api.Data;
using LMSPro.Api.Data.DataSeeder;
using LMSPro.Api.Dtos;
using LMSPro.Api.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace LMSPro.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddValidatorsFromAssembly(typeof(CourseCreateDto).Assembly);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        builder.Services.AddHealthChecks()
            .AddCheck<HealthChecks.DatabaseHealthCheck>("database");

        builder.ConfigureSerilog();
        builder.ConfigureDB();
        builder.ConfigureDI();
        builder.ConfigureCache();
        builder.ConfigureJwt();
        builder.AddJwtAuthentication();


        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    await DbSeeder.SeedAsync(context);
        //}

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseOutputCache();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseRequestLogging();

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Status = report.Status.ToString(),
                    TotalDurationMs = report.TotalDuration.TotalMilliseconds,
                    Checks = report.Entries.Select(e => new
                    {
                        Name = e.Key,
                        Status = e.Value.Status.ToString(),
                        Description = e.Value.Description,
                        DurationMs = e.Value.Duration.TotalMilliseconds
                    })
                });
            }
        });
        app.MapControllers();

        app.Run();
    }
}
