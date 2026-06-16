using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Interfaces;
using CsharpAcademy.Infrastructure.Data;
using CsharpAcademy.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpAcademy.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CONNECTION_STRING"] ?? configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            options.UseMySql(connectionString, serverVersion);
        });

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
