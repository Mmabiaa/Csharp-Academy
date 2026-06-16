using CsharpAcademy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private static readonly DateTime SeedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
    public DbSet<QuizAttempt> QuizAttempts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.UserId, e.CourseId })
            .IsUnique();

        modelBuilder.Entity<Progress>()
            .HasIndex(p => new { p.UserId, p.LessonId })
            .IsUnique();

        modelBuilder.Entity<CourseModule>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.CourseModule)
            .WithMany(m => m.Lessons)
            .HasForeignKey(l => l.CourseModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.User)
            .WithMany(u => u.ProgressRecords)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.Lesson)
            .WithMany()
            .HasForeignKey(p => p.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.Quiz)
            .WithMany()
            .HasForeignKey(a => a.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "C# Fundamentals for Beginners",
                Description = "Learn the basics of C# programming language, including variables, loops, and functions.",
                CreatedAt = SeedDate
            },
            new Course
            {
                Id = 2,
                Title = "Object-Oriented Programming with C#",
                Description = "Master OOP concepts like classes, inheritance, and polymorphism in C#.",
                CreatedAt = SeedDate
            }
        );

        modelBuilder.Entity<CourseModule>().HasData(
            new CourseModule
            {
                Id = 1,
                CourseId = 1,
                Title = "Getting Started",
                Description = "Introduction to C# and your development environment.",
                Order = 1,
                CreatedAt = SeedDate
            },
            new CourseModule
            {
                Id = 2,
                CourseId = 1,
                Title = "Variables and Data Types",
                Description = "Learn about variables, constants, and basic data types in C#.",
                Order = 2,
                CreatedAt = SeedDate
            },
            new CourseModule
            {
                Id = 3,
                CourseId = 2,
                Title = "Classes and Objects",
                Description = "Understand the building blocks of object-oriented programming.",
                Order = 1,
                CreatedAt = SeedDate
            }
        );

        modelBuilder.Entity<Lesson>().HasData(
            new Lesson
            {
                Id = 1,
                CourseModuleId = 1,
                Title = "What is C#?",
                Content = "C# is a modern, object-oriented programming language developed by Microsoft. It runs on the .NET platform and is widely used for web, desktop, and mobile applications.",
                Order = 1,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 2,
                CourseModuleId = 1,
                Title = "Setting Up Your Environment",
                Content = "To start coding in C#, you need the .NET SDK and a code editor like Visual Studio or VS Code. Download the SDK from dotnet.microsoft.com and verify installation with `dotnet --version`.",
                Order = 2,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 3,
                CourseModuleId = 2,
                Title = "Declaring Variables",
                Content = "Variables store data values. In C#, you declare a variable with a type and name:\n\n```csharp\nint age = 25;\nstring name = \"Alice\";\ndouble price = 19.99;\n```",
                Order = 1,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 4,
                CourseModuleId = 3,
                Title = "Creating Classes",
                Content = "A class is a blueprint for creating objects. It defines properties and methods:\n\n```csharp\npublic class Person\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n}\n```",
                Order = 1,
                CreatedAt = SeedDate
            }
        );

        modelBuilder.Entity<Badge>().HasData(
            new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 2, Name = "On Fire", Description = "Maintain a 3-day learning streak", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 3, Name = "Quick Learner", Description = "Complete 3 lessons", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 4, Name = "Quiz Whiz", Description = "Pass your first quiz", ImageUrl = "", CreatedAt = SeedDate }
        );

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz { Id = 1, LessonId = 1, Title = "C# Basics Quiz", CreatedAt = SeedDate },
            new Quiz { Id = 2, LessonId = 3, Title = "Variables Quiz", CreatedAt = SeedDate }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { Id = 1, QuizId = 1, Text = "Who developed C#?", Type = QuestionType.MultipleChoice, CreatedAt = SeedDate },
            new Question { Id = 2, QuizId = 1, Text = "C# runs on the .NET platform.", Type = QuestionType.TrueFalse, CreatedAt = SeedDate },
            new Question { Id = 3, QuizId = 2, Text = "Which keyword declares an integer variable?", Type = QuestionType.MultipleChoice, CreatedAt = SeedDate }
        );

        modelBuilder.Entity<QuestionOption>().HasData(
            new QuestionOption { Id = 1, QuestionId = 1, Text = "Microsoft", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 2, QuestionId = 1, Text = "Google", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 3, QuestionId = 1, Text = "Apple", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 4, QuestionId = 2, Text = "True", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 5, QuestionId = 2, Text = "False", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 6, QuestionId = 3, Text = "int", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 7, QuestionId = 3, Text = "string", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 8, QuestionId = 3, Text = "bool", IsCorrect = false, CreatedAt = SeedDate }
        );
    }
}
