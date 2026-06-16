using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Interfaces;
using CsharpAcademy.Infrastructure.Data;
using CsharpAcademy.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpAcademy.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CONNECTION_STRING"]
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string not found. Set CONNECTION_STRING in src/backend/.env or ConnectionStrings:DefaultConnection in appsettings.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            options.UseMySql(connectionString, serverVersion);
        });

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IGamificationService, GamificationService>();
        services.AddScoped<ICodeExecutionService, RoslynCodeExecutionService>();
        services.AddScoped<IAiAssistantService, AiAssistantService>();

        return services;
    }
}
