using LMSPro.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LMSPro.Api.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext DbContext;

    public DatabaseHealthCheck(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var canConnect = await DbContext.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? HealthCheckResult.Healthy("Database connection is healthy.")
            : HealthCheckResult.Unhealthy("Database connection failed.");
    }
}
