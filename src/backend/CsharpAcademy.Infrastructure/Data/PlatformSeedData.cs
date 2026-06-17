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
        new Course
        {
            Id = 1,
            Title = "C# Fundamentals",
            Description = "Master C# from zero. Learn variables, data types, control flow, methods, and .NET basics. Perfect for complete beginners!",
            Level = "Beginner",
            EstimatedHours = 12,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 2,
            Title = "Object-Oriented Programming in C#",
            Description = "Deep dive into OOP concepts: classes, inheritance, polymorphism, encapsulation, and design principles like SOLID.",
            Level = "Intermediate",
            EstimatedHours = 16,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 3,
            Title = "ASP.NET Core Web APIs",
            Description = "Build professional RESTful APIs with ASP.NET Core, EF Core, authentication, and modern architecture patterns.",
            Level = "Intermediate",
            EstimatedHours = 20,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 4,
            Title = "Data Structures & Algorithms with C#",
            Description = "Master essential data structures (arrays, lists, stacks, queues, trees) and algorithms (sorting, searching, dynamic programming).",
            Level = "Advanced",
            EstimatedHours = 24,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 5,
            Title = "LINQ & Functional C#",
            Description = "Harness the power of LINQ for data querying and manipulation. Learn lambdas, delegates, and functional programming patterns.",
            Level = "Intermediate",
            EstimatedHours = 10,
            IsPublished = true,
            CreatedAt = d
        }
    );

    private static void SeedModules(ModelBuilder mb, DateTime d) => mb.Entity<CourseModule>().HasData(
        // C# Fundamentals Modules
        new CourseModule
        {
            Id = 1,
            CourseId = 1,
            Title = "Getting Started with C#",
            Description = "Introduction to C#, .NET, and your development environment setup.",
            LearningObjectives = "Understand C# history; Set up .NET SDK; Create first console app",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 2,
            CourseId = 1,
            Title = "Variables & Data Types",
            Description = "Working with different data types, variables, operators, and type conversions.",
            LearningObjectives = "Declare variables; Use operators; Convert between types",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 3,
            CourseId = 1,
            Title = "Control Flow",
            Description = "Conditionals, loops, and branching to control program execution.",
            LearningObjectives = "Write if/else statements; Use for/while loops; Work with switch expressions",
            Order = 3,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 4,
            CourseId = 1,
            Title = "Methods & Functions",
            Description = "Creating reusable methods with parameters and return values.",
            LearningObjectives = "Define methods; Use parameters; Return values",
            Order = 4,
            CreatedAt = d
        },
        // OOP Modules
        new CourseModule
        {
            Id = 5,
            CourseId = 2,
            Title = "Classes & Objects",
            Description = "The basics of classes, objects, properties, and methods.",
            LearningObjectives = "Define classes; Create instances; Use properties",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 6,
            CourseId = 2,
            Title = "Inheritance & Polymorphism",
            Description = "Extending classes and working with polymorphic behavior.",
            LearningObjectives = "Use inheritance; Override methods; Apply polymorphism",
            Order = 2,
            CreatedAt = d
        },
        // ASP.NET Core Modules
        new CourseModule
        {
            Id = 7,
            CourseId = 3,
            Title = "Web API Basics",
            Description = "Building RESTful APIs with ASP.NET Core.",
            LearningObjectives = "Create controllers; Handle HTTP verbs; Return JSON responses",
            Order = 1,
            CreatedAt = d
        },
        // Data Structures Modules
        new CourseModule
        {
            Id = 8,
            CourseId = 4,
            Title = "Arrays & Lists",
            Description = "Understanding and implementing linear data structures.",
            LearningObjectives = "Implement arrays; Work with List<T>",
            Order = 1,
            CreatedAt = d
        },
        // LINQ Modules
        new CourseModule
        {
            Id = 9,
            CourseId = 5,
            Title = "LINQ Essentials",
            Description = "Query syntax, method syntax, and standard query operators.",
            LearningObjectives = "Write LINQ queries; Use lambda expressions",
            Order = 1,
            CreatedAt = d
        }
    );

    private static void SeedLessons(ModelBuilder mb, DateTime d) => mb.Entity<Lesson>().HasData(
        // C# Fundamentals Lessons (Module 1)
        new Lesson
        {
            Id = 1,
            CourseModuleId = 1,
            Title = "Introduction to C# & .NET",
            Content = """
# Introduction to C# & .NET

C# is a **modern, statically-typed, object-oriented programming language** developed by Microsoft. It runs on the .NET platform, which provides:

- A runtime environment (CLR - Common Language Runtime)
- Extensive class libraries (BCL - Base Class Library)
- Tools for building various application types: console, web, mobile, desktop, cloud, and more!

## Why Learn C#?

- **Cross-platform**: Runs on Windows, macOS, Linux
- **Strong ecosystem**: Large community, lots of libraries and frameworks
- **Professional**: Used extensively in enterprise software development
- **Modern**: Regular updates with new features (C# 12 is the latest as of 2025)

## Your First Look at C# Code

```csharp
// This is a comment
Console.WriteLine("Hello, World!");
```
""",
            BestPractices = """
- Follow C# naming conventions: PascalCase for classes, methods, properties; camelCase for local variables and parameters
- Use meaningful variable names
- Write comments for non-obvious logic
""",
            VoiceSummary = "C sharp is a modern, object-oriented language developed by Microsoft. It runs on the .NET platform, which provides a runtime environment and extensive class libraries for building many types of applications.",
            Type = LessonType.Mixed,
            DurationMinutes = 15,
            Order = 1,
            CreatedAt = d
        },
        new Lesson
        {
            Id = 2,
            CourseModuleId = 1,
            Title = "Setting Up Your Development Environment",
            Content = """
# Setting Up .NET

## Step 1: Install the .NET SDK

1. Go to [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. Download and install the latest .NET SDK (Software Development Kit)
3. Verify installation by opening a terminal and running:

```bash
dotnet --version
```

## Step 2: Create Your First Console App

```bash
dotnet new console -n MyFirstApp
cd MyFirstApp
dotnet run
```

## Step 3: Hello World!

The template creates a file called `Program.cs` with:

```csharp
Console.WriteLine("Hello, World!");
```
""",
            BestPractices = "- Pin SDK version with global.json; Use git for version control",
            VoiceSummary = "Install the .NET SDK, create a new console app, and run your first Hello World program.",
            Type = LessonType.Reading,
            DurationMinutes = 20,
            Order = 2,
            CreatedAt = d
        },
        new Lesson
        {
            Id = 3,
            CourseModuleId = 1,
            Title = "Your First C# Program: Hello World",
            Content = """
# Hello, C# Academy!

Let's write a simple program that prints a message to the console.

```csharp
Console.WriteLine("Hello, C# Academy!");
```

## Try It Yourself!

Change the message to print your name!
""",
            BestPractices = "- Start with simple programs; Comment your code",
            VoiceSummary = "Write your first program that prints a greeting to the console.",
            Type = LessonType.Practice,
            DurationMinutes = 15,
            Order = 3,
            CreatedAt = d
        },
        // Module 2
        new Lesson
        {
            Id = 4,
            CourseModuleId = 2,
            Title = "Variables & Data Types",
            Content = """
# Variables & Data Types

C# is a **statically-typed** language, which means you must declare the type of a variable before you use it (or use `var` for type inference).

## Common Data Types

```csharp
// Integer types
int age = 25;
long bigNumber = 9999999999999;

// Floating-point types
double pi = 3.14159;
decimal price = 19.99m; // Use for financial calculations

// Text
string name = "Alice";
char initial = 'A';

// Boolean
bool isStudent = true;
bool hasGraduated = false;
```
""",
            BestPractices = "Initialize variables when declaring; Use var for type inference when appropriate; Use decimal for financial calculations",
            VoiceSummary = "Variables store data in memory. C# has many built-in data types like int for integers, string for text, and bool for true/false values.",
            Type = LessonType.Mixed,
            DurationMinutes = 20,
            Order = 1,
            CreatedAt = d
        },
        new Lesson
        {
            Id = 5,
            CourseModuleId = 2,
            Title = "Operators",
            Content = """
# Operators

## Arithmetic Operators
- `+` Add
- `-` Subtract
- `*` Multiply
- `/` Divide
- `%` Modulo (remainder)

## Comparison Operators
- `==` Equal to
- `!=` Not equal to
- `<` Less than
- `>` Greater than
- `<=` Less than or equal to
- `>=` Greater than or equal to

## Logical Operators
- `&&` Logical AND
- `||` Logical OR
- `!` Logical NOT
""",
            BestPractices = "Use parentheses for clarity in complex expressions",
            VoiceSummary = "Operators let you perform arithmetic, comparisons, and logical operations.",
            Type = LessonType.Reading,
            DurationMinutes = 10,
            Order = 2,
            CreatedAt = d
        },
        // Module 3
        new Lesson
        {
            Id = 6,
            CourseModuleId = 3,
            Title = "If-Else Statements",
            Content = """
# If-Else Statements

Make decisions in your code:

```csharp
int score = 85;

if (score >= 90)
{
    Console.WriteLine("A");
}
else if (score >= 80)
{
    Console.WriteLine("B");
}
else if (score >= 70)
{
    Console.WriteLine("C");
}
else
{
    Console.WriteLine("F");
}
```
""",
            BestPractices = "Keep conditions simple; Use switch expressions for multiple cases",
            VoiceSummary = "If-else statements let your program make decisions based on conditions.",
            Type = LessonType.Practice,
            DurationMinutes = 20,
            Order = 1,
            CreatedAt = d
        },
        new Lesson
        {
            Id = 7,
            CourseModuleId = 3,
            Title = "Loops: For, While, Foreach",
            Content = """
# Loops

Repeat actions with loops:

## For Loop
```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

## Foreach Loop (for collections)
```csharp
var numbers = new[] { 1, 2, 3, 4, 5 };
foreach (var num in numbers)
{
    Console.WriteLine(num);
}
```

## While Loop
```csharp
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}
```
""",
            BestPractices = "Prefer foreach when possible; Avoid infinite loops!",
            VoiceSummary = "Loops let you repeat code multiple times. Use for for counting, foreach for collections, and while for conditions.",
            Type = LessonType.Practice,
            DurationMinutes = 25,
            Order = 2,
            CreatedAt = d
        },
        // OOP Module 1
        new Lesson
        {
            Id = 8,
            CourseModuleId = 5,
            Title = "Defining Classes",
            Content = """
# Defining Classes

Classes are blueprints for creating objects.

```csharp
public class Person
{
    // Property
    public string Name { get; set; }
    
    // Property
    public int Age { get; set; }
    
    // Method
    public void Greet()
    {
        Console.WriteLine($"Hello, my name is {Name}!");
    }
}
```

## Using the Class
```csharp
var alice = new Person
{
    Name = "Alice",
    Age = 25
};

alice.Greet();
```
""",
            BestPractices = "Use properties instead of public fields; Follow PascalCase for class and method names",
            VoiceSummary = "Classes define the blueprint for objects. They have properties (data) and methods (behavior).",
            Type = LessonType.Mixed,
            DurationMinutes = 25,
            Order = 1,
            CreatedAt = d
        },
        // OOP Module 2
        new Lesson
        {
            Id = 9,
            CourseModuleId = 6,
            Title = "Inheritance",
            Content = """
# Inheritance

Derive classes from base classes to reuse code:

```csharp
public class Animal
{
    public string Name { get; set; }
    
    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }
}

public class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine("Woof!");
    }
}
```
""",
            BestPractices = "Favor composition over inheritance; Use sealed when appropriate",
            VoiceSummary = "Inheritance lets you create new classes based on existing ones, reusing their code.",
            Type = LessonType.Video,
            DurationMinutes = 18,
            Order = 1,
            CreatedAt = d
        },
        // ASP.NET Core
        new Lesson
        {
            Id = 10,
            CourseModuleId = 7,
            Title = "Your First API",
            Content = """
# Your First ASP.NET Core Web API

1. Create a new Web API project:
```bash
dotnet new webapi -n MyFirstApi
cd MyFirstApi
dotnet run
```

2. You now have a running API!
""",
            BestPractices = "Follow REST conventions; Use proper HTTP status codes",
            VoiceSummary = "Create your first ASP.NET Core Web API and run it.",
            Type = LessonType.Reading,
            DurationMinutes = 25,
            Order = 1,
            CreatedAt = d
        },
        // Data Structures
        new Lesson
        {
            Id = 11,
            CourseModuleId = 8,
            Title = "Arrays in C#",
            Content = """
# Arrays in C#

Fixed-size collections of elements of the same type:

```csharp
int[] numbers = new int[5];
numbers[0] = 10;
numbers[1] = 20;

// Initializer syntax
string[] names = { "Alice", "Bob", "Charlie" };
```
""",
            BestPractices = "Check array bounds; Prefer List<T> for dynamic size",
            VoiceSummary = "Arrays store fixed-size collections of elements. Use List<T> when you need a dynamically-sized collection.",
            Type = LessonType.Practice,
            DurationMinutes = 15,
            Order = 1,
            CreatedAt = d
        },
        // LINQ
        new Lesson
        {
            Id = 12,
            CourseModuleId = 9,
            Title = "Introduction to LINQ",
            Content = """
# Introduction to LINQ

LINQ (Language-Integrated Query) lets you query collections in C#:

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Get even numbers
var evenNumbers = numbers.Where(x => x % 2 == 0);

// Get numbers greater than 5
var bigNumbers = numbers.Where(x => x > 5);

// Sum all numbers
var sum = numbers.Sum();
```
""",
            BestPractices = "Use method syntax for most queries; Understand deferred execution",
            VoiceSummary = "LINQ is a powerful way to query and manipulate data in collections.",
            Type = LessonType.Practice,
            DurationMinutes = 25,
            Order = 1,
            CreatedAt = d
        }
    );

    private static void SeedBadges(ModelBuilder mb, DateTime d) => mb.Entity<Badge>().HasData(
        new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 2, Name = "On Fire", Description = "3-day streak", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 3, Name = "Quick Learner", Description = "Complete 3 lessons", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 4, Name = "Quiz Whiz", Description = "Pass your first quiz", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 5, Name = "C# Graduate", Description = "Complete C# Fundamentals course", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 6, Name = "Challenge Master", Description = "Solve 5 coding challenges", ImageUrl = "", CreatedAt = d },
        new Badge { Id = 7, Name = "OOP Pro", Description = "Complete OOP course", ImageUrl = "", CreatedAt = d }
    );

    private static void SeedQuizzes(ModelBuilder mb, DateTime d) => mb.Entity<Quiz>().HasData(
        new Quiz { Id = 1, LessonId = 1, Title = "C# & .NET Basics Quiz", CreatedAt = d },
        new Quiz { Id = 2, LessonId = 4, Title = "Variables & Data Types Quiz", CreatedAt = d },
        new Quiz { Id = 3, LessonId = 6, Title = "Control Flow Quiz", CreatedAt = d }
    );

    private static void SeedQuestions(ModelBuilder mb, DateTime d)
    {
        mb.Entity<Question>().HasData(
            new Question { Id = 1, QuizId = 1, Text = "Who developed C#?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 2, QuizId = 1, Text = "C# runs on .NET.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 3, QuizId = 1, Text = "What is the output of Console.WriteLine(2 + 3)?", Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 4, QuizId = 2, Text = "Which keyword declares an integer?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 5, QuizId = 2, Text = "Fill in the blank: Text type is ___", Type = QuestionType.FillInTheBlank, CorrectAnswer = "string", CreatedAt = d },
            new Question { Id = 6, QuizId = 3, Text = "Which loop is best for iterating over collections?", Type = QuestionType.MultipleChoice, CreatedAt = d }
        );
        mb.Entity<QuestionOption>().HasData(
            // Quiz 1
            new QuestionOption { Id = 1, QuestionId = 1, Text = "Microsoft", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 2, QuestionId = 1, Text = "Google", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 3, QuestionId = 1, Text = "Apple", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 4, QuestionId = 2, Text = "True", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 5, QuestionId = 2, Text = "False", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 6, QuestionId = 3, Text = "5", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 7, QuestionId = 3, Text = "23", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 8, QuestionId = 3, Text = "2+3", IsCorrect = false, CreatedAt = d },
            // Quiz 2
            new QuestionOption { Id = 9, QuestionId = 4, Text = "int", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 10, QuestionId = 4, Text = "string", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 11, QuestionId = 4, Text = "bool", IsCorrect = false, CreatedAt = d },
            // Quiz 3
            new QuestionOption { Id = 12, QuestionId = 6, Text = "foreach", IsCorrect = true, CreatedAt = d },
            new QuestionOption { Id = 13, QuestionId = 6, Text = "for", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 14, QuestionId = 6, Text = "while", IsCorrect = false, CreatedAt = d }
        );
    }

    private static void SeedExercises(ModelBuilder mb, DateTime d) => mb.Entity<CodingExercise>().HasData(
        new CodingExercise
        {
            Id = 1,
            LessonId = 3,
            Title = "Hello, C#!",
            Order = 1,
            Difficulty = 1,
            Instructions = "Write a program that prints exactly: Hello, C# Academy!",
            StarterCode = "",
            ExpectedOutput = "Hello, C# Academy!",
            Hint = "Use Console.WriteLine()",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 2,
            LessonId = 4,
            Title = "Add Numbers",
            Order = 1,
            Difficulty = 1,
            Instructions = "Declare two integers (10 and 20), add them, and print the result.",
            StarterCode = "int a = 10;\nint b = 20;",
            ExpectedOutput = "30",
            Hint = "Console.WriteLine(a + b);",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 3,
            LessonId = 6,
            Title = "Grade Calculator",
            Order = 1,
            Difficulty = 2,
            Instructions = "Given a score of 85, print 'Pass' if >=70, else 'Fail'.",
            StarterCode = "int score = 85;",
            ExpectedOutput = "Pass",
            Hint = "Use an if-else statement",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 4,
            LessonId = 7,
            Title = "Count to 10",
            Order = 1,
            Difficulty = 1,
            Instructions = "Use a loop to print numbers from 1 to 10, each on a new line.",
            StarterCode = "",
            ExpectedOutput = "1\n2\n3\n4\n5\n6\n7\n8\n9\n10",
            Hint = "Use a for loop",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 5,
            LessonId = 8,
            Title = "Create Person",
            Order = 1,
            Difficulty = 2,
            Instructions = "Create a Person object with Name 'Bob' and call Greet().",
            StarterCode = "public class Person\n{\n    public string Name { get; set; }\n    \n    public void Greet()\n    {\n        Console.WriteLine($\"Hello, {Name}!\");\n    }\n}",
            ExpectedOutput = "Hello, Bob!",
            Hint = "var person = new Person { Name = \"Bob\" }; person.Greet();",
            CreatedAt = d
        }
    );

    private static void SeedTutorials(ModelBuilder mb, DateTime d) => mb.Entity<TutorialStep>().HasData(
        new TutorialStep
        {
            Id = 1,
            LessonId = 1,
            Order = 1,
            Title = "What is a Programming Language?",
            Content = "Programming languages give instructions to computers to perform tasks.",
            CreatedAt = d
        },
        new TutorialStep
        {
            Id = 2,
            LessonId = 1,
            Order = 2,
            Title = "First Code",
            Content = "Output text to the console:",
            CodeSample = "Console.WriteLine(\"Hello!\");",
            CreatedAt = d
        },
        new TutorialStep
        {
            Id = 3,
            LessonId = 3,
            Order = 1,
            Title = "The Console Class",
            Content = "Console is a built-in class for input/output.",
            CodeSample = "Console.WriteLine(\"Hi!\");",
            CreatedAt = d
        },
        new TutorialStep
        {
            Id = 4,
            LessonId = 4,
            Order = 1,
            Title = "Variables",
            Content = "Store values with types.",
            CodeSample = "int x = 5;",
            CreatedAt = d
        }
    );

    private static void SeedVideos(ModelBuilder mb, DateTime d) => mb.Entity<LessonVideo>().HasData(
        new LessonVideo
        {
            Id = 1,
            LessonId = 1,
            Title = "C# in 100 Seconds",
            VideoUrl = "https://www.youtube.com/watch?v=ravLFzWr5H4",
            Provider = VideoProvider.YouTube,
            Order = 1,
            DurationMinutes = 2,
            CreatedAt = d
        },
        new LessonVideo
        {
            Id = 2,
            LessonId = 1,
            Title = "Introduction to C# (Full Course)",
            VideoUrl = "https://www.youtube.com/watch?v=Wx2TKSol0I8",
            Provider = VideoProvider.YouTube,
            Order = 2,
            DurationMinutes = 60,
            CreatedAt = d
        },
        new LessonVideo
        {
            Id = 3,
            LessonId = 9,
            Title = "OOP in C# Explained",
            VideoUrl = "https://www.youtube.com/watch?v=wqUfllZyeq4",
            Provider = VideoProvider.YouTube,
            Order = 1,
            DurationMinutes = 20,
            CreatedAt = d
        },
        new LessonVideo
        {
            Id = 4,
            LessonId = 10,
            Title = "ASP.NET Core Tutorial for Beginners",
            VideoUrl = "https://www.youtube.com/watch?v=AhAxLiGC7Pc",
            Provider = VideoProvider.YouTube,
            Order = 1,
            DurationMinutes = 30,
            CreatedAt = d
        }
    );

    private static void SeedChallenges(ModelBuilder mb, DateTime d) => mb.Entity<CodingChallenge>().HasData(
        new CodingChallenge
        {
            Id = 1,
            Title = "FizzBuzz",
            Description = "Print numbers from 1 to 15. For multiples of 3, print 'Fizz'; for multiples of 5, print 'Buzz'; for both, print 'FizzBuzz'.",
            Difficulty = "Easy",
            StarterCode = "for (int i = 1; i <= 15; i++)\n{\n    // Your code here\n}",
            ExpectedOutput = "1\n2\nFizz\n4\nBuzz\nFizz\n7\n8\nFizz\nBuzz\n11\nFizz\n13\n14\nFizzBuzz",
            Hint = "Use modulo operator %",
            Tags = "loops,conditionals",
            Order = 1,
            XpReward = 25,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 2,
            Title = "Reverse String",
            Description = "Reverse the string 'hello' and print it.",
            Difficulty = "Easy",
            StarterCode = "string s = \"hello\";",
            ExpectedOutput = "olleh",
            Hint = "Convert to char array, reverse, then new string",
            Tags = "strings",
            Order = 2,
            XpReward = 20,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 3,
            Title = "Sum Array",
            Description = "Sum the array [1,2,3,4,5] and print the result.",
            Difficulty = "Easy",
            StarterCode = "int[] numbers = { 1, 2, 3, 4, 5 };",
            ExpectedOutput = "15",
            Hint = "Use a loop or LINQ Sum()",
            Tags = "arrays,loops",
            Order = 3,
            XpReward = 25,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 4,
            Title = "Factorial",
            Description = "Calculate and print the factorial of 5 (5! = 120).",
            Difficulty = "Medium",
            StarterCode = "int n = 5;",
            ExpectedOutput = "120",
            Hint = "Multiply 1*2*3*4*5",
            Tags = "math,loops",
            Order = 4,
            XpReward = 30,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 5,
            Title = "Palindrome Check",
            Description = "Check if 'racecar' is a palindrome (reads same forwards/backwards). Print 'True' or 'False'.",
            Difficulty = "Medium",
            StarterCode = "string word = \"racecar\";",
            ExpectedOutput = "True",
            Hint = "Compare word with reversed word",
            Tags = "strings",
            Order = 5,
            XpReward = 30,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 6,
            Title = "Fibonacci",
            Description = "Print the 7th Fibonacci number (sequence: 1,1,2,3,5,8,13).",
            Difficulty = "Medium",
            StarterCode = "// fib sequence: 1,1,2,3,5,8,13",
            ExpectedOutput = "13",
            Hint = "Iterate with two variables",
            Tags = "math",
            Order = 6,
            XpReward = 35,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 7,
            Title = "Prime Check",
            Description = "Check if 17 is a prime number. Print 'Prime' or 'Not Prime'.",
            Difficulty = "Hard",
            StarterCode = "int num = 17;",
            ExpectedOutput = "Prime",
            Hint = "Check divisibility from 2 to sqrt(num)",
            Tags = "math,algorithms",
            Order = 7,
            XpReward = 40,
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 8,
            Title = "Binary Search",
            Description = "Implement binary search to find index of 7 in sorted array [1,3,5,7,9].",
            Difficulty = "Hard",
            StarterCode = "int[] arr = { 1, 3, 5, 7, 9 };\nint target = 7;",
            ExpectedOutput = "3",
            Hint = "Classic binary search algorithm",
            Tags = "algorithms",
            Order = 8,
            XpReward = 50,
            CreatedAt = d
        }
    );
}
