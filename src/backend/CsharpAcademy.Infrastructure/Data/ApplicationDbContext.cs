using CsharpAcademy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Progress> ProgressRecords { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<UserBadge> UserBadges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial courses
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "C# Fundamentals for Beginners",
                Description = "Learn the basics of C# programming language, including variables, loops, and functions.",
                CreatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = 2,
                Title = "Object-Oriented Programming with C#",
                Description = "Master OOP concepts like classes, inheritance, and polymorphism in C#.",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
