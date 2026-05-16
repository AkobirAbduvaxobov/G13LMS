using LMSPro.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMSPro.Api.Configurations;

public static class DatabaseConfigurations
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}
