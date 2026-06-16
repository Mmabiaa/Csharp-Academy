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
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomMember> ClassroomMembers { get; set; }
    public DbSet<CodingExercise> CodingExercises { get; set; }
    public DbSet<TutorialStep> TutorialSteps { get; set; }
    public DbSet<PracticeCompletion> PracticeCompletions { get; set; }

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

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => c.CertificateCode)
            .IsUnique();

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => new { c.UserId, c.CourseId })
            .IsUnique();

        modelBuilder.Entity<Certificate>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Certificate>()
            .HasOne(c => c.Course)
            .WithMany()
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Classroom>()
            .HasIndex(c => c.JoinCode)
            .IsUnique();

        modelBuilder.Entity<ClassroomMember>()
            .HasIndex(m => new { m.ClassroomId, m.UserId })
            .IsUnique();

        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Teacher)
            .WithMany()
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Course)
            .WithMany()
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ClassroomMember>()
            .HasOne(m => m.Classroom)
            .WithMany(c => c.Members)
            .HasForeignKey(m => m.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClassroomMember>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CodingExercise>()
            .HasOne(e => e.Lesson)
            .WithMany(l => l.CodingExercises)
            .HasForeignKey(e => e.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TutorialStep>()
            .HasOne(s => s.Lesson)
            .WithMany(l => l.TutorialSteps)
            .HasForeignKey(s => s.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PracticeCompletion>()
            .HasIndex(p => new { p.UserId, p.ExerciseId })
            .IsUnique();

        modelBuilder.Entity<PracticeCompletion>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PracticeCompletion>()
            .HasOne(p => p.Exercise)
            .WithMany()
            .HasForeignKey(p => p.ExerciseId)
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
                BestPractices = "- Use meaningful names for variables and methods\n- Follow C# naming conventions (PascalCase for types/methods, camelCase for locals)\n- Prefer `var` when the type is obvious from the right-hand side\n- Keep methods small and focused on one task",
                VoiceSummary = "C# is a modern object-oriented language from Microsoft. It runs on the .NET platform and is used for web, desktop, and mobile apps. In this lesson you'll learn what makes C# popular and where it's used.",
                Order = 1,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 2,
                CourseModuleId = 1,
                Title = "Setting Up Your Environment",
                Content = "To start coding in C#, you need the .NET SDK and a code editor like Visual Studio or VS Code. Download the SDK from dotnet.microsoft.com and verify installation with `dotnet --version`.",
                BestPractices = "- Pin your SDK version in a `global.json` for team projects\n- Use VS Code with the C# Dev Kit extension for a lightweight setup\n- Run `dotnet --info` to verify SDK and runtime versions\n- Create projects with `dotnet new` rather than copying folders",
                VoiceSummary = "To start coding in C sharp, install the dot NET SDK and a code editor like Visual Studio Code. Download from dot net dot microsoft dot com and verify with dotnet dash dash version in your terminal.",
                Order = 2,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 3,
                CourseModuleId = 2,
                Title = "Declaring Variables",
                Content = "Variables store data values. In C#, you declare a variable with a type and name:\n\n```csharp\nint age = 25;\nstring name = \"Alice\";\ndouble price = 19.99;\n```",
                BestPractices = "- Initialize variables when you declare them when possible\n- Use `const` for values that never change\n- Choose the smallest appropriate type (`int` vs `long`)\n- Avoid magic numbers — use named constants instead",
                VoiceSummary = "Variables store data in C sharp. Declare them with a type and name, like int age equals twenty five, or string name equals Alice. C sharp is statically typed, so the compiler checks types at build time.",
                Order = 1,
                CreatedAt = SeedDate
            },
            new Lesson
            {
                Id = 4,
                CourseModuleId = 3,
                Title = "Creating Classes",
                Content = "A class is a blueprint for creating objects. It defines properties and methods:\n\n```csharp\npublic class Person\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n}\n```",
                BestPractices = "- Use properties instead of public fields\n- Apply encapsulation — expose only what's needed\n- Name classes with nouns (Person, OrderService)\n- Keep one responsibility per class (Single Responsibility Principle)",
                VoiceSummary = "A class is a blueprint for objects in C sharp. Define properties like Name and Age, then create instances with the new keyword. Classes are the foundation of object-oriented programming.",
                Order = 1,
                CreatedAt = SeedDate
            }
        );

        modelBuilder.Entity<Badge>().HasData(
            new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 2, Name = "On Fire", Description = "Maintain a 3-day learning streak", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 3, Name = "Quick Learner", Description = "Complete 3 lessons", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 4, Name = "Quiz Whiz", Description = "Pass your first quiz", ImageUrl = "", CreatedAt = SeedDate },
            new Badge { Id = 5, Name = "Graduate", Description = "Complete an entire course", ImageUrl = "", CreatedAt = SeedDate }
        );

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz { Id = 1, LessonId = 1, Title = "C# Basics Quiz", CreatedAt = SeedDate },
            new Quiz { Id = 2, LessonId = 3, Title = "Variables Quiz", CreatedAt = SeedDate }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { Id = 1, QuizId = 1, Text = "Who developed C#?", Type = QuestionType.MultipleChoice, CreatedAt = SeedDate },
            new Question { Id = 2, QuizId = 1, Text = "C# runs on the .NET platform.", Type = QuestionType.TrueFalse, CreatedAt = SeedDate },
            new Question { Id = 3, QuizId = 2, Text = "Which keyword declares an integer variable?", Type = QuestionType.MultipleChoice, CreatedAt = SeedDate },
            new Question { Id = 4, QuizId = 2, Text = "Fill in the blank: The keyword for text/strings in C# is ___.", Type = QuestionType.FillInTheBlank, CorrectAnswer = "string", CreatedAt = SeedDate },
            new Question { Id = 5, QuizId = 1, Text = "What is the output of: Console.WriteLine(2 + 3);", Type = QuestionType.OutputPrediction, CreatedAt = SeedDate }
        );

        modelBuilder.Entity<QuestionOption>().HasData(
            new QuestionOption { Id = 1, QuestionId = 1, Text = "Microsoft", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 2, QuestionId = 1, Text = "Google", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 3, QuestionId = 1, Text = "Apple", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 4, QuestionId = 2, Text = "True", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 5, QuestionId = 2, Text = "False", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 6, QuestionId = 3, Text = "int", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 7, QuestionId = 3, Text = "string", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 8, QuestionId = 3, Text = "bool", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 9, QuestionId = 5, Text = "5", IsCorrect = true, CreatedAt = SeedDate },
            new QuestionOption { Id = 10, QuestionId = 5, Text = "23", IsCorrect = false, CreatedAt = SeedDate },
            new QuestionOption { Id = 11, QuestionId = 5, Text = "Error", IsCorrect = false, CreatedAt = SeedDate }
        );

        modelBuilder.Entity<CodingExercise>().HasData(
            new CodingExercise
            {
                Id = 1, LessonId = 1, Title = "Hello, C#!", Order = 1, Difficulty = 1,
                Instructions = "Use Console.WriteLine to print exactly: Hello, C#!",
                StarterCode = "// Print your message below\n",
                ExpectedOutput = "Hello, C#!",
                Hint = "Use Console.WriteLine(\"Hello, C#!\");",
                CreatedAt = SeedDate
            },
            new CodingExercise
            {
                Id = 2, LessonId = 3, Title = "Add Two Numbers", Order = 1, Difficulty = 1,
                Instructions = "Declare two integers (10 and 20) and print their sum using Console.WriteLine.",
                StarterCode = "int a = 10;\nint b = 20;\n// Print the sum\n",
                ExpectedOutput = "30",
                Hint = "Use Console.WriteLine(a + b);",
                CreatedAt = SeedDate
            },
            new CodingExercise
            {
                Id = 3, LessonId = 4, Title = "Create a Person", Order = 1, Difficulty = 2,
                Instructions = "Create a Person class with Name property, instantiate it with name \"Bob\", and print the name.",
                StarterCode = "public class Person { public string Name { get; set; } }\nvar p = new Person { Name = \"Bob\" };\n// Print p.Name\n",
                ExpectedOutput = "Bob",
                Hint = "Use Console.WriteLine(p.Name);",
                CreatedAt = SeedDate
            }
        );

        modelBuilder.Entity<TutorialStep>().HasData(
            new TutorialStep { Id = 1, LessonId = 1, Order = 1, Title = "What is a programming language?",
                Content = "A programming language lets you give instructions to a computer. C# is designed to be readable and powerful.",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 2, LessonId = 1, Order = 2, Title = "Who makes C#?",
                Content = "Microsoft created C# together with the .NET platform. It's open-source and cross-platform today.",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 3, LessonId = 1, Order = 3, Title = "Your first line of code",
                Content = "Every C# program can output text to the console:", CodeSample = "Console.WriteLine(\"Hello, World!\");",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 4, LessonId = 3, Order = 1, Title = "Why variables?",
                Content = "Variables label memory locations so you can reuse and update values throughout your program.",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 5, LessonId = 3, Order = 2, Title = "Declaring an integer",
                Content = "Use `int` for whole numbers:", CodeSample = "int count = 0;",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 6, LessonId = 3, Order = 3, Title = "Declaring a string",
                Content = "Use `string` for text. Strings use double quotes:", CodeSample = "string greeting = \"Hello\";",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 7, LessonId = 4, Order = 1, Title = "Classes vs objects",
                Content = "A class defines structure; an object is a specific instance created from that class.",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 8, LessonId = 4, Order = 2, Title = "Defining a class",
                Content = "Use the class keyword and add properties:", CodeSample = "public class Car { public string Model { get; set; } }",
                CreatedAt = SeedDate },
            new TutorialStep { Id = 9, LessonId = 4, Order = 3, Title = "Creating an object",
                Content = "Use `new` to create an instance:", CodeSample = "var car = new Car { Model = \"Sedan\" };",
                CreatedAt = SeedDate }
        );
    }
}
