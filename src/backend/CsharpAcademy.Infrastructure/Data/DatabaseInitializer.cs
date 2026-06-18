using CsharpAcademy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CsharpAcademy.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            await context.Database.MigrateAsync();

            string[] roles = ["Student", "Teacher", "Admin"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
            }

            await SeedDemoUsersAsync(userManager, context, logger);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database migration or seeding failed. Ensure MySQL is running and CONNECTION_STRING is configured.");
        }
    }

    private static async Task SeedDemoUsersAsync(UserManager<User> userManager, ApplicationDbContext context, ILogger logger)
    {
        var demos = new[]
        {
            ("teacher@academy.com", "Teacher123!", "Demo", "Teacher", "Teacher"),
            ("admin@academy.com", "Admin123!", "Platform", "Admin", "Admin")
        };

        User? teacher = null;
        foreach (var (email, password, first, last, role) in demos)
        {
            if (await userManager.FindByEmailAsync(email) is not null) continue;

            var user = new User
            {
                UserName = email,
                Email = email,
                FirstName = first,
                LastName = last,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                logger.LogInformation("Seeded demo user: {Email} ({Role})", email, role);
                if (role == "Teacher") teacher = user;
            }
        }

        if (teacher is null)
            teacher = await userManager.FindByEmailAsync("teacher@academy.com");

        if (teacher is not null && !await context.Assignments.AnyAsync())
        {
            context.Assignments.AddRange(
                new Assignment
                {
                    Title = "Hello World Program",
                    Description = "Write your first C# console application.",
                    Instructions = "Submit code that prints 'Hello, Academy!' using Console.WriteLine.",
                    CourseId = 1,
                    CreatedById = teacher.Id,
                    DueDate = DateTime.UtcNow.AddDays(14),
                    MaxPoints = 100,
                    RequiresCode = true
                },
                new Assignment
                {
                    Title = "Variables Practice",
                    Description = "Demonstrate variable usage in C#.",
                    Instructions = "Declare int, string, and bool variables. Submit code that prints all three values.",
                    CourseId = 1,
                    LessonId = 3,
                    CreatedById = teacher.Id,
                    DueDate = DateTime.UtcNow.AddDays(21),
                    MaxPoints = 100,
                    RequiresCode = true
                },
                new Assignment
                {
                    Title = "OOP Reflection Essay",
                    Description = "Explain OOP concepts in your own words.",
                    Instructions = "Write 200+ words explaining classes, objects, and inheritance in C#.",
                    CourseId = 2,
                    CreatedById = teacher.Id,
                    DueDate = DateTime.UtcNow.AddDays(30),
                    MaxPoints = 50,
                    RequiresCode = false
                }
            );
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded demo assignments.");
        }
    }
}
