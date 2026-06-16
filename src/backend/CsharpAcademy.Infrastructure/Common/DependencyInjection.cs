using CsharpAcademy.Infrastructure.Data;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpAcademy.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Get connection string from environment variable with fallback to appsettings
        var connectionString = configuration["CONNECTION_STRING"] ?? configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Register repositories
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
