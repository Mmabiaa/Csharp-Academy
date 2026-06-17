using CsharpAcademy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

/// <summary>
/// Comprehensive platform seed data — courses, sections, topics, challenges, videos.
/// </summary>
public static class PlatformSeedData
{
    public static void Apply(ModelBuilder modelBuilder, DateTime seedDate)
    {
        SeedCourses(modelBuilder, seedDate);
        SeedModules(modelBuilder, seedDate);
        SeedLessons(modelBuilder, seedDate);
        SeedBadges(modelBuilder, seedDate);
        SeedQuizzes(modelBuilder, seedDate);
        SeedQuestions(modelBuilder, seedDate);
        SeedExercises(modelBuilder, seedDate);
        SeedTutorials(modelBuilder, seedDate);
        SeedVideos(modelBuilder, seedDate);
        SeedChallenges(modelBuilder, seedDate);
    }

    private static void SeedCourses(ModelBuilder mb, DateTime d) => mb.Entity<Course>().HasData(
        new Course { Id = 1, Title = "C# Fundamentals", Description = "Master C# from zero — variables, control flow, methods, and the .NET ecosystem. Perfect for beginners.", Level = "Beginner", EstimatedHours = 8, IsPublished = true, CreatedAt = d },
        new Course { Id = 2, Title = "Object-Oriented Programming", Description = "Classes, inheritance, polymorphism, interfaces, and design principles in C#.", Level = "Intermediate", EstimatedHours = 10, IsPublished = true, CreatedAt = d },
        new Course { Id = 3, Title = "ASP.NET Core Web Development", Description = "Build modern web APIs and MVC apps with ASP.NET Core, EF Core, and REST best practices.", Level = "Intermediate", EstimatedHours = 12, IsPublished = true, CreatedAt = d },
        new Course { Id = 4, Title = "Data Structures & Algorithms", Description = "Arrays, lists, stacks, queues, sorting, and searching — implemented in C#.", Level = "Advanced", EstimatedHours = 15, IsPublished = true, CreatedAt = d },
        new Course { Id = 5, Title = "LINQ & Functional C#", Description = "Query data with LINQ, lambdas, delegates, and functional patterns.", Level = "Intermediate", EstimatedHours = 6, IsPublished = true, CreatedAt = d }
    );

    private static void SeedModules(ModelBuilder mb, DateTime d) => mb.Entity<CourseModule>().HasData(
        new CourseModule { Id = 1, CourseId = 1, Title = "Section 1: Getting Started", Description = "Introduction to C# and your dev environment.", LearningObjectives = "Understand C# history; Set up .NET SDK; Run first program", Order = 1, CreatedAt = d },
        new CourseModule { Id = 2, CourseId = 1, Title = "Section 2: Variables & Types", Description = "Data types, variables, and operators.", LearningObjectives = "Declare variables; Use operators; Convert types", Order = 2, CreatedAt = d },
        new CourseModule { Id = 3, CourseId = 1, Title = "Section 3: Control Flow", Description = "Conditionals and loops.", LearningObjectives = "Write if/else; Use for and while loops", Order = 3, CreatedAt = d },
        new CourseModule { Id = 4, CourseId = 2, Title = "Section 1: Classes & Objects", Description = "OOP foundations.", LearningObjectives = "Define classes; Create objects; Use properties", Order = 1, CreatedAt = d },
        new CourseModule { Id = 5, CourseId = 2, Title = "Section 2: Inheritance", Description = "Extending and reusing code.", LearningObjectives = "Use inheritance; Override methods; Apply polymorphism", Order = 2, CreatedAt = d },
        new CourseModule { Id = 6, CourseId = 3, Title = "Section 1: Web API Basics", Description = "REST APIs with ASP.NET Core.", LearningObjectives = "Create controllers; Handle HTTP verbs; Return JSON", Order = 1, CreatedAt = d },
        new CourseModule { Id = 7, CourseId = 4, Title = "Section 1: Linear Structures", Description = "Arrays and linked lists.", LearningObjectives = "Implement arrays; Work with List<T>", Order = 1, CreatedAt = d },
        new CourseModule { Id = 8, CourseId = 5, Title = "Section 1: LINQ Essentials", Description = "Query syntax and method syntax.", LearningObjectives = "Write LINQ queries; Use lambdas", Order = 1, CreatedAt = d }
    );

    private static void SeedLessons(ModelBuilder mb, DateTime d) => mb.Entity<Lesson>().HasData(
        new Lesson { Id = 1, CourseModuleId = 1, Title = "What is C#?", Content = "C# is a modern, object-oriented language by Microsoft for the .NET platform.", BestPractices = "- Follow naming conventions\n- Use meaningful names", VoiceSummary = "C sharp is a modern language for dot NET.", Type = LessonType.Mixed, DurationMinutes = 10, Order = 1, CreatedAt = d },
        new Lesson { Id = 2, CourseModuleId = 1, Title = "Setting Up .NET", Content = "Install the .NET SDK from dotnet.microsoft.com and verify with `dotnet --version`.", BestPractices = "- Pin SDK with global.json", VoiceSummary = "Install dot NET SDK to start coding.", Type = LessonType.Reading, DurationMinutes = 15, Order = 2, CreatedAt = d },
        new Lesson { Id = 3, CourseModuleId = 2, Title = "Declaring Variables", Content = "```csharp\nint age = 25;\nstring name = \"Alice\";\n```", BestPractices = "- Initialize on declaration", VoiceSummary = "Variables store typed data.", Type = LessonType.Practice, DurationMinutes = 12, Order = 1, CreatedAt = d },
        new Lesson { Id = 4, CourseModuleId = 2, Title = "Operators", Content = "Arithmetic: +, -, *, /. Comparison: ==, !=, <, >.", Type = LessonType.Reading, DurationMinutes = 10, Order = 2, CreatedAt = d },
        new Lesson { Id = 5, CourseModuleId = 3, Title = "If Statements", Content = "```csharp\nif (score >= 70) Console.WriteLine(\"Pass\");\n```", Type = LessonType.Reading, DurationMinutes = 8, Order = 1, CreatedAt = d },
        new Lesson { Id = 6, CourseModuleId = 3, Title = "Loops", Content = "for, while, and foreach loops control repetition.", Type = LessonType.Practice, DurationMinutes = 15, Order = 2, CreatedAt = d },
        new Lesson { Id = 7, CourseModuleId = 4, Title = "Creating Classes", Content = "```csharp\npublic class Person { public string Name { get; set; } }\n```", BestPractices = "- Use properties not public fields", Type = LessonType.Mixed, DurationMinutes = 20, Order = 1, CreatedAt = d },
        new Lesson { Id = 8, CourseModuleId = 5, Title = "Inheritance", Content = "Derive classes with `: BaseClass` syntax.", Type = LessonType.Video, DurationMinutes = 18, Order = 1, CreatedAt = d },
        new Lesson { Id = 9, CourseModuleId = 6, Title = "Your First API", Content = "Create a Web API project with `dotnet new webapi`.", Type = LessonType.Reading, DurationMinutes = 25, Order = 1, CreatedAt = d },
        new Lesson { Id = 10, CourseModuleId = 7, Title = "Arrays in C#", Content = "Fixed-size collections: `int[] nums = new int[5];`", Type = LessonType.Practice, DurationMinutes = 15, Order = 1, CreatedAt = d },
        new Lesson { Id = 11, CourseModuleId = 8, Title = "Introduction to LINQ", Content = "Query collections: `items.Where(x => x > 5)`", Type = LessonType.Reading, DurationMinutes = 12, Order = 1, CreatedAt = d }
    );

    private static void SeedBadges(ModelBuilder mb, DateTime d) => mb.Entity<Badge>().HasData(
        new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 2, Name = "On Fire", Description = "3-day streak", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 3, Name = "Quick Learner", Description = "Complete 3 lessons", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 4, Name = "Quiz Whiz", Description = "Pass your first quiz", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 5, Name = "Graduate", Description = "Complete a course", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 6, Name = "Challenge Master", Description = "Solve 5 coding challenges", ImageUrl = "", CreatedAt = d }
    );

    private static void SeedQuizzes(ModelBuilder mb, DateTime d) => mb.Entity<Quiz>().HasData(
        new Quiz { Id = 1, LessonId = 1, Title = "C# Basics Quiz", CreatedAt = d },
        new Quiz { Id = 2, LessonId = 3, Title = "Variables Quiz", CreatedAt = d }
    );

    private static void SeedQuestions(ModelBuilder mb, DateTime d)
    {
        mb.Entity<Question>().HasData(
            new Question { Id = 1, QuizId = 1, Text = "Who developed C#?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 2, QuizId = 1, Text = "C# runs on .NET.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 3, QuizId = 2, Text = "Which keyword declares int?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 4, QuizId = 2, Text = "Fill in: text type is ___.", Type = QuestionType.FillInTheBlank, CorrectAnswer = "string", CreatedAt = d },
            new Question { Id = 5, QuizId = 1, Text = "Output of Console.WriteLine(2+3);", Type = QuestionType.OutputPrediction, CreatedAt = d }
        );
        mb.Entity<QuestionOption>().HasData(
            new QuestionOption { Id = 1, QuestionId = 1, Text = "Microsoft", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 2, QuestionId = 1, Text = "Google", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 3, QuestionId = 2, Text = "True", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 4, QuestionId = 2, Text = "False", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 5, QuestionId = 3, Text = "int", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 6, QuestionId = 3, Text = "string", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 7, QuestionId = 5, Text = "5", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 8, QuestionId = 5, Text = "23", IsCorrect = false, CreatedAt = d }
        );
    }

    private static void SeedExercises(ModelBuilder mb, DateTime d) => mb.Entity<CodingExercise>().HasData(
        new CodingExercise { Id = 1, LessonId = 1, Title = "Hello, C#!", Order = 1, Difficulty = 1, Instructions = "Print: Hello, C#!", StarterCode = "", ExpectedOutput = "Hello, C#!", Hint = "Console.WriteLine(\"Hello, C#!\");", CreatedAt = d },
        new CodingExercise { Id = 2, LessonId = 3, Title = "Add Numbers", Order = 1, Difficulty = 1, Instructions = "Print sum of 10 and 20.", StarterCode = "int a=10;int b=20;", ExpectedOutput = "30", Hint = "Console.WriteLine(a+b);", CreatedAt = d },
        new CodingExercise { Id = 3, LessonId = 7, Title = "Create Person", Order = 1, Difficulty = 2, Instructions = "Print name Bob.", StarterCode = "public class Person{public string Name{get;set;}}", ExpectedOutput = "Bob", Hint = "Console.WriteLine(p.Name);", CreatedAt = d }
    );

    private static void SeedTutorials(ModelBuilder mb, DateTime d) => mb.Entity<TutorialStep>().HasData(
        new TutorialStep { Id = 1, LessonId = 1, Order = 1, Title = "What is a language?", Content = "Languages give instructions to computers.", CreatedAt = d },
        new TutorialStep { Id = 2, LessonId = 1, Order = 2, Title = "First code", Content = "Output text:", CodeSample = "Console.WriteLine(\"Hello!\");", CreatedAt = d },
        new TutorialStep { Id = 3, LessonId = 3, Order = 1, Title = "Variables", Content = "Store values with types.", CodeSample = "int x = 5;", CreatedAt = d }
    );

    private static void SeedVideos(ModelBuilder mb, DateTime d) => mb.Entity<LessonVideo>().HasData(
        new LessonVideo { Id = 1, LessonId = 1, Title = "C# in 100 Seconds", VideoUrl = "https://www.youtube.com/watch?v=ravLFzWr5H4", Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 2, CreatedAt = d },
        new LessonVideo { Id = 2, LessonId = 1, Title = "Introduction to C#", VideoUrl = "https://www.youtube.com/watch?v=Wx2TKSol0I8", Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 15, CreatedAt = d },
        new LessonVideo { Id = 3, LessonId = 8, Title = "OOP in C#", VideoUrl = "https://www.youtube.com/watch?v=wqUfllZyeq4", Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 20, CreatedAt = d },
        new LessonVideo { Id = 4, LessonId = 9, Title = "ASP.NET Core Tutorial", VideoUrl = "https://www.youtube.com/watch?v=AhAxLiGC7Pc", Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 30, CreatedAt = d }
    );

    private static void SeedChallenges(ModelBuilder mb, DateTime d) => mb.Entity<CodingChallenge>().HasData(
        new CodingChallenge { Id = 1, Title = "FizzBuzz", Description = "Print numbers 1-5, replacing multiples of 3 with Fizz.", Difficulty = "Easy", StarterCode = "for(int i=1;i<=5;i++){}", ExpectedOutput = "1\n2\nFizz\n4\nFizz", Hint = "Use modulo operator %", Tags = "loops,conditionals", Order = 1, XpReward = 20, CreatedAt = d },
        new CodingChallenge { Id = 2, Title = "Reverse String", Description = "Reverse the string \"hello\" and print it.", Difficulty = "Easy", StarterCode = "string s=\"hello\";", ExpectedOutput = "olleh", Hint = "Use new string(s.Reverse().ToArray())", Tags = "strings", Order = 2, XpReward = 20, CreatedAt = d },
        new CodingChallenge { Id = 3, Title = "Sum Array", Description = "Sum the array [1,2,3,4,5] and print result.", Difficulty = "Easy", StarterCode = "int[] a={1,2,3,4,5};", ExpectedOutput = "15", Hint = "Use a loop or a.Sum()", Tags = "arrays", Order = 3, XpReward = 25, CreatedAt = d },
        new CodingChallenge { Id = 4, Title = "Factorial", Description = "Print factorial of 5 (120).", Difficulty = "Medium", StarterCode = "// compute 5!", ExpectedOutput = "120", Hint = "Multiply 1*2*3*4*5", Tags = "math,loops", Order = 4, XpReward = 30, CreatedAt = d },
        new CodingChallenge { Id = 5, Title = "Palindrome Check", Description = "Print True if \"racecar\" is palindrome.", Difficulty = "Medium", StarterCode = "string w=\"racecar\";", ExpectedOutput = "True", Hint = "Compare with reversed string", Tags = "strings", Order = 5, XpReward = 30, CreatedAt = d },
        new CodingChallenge { Id = 6, Title = "Fibonacci", Description = "Print the 7th Fibonacci number (13).", Difficulty = "Medium", StarterCode = "// fib sequence: 1,1,2,3,5,8,13", ExpectedOutput = "13", Hint = "Iterate with two variables", Tags = "math", Order = 6, XpReward = 35, CreatedAt = d },
        new CodingChallenge { Id = 7, Title = "Prime Check", Description = "Print True if 17 is prime.", Difficulty = "Hard", StarterCode = "int n=17;", ExpectedOutput = "True", Hint = "Check divisors up to sqrt(n)", Tags = "math", Order = 7, XpReward = 40, CreatedAt = d },
        new CodingChallenge { Id = 8, Title = "Binary Search", Description = "Print index of 7 in sorted array [1,3,5,7,9].", Difficulty = "Hard", StarterCode = "int[] a={1,3,5,7,9}; int target=7;", ExpectedOutput = "3", Hint = "Classic binary search", Tags = "algorithms", Order = 8, XpReward = 50, CreatedAt = d }
    );
}
