using CsharpAcademy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

/// <summary>
/// Comprehensive platform seed data — courses, modules, lessons, quizzes,
/// exercises, challenges, tutorials, and videos. Production-ready content
/// covering C# Fundamentals → OOP → ASP.NET Core → Data Structures → LINQ.
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

    // ───────────────────────────────────────────────
    // COURSES
    // ───────────────────────────────────────────────
    private static void SeedCourses(ModelBuilder mb, DateTime d) => mb.Entity<Course>().HasData(
        new Course
        {
            Id = 1,
            Title = "C# Fundamentals",
            Description = "Master C# from absolute zero. Learn variables, data types, operators, control flow, methods, arrays, and core .NET basics. Perfect for complete beginners with no prior programming experience.",
            Level = "Beginner",
            EstimatedHours = 20,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 2,
            Title = "Object-Oriented Programming in C#",
            Description = "Deep dive into OOP concepts: classes, objects, constructors, inheritance, polymorphism, encapsulation, interfaces, abstract classes, and SOLID design principles.",
            Level = "Intermediate",
            EstimatedHours = 22,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 3,
            Title = "ASP.NET Core Web APIs",
            Description = "Build professional RESTful APIs with ASP.NET Core 8, Entity Framework Core, JWT authentication, middleware, dependency injection, and clean architecture patterns.",
            Level = "Intermediate",
            EstimatedHours = 28,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 4,
            Title = "Data Structures & Algorithms with C#",
            Description = "Master essential data structures (arrays, linked lists, stacks, queues, hash tables, trees, graphs) and algorithms (sorting, searching, dynamic programming, graph traversal).",
            Level = "Advanced",
            EstimatedHours = 30,
            IsPublished = true,
            CreatedAt = d
        },
        new Course
        {
            Id = 5,
            Title = "LINQ & Functional C#",
            Description = "Harness the full power of LINQ for data querying and transformation. Learn delegates, lambdas, Func/Action, expression trees, and functional programming patterns in C#.",
            Level = "Intermediate",
            EstimatedHours = 14,
            IsPublished = true,
            CreatedAt = d
        }
    );

    // ───────────────────────────────────────────────
    // MODULES
    // ───────────────────────────────────────────────
    private static void SeedModules(ModelBuilder mb, DateTime d) => mb.Entity<CourseModule>().HasData(

        // ── Course 1: C# Fundamentals ──
        new CourseModule
        {
            Id = 1,
            CourseId = 1,
            Title = "Getting Started with C#",
            Description = "Introduction to C#, the .NET ecosystem, and setting up your development environment.",
            LearningObjectives = "Understand C# history and purpose; Install the .NET SDK; Create and run your first console application; Understand the structure of a C# program",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 2,
            CourseId = 1,
            Title = "Variables, Data Types & Operators",
            Description = "Working with different data types, declaring variables, performing operations, and converting between types.",
            LearningObjectives = "Declare and initialise variables; Use value and reference types; Apply arithmetic, comparison, and logical operators; Perform type conversions and casting",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 3,
            CourseId = 1,
            Title = "Control Flow",
            Description = "Conditionals, loops, switch statements, and branching to control program execution.",
            LearningObjectives = "Write if/else and switch statements; Use for, while, do-while, and foreach loops; Apply break, continue, and return; Use ternary and switch expressions",
            Order = 3,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 4,
            CourseId = 1,
            Title = "Methods & Functions",
            Description = "Creating reusable methods with parameters, return values, overloading, and recursion.",
            LearningObjectives = "Define and call methods; Use parameters and return types; Overload methods; Understand scope and recursion; Use optional and named parameters",
            Order = 4,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 5,
            CourseId = 1,
            Title = "Arrays & Collections",
            Description = "Working with arrays, multi-dimensional arrays, List<T>, Dictionary<TKey,TValue>, and other collection types.",
            LearningObjectives = "Declare and use arrays; Work with multi-dimensional arrays; Use List<T> and Dictionary<TKey,TValue>; Iterate collections with loops and foreach",
            Order = 5,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 6,
            CourseId = 1,
            Title = "Strings & Text Processing",
            Description = "String manipulation, formatting, parsing, and working with the StringBuilder class.",
            LearningObjectives = "Manipulate strings with built-in methods; Format strings with interpolation and format specifiers; Parse user input; Use StringBuilder for performance",
            Order = 6,
            CreatedAt = d
        },

        // ── Course 2: OOP ──
        new CourseModule
        {
            Id = 7,
            CourseId = 2,
            Title = "Classes & Objects",
            Description = "The fundamentals of classes, objects, constructors, properties, and fields.",
            LearningObjectives = "Define classes with fields and properties; Create objects with constructors; Apply access modifiers; Understand this keyword and object lifetime",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 8,
            CourseId = 2,
            Title = "Encapsulation & Properties",
            Description = "Data hiding, access modifiers, auto-properties, computed properties, and init-only setters.",
            LearningObjectives = "Apply public, private, protected, internal; Use auto-properties and backing fields; Create computed properties; Use init-only setters in records",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 9,
            CourseId = 2,
            Title = "Inheritance & Polymorphism",
            Description = "Extending classes, method overriding, virtual/override/sealed, and polymorphic behaviour.",
            LearningObjectives = "Create inheritance hierarchies; Override and extend methods; Understand base class constructors; Apply polymorphism through references",
            Order = 3,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 10,
            CourseId = 2,
            Title = "Interfaces & Abstract Classes",
            Description = "Defining contracts with interfaces, abstract base classes, and multiple interface implementation.",
            LearningObjectives = "Define and implement interfaces; Create abstract classes; Distinguish interface from abstract class; Apply dependency inversion principle",
            Order = 4,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 11,
            CourseId = 2,
            Title = "SOLID Principles",
            Description = "Understanding and applying SOLID design principles in real-world C# code.",
            LearningObjectives = "Apply Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion principles",
            Order = 5,
            CreatedAt = d
        },

        // ── Course 3: ASP.NET Core ──
        new CourseModule
        {
            Id = 12,
            CourseId = 3,
            Title = "ASP.NET Core Fundamentals",
            Description = "Project structure, middleware pipeline, dependency injection, and configuration.",
            LearningObjectives = "Create and run ASP.NET Core projects; Understand the middleware pipeline; Register services with DI; Read from appsettings.json",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 13,
            CourseId = 3,
            Title = "Building RESTful Controllers",
            Description = "Creating controllers, handling HTTP verbs, routing, model binding, and returning proper responses.",
            LearningObjectives = "Create API controllers; Map HTTP verbs to actions; Use route attributes; Bind models from body, query, and route; Return correct status codes",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 14,
            CourseId = 3,
            Title = "Entity Framework Core",
            Description = "Database access with EF Core: DbContext, migrations, CRUD operations, and relationships.",
            LearningObjectives = "Configure DbContext; Define entity models; Create and apply migrations; Perform CRUD with EF Core; Map entity relationships",
            Order = 3,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 15,
            CourseId = 3,
            Title = "Authentication & Security",
            Description = "JWT authentication, authorisation, data validation, and API security best practices.",
            LearningObjectives = "Implement JWT bearer authentication; Apply [Authorize] policies; Validate input with data annotations and FluentValidation; Secure API endpoints",
            Order = 4,
            CreatedAt = d
        },

        // ── Course 4: DSA ──
        new CourseModule
        {
            Id = 16,
            CourseId = 4,
            Title = "Arrays & Linked Lists",
            Description = "Linear data structures: arrays, dynamic arrays, and singly/doubly linked lists.",
            LearningObjectives = "Implement arrays and dynamic arrays; Build singly and doubly linked lists; Analyse time and space complexity; Compare array vs linked list trade-offs",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 17,
            CourseId = 4,
            Title = "Stacks & Queues",
            Description = "Stack and queue implementations using arrays and linked lists, and their applications.",
            LearningObjectives = "Implement stacks with push/pop; Implement queues with enqueue/dequeue; Use Stack<T> and Queue<T> from .NET; Solve problems using stacks and queues",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 18,
            CourseId = 4,
            Title = "Sorting Algorithms",
            Description = "Classic sorting algorithms: Bubble, Selection, Insertion, Merge, Quick, and their complexities.",
            LearningObjectives = "Implement and compare sorting algorithms; Understand O(n²) vs O(n log n); Choose the right sort for the use case",
            Order = 3,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 19,
            CourseId = 4,
            Title = "Trees & Binary Search Trees",
            Description = "Tree data structures, binary trees, BST operations, and tree traversal algorithms.",
            LearningObjectives = "Implement a binary tree; Perform in-order, pre-order, post-order traversal; Build a BST with insert, search, delete; Calculate tree height and balance",
            Order = 4,
            CreatedAt = d
        },

        // ── Course 5: LINQ ──
        new CourseModule
        {
            Id = 20,
            CourseId = 5,
            Title = "Delegates & Lambda Expressions",
            Description = "Understanding delegates, Func<T>, Action<T>, and lambda expression syntax.",
            LearningObjectives = "Declare and invoke delegates; Use Func<T> and Action<T>; Write lambda expressions; Understand closures and captured variables",
            Order = 1,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 21,
            CourseId = 5,
            Title = "LINQ Essentials",
            Description = "Query syntax vs method syntax, standard query operators, and deferred execution.",
            LearningObjectives = "Write LINQ in query and method syntax; Apply Where, Select, OrderBy, GroupBy, Join; Understand deferred vs immediate execution; Use ToList, ToArray, First, Single",
            Order = 2,
            CreatedAt = d
        },
        new CourseModule
        {
            Id = 22,
            CourseId = 5,
            Title = "Advanced LINQ",
            Description = "Projections, joins, grouping, aggregation, and writing efficient LINQ queries.",
            LearningObjectives = "Perform inner and group joins; Create custom projections with anonymous types; Aggregate with Sum, Count, Min, Max, Average; Chain multiple operators efficiently",
            Order = 3,
            CreatedAt = d
        }
    );

    // ───────────────────────────────────────────────
    // LESSONS — full production content
    // ───────────────────────────────────────────────
    private static void SeedLessons(ModelBuilder mb, DateTime d) => mb.Entity<Lesson>().HasData(

        // ══════════════════════════════════
        // MODULE 1 — Getting Started with C#
        // ══════════════════════════════════

        new Lesson
        {
            Id = 1,
            CourseModuleId = 1,
            Title = "Introduction to C# & the .NET Ecosystem",
            Content = """
# Introduction to C# & the .NET Ecosystem

## What is C#?

**C#** (pronounced "C-Sharp") is a modern, general-purpose, statically-typed, object-oriented programming language developed by Microsoft and first released in 2002. It is designed for building a wide range of applications — from simple console programs to large-scale enterprise web services, mobile apps, games, and cloud workloads.

C# is part of the **.NET ecosystem**, which provides:

- **CLR (Common Language Runtime)** — the virtual machine that manages memory, handles exceptions, and runs your compiled code.
- **BCL (Base Class Library)** — thousands of pre-built classes for I/O, networking, collections, threading, cryptography, and more.
- **SDK & Tooling** — `dotnet` CLI, NuGet package manager, and first-class IDE support (Visual Studio, VS Code, Rider).

## Why Learn C#?

| Reason | Detail |
|---|---|
| Cross-platform | Runs on Windows, macOS, and Linux |
| Versatile | Web APIs, desktop (WPF/WinForms/MAUI), games (Unity), mobile (MAUI), cloud (Azure) |
| Modern Language | Records, pattern matching, nullable reference types, top-level programs |
| Strong Job Market | Widely used in enterprise, gaming, and finance |
| Performance | Near-native speed with AOT compilation in .NET 8+ |

## A Brief History

- **2002** — C# 1.0 ships with .NET Framework
- **2007** — C# 3.0 introduces LINQ, lambdas, extension methods
- **2017** — .NET Core 1.0 — truly cross-platform
- **2020** — .NET 5 unifies .NET Core and .NET Framework
- **2024** — C# 13 with .NET 9 (latest)

## Your First Glimpse of C# Code

```csharp
// Program.cs — top-level statements (C# 9+)
Console.WriteLine("Hello, C# Academy!");

// Read user input
Console.Write("Enter your name: ");
string name = Console.ReadLine()!;
Console.WriteLine($"Welcome, {name}!");
```

The `//` prefix starts a single-line comment — the compiler ignores it. `Console.WriteLine` writes a line of text to the terminal. `$"..."` is a **string interpolation** — we'll cover this fully in the Strings module.

## The .NET Compilation Pipeline

```
Your .cs files
     │
     ▼  C# Compiler (Roslyn)
IL (Intermediate Language) in .dll/.exe
     │
     ▼  JIT Compiler (at runtime)
Native Machine Code
     │
     ▼  CPU executes
```

This two-step process means C# code is portable across architectures, while still running at near-native speed.
""",
            BestPractices = """
- Use top-level statements for small programs and scripts (C# 9+)
- Always use meaningful file and class names
- Keep Program.cs minimal — delegate logic to separate classes
- Use `dotnet --version` to confirm your SDK is installed before starting any project
""",
            VoiceSummary = "C sharp is a modern, cross-platform, object-oriented language built by Microsoft. It runs on the dot NET ecosystem, which provides a runtime, class libraries, and tooling. You can use C sharp to build web APIs, desktop apps, mobile apps, games, and cloud services. The compiler turns your code into Intermediate Language, which the runtime then JIT-compiles to native machine code at runtime.",
            Type = LessonType.Mixed,
            DurationMinutes = 20,
            Order = 1,
            CreatedAt = d
        },

        new Lesson
        {
            Id = 2,
            CourseModuleId = 1,
            Title = "Setting Up Your Development Environment",
            Content = """
# Setting Up Your Development Environment

## Step 1: Install the .NET SDK

The **.NET SDK** includes the compiler, runtime, and the `dotnet` CLI tool.

1. Visit [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. Download the **latest LTS version** (e.g., .NET 8)
3. Run the installer and follow the prompts

**Verify the installation:**
```bash
dotnet --version
# Expected output: 8.0.xxx
```

## Step 2: Choose an Editor

| Editor | Best For | Cost |
|---|---|---|
| **Visual Studio 2022** | Full-featured Windows/Mac IDE | Free (Community) |
| **VS Code + C# Dev Kit** | Lightweight, cross-platform | Free |
| **JetBrains Rider** | Advanced IntelliJ-based IDE | Paid (free for students) |

**Recommended for beginners:** VS Code with the **C# Dev Kit** extension.

Install VS Code extensions:
- C# Dev Kit (`ms-dotnettools.csdevkit`)
- .NET Install Tool (`ms-dotnettools.vscode-dotnet-runtime`)

## Step 3: Create Your First Project

Open a terminal and run:

```bash
# Create a new console application
dotnet new console -n HelloCSharp

# Navigate into the project folder
cd HelloCSharp

# Open in VS Code
code .

# Run the project
dotnet run
```

**Expected output:**
```
Hello, World!
```

## Understanding the Project Structure

```
HelloCSharp/
├── HelloCSharp.csproj    ← Project file (XML config)
├── Program.cs            ← Your main code file
├── obj/                  ← Build intermediaries (ignore)
└── bin/                  ← Compiled output
```

**HelloCSharp.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>
```

- `OutputType>Exe` — builds a runnable executable
- `TargetFramework` — targets .NET 8
- `Nullable>enable` — enables nullable reference type warnings
- `ImplicitUsings>enable` — auto-imports common namespaces (`System`, `System.Collections.Generic`, etc.)

## Useful dotnet CLI Commands

```bash
dotnet new console -n MyApp    # Create console app
dotnet new webapi  -n MyApi    # Create Web API
dotnet run                     # Build and run
dotnet build                   # Build only
dotnet test                    # Run tests
dotnet add package Newtonsoft.Json  # Add NuGet package
dotnet restore                 # Restore packages
```
""",
            BestPractices = """
- Pin your SDK version with a `global.json` file to ensure consistent builds across machines
- Use `dotnet new gitignore` to generate a proper .gitignore for .NET projects
- Never commit the `bin/` and `obj/` folders to version control
- Enable nullable reference types (`<Nullable>enable</Nullable>`) from the start — it prevents null reference exceptions
""",
            VoiceSummary = "Install the dot NET SDK, verify it with dotnet --version, and choose an editor like VS Code or Visual Studio. Create projects with the dotnet new command, navigate into the folder, and run with dotnet run. The project file controls target framework, nullability, and implicit usings.",
            Type = LessonType.Reading,
            DurationMinutes = 20,
            Order = 2,
            CreatedAt = d
        },

        new Lesson
        {
            Id = 3,
            CourseModuleId = 1,
            Title = "Your First C# Program — Hello World",
            Content = """
# Your First C# Program

## The Classic Hello World

```csharp
Console.WriteLine("Hello, World!");
```

That single line is a complete, runnable C# program (using top-level statements from C# 9+). Let's break it down:

| Part | Meaning |
|---|---|
| `Console` | A built-in class in the `System` namespace for terminal I/O |
| `.` | Member access operator — accesses a member of `Console` |
| `WriteLine` | A method that outputs text followed by a newline |
| `"Hello, World!"` | A **string literal** — text enclosed in double quotes |
| `;` | Statement terminator — required at the end of every statement |

## Writing vs Writing a Line

```csharp
Console.Write("Hello, ");      // No newline
Console.Write("World!");       // Continues on same line
Console.WriteLine();           // Prints an empty line

Console.WriteLine("New line"); // Writes then moves to next line
```

**Output:**
```
Hello, World!

New line
```

## Reading User Input

```csharp
Console.Write("What is your name? ");
string name = Console.ReadLine()!;
Console.WriteLine($"Hello, {name}! Welcome to C# Academy.");
```

`Console.ReadLine()` returns a `string?` (nullable string). The `!` tells the compiler "trust me, this won't be null" — we'll cover nullability properly later.

## Displaying Multiple Values

```csharp
string firstName = "Alice";
int age = 25;
double gpa = 3.85;

Console.WriteLine($"Name: {firstName}");
Console.WriteLine($"Age:  {age}");
Console.WriteLine($"GPA:  {gpa:F2}"); // F2 = 2 decimal places

// All on one line using string concatenation
Console.WriteLine("Name: " + firstName + ", Age: " + age);
```

## Escape Sequences

Inside strings, you can use escape sequences for special characters:

```csharp
Console.WriteLine("Line 1\nLine 2");        // \n = newline
Console.WriteLine("Column1\tColumn2");      // \t = tab
Console.WriteLine("She said \"Hello!\"");   // \" = literal quote
Console.WriteLine("C:\\Users\\Alice");      // \\ = literal backslash

// Verbatim strings avoid escape sequences:
Console.WriteLine(@"C:\Users\Alice");       // Same output, easier to read
```

## Comments

```csharp
// Single-line comment — ignored by compiler

/*
   Multi-line comment
   Useful for longer explanations
*/

/// <summary>
/// XML documentation comment — used by IDEs and doc generators
/// </summary>
```
""",
            BestPractices = """
- Use `Console.WriteLine` for output and `Console.ReadLine` for input in console apps
- Prefer string interpolation `$"..."` over concatenation `+` for readability
- Use verbatim strings `@"..."` for file paths and multi-line strings to avoid escape noise
- Add XML doc comments (`///`) to public methods and classes from the start
""",
            VoiceSummary = "Console dot WriteLine writes text to the terminal. Console dot ReadLine reads a line of input from the user. String interpolation lets you embed variables directly inside strings using the dollar sign prefix. Escape sequences like backslash n for newline and backslash t for tab control how text is formatted.",
            Type = LessonType.Practice,
            DurationMinutes = 20,
            Order = 3,
            CreatedAt = d
        },

        // ══════════════════════════════════════════
        // MODULE 2 — Variables, Data Types & Operators
        // ══════════════════════════════════════════

        new Lesson
        {
            Id = 4,
            CourseModuleId = 2,
            Title = "Variables & Data Types",
            Content = """
# Variables & Data Types

## What is a Variable?

A **variable** is a named storage location in memory that holds a value. In C#, every variable has a **type** that determines what kind of data it can store.

```csharp
// Syntax: type name = value;
int age = 25;
string city = "Accra";
bool isLoggedIn = true;
```

## Value Types vs Reference Types

| Category | Types | Stored In |
|---|---|---|
| **Value types** | int, double, bool, char, struct, enum | Stack |
| **Reference types** | string, class, array, interface | Heap |

Value types store their data directly. Reference types store a *reference (pointer)* to where the data lives on the heap.

## Integer Types

```csharp
byte   b = 255;              // 8-bit,  0 to 255
sbyte  sb = -128;            // 8-bit, -128 to 127
short  s = 32_000;           // 16-bit
ushort us = 65_535;          // 16-bit unsigned
int    i = 2_147_483_647;    // 32-bit (most common)
uint   ui = 4_294_967_295u;  // 32-bit unsigned
long   l = 9_223_372_036L;   // 64-bit
ulong  ul = 18_446_744_073uL;// 64-bit unsigned

// Digit separators (_) improve readability — only cosmetic
int population = 33_000_000;
```

**Rule of thumb:** Use `int` for whole numbers unless you have a specific reason for a different size.

## Floating-Point Types

```csharp
float  f = 3.14f;        // 32-bit, ~7 digits precision  (suffix f)
double d = 3.14159265;   // 64-bit, ~15 digits precision (default)
decimal m = 19.99m;      // 128-bit, 28–29 digits (suffix m)
```

> ⚠️ **Critical:** Use `decimal` for financial calculations (money). `float` and `double` have binary rounding errors — `0.1 + 0.2` is not exactly `0.3` in floating-point.

```csharp
double bad  = 0.1 + 0.2;    // 0.30000000000000004
decimal ok  = 0.1m + 0.2m;  // 0.3
```

## Text Types

```csharp
char   c = 'A';             // Single character (single quotes)
string s = "Hello, World!"; // Sequence of characters (double quotes)

// Strings are immutable — each operation creates a new string
string greeting = "Hello";
greeting = greeting + ", Alice!"; // New string created in memory
```

## Boolean

```csharp
bool isActive  = true;
bool isDeleted = false;

// Common in conditions
if (isActive && !isDeleted)
{
    Console.WriteLine("User is active.");
}
```

## Type Inference with `var`

The `var` keyword lets the compiler infer the type from the right-hand side:

```csharp
var name    = "Alice";        // inferred as string
var age     = 30;             // inferred as int
var price   = 9.99m;         // inferred as decimal
var numbers = new[] { 1, 2, 3 }; // inferred as int[]
```

`var` is still **statically typed** — the type is fixed at compile time. It's not dynamic.

## Constants & Readonly

```csharp
// const — value fixed at compile time
const double Pi = 3.14159265358979;
const int DaysInWeek = 7;

// readonly — value fixed at runtime (can be set in constructor)
readonly DateTime CreatedAt = DateTime.UtcNow;
```

## Nullable Types

Value types cannot normally be `null`. Add `?` to allow null:

```csharp
int  age      = 25;    // cannot be null
int? maybeAge = null;  // nullable int

if (maybeAge.HasValue)
    Console.WriteLine(maybeAge.Value);

// Null-coalescing operator: use default if null
int display = maybeAge ?? 0;
```
""",
            BestPractices = """
- Always use `decimal` for money/currency — never `float` or `double`
- Use `var` when the type is obvious from the right-hand side to reduce noise
- Prefer `const` over magic numbers scattered through code (e.g., `const int MaxRetries = 3`)
- Enable nullable reference types in the project file to catch null issues at compile time
- Use digit separators `_` in large numeric literals for readability
""",
            VoiceSummary = "Variables are named memory locations with a fixed type. C sharp has value types like int, double, bool, and char stored on the stack, and reference types like string and class stored on the heap. Use decimal for money, int for whole numbers, and string for text. The var keyword lets the compiler infer the type automatically. Add a question mark to make value types nullable.",
            Type = LessonType.Mixed,
            DurationMinutes = 30,
            Order = 1,
            CreatedAt = d
        },

        new Lesson
        {
            Id = 5,
            CourseModuleId = 2,
            Title = "Operators & Expressions",
            Content = """
# Operators & Expressions

## Arithmetic Operators

```csharp
int a = 17, b = 5;

Console.WriteLine(a + b);   // 22  — Addition
Console.WriteLine(a - b);   // 12  — Subtraction
Console.WriteLine(a * b);   // 85  — Multiplication
Console.WriteLine(a / b);   // 3   — Integer division (truncates!)
Console.WriteLine(a % b);   // 2   — Modulo (remainder)

// For floating-point division:
double result = (double)a / b;  // 3.4
```

> ⚠️ Integer division truncates: `17 / 5 = 3`, not 3.4. Cast to double first.

## Increment & Decrement

```csharp
int x = 10;
x++;    // Post-increment: x becomes 11
++x;    // Pre-increment:  x becomes 12
x--;    // Post-decrement: x becomes 11
--x;    // Pre-decrement:  x becomes 10

// Post vs pre matters in expressions:
int a = 5;
int b = a++;  // b = 5, a = 6  (old value returned)
int c = ++a;  // c = 7, a = 7  (new value returned)
```

## Compound Assignment

```csharp
int n = 10;
n += 5;   // n = 15  (same as n = n + 5)
n -= 3;   // n = 12
n *= 2;   // n = 24
n /= 4;   // n = 6
n %= 4;   // n = 2
```

## Comparison Operators

```csharp
int x = 10, y = 20;

Console.WriteLine(x == y);   // False — Equal to
Console.WriteLine(x != y);   // True  — Not equal to
Console.WriteLine(x < y);    // True  — Less than
Console.WriteLine(x > y);    // False — Greater than
Console.WriteLine(x <= y);   // True  — Less than or equal
Console.WriteLine(x >= y);   // False — Greater than or equal
```

Comparison operators return a `bool` value.

## Logical Operators

```csharp
bool a = true, b = false;

Console.WriteLine(a && b);   // False — AND: both must be true
Console.WriteLine(a || b);   // True  — OR:  at least one true
Console.WriteLine(!a);       // False — NOT: inverts the value
Console.WriteLine(a ^ b);    // True  — XOR: exactly one true
```

**Short-circuit evaluation:**
- `&&` stops evaluating if the first operand is `false`
- `||` stops evaluating if the first operand is `true`

```csharp
string? s = null;
// Safe — second part not evaluated if s is null
if (s != null && s.Length > 0)
    Console.WriteLine(s);
```

## Bitwise Operators (for advanced use)

```csharp
int a = 0b_1100;   // 12 in binary
int b = 0b_1010;   // 10 in binary

Console.WriteLine(a & b);    // 0b_1000 = 8  — Bitwise AND
Console.WriteLine(a | b);    // 0b_1110 = 14 — Bitwise OR
Console.WriteLine(a ^ b);    // 0b_0110 = 6  — Bitwise XOR
Console.WriteLine(~a);       // Bitwise NOT (flips all bits)
Console.WriteLine(a << 1);   // 24 — Left shift (multiply by 2)
Console.WriteLine(a >> 1);   // 6  — Right shift (divide by 2)
```

## Operator Precedence

From highest to lowest (simplified):

```
1. () — parentheses
2. ++ -- ! ~ (unary)
3. * / %
4. + -
5. < > <= >=
6. == !=
7. &&
8. ||
9. = += -= (assignment)
```

**Best practice: use parentheses** to make your intent clear rather than relying on precedence rules.

```csharp
int result = 2 + 3 * 4;        // 14 (not 20)
int clear  = 2 + (3 * 4);     // 14 — explicit
int also   = (2 + 3) * 4;     // 20 — different intent
```

## Type Conversion

```csharp
// Implicit conversion (no data loss)
int i = 42;
long l = i;       // int fits in long — safe
double d = i;     // int fits in double — safe

// Explicit cast (may lose data)
double pi = 3.14159;
int truncated = (int)pi;   // 3 — fractional part lost!

// Convert class
string s = "42";
int n = Convert.ToInt32(s);    // 42
double x = Convert.ToDouble("3.14");

// TryParse — safe conversion, no exception on failure
if (int.TryParse("123abc", out int result))
    Console.WriteLine(result);
else
    Console.WriteLine("Not a valid integer");
```
""",
            BestPractices = """
- Use parentheses to clarify operator precedence — don't rely on memorising it
- Always cast or use `Convert` when narrowing numeric types (e.g., double to int)
- Prefer `int.TryParse` over `int.Parse` when dealing with user input — it won't throw
- Use `%` (modulo) to check even/odd: `n % 2 == 0` means even
- Avoid side effects in compound expressions with ++ and -- to prevent hard-to-spot bugs
""",
            VoiceSummary = "Operators perform arithmetic, comparison, logical, and assignment operations. Integer division truncates, so cast to double for decimal results. Logical operators short-circuit — and-and stops at the first false, or-or stops at the first true. Use TryParse for safe user-input conversion. Always use parentheses to make your intent explicit.",
            Type = LessonType.Reading,
            DurationMinutes = 25,
            Order = 2,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 3 — Control Flow
        // ══════════════════════════════

        new Lesson
        {
            Id = 6,
            CourseModuleId = 3,
            Title = "If-Else Statements & Switch Expressions",
            Content = """
# If-Else Statements & Switch Expressions

## Basic If-Else

```csharp
int score = 85;

if (score >= 90)
{
    Console.WriteLine("Grade: A");
}
else if (score >= 80)
{
    Console.WriteLine("Grade: B");
}
else if (score >= 70)
{
    Console.WriteLine("Grade: C");
}
else if (score >= 60)
{
    Console.WriteLine("Grade: D");
}
else
{
    Console.WriteLine("Grade: F");
}
```

## Ternary Operator

A concise one-line conditional:

```csharp
int age = 20;
string status = age >= 18 ? "Adult" : "Minor";
Console.WriteLine(status);  // Adult

// Nested (avoid for readability — prefer if-else)
string label = score >= 90 ? "A" : score >= 80 ? "B" : "C";
```

## Null-Coalescing Operators

```csharp
string? input = null;

// ?? — use right side if left is null
string display = input ?? "Default Value";
Console.WriteLine(display);  // Default Value

// ??= — assign only if currently null
input ??= "Assigned";
Console.WriteLine(input);   // Assigned

// ?. — null-conditional: only call member if not null
int? length = input?.Length;
```

## Switch Statement

```csharp
string day = "Monday";

switch (day)
{
    case "Monday":
    case "Tuesday":
    case "Wednesday":
    case "Thursday":
    case "Friday":
        Console.WriteLine("Weekday");
        break;
    case "Saturday":
    case "Sunday":
        Console.WriteLine("Weekend");
        break;
    default:
        Console.WriteLine("Unknown day");
        break;
}
```

## Switch Expression (C# 8+) — Preferred Modern Style

```csharp
// Returns a value directly
string dayType = day switch
{
    "Saturday" or "Sunday" => "Weekend",
    "Monday" or "Tuesday" or "Wednesday" or "Thursday" or "Friday" => "Weekday",
    _ => "Unknown"    // _ is the default arm
};
Console.WriteLine(dayType);

// With guard conditions
int score2 = 75;
string grade = score2 switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    >= 60 => "D",
    _     => "F"
};
Console.WriteLine(grade);  // C
```

## Pattern Matching

```csharp
object value = 42;

// Type pattern
if (value is int n)
    Console.WriteLine($"It's an int: {n}");

// Switch expression with type patterns
string Describe(object obj) => obj switch
{
    int i    => $"Integer: {i}",
    double d => $"Double: {d:F2}",
    string s => $"String of length {s.Length}",
    null     => "It's null",
    _        => "Unknown type"
};

Console.WriteLine(Describe(42));       // Integer: 42
Console.WriteLine(Describe(3.14));     // Double: 3.14
Console.WriteLine(Describe("hello"));  // String of length 5
```
""",
            BestPractices = """
- Prefer switch expressions over switch statements for value-returning logic
- Use the ternary operator only for simple conditions — keep it on one line
- Never leave out `break` in a switch statement (unlike C/C++ it won't fall through, but it's required)
- Prefer pattern matching over explicit type checks (`obj is int n` rather than `(int)obj`)
- Use `?.` (null-conditional) to safely access members of potentially null objects
""",
            VoiceSummary = "If-else statements let your program make decisions based on conditions. The ternary operator is a compact one-line if-else. Switch expressions, introduced in C sharp 8, return a value directly and are more concise than traditional switch statements. Pattern matching in C sharp lets you branch on the type or value of an object using the is keyword and switch expressions.",
            Type = LessonType.Practice,
            DurationMinutes = 30,
            Order = 1,
            CreatedAt = d
        },

        new Lesson
        {
            Id = 7,
            CourseModuleId = 3,
            Title = "Loops — For, While, Do-While & Foreach",
            Content = """
# Loops

Loops repeat a block of code while a condition is true, or for a specific number of iterations.

## For Loop

Best when you know how many iterations you need:

```csharp
// Basic: count up
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Iteration {i}");
}
// Output: Iteration 0, 1, 2, 3, 4

// Count down
for (int i = 10; i >= 1; i--)
    Console.Write(i + " ");
// Output: 10 9 8 7 6 5 4 3 2 1

// Step by 2
for (int i = 0; i <= 10; i += 2)
    Console.Write(i + " ");
// Output: 0 2 4 6 8 10
```

## While Loop

Best when you don't know the iteration count in advance:

```csharp
int n = 1;
while (n <= 10)
{
    Console.Write(n + " ");
    n++;
}
// Output: 1 2 3 4 5 6 7 8 9 10

// Reading input until valid
string? input;
do
{
    Console.Write("Enter 'quit' to exit: ");
    input = Console.ReadLine();
    Console.WriteLine($"You entered: {input}");
} while (input != "quit");
```

## Do-While Loop

Guarantees the body runs **at least once**:

```csharp
int attempt = 0;
do
{
    Console.WriteLine($"Attempt #{attempt + 1}");
    attempt++;
} while (attempt < 3);
// Output: Attempt #1, Attempt #2, Attempt #3
```

## Foreach Loop

Best for iterating over collections — clean and concise:

```csharp
string[] fruits = { "Apple", "Banana", "Cherry", "Date" };

foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}

// Works with any IEnumerable
var numbers = new List<int> { 1, 2, 3, 4, 5 };
foreach (var num in numbers)
{
    Console.Write(num * num + " ");  // 1 4 9 16 25
}
```

## Break & Continue

```csharp
// break — exits the loop entirely
for (int i = 0; i < 10; i++)
{
    if (i == 5)
        break;               // stops at 5
    Console.Write(i + " ");  // 0 1 2 3 4
}

// continue — skips current iteration
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0)
        continue;            // skip even numbers
    Console.Write(i + " ");  // 1 3 5 7 9
}
```

## Nested Loops

```csharp
// Multiplication table
for (int row = 1; row <= 5; row++)
{
    for (int col = 1; col <= 5; col++)
    {
        Console.Write($"{row * col,4}");  // ,4 = right-align in 4 chars
    }
    Console.WriteLine();
}
```

## Loop Best Practices & Common Patterns

```csharp
// Sum all numbers 1-100
int sum = 0;
for (int i = 1; i <= 100; i++)
    sum += i;
Console.WriteLine(sum);  // 5050

// Find first element matching a condition
int[] data = { 3, 7, 1, 9, 4, 6 };
int target = -1;
foreach (int x in data)
{
    if (x > 5)
    {
        target = x;
        break;  // Stop once found
    }
}
Console.WriteLine(target);  // 7

// Collecting results in a list
var evens = new List<int>();
for (int i = 1; i <= 20; i++)
    if (i % 2 == 0)
        evens.Add(i);
// evens: 2, 4, 6, ..., 20
```
""",
            BestPractices = """
- Prefer `foreach` over `for` when you just need to iterate a collection without tracking an index
- Use `for` when you need the index, or need to iterate backwards or in steps
- Use `while` when the loop count depends on a runtime condition (user input, network, etc.)
- Never modify a collection while iterating it with `foreach` — use `for` or iterate a copy instead
- Avoid infinite loops — ensure your loop condition will eventually become false
- Break complex nested loops into separate methods for readability
""",
            VoiceSummary = "C sharp has four main loop types. The for loop is used for a known number of iterations with an initialiser, condition, and increment. The while loop runs while a condition is true. The do-while loop runs at least once before checking its condition. The foreach loop cleanly iterates every element in a collection. Use break to exit a loop early and continue to skip to the next iteration.",
            Type = LessonType.Practice,
            DurationMinutes = 30,
            Order = 2,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 4 — Methods
        // ══════════════════════════════

        new Lesson
        {
            Id = 8,
            CourseModuleId = 4,
            Title = "Defining & Calling Methods",
            Content = """
# Methods in C#

A **method** is a named block of code that performs a specific task. Methods allow you to write code once and reuse it throughout your program.

## Basic Method Syntax

```csharp
// returnType MethodName(parameters)
void SayHello()
{
    Console.WriteLine("Hello!");
}

// Call it:
SayHello();  // Hello!
```

## Methods with Parameters

```csharp
void Greet(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

Greet("Alice");  // Hello, Alice!
Greet("Bob");    // Hello, Bob!
```

Multiple parameters:

```csharp
void PrintSum(int a, int b)
{
    Console.WriteLine($"{a} + {b} = {a + b}");
}

PrintSum(3, 5);   // 3 + 5 = 8
PrintSum(10, 20); // 10 + 20 = 30
```

## Return Values

```csharp
// Return type before method name
int Add(int a, int b)
{
    return a + b;
}

double CircleArea(double radius)
{
    return Math.PI * radius * radius;
}

int result = Add(5, 3);
Console.WriteLine(result);           // 8
Console.WriteLine(CircleArea(5.0));  // 78.539...
```

## Expression-Bodied Methods (C# 6+)

For single-expression methods, use the `=>` arrow:

```csharp
int Add(int a, int b) => a + b;
double CircleArea(double r) => Math.PI * r * r;
string Greet(string name) => $"Hello, {name}!";
bool IsEven(int n) => n % 2 == 0;
```

## Optional (Default) Parameters

```csharp
void CreateUser(string name, string role = "User", bool isActive = true)
{
    Console.WriteLine($"Name: {name}, Role: {role}, Active: {isActive}");
}

CreateUser("Alice");                        // Alice, User, True
CreateUser("Bob", "Admin");                // Bob, Admin, True
CreateUser("Charlie", isActive: false);    // Named argument
```

## Named Arguments

```csharp
void DisplayInfo(string firstName, string lastName, int age)
{
    Console.WriteLine($"{firstName} {lastName}, Age: {age}");
}

// Use names to pass in any order
DisplayInfo(lastName: "Mensah", age: 30, firstName: "Ama");
```

## Method Overloading

Multiple methods with the same name but different parameter lists:

```csharp
int Multiply(int a, int b) => a * b;
double Multiply(double a, double b) => a * b;
int Multiply(int a, int b, int c) => a * b * c;

Console.WriteLine(Multiply(3, 4));         // 12
Console.WriteLine(Multiply(3.0, 4.5));    // 13.5
Console.WriteLine(Multiply(2, 3, 4));     // 24
```

The compiler selects the correct overload based on the argument types.

## Recursion

A method that calls itself:

```csharp
int Factorial(int n)
{
    if (n <= 1) return 1;          // Base case
    return n * Factorial(n - 1);   // Recursive call
}

Console.WriteLine(Factorial(5));  // 120 (5*4*3*2*1)
Console.WriteLine(Factorial(10)); // 3628800
```

## Out Parameters

Return multiple values from a method:

```csharp
bool TryDivide(int a, int b, out double result)
{
    if (b == 0)
    {
        result = 0;
        return false;
    }
    result = (double)a / b;
    return true;
}

if (TryDivide(10, 3, out double answer))
    Console.WriteLine($"Result: {answer:F2}");  // Result: 3.33
```

## Params — Variable Number of Arguments

```csharp
int Sum(params int[] numbers)
{
    int total = 0;
    foreach (int n in numbers)
        total += n;
    return total;
}

Console.WriteLine(Sum(1, 2, 3));           // 6
Console.WriteLine(Sum(10, 20, 30, 40));    // 100
Console.WriteLine(Sum());                  // 0
```
""",
            BestPractices = """
- Keep methods short and focused on a single task (Single Responsibility Principle)
- Use expression-bodied methods for simple one-liners to improve readability
- Limit the number of parameters to 3-4; if you need more, consider a parameter object
- Prefer returning a value over using `out` parameters when possible
- Name methods with a verb that describes what they do: `CalculateTax`, `GetUser`, `IsValid`
- Avoid deep recursion on large inputs — use iteration instead to prevent stack overflow
""",
            VoiceSummary = "Methods are named blocks of reusable code. They can take parameters as inputs and return values as outputs. Expression-bodied methods with the arrow syntax provide a concise one-line form. Method overloading lets you define multiple methods with the same name but different parameter types. Recursion is when a method calls itself — always define a base case to stop it. The params keyword lets a method accept a variable number of arguments.",
            Type = LessonType.Mixed,
            DurationMinutes = 35,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 5 — Arrays & Collections
        // ══════════════════════════════

        new Lesson
        {
            Id = 9,
            CourseModuleId = 5,
            Title = "Arrays in C#",
            Content = """
# Arrays in C#

An **array** is a fixed-size, ordered collection of elements of the same type.

## Declaring & Initialising Arrays

```csharp
// Declare with size — elements initialised to default (0 for int)
int[] scores = new int[5];
scores[0] = 95;
scores[1] = 87;
scores[2] = 72;

// Initialiser syntax — size inferred
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

// Combined syntax
double[] prices = new double[] { 9.99, 14.99, 4.50 };

// Implicit type
var names = new[] { "Alice", "Bob", "Charlie" };
```

## Accessing Elements

Arrays are **zero-indexed** — the first element is at index 0:

```csharp
string[] fruits = { "Apple", "Banana", "Cherry" };

Console.WriteLine(fruits[0]);   // Apple
Console.WriteLine(fruits[2]);   // Cherry
Console.WriteLine(fruits[^1]);  // Cherry  — index from end (C# 8+)
Console.WriteLine(fruits[^2]);  // Banana

// Array length
Console.WriteLine(fruits.Length);   // 3
```

## Iterating Arrays

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// For loop (use when you need the index)
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine($"[{i}] = {numbers[i]}");
}

// Foreach (use when you just need values)
foreach (int n in numbers)
    Console.Write(n + " ");
```

## Common Array Operations

```csharp
int[] data = { 5, 2, 8, 1, 9, 3 };

// Sort
Array.Sort(data);
// data: 1, 2, 3, 5, 8, 9

// Reverse
Array.Reverse(data);
// data: 9, 8, 5, 3, 2, 1

// Search (array must be sorted for BinarySearch)
Array.Sort(data);
int index = Array.BinarySearch(data, 5);
Console.WriteLine(index);  // index of 5

// Find
int firstOver5 = Array.Find(data, x => x > 5);

// Copy
int[] copy = new int[data.Length];
Array.Copy(data, copy, data.Length);
```

## Multi-Dimensional Arrays

```csharp
// 2D array (matrix)
int[,] matrix = new int[3, 3];
matrix[0, 0] = 1;
matrix[1, 1] = 5;
matrix[2, 2] = 9;

// 2D initialiser
int[,] grid = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// Access element at row 1, col 2
Console.WriteLine(grid[1, 2]);  // 6

// Iterate
for (int row = 0; row < 3; row++)
{
    for (int col = 0; col < 3; col++)
        Console.Write($"{grid[row, col]} ");
    Console.WriteLine();
}
```

## Jagged Arrays (Array of Arrays)

```csharp
int[][] jagged = new int[3][];
jagged[0] = new int[] { 1, 2 };
jagged[1] = new int[] { 3, 4, 5 };
jagged[2] = new int[] { 6 };

foreach (var row in jagged)
{
    foreach (var val in row)
        Console.Write(val + " ");
    Console.WriteLine();
}
```

## Array Ranges & Slices (C# 8+)

```csharp
int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

int[] slice1 = nums[2..5];    // { 2, 3, 4 }
int[] slice2 = nums[..3];     // { 0, 1, 2 }
int[] slice3 = nums[7..];     // { 7, 8, 9 }
int[] last3  = nums[^3..];    // { 7, 8, 9 }
```
""",
            BestPractices = """
- Use `Array.Sort` before `Array.BinarySearch` — binary search requires sorted data
- Prefer `List<T>` over arrays when the size may change at runtime
- Use index-from-end syntax `[^1]` for the last element instead of `[array.Length - 1]`
- Use range slices `[2..5]` for clean sub-array extraction
- Always check `Length` before accessing elements to avoid `IndexOutOfRangeException`
""",
            VoiceSummary = "Arrays store fixed-size, ordered collections of the same type. They are zero-indexed, meaning the first element is at index zero. Use Array dot Sort and Array dot Reverse for in-place sorting. Multi-dimensional arrays represent matrices, and jagged arrays are arrays of arrays with varying row sizes. C sharp 8 added index-from-end syntax and range slices for cleaner element access.",
            Type = LessonType.Practice,
            DurationMinutes = 30,
            Order = 1,
            CreatedAt = d
        },

        new Lesson
        {
            Id = 10,
            CourseModuleId = 5,
            Title = "List<T> & Dictionary<TKey, TValue>",
            Content = """
# List<T> & Dictionary<TKey, TValue>

## List<T> — Dynamic Array

`List<T>` is the most commonly used collection — a dynamically-resizable, ordered list of typed elements.

```csharp
// Create
var fruits = new List<string>();

// Add elements
fruits.Add("Apple");
fruits.Add("Banana");
fruits.Add("Cherry");
fruits.AddRange(new[] { "Date", "Elderberry" });

// Access
Console.WriteLine(fruits[0]);        // Apple
Console.WriteLine(fruits.Count);     // 5

// Insert at specific position
fruits.Insert(1, "Avocado");  // Insert at index 1

// Remove
fruits.Remove("Banana");             // Remove by value
fruits.RemoveAt(0);                   // Remove by index

// Contains
bool hasApple = fruits.Contains("Apple");  // true

// Find
string? first = fruits.Find(f => f.StartsWith("C"));  // Cherry

// Sort
fruits.Sort();                        // Alphabetical
fruits.Sort((a, b) => b.CompareTo(a)); // Reverse alphabetical

// Convert to array
string[] arr = fruits.ToArray();
```

## List<T> — Initialiser Syntax

```csharp
var scores = new List<int> { 95, 87, 72, 91, 68 };

// LINQ on List
int max   = scores.Max();             // 95
int min   = scores.Min();             // 68
double avg = scores.Average();        // 82.6
int sum   = scores.Sum();             // 413

// Filter
var passing = scores.Where(s => s >= 70).ToList();

// Iterate
foreach (int s in scores)
    Console.WriteLine(s);
```

## Dictionary<TKey, TValue> — Key-Value Lookup

A `Dictionary` stores key-value pairs, providing O(1) average lookup by key.

```csharp
// Create
var capitals = new Dictionary<string, string>
{
    { "Ghana", "Accra" },
    { "Nigeria", "Abuja" },
    { "Kenya", "Nairobi" }
};

// Add
capitals["Egypt"] = "Cairo";
capitals.Add("South Africa", "Pretoria");

// Access by key
Console.WriteLine(capitals["Ghana"]);   // Accra

// Safe access — avoid KeyNotFoundException
if (capitals.TryGetValue("Uganda", out string? capital))
    Console.WriteLine(capital);
else
    Console.WriteLine("Not found");

// Check existence
bool hasNigeria = capitals.ContainsKey("Nigeria");   // true

// Remove
capitals.Remove("Egypt");

// Iterate
foreach (var kvp in capitals)
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");

// Iterate keys or values
foreach (string country in capitals.Keys)
    Console.WriteLine(country);

foreach (string city in capitals.Values)
    Console.WriteLine(city);
```

## Word Frequency — Common Dictionary Pattern

```csharp
string text = "the quick brown fox jumps over the lazy dog the fox";
var words = text.Split(' ');
var freq = new Dictionary<string, int>();

foreach (string word in words)
{
    if (freq.ContainsKey(word))
        freq[word]++;
    else
        freq[word] = 1;
    // Or: freq.TryGetValue(word, out int c); freq[word] = c + 1;
    // Or (cleaner): freq[word] = freq.GetValueOrDefault(word, 0) + 1;
}

foreach (var kvp in freq.OrderByDescending(k => k.Value))
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
```

## Other Useful Collections

```csharp
// HashSet<T> — unique elements, O(1) lookup
var unique = new HashSet<int> { 1, 2, 3 };
unique.Add(2);  // Ignored — already exists
unique.Add(4);
Console.WriteLine(unique.Count);  // 4

// Stack<T> — LIFO
var stack = new Stack<string>();
stack.Push("first");
stack.Push("second");
stack.Push("third");
Console.WriteLine(stack.Pop());    // third
Console.WriteLine(stack.Peek());   // second (doesn't remove)

// Queue<T> — FIFO
var queue = new Queue<string>();
queue.Enqueue("first");
queue.Enqueue("second");
Console.WriteLine(queue.Dequeue()); // first
Console.WriteLine(queue.Peek());    // second
```
""",
            BestPractices = """
- Use `List<T>` as your default collection — it's flexible, typed, and has great LINQ support
- Use `Dictionary<TKey, TValue>` for O(1) key lookups instead of linear search over a list
- Always use `TryGetValue` instead of direct dictionary indexing when the key may not exist
- Use `HashSet<T>` when you need unique elements and fast membership testing
- Use `GetValueOrDefault(key, defaultValue)` for concise dictionary access with a fallback
""",
            VoiceSummary = "List of T is a dynamic array that grows automatically. Use Add, Remove, Contains, and Sort to manage elements. Dictionary of TKey TValue stores key-value pairs with O(1) average lookup. Always use TryGetValue for safe key access. HashSet provides unique element storage. Stack is last-in-first-out and Queue is first-in-first-out.",
            Type = LessonType.Mixed,
            DurationMinutes = 35,
            Order = 2,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 6 — Strings
        // ══════════════════════════════

        new Lesson
        {
            Id = 11,
            CourseModuleId = 6,
            Title = "String Manipulation & Formatting",
            Content = """"
# String Manipulation & Formatting

## Strings are Immutable

In C#, strings are **immutable** — once created, they cannot be changed. Every string operation that "modifies" a string actually creates a new one.

```csharp
string s = "hello";
s.ToUpper();    // Creates a new string, but doesn't change s!
Console.WriteLine(s);          // hello (unchanged)

string upper = s.ToUpper();    // Capture the result
Console.WriteLine(upper);      // HELLO
```

## Common String Methods

```csharp
string text = "  Hello, C# Academy!  ";

// Case
Console.WriteLine(text.ToUpper());           // "  HELLO, C# ACADEMY!  "
Console.WriteLine(text.ToLower());           // "  hello, c# academy!  "

// Trim whitespace
Console.WriteLine(text.Trim());              // "Hello, C# Academy!"
Console.WriteLine(text.TrimStart());         // "Hello, C# Academy!  "
Console.WriteLine(text.TrimEnd());           // "  Hello, C# Academy!"

// Contains, StartsWith, EndsWith
Console.WriteLine(text.Contains("Academy")); // True
Console.WriteLine(text.StartsWith("  He"));  // True
Console.WriteLine(text.EndsWith("!  "));     // True

// Length
Console.WriteLine("hello".Length);           // 5

// Replace
string replaced = text.Replace("Academy", "World");

// Split
string csv = "Alice,Bob,Charlie,Dave";
string[] names = csv.Split(',');
// names: ["Alice", "Bob", "Charlie", "Dave"]

// Join
string joined = string.Join(" | ", names);
// "Alice | Bob | Charlie | Dave"

// Substring
string sub = "Hello, World!".Substring(7, 5);  // "World"
// Or with range: "Hello, World!"[7..12] = "World"

// IndexOf
int pos = "Hello, World!".IndexOf('W');   // 7

// Remove
string removed = "Hello, World!".Remove(5, 7); // "Hello!"
```

## String Interpolation

The preferred way to build strings with embedded values:

```csharp
string name = "Alice";
int age = 25;
double score = 98.7654;

// Basic interpolation
Console.WriteLine($"Name: {name}, Age: {age}");

// Format specifiers inside {}
Console.WriteLine($"Score: {score:F2}");         // 98.77 (2 decimals)
Console.WriteLine($"Score: {score:P1}");         // 9,876.5% (percentage)
Console.WriteLine($"Price: {9.99m:C}");          // $9.99 (currency)
Console.WriteLine($"Hex: {255:X}");              // FF
Console.WriteLine($"Date: {DateTime.Now:dd/MM/yyyy}");

// Padding and alignment
Console.WriteLine($"{"Left",-10}|{"Right",10}"); // Left-align, right-align
Console.WriteLine($"{42,8}");                    // Right-aligned in 8 chars
```

## Verbatim & Raw String Literals

```csharp
// Verbatim string — backslashes treated literally
string path = @"C:\Users\Alice\Documents\file.txt";

// Multi-line verbatim
string json = @"{
    ""name"": ""Alice"",
    ""age"": 25
}";

// Raw string literal (C# 11+) — no escaping needed
string raw = """
    {
        "name": "Alice",
        "age": 25
    }
    """;
```

## String Comparison

```csharp
string a = "Hello";
string b = "hello";

// Case-sensitive (default)
Console.WriteLine(a == b);                    // False
Console.WriteLine(a.Equals(b));               // False

// Case-insensitive
Console.WriteLine(a.Equals(b, StringComparison.OrdinalIgnoreCase));  // True
Console.WriteLine(string.Compare(a, b, ignoreCase: true));           // 0

// Check null or empty
string? s = null;
Console.WriteLine(string.IsNullOrEmpty(s));        // True
Console.WriteLine(string.IsNullOrWhiteSpace("  ")); // True
```

## StringBuilder — For Performance

When building strings in a loop, use `StringBuilder` to avoid creating many intermediate string objects:

```csharp
using System.Text;

// Slow — creates 1000 strings
string result = "";
for (int i = 0; i < 1000; i++)
    result += i.ToString();

// Fast — single buffer, mutates in place
var sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
    sb.Append(i);

string fastResult = sb.ToString();

// Other StringBuilder methods
sb.AppendLine("Next line");
sb.Insert(0, "Start: ");
sb.Replace("old", "new");
```
"""",
            BestPractices = """"
- Always use string interpolation `$"..."` instead of concatenation with `+` for multi-variable strings
- Use `StringBuilder` when concatenating in a loop (10+ iterations) for performance
- Use `string.IsNullOrWhiteSpace` rather than `== null || == ""` for robust null/empty checks
- Use `StringComparison.OrdinalIgnoreCase` for case-insensitive comparisons to be explicit and culture-safe
- Use raw string literals `"""..."""` (C# 11+) for multi-line strings that contain quotes or backslashes
"""",
            VoiceSummary = "Strings in C sharp are immutable, so every method returns a new string. Key methods include ToUpper, ToLower, Trim, Contains, Split, Join, Replace, and Substring. String interpolation with the dollar sign prefix embeds variables and format specifiers directly in strings. Use StringBuilder when building strings in loops to avoid creating many temporary string objects.",
            Type = LessonType.Mixed,
            DurationMinutes = 30,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 7 — OOP: Classes & Objects
        // ══════════════════════════════

        new Lesson
        {
            Id = 12,
            CourseModuleId = 7,
            Title = "Classes, Objects & Constructors",
            Content = """
# Classes, Objects & Constructors

## What is a Class?

A **class** is a blueprint for creating objects. It defines the data (fields/properties) and behaviour (methods) that objects of that class will have.

```csharp
public class BankAccount
{
    // Fields (private data)
    private string _owner;
    private decimal _balance;

    // Constructor — called when creating an object
    public BankAccount(string owner, decimal initialBalance)
    {
        _owner = owner;
        _balance = initialBalance;
    }

    // Property (public access to private data)
    public string Owner => _owner;
    public decimal Balance => _balance;

    // Methods (behaviour)
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.");
        _balance += amount;
        Console.WriteLine($"Deposited {amount:C}. Balance: {_balance:C}");
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > _balance)
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }
        _balance -= amount;
        Console.WriteLine($"Withdrew {amount:C}. Balance: {_balance:C}");
        return true;
    }

    public override string ToString() =>
        $"Account[{_owner}]: {_balance:C}";
}
```

## Creating & Using Objects

```csharp
// Instantiate with new keyword
var account = new BankAccount("Alice", 1000m);

account.Deposit(500m);      // Deposited $500.00. Balance: $1,500.00
account.Withdraw(200m);     // Withdrew $200.00.  Balance: $1,300.00
account.Withdraw(2000m);    // Insufficient funds.

Console.WriteLine(account); // Account[Alice]: $1,300.00
Console.WriteLine(account.Balance);  // 1300
```

## Properties in Detail

Properties expose data with optional logic in getters and setters:

```csharp
public class Person
{
    // Auto-property — compiler generates backing field
    public string FirstName { get; set; } = "";
    public string LastName  { get; set; } = "";

    // Computed property — no backing field
    public string FullName => $"{FirstName} {LastName}";

    // Property with validation in setter
    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 150)
                throw new ArgumentOutOfRangeException(nameof(value), "Age must be 0-150.");
            _age = value;
        }
    }

    // Init-only property (C# 9+) — set only in constructor or initialiser
    public DateTime DateOfBirth { get; init; }
}

// Object initialiser syntax (requires parameterless constructor or init properties)
var person = new Person
{
    FirstName = "Ama",
    LastName = "Mensah",
    Age = 28,
    DateOfBirth = new DateTime(1996, 3, 15)
};

Console.WriteLine(person.FullName);     // Ama Mensah
```

## Multiple Constructors (Constructor Overloading)

```csharp
public class Rectangle
{
    public double Width  { get; }
    public double Height { get; }

    // Parameterless constructor — square
    public Rectangle() : this(1.0, 1.0) { }

    // Single parameter — square with given side
    public Rectangle(double side) : this(side, side) { }

    // Full constructor
    public Rectangle(double width, double height)
    {
        Width  = width;
        Height = height;
    }

    public double Area()      => Width * Height;
    public double Perimeter() => 2 * (Width + Height);

    public override string ToString() =>
        $"Rectangle({Width}×{Height}) Area={Area()}";
}

var r1 = new Rectangle();           // 1×1
var r2 = new Rectangle(5.0);        // 5×5
var r3 = new Rectangle(4.0, 6.0);   // 4×6

Console.WriteLine(r3.Area());       // 24
Console.WriteLine(r3);              // Rectangle(4×6) Area=24
```

## Static Members

Static members belong to the class itself, not to any instance:

```csharp
public class MathHelper
{
    public static double Pi = 3.14159265358979;

    public static double CircleArea(double r) => Pi * r * r;
    public static int Clamp(int value, int min, int max) =>
        Math.Max(min, Math.Min(max, value));
}

// Call without creating an instance
Console.WriteLine(MathHelper.CircleArea(5)); // 78.539...
Console.WriteLine(MathHelper.Clamp(150, 0, 100)); // 100
```
""",
            BestPractices = """
- Keep fields `private` and expose data through properties — this is encapsulation
- Use `readonly` fields or `init`-only properties for immutable data
- Override `ToString()` on your classes to make debugging and logging cleaner
- Use `: this(...)` constructor chaining to avoid duplicating initialisation logic
- Validate inputs in constructors and setters to maintain object invariants
- Use `nameof(paramName)` in exceptions instead of hard-coded strings — safer for refactoring
""",
            VoiceSummary = "A class is a blueprint for objects, defining their data as properties and their behaviour as methods. The constructor runs when an object is created with the new keyword. Properties expose private fields with optional validation logic. Use init-only properties for immutable data. Static members belong to the class itself, not to individual instances. Override ToString to get useful text representations of your objects.",
            Type = LessonType.Mixed,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 9 — Inheritance & Polymorphism
        // ══════════════════════════════

        new Lesson
        {
            Id = 13,
            CourseModuleId = 9,
            Title = "Inheritance & Polymorphism",
            Content = """
# Inheritance & Polymorphism

## Inheritance

**Inheritance** lets a class (derived/child) inherit members from another class (base/parent), promoting code reuse and establishing an "is-a" relationship.

```csharp
// Base class
public class Animal
{
    public string Name   { get; set; }
    public int    Age    { get; set; }

    public Animal(string name, int age)
    {
        Name = name;
        Age  = age;
    }

    // virtual — can be overridden in derived classes
    public virtual string Speak()
    {
        return "...";
    }

    public virtual void Describe()
    {
        Console.WriteLine($"I am {Name}, a {GetType().Name} aged {Age}.");
    }
}

// Derived class
public class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, int age, string breed)
        : base(name, age)   // Call base constructor
    {
        Breed = breed;
    }

    // override — replaces the base implementation
    public override string Speak() => "Woof! Woof!";

    public override void Describe()
    {
        base.Describe();    // Call base method first
        Console.WriteLine($"I am a {Breed}.");
    }
}

public class Cat : Animal
{
    public Cat(string name, int age) : base(name, age) { }

    public override string Speak() => "Meow!";
}
```

## Polymorphism

**Polymorphism** means "many forms" — the same method call produces different behaviour depending on the actual runtime type:

```csharp
// All treated as Animals (base type)
Animal[] animals =
{
    new Dog("Rex",   3, "German Shepherd"),
    new Cat("Luna",  2),
    new Dog("Buddy", 5, "Labrador"),
    new Cat("Kitty", 1)
};

// Same method call — different output per type
foreach (Animal animal in animals)
{
    Console.WriteLine($"{animal.Name} says: {animal.Speak()}");
}
// Rex   says: Woof! Woof!
// Luna  says: Meow!
// Buddy says: Woof! Woof!
// Kitty says: Meow!
```

## The `sealed` Keyword

Prevent further inheritance with `sealed`:

```csharp
public sealed class GoldenRetriever : Dog
{
    public GoldenRetriever(string name, int age)
        : base(name, age, "Golden Retriever") { }

    public override string Speak() => "Bark! (very friendly)";
}

// ERROR: cannot inherit from sealed class
// public class GoldenRetrieverMix : GoldenRetriever { }
```

## Casting & Type Checking

```csharp
Animal animal = new Dog("Rex", 3, "Husky");

// 'is' — type check
if (animal is Dog)
    Console.WriteLine("It's a dog!");

// 'is' with pattern — check and cast in one step
if (animal is Dog dog)
    Console.WriteLine($"Breed: {dog.Breed}");

// 'as' — safe cast (returns null if fails, no exception)
Dog? d = animal as Dog;
if (d != null)
    Console.WriteLine(d.Breed);

// Direct cast — throws InvalidCastException if type mismatch
Dog rex = (Dog)animal;
```

## Object — The Root of All Classes

Every class in C# implicitly inherits from `System.Object`, which provides:

```csharp
string s = "Hello";
Console.WriteLine(s.ToString());       // Hello
Console.WriteLine(s.GetType());        // System.String
Console.WriteLine(s.Equals("Hello")); // True
Console.WriteLine(s.GetHashCode());    // some int
```
""",
            BestPractices = """
- Favour composition over inheritance — "has-a" is often better than "is-a"
- Only use inheritance when there is a genuine "is-a" relationship
- Mark methods as `virtual` only if you intend them to be overridden
- Use `sealed` on classes or methods to prevent unintended overriding
- Prefer `is` pattern matching over `as` followed by null check — it's more concise
- Use `base.Method()` when you want to extend (not fully replace) the base behaviour
""",
            VoiceSummary = "Inheritance lets a derived class reuse code from a base class. The colon syntax declares the parent, and the base keyword calls the parent's constructor or methods. Virtual methods in the base class can be overridden in derived classes. Polymorphism means a base class reference can point to any derived class object, and the correct overridden method is called at runtime. Use the is keyword for safe type checking and pattern matching.",
            Type = LessonType.Video,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 10 — Interfaces & Abstract
        // ══════════════════════════════

        new Lesson
        {
            Id = 14,
            CourseModuleId = 10,
            Title = "Interfaces & Abstract Classes",
            Content = """
# Interfaces & Abstract Classes

## Abstract Classes

An **abstract class** cannot be instantiated directly. It defines a template with some methods implemented and some abstract (must be overridden):

```csharp
public abstract class Shape
{
    public string Color { get; set; } = "Black";

    // Abstract method — no implementation, MUST be overridden
    public abstract double Area();
    public abstract double Perimeter();

    // Concrete method — has implementation, inherited as-is
    public void PrintInfo()
    {
        Console.WriteLine($"{GetType().Name}: Area={Area():F2}, Perimeter={Perimeter():F2}, Color={Color}");
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(double radius) => Radius = radius;

    public override double Area()      => Math.PI * Radius * Radius;
    public override double Perimeter() => 2 * Math.PI * Radius;
}

public class Rectangle2 : Shape
{
    public double Width  { get; set; }
    public double Height { get; set; }

    public Rectangle2(double w, double h) { Width = w; Height = h; }

    public override double Area()      => Width * Height;
    public override double Perimeter() => 2 * (Width + Height);
}

// Usage
Shape[] shapes = { new Circle(5), new Rectangle2(4, 6) };
foreach (var shape in shapes)
    shape.PrintInfo();
```

## Interfaces

An **interface** defines a contract — a set of members that implementing classes must provide. Interfaces enable multiple "type inheritance" in C#.

```csharp
public interface IDrawable
{
    void Draw();
    string Color { get; set; }
}

public interface IResizable
{
    void Resize(double factor);
}

// A class can implement multiple interfaces
public class GraphicCircle : IDrawable, IResizable
{
    public double Radius { get; private set; }
    public string Color  { get; set; } = "Red";

    public GraphicCircle(double radius) => Radius = radius;

    public void Draw()
    {
        Console.WriteLine($"Drawing a {Color} circle with radius {Radius}");
    }

    public void Resize(double factor)
    {
        Radius *= factor;
        Console.WriteLine($"Resized to radius {Radius}");
    }
}

GraphicCircle gc = new GraphicCircle(5);
gc.Draw();         // Drawing a Red circle with radius 5
gc.Resize(2.0);    // Resized to radius 10
gc.Draw();         // Drawing a Red circle with radius 10

// Use interface as type — polymorphism
IDrawable drawable = gc;
drawable.Draw();
```

## Interface Default Methods (C# 8+)

```csharp
public interface ILogger
{
    void Log(string message);

    // Default implementation — classes can optionally override
    void LogError(string message) => Log($"[ERROR] {message}");
    void LogInfo(string message)  => Log($"[INFO]  {message}");
}

public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);
    // LogError and LogInfo are inherited from the interface
}

var logger = new ConsoleLogger();
logger.LogInfo("App started");    // [INFO]  App started
logger.LogError("Crash!");       // [ERROR] Crash!
```

## Interface vs Abstract Class

| Feature | Abstract Class | Interface |
|---|---|---|
| Multiple inheritance | ❌ Single only | ✅ Multiple interfaces |
| Constructor | ✅ Can have | ❌ Cannot have |
| Fields | ✅ Can have | ❌ Properties only |
| Default impl | ✅ Concrete methods | ✅ C# 8+ default methods |
| Access modifiers | ✅ public/protected/private | ✅ All public by default |
| Use when | Share code among related types | Define a contract for unrelated types |

## Common Built-in Interfaces

```csharp
// IComparable<T> — defines natural ordering
public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public double GPA  { get; set; }

    public int CompareTo(Student? other)
    {
        if (other is null) return 1;
        return GPA.CompareTo(other.GPA); // Sort by GPA
    }
}

var students = new List<Student>
{
    new() { Name = "Alice", GPA = 3.8 },
    new() { Name = "Bob",   GPA = 3.5 },
    new() { Name = "Carol", GPA = 3.9 }
};
students.Sort();
// Sorted: Bob (3.5), Alice (3.8), Carol (3.9)
```
""",
            BestPractices = """
- Program to interfaces, not implementations — depend on `ILogger`, not `ConsoleLogger`
- Use abstract classes when related types share significant common behaviour
- Use interfaces for contracts that unrelated types should satisfy (e.g., `IDisposable`, `IComparable`)
- Keep interfaces small and focused — prefer many small interfaces over one fat interface (ISP)
- Prefix interface names with `I` by convention: `IRepository`, `IPaymentService`
""",
            VoiceSummary = "Abstract classes define templates with some implemented and some abstract methods that derived classes must fill in. Interfaces define contracts — a set of members that any implementing class must provide. A class can implement multiple interfaces but only inherit from one class. Use abstract classes when related types share code, and interfaces when defining behaviour contracts across unrelated types.",
            Type = LessonType.Mixed,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 12 — ASP.NET Core Fundamentals
        // ══════════════════════════════

        new Lesson
        {
            Id = 15,
            CourseModuleId = 12,
            Title = "ASP.NET Core Project Structure & Dependency Injection",
            Content = """
# ASP.NET Core Project Structure & Dependency Injection

## Creating a Web API Project

```bash
dotnet new webapi -n TodoApi --use-controllers
cd TodoApi
dotnet run
```

Browse to `https://localhost:xxxx/swagger` to see the auto-generated API documentation.

## Project Structure

```
TodoApi/
├── Controllers/          ← API controllers (request handlers)
├── Models/               ← Data models and DTOs
├── Services/             ← Business logic
├── Data/                 ← DbContext and migrations
├── Program.cs            ← App bootstrap and DI registration
├── appsettings.json      ← Configuration
└── TodoApi.csproj        ← Project file
```

## Program.cs — The Composition Root

```csharp
var builder = WebApplication.CreateBuilder(args);

// ── Register services in the DI container ──
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your own services
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddSingleton<ILogger, ConsoleLogger>();

// Add EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// ── Configure the middleware pipeline ──
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## Dependency Injection

DI is built into ASP.NET Core. Register services in `Program.cs`, then request them in constructors:

```csharp
// Service lifetimes:
// Transient  — new instance per request to the DI container
// Scoped     — new instance per HTTP request
// Singleton  — one instance for the entire application

builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
```

```csharp
// Controller receives its dependency via constructor injection
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;  // Injected by the DI container
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos);
    }
}
```

## Configuration

```json
// appsettings.json
{
  "ConnectionStrings": {
    "Default": "Data Source=todo.db"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "TodoApi",
    "ExpiryMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

```csharp
// Reading config in a service
public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GetIssuer() => _config["JwtSettings:Issuer"]!;
}

// Strongly-typed options (preferred)
public class JwtSettings
{
    public string SecretKey    { get; set; } = "";
    public string Issuer       { get; set; } = "";
    public int    ExpiryMinutes { get; set; }
}

// In Program.cs:
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// In a class:
public class TokenService
{
    private readonly JwtSettings _jwt;

    public TokenService(IOptions<JwtSettings> opts)
    {
        _jwt = opts.Value;
    }
}
```
""",
            BestPractices = """
- Always register dependencies in `Program.cs`, not scattered across the codebase
- Prefer `Scoped` for database-related services, `Singleton` for stateless services
- Never create `new SomeService()` directly — let the DI container manage object lifetimes
- Use strongly-typed options (`IOptions<T>`) rather than raw `IConfiguration` access
- Keep `Program.cs` clean — extract service registration to extension methods as the project grows
""",
            VoiceSummary = "ASP.NET Core apps are bootstrapped in Program dot cs, where you register services with the DI container and configure the middleware pipeline. Dependency injection is a first-class citizen — register your services as Transient, Scoped, or Singleton, then request them via constructor parameters. Configuration is read from appsettings dot json and exposed via IConfiguration or strongly-typed IOptions.",
            Type = LessonType.Reading,
            DurationMinutes = 35,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 13 — RESTful Controllers
        // ══════════════════════════════

        new Lesson
        {
            Id = 16,
            CourseModuleId = 13,
            Title = "Building RESTful Controllers",
            Content = """
# Building RESTful Controllers

## REST Conventions

| HTTP Verb | Route | Action | Success Code |
|---|---|---|---|
| GET | /api/items | List all items | 200 OK |
| GET | /api/items/{id} | Get one item | 200 OK |
| POST | /api/items | Create item | 201 Created |
| PUT | /api/items/{id} | Replace item | 200 OK / 204 No Content |
| PATCH | /api/items/{id} | Partial update | 200 OK |
| DELETE | /api/items/{id} | Delete item | 204 No Content |

## Complete CRUD Controller

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
        => _service = service;

    // GET api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    // GET api/products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product is null)
            return NotFound(new { message = $"Product {id} not found." });
        return Ok(product);
    }

    // POST api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/products/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        if (!await _service.ExistsAsync(id))
            return NotFound();

        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE api/products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _service.ExistsAsync(id))
            return NotFound();

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
```

## Model Binding & Validation

```csharp
public class CreateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = "";

    [Required]
    [Range(0.01, 100_000)]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}
```

When `[ApiController]` is applied, model validation runs automatically before the action method executes — returning a 400 Bad Request with validation errors if any.

## Route Constraints

```csharp
[HttpGet("{id:int}")]          // Only matches integers
[HttpGet("{name:alpha}")]      // Only matches alphabetic strings
[HttpGet("{id:int:min(1)}")]   // Only matches int >= 1
[HttpGet("{slug:regex(^[a-z0-9-]+$)}")]  // Regex constraint
```

## Returning Proper Responses

```csharp
return Ok(data);                              // 200 with body
return Created(uri, data);                    // 201 with location header
return CreatedAtAction(nameof(Get), new { id }, data); // 201 with action link
return NoContent();                           // 204 no body
return BadRequest(new { error = "msg" });     // 400 with details
return Unauthorized();                        // 401
return Forbid();                              // 403
return NotFound(new { message = "..." });    // 404 with details
return Conflict(new { message = "..." });    // 409
return StatusCode(500, new { message = "..." }); // 500
```
""",
            BestPractices = """
- Always return typed `ActionResult<T>` from GET actions — provides better Swagger documentation
- Use `CreatedAtAction` on POST — it sets the `Location` header to the new resource's URL
- Return `NoContent()` (204) from PUT/DELETE, not `Ok()` with an empty body
- Let `[ApiController]` handle model validation automatically — don't repeat validation logic
- Use route constraints like `{id:int}` to prevent invalid routes reaching your controller
- Never return internal exception details in production responses — use problem details
""",
            VoiceSummary = "RESTful controllers map HTTP verbs to CRUD actions using attributes like HttpGet, HttpPost, HttpPut, and HttpDelete. Return typed ActionResult of T for clear Swagger documentation. Use 200 OK for successful GET, 201 Created for POST, 204 No Content for PUT and DELETE, and 404 Not Found when a resource doesn't exist. The ApiController attribute enables automatic model validation.",
            Type = LessonType.Mixed,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 16 — Arrays & Linked Lists (DSA)
        // ══════════════════════════════

        new Lesson
        {
            Id = 17,
            CourseModuleId = 16,
            Title = "Big O Notation & Array Complexity",
            Content = """
# Big O Notation & Algorithm Complexity

## Why Complexity Matters

Two programs can produce the same result but with very different performance characteristics. **Big O notation** describes how the runtime or memory usage of an algorithm scales with input size `n`.

## Common Complexities (Best to Worst)

| Notation | Name | Example | n=100 ops |
|---|---|---|---|
| O(1) | Constant | Array index access | 1 |
| O(log n) | Logarithmic | Binary search | 7 |
| O(n) | Linear | Linear search | 100 |
| O(n log n) | Linearithmic | Merge sort | 664 |
| O(n²) | Quadratic | Bubble sort | 10,000 |
| O(2ⁿ) | Exponential | Recursive Fibonacci | 2^100 |

## Array Operations & Complexity

```csharp
int[] arr = { 10, 20, 30, 40, 50 };

// O(1) — Constant time: direct index access
int first = arr[0];                    // Always 1 operation
int last  = arr[arr.Length - 1];      // Always 1 operation

// O(n) — Linear time: iterate entire array
int sum = 0;
foreach (int x in arr) sum += x;      // Scales with size

// O(n) — Linear search (unsorted array)
int LinearSearch(int[] a, int target)
{
    for (int i = 0; i < a.Length; i++)
        if (a[i] == target) return i;
    return -1;
}

// O(log n) — Binary search (sorted array only)
int BinarySearch(int[] a, int target)
{
    int lo = 0, hi = a.Length - 1;
    while (lo <= hi)
    {
        int mid = lo + (hi - lo) / 2;
        if (a[mid] == target) return mid;
        if (a[mid] < target)  lo = mid + 1;
        else                  hi = mid - 1;
    }
    return -1;
}
```

## Linked List Implementation

A **singly linked list** consists of nodes, where each node holds a value and a reference to the next node.

```csharp
public class Node<T>
{
    public T     Data { get; set; }
    public Node<T>? Next { get; set; }

    public Node(T data) => Data = data;
}

public class LinkedList<T>
{
    private Node<T>? _head;
    public int Count { get; private set; }

    // O(1) — Insert at front
    public void AddFirst(T data)
    {
        var newNode = new Node<T>(data);
        newNode.Next = _head;
        _head = newNode;
        Count++;
    }

    // O(n) — Insert at end
    public void AddLast(T data)
    {
        var newNode = new Node<T>(data);
        if (_head is null)
        {
            _head = newNode;
        }
        else
        {
            var current = _head;
            while (current.Next is not null)
                current = current.Next;
            current.Next = newNode;
        }
        Count++;
    }

    // O(n) — Search
    public bool Contains(T data)
    {
        var current = _head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                return true;
            current = current.Next;
        }
        return false;
    }

    // O(n) — Print all
    public void Print()
    {
        var current = _head;
        while (current is not null)
        {
            Console.Write(current.Data + " → ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }
}

// Usage
var list = new LinkedList<int>();
list.AddFirst(30);
list.AddFirst(20);
list.AddFirst(10);
list.AddLast(40);
list.Print();           // 10 → 20 → 30 → 40 → null
Console.WriteLine(list.Contains(20));  // True
```

## Array vs Linked List

| Operation | Array | Linked List |
|---|---|---|
| Access by index | O(1) | O(n) |
| Search | O(n) | O(n) |
| Insert at start | O(n) | O(1) |
| Insert at end | O(1)* | O(n) |
| Delete at start | O(n) | O(1) |
| Memory | Contiguous | Scattered with pointers |

*O(1) amortised for dynamic arrays like `List<T>`
""",
            BestPractices = """
- Always think about the Big O of your chosen data structure before writing code
- Use arrays or `List<T>` when you need O(1) index access
- Use a linked list when you need frequent O(1) insertions/deletions at the front
- Always sort before binary searching — `Array.Sort` uses an O(n log n) algorithm
- Avoid O(n²) algorithms on large inputs — they become unusable quickly
""",
            VoiceSummary = "Big O notation describes how an algorithm's runtime scales with input size. O of 1 is constant, O of log n is logarithmic like binary search, O of n is linear like a simple loop, and O of n squared is quadratic like nested loops. Arrays offer O of 1 index access but O of n insertion at the start. Linked lists offer O of 1 insertion at the front but O of n access by index.",
            Type = LessonType.Mixed,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 20 — Delegates & Lambdas
        // ══════════════════════════════

        new Lesson
        {
            Id = 18,
            CourseModuleId = 20,
            Title = "Delegates, Func<T>, Action<T> & Lambdas",
            Content = """
# Delegates, Func<T>, Action<T> & Lambda Expressions

## What is a Delegate?

A **delegate** is a type that holds a reference to a method. It lets you treat methods as first-class values — pass them as arguments, store them in variables, and invoke them later.

```csharp
// Declare a delegate type
delegate int MathOperation(int a, int b);

// Methods that match the signature
int Add(int a, int b) => a + b;
int Multiply(int a, int b) => a * b;

// Assign and invoke
MathOperation op = Add;
Console.WriteLine(op(5, 3));   // 8

op = Multiply;
Console.WriteLine(op(5, 3));   // 15
```

## Multicast Delegates

```csharp
delegate void Notify(string message);

void OnEmail(string msg) => Console.WriteLine($"Email: {msg}");
void OnSms(string msg)   => Console.WriteLine($"SMS:   {msg}");

Notify notify = OnEmail;
notify += OnSms;           // Add another handler
notify("Server is down");  // Both methods called

notify -= OnEmail;         // Remove a handler
notify("All clear");       // Only SMS now
```

## Built-in Delegates: Func & Action

C# provides generic delegate types so you rarely need to declare your own:

```csharp
// Func<TResult> — no parameters, returns TResult
Func<int> getRandom = () => new Random().Next(1, 100);

// Func<T, TResult> — one parameter
Func<int, bool>   isEven    = n => n % 2 == 0;
Func<string, int> strLength = s => s.Length;

// Func<T1, T2, TResult> — two parameters
Func<int, int, int>    add     = (a, b) => a + b;
Func<string, string, string> concat = (a, b) => a + b;

Console.WriteLine(isEven(4));        // True
Console.WriteLine(strLength("Hi")); // 2
Console.WriteLine(add(3, 7));       // 10

// Action<T> — has parameters, returns void
Action<string> print  = msg => Console.WriteLine(msg);
Action<int, int> printSum = (a, b) => Console.WriteLine(a + b);

print("Hello!");      // Hello!
printSum(5, 3);       // 8

// Predicate<T> — shorthand for Func<T, bool>
Predicate<int> isPositive = n => n > 0;
Console.WriteLine(isPositive(-5));  // False
```

## Lambda Expressions

A **lambda expression** is an anonymous method written inline:

```csharp
// Syntax: (parameters) => expression
// One liner:
Func<int, int> square = x => x * x;

// Multi-line (statement lambda):
Func<int, int> factorial = n =>
{
    int result = 1;
    for (int i = 2; i <= n; i++)
        result *= i;
    return result;
};

Console.WriteLine(square(5));      // 25
Console.WriteLine(factorial(5));   // 120
```

## Closures — Capturing Variables

Lambdas can capture variables from their enclosing scope:

```csharp
int multiplier = 3;
Func<int, int> triple = x => x * multiplier;

Console.WriteLine(triple(5));  // 15

multiplier = 10;
Console.WriteLine(triple(5));  // 50  ← uses current value, not original!
```

> ⚠️ Closures capture the **variable**, not the value. Changes to the captured variable are reflected.

## Higher-Order Functions

Functions that take other functions as parameters or return functions:

```csharp
// Apply a function to each element
void ForEach(int[] numbers, Action<int> action)
{
    foreach (int n in numbers)
        action(n);
}

int[] nums = { 1, 2, 3, 4, 5 };
ForEach(nums, n => Console.Write(n * n + " "));  // 1 4 9 16 25

// Filter elements using a predicate
int[] Filter(int[] numbers, Func<int, bool> predicate)
{
    var result = new List<int>();
    foreach (int n in numbers)
        if (predicate(n))
            result.Add(n);
    return result.ToArray();
}

int[] evens = Filter(nums, n => n % 2 == 0);
// evens: 2, 4
```

This is essentially what LINQ's `Where` method does — and we'll explore that next.
""",
            BestPractices = """
- Use `Func<T>` and `Action<T>` instead of declaring custom delegates in most cases
- Be careful with closures in loops — capture loop variables explicitly if needed
- Keep lambda bodies short — if the body is more than 2-3 lines, extract it to a named method
- Use `Predicate<T>` for filter predicates to communicate intent clearly
- Multicast delegates call handlers in the order they were added — be aware of side effects
""",
            VoiceSummary = "Delegates are types that hold references to methods, letting you pass methods as values. Func of T is a built-in delegate that takes parameters and returns a value. Action of T takes parameters but returns nothing. Lambda expressions are anonymous inline methods using the arrow syntax. Closures let lambdas capture variables from the surrounding scope, but they capture the variable itself, not its value at the time of capture.",
            Type = LessonType.Mixed,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 21 — LINQ Essentials
        // ══════════════════════════════

        new Lesson
        {
            Id = 19,
            CourseModuleId = 21,
            Title = "LINQ — Language Integrated Query",
            Content = """
# LINQ — Language Integrated Query

**LINQ** lets you query and transform data using a consistent, expressive syntax directly in C# — no matter whether the data is in a list, array, database, or XML.

## Method Syntax vs Query Syntax

```csharp
int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// ── Method syntax (preferred, more composable) ──
var evens = numbers
    .Where(n => n % 2 == 0)   // Filter
    .OrderByDescending(n => n) // Sort
    .ToList();                 // Execute
// [10, 8, 6, 4, 2]

// ── Query syntax (SQL-like, good for complex joins) ──
var evensQuery =
    (from n in numbers
     where n % 2 == 0
     orderby n descending
     select n)
    .ToList();
// Same result
```

## Core LINQ Operators

```csharp
var nums = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };

// WHERE — filter
var big = nums.Where(n => n > 4);           // 5, 9, 6, 5, 5

// SELECT — project/transform
var doubled = nums.Select(n => n * 2);      // 6, 2, 8, 2, 10, ...

// ORDER BY
var sorted  = nums.OrderBy(n => n);         // ascending
var desc    = nums.OrderByDescending(n => n); // descending

// DISTINCT — remove duplicates
var unique  = nums.Distinct();              // 3, 1, 4, 5, 9, 2, 6

// TAKE / SKIP — paging
var first3  = nums.Take(3);                 // 3, 1, 4
var skip3   = nums.Skip(3);                 // 1, 5, 9, ...
var page2   = nums.Skip(3).Take(3);         // 1, 5, 9

// AGGREGATE functions
int  sum    = nums.Sum();                   // 44
int  min    = nums.Min();                   // 1
int  max    = nums.Max();                   // 9
double avg  = nums.Average();               // 4.0
int  count  = nums.Count();                 // 11
int  countB = nums.Count(n => n > 4);       // 4
```

## Working with Objects

```csharp
public record Product(int Id, string Name, string Category, decimal Price, int Stock);

var products = new List<Product>
{
    new(1, "Laptop",     "Electronics", 999.99m,  50),
    new(2, "Mouse",      "Electronics", 29.99m,  200),
    new(3, "Desk",       "Furniture",   349.00m,  15),
    new(4, "Chair",      "Furniture",   249.00m,  30),
    new(5, "Monitor",    "Electronics", 399.00m,  75),
    new(6, "Keyboard",   "Electronics", 79.99m,  150),
    new(7, "Bookshelf",  "Furniture",   199.00m,  20),
};

// Filter + Order
var expensive = products
    .Where(p => p.Price > 200)
    .OrderBy(p => p.Price)
    .ToList();

// Project — select specific fields
var names = products
    .Select(p => new { p.Name, p.Price })
    .OrderBy(x => x.Price);

// GroupBy
var byCategory = products
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        Count    = g.Count(),
        TotalValue = g.Sum(p => p.Price * p.Stock)
    });

foreach (var cat in byCategory)
    Console.WriteLine($"{cat.Category}: {cat.Count} items, ${cat.TotalValue:N0} total value");

// First, Single, Any, All
var cheapest = products.MinBy(p => p.Price);
var first    = products.First(p => p.Category == "Electronics");
bool anyOut  = products.Any(p => p.Stock == 0);
bool allIn   = products.All(p => p.Stock > 0);
```

## Deferred Execution

LINQ queries are **lazy** — they don't run until you enumerate them:

```csharp
var data = new List<int> { 1, 2, 3, 4, 5 };

// This builds a query but does NOT execute yet
var query = data.Where(n => n > 2);

data.Add(10);  // Add element AFTER defining query

// Execution happens here — includes 10!
foreach (int n in query)
    Console.Write(n + " ");
// Output: 3 4 5 10

// Force immediate execution:
var snapshot = data.Where(n => n > 2).ToList();  // Executes NOW
data.Add(99);  // Does NOT affect snapshot
```

## Joining Collections

```csharp
var orders = new[]
{
    new { OrderId = 1, ProductId = 1, Qty = 2 },
    new { OrderId = 2, ProductId = 3, Qty = 1 },
    new { OrderId = 3, ProductId = 1, Qty = 3 },
};

var orderDetails =
    from o in orders
    join p in products on o.ProductId equals p.Id
    select new { o.OrderId, p.Name, o.Qty, Total = p.Price * o.Qty };

foreach (var detail in orderDetails)
    Console.WriteLine($"Order {detail.OrderId}: {detail.Name} x{detail.Qty} = ${detail.Total:N2}");
```
""",
            BestPractices = """
- Prefer method syntax — it chains naturally and is more composable than query syntax
- Always call `ToList()` or `ToArray()` to materialise a query when you need a snapshot
- Never enumerate a LINQ query multiple times without materialising — it re-executes each time
- Use `FirstOrDefault` instead of `First` to avoid exceptions when elements may not exist
- Avoid LINQ in tight inner loops — the overhead of lambda calls can matter at scale
- Use `Any()` instead of `Count() > 0` — it short-circuits on the first match
""",
            VoiceSummary = "LINQ provides a consistent way to query and transform data using method or query syntax. Core operators include Where for filtering, Select for projection, OrderBy for sorting, GroupBy for grouping, and aggregate methods like Sum, Min, Max, and Average. LINQ uses deferred execution — queries don't run until you enumerate them. Call ToList or ToArray to materialise results immediately.",
            Type = LessonType.Practice,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 8 — Encapsulation & Properties
        // ══════════════════════════════

        new Lesson
        {
            Id = 20,
            CourseModuleId = 8,
            Title = "Encapsulation, Properties & Access Modifiers",
            Content = """
# Encapsulation, Properties & Access Modifiers

## What is Encapsulation?

**Encapsulation** is the OOP principle of bundling data (fields) and the methods that operate on that data into a single unit (class), while restricting direct access to the internal state. This protects data integrity and hides implementation details.

```csharp
// ❌ BAD — public fields, no control
public class BadBankAccount
{
    public decimal Balance;  // Anyone can set this to negative!
}

// ✅ GOOD — private field with controlled access
public class BankAccount
{
    private decimal _balance;

    public decimal Balance => _balance;  // Read-only property

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit must be positive.");
        _balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > _balance) return false;
        _balance -= amount;
        return true;
    }
}
```

## Access Modifiers

| Modifier | Accessible From |
|---|---|
| `public` | Anywhere |
| `private` | Same class only |
| `protected` | Same class + derived classes |
| `internal` | Same assembly (project) |
| `protected internal` | Same assembly OR derived classes |
| `private protected` | Same class + derived classes in same assembly |

```csharp
public class Employee
{
    public   string Name { get; set; }        // Anywhere
    private  decimal _salary;                 // This class only
    protected int DepartmentId { get; set; }  // This + derived
    internal string EmployeeCode { get; set; } // Same project
}
```

**Rule of thumb:** Start with `private` and only widen access as needed.

## Properties — The C# Way

Properties look like fields but have getter/setter logic behind them.

### Auto-Properties

```csharp
public class Product
{
    // Auto-implemented — compiler generates the backing field
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; private set; }  // Public get, private set

    public void Restock(int quantity) => Stock += quantity;
}
```

### Full Properties with Backing Fields

```csharp
public class Temperature
{
    private double _celsius;

    public double Celsius
    {
        get => _celsius;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("Below absolute zero!");
            _celsius = value;
        }
    }

    // Computed property — no backing field
    public double Fahrenheit => (_celsius * 9.0 / 5.0) + 32;
    public double Kelvin => _celsius + 273.15;
}

var t = new Temperature { Celsius = 100 };
Console.WriteLine(t.Fahrenheit);  // 212
Console.WriteLine(t.Kelvin);     // 373.15
```

### Init-Only Properties (C# 9+)

```csharp
public class User
{
    public int Id { get; init; }          // Can only be set during initialisation
    public string Email { get; init; }
    public string Name { get; set; }      // Can be changed later
}

var user = new User { Id = 1, Email = "alice@example.com", Name = "Alice" };
user.Name = "Alice M.";    // ✅ OK
// user.Id = 2;            // ❌ Compile error — init-only
```

### Required Properties (C# 11+)

```csharp
public class Order
{
    public required string CustomerId { get; init; }
    public required decimal Total { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}

// Must provide required properties or compiler error
var order = new Order { CustomerId = "C001", Total = 199.99m };
```

## Records — Immutable Data Types

```csharp
// Record class — immutable by default, value-based equality
public record Person(string FirstName, string LastName, int Age);

var p1 = new Person("Alice", "Mensah", 25);
var p2 = new Person("Alice", "Mensah", 25);

Console.WriteLine(p1 == p2);  // True — value equality
Console.WriteLine(p1);        // Person { FirstName = Alice, LastName = Mensah, Age = 25 }

// With-expression — create a modified copy
var p3 = p1 with { Age = 26 };
Console.WriteLine(p3);        // Person { FirstName = Alice, LastName = Mensah, Age = 26 }
```
""",
            BestPractices = """
- Default to `private` for fields — expose only what's necessary through properties
- Use auto-properties for simple get/set; full properties when you need validation
- Use `init` setters for properties that should only be set during object creation
- Prefer records for immutable data transfer objects (DTOs) and value objects
- Never expose collection fields directly — return read-only views instead
- Use `required` keyword (C# 11+) to enforce mandatory properties at compile time
""",
            VoiceSummary = "Encapsulation means hiding internal data behind controlled access points. In C sharp, properties provide getter and setter methods wrapped in field-like syntax. Auto-properties let the compiler generate the backing field, while full properties give you validation control. Access modifiers like private, protected, internal, and public control visibility. Init-only and required properties enforce immutability and mandatory values at compile time.",
            Type = LessonType.Mixed,
            DurationMinutes = 35,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 11 — SOLID Principles
        // ══════════════════════════════

        new Lesson
        {
            Id = 21,
            CourseModuleId = 11,
            Title = "SOLID Design Principles in C#",
            Content = """
# SOLID Design Principles

SOLID is a set of five design principles that make software easier to understand, maintain, and extend.

## S — Single Responsibility Principle (SRP)

> A class should have only **one reason to change**.

```csharp
// ❌ BAD — one class doing everything
public class UserService
{
    public void Register(string email, string password) { /* ... */ }
    public void SendWelcomeEmail(string email) { /* ... */ }
    public void LogActivity(string action) { /* ... */ }
}

// ✅ GOOD — separated responsibilities
public class UserService
{
    public void Register(string email, string password) { /* ... */ }
}

public class EmailService
{
    public void SendWelcomeEmail(string email) { /* ... */ }
}

public class ActivityLogger
{
    public void LogActivity(string action) { /* ... */ }
}
```

## O — Open/Closed Principle (OCP)

> Classes should be **open for extension** but **closed for modification**.

```csharp
// ❌ BAD — must modify class to add new discount types
public class DiscountCalculator
{
    public decimal Calculate(string type, decimal price)
    {
        if (type == "Regular") return price * 0.1m;
        if (type == "Premium") return price * 0.2m;
        // Must edit this method for every new type!
        return 0;
    }
}

// ✅ GOOD — extend via new classes, never modify existing code
public interface IDiscount
{
    decimal Calculate(decimal price);
}

public class RegularDiscount : IDiscount
{
    public decimal Calculate(decimal price) => price * 0.1m;
}

public class PremiumDiscount : IDiscount
{
    public decimal Calculate(decimal price) => price * 0.2m;
}

// Just add a new class for a new discount type — no existing code changes
public class StudentDiscount : IDiscount
{
    public decimal Calculate(decimal price) => price * 0.15m;
}
```

## L — Liskov Substitution Principle (LSP)

> Objects of a base class should be **replaceable** with objects of derived classes **without breaking** the program.

```csharp
// ❌ BAD — Square breaks Rectangle's contract
public class Rectangle
{
    public virtual int Width  { get; set; }
    public virtual int Height { get; set; }
    public int Area() => Width * Height;
}

public class Square : Rectangle
{
    public override int Width
    {
        set { base.Width = value; base.Height = value; }
    }
    public override int Height
    {
        set { base.Width = value; base.Height = value; }
    }
}

// ✅ GOOD — use an abstraction instead
public interface IShape
{
    double Area();
}

public class Rectangle : IShape
{
    public int Width { get; set; }
    public int Height { get; set; }
    public double Area() => Width * Height;
}

public class Square : IShape
{
    public int Side { get; set; }
    public double Area() => Side * Side;
}
```

## I — Interface Segregation Principle (ISP)

> No client should be forced to depend on methods it **does not use**.

```csharp
// ❌ BAD — fat interface
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

// A robot worker doesn't eat or sleep!

// ✅ GOOD — small, specific interfaces
public interface IWorkable { void Work(); }
public interface IFeedable { void Eat(); }
public interface ISleepable { void Sleep(); }

public class Human : IWorkable, IFeedable, ISleepable
{
    public void Work()  { /* ... */ }
    public void Eat()   { /* ... */ }
    public void Sleep() { /* ... */ }
}

public class Robot : IWorkable
{
    public void Work() { /* ... */ }
}
```

## D — Dependency Inversion Principle (DIP)

> High-level modules should not depend on low-level modules. Both should depend on **abstractions**.

```csharp
// ❌ BAD — directly coupled to a specific database
public class OrderService
{
    private readonly SqlDatabase _db = new SqlDatabase();

    public void Save(Order order) => _db.Insert(order);
}

// ✅ GOOD — depends on an interface, not a concrete class
public interface IOrderRepository
{
    void Save(Order order);
    Order? GetById(int id);
}

public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order) { /* SQL logic */ }
    public Order? GetById(int id) { /* SQL logic */ return null; }
}

public class OrderService
{
    private readonly IOrderRepository _repo;

    // Inject the dependency — this is DIP + DI together
    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public void PlaceOrder(Order order) => _repo.Save(order);
}
```
""",
            BestPractices = """
- SRP: If you can't describe a class's purpose in one sentence, it's doing too much
- OCP: Use interfaces and polymorphism instead of switch/if-else chains on types
- LSP: A derived class must honour the promises (contract) of the base class
- ISP: Keep interfaces small and focused — prefer many specific interfaces over one large one
- DIP: Always inject dependencies via constructors — never create them with `new` inside a class
- Apply SOLID pragmatically — not every class needs all five. Start with SRP and DIP.
""",
            VoiceSummary = "SOLID is five design principles. Single Responsibility means each class has one job. Open-Closed means you extend classes through new code rather than modifying existing code. Liskov Substitution means derived classes must honour the base class contract. Interface Segregation means interfaces should be small and specific. Dependency Inversion means high-level code should depend on abstractions, not concrete implementations. Together they produce flexible, maintainable code.",
            Type = LessonType.Reading,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 14 — Entity Framework Core
        // ══════════════════════════════

        new Lesson
        {
            Id = 22,
            CourseModuleId = 14,
            Title = "Entity Framework Core — Database Access in C#",
            Content = """
# Entity Framework Core

**Entity Framework Core (EF Core)** is an Object-Relational Mapper (ORM) that lets you work with databases using C# objects instead of raw SQL.

## Setup

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## Define Entity Models

```csharp
public class Product
{
    public int Id { get; set; }         // Convention: Id = primary key
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; } // Foreign key

    // Navigation property
    public Category Category { get; set; } = null!;
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    // Collection navigation
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
```

## Create the DbContext

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Price).HasPrecision(18, 2);
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
```

## Migrations

```bash
# Create a migration
dotnet ef migrations add InitialCreate

# Apply to database
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove
```

## CRUD Operations

### Create

```csharp
var category = new Category { Name = "Electronics" };
context.Categories.Add(category);
await context.SaveChangesAsync();

var product = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    CategoryId = category.Id
};
context.Products.Add(product);
await context.SaveChangesAsync();
```

### Read

```csharp
// Get all
var allProducts = await context.Products.ToListAsync();

// Get by ID
var product = await context.Products.FindAsync(1);

// Query with filtering
var expensive = await context.Products
    .Where(p => p.Price > 500)
    .OrderBy(p => p.Name)
    .ToListAsync();

// Include related data (eager loading)
var productsWithCategory = await context.Products
    .Include(p => p.Category)
    .ToListAsync();

// Projection
var summary = await context.Products
    .Select(p => new { p.Name, p.Price, Category = p.Category.Name })
    .ToListAsync();
```

### Update

```csharp
var product = await context.Products.FindAsync(1);
if (product is not null)
{
    product.Price = 899.99m;
    await context.SaveChangesAsync();
}
```

### Delete

```csharp
var product = await context.Products.FindAsync(1);
if (product is not null)
{
    context.Products.Remove(product);
    await context.SaveChangesAsync();
}
```

## Relationships

```csharp
// One-to-Many: A Category has many Products
modelBuilder.Entity<Product>()
    .HasOne(p => p.Category)
    .WithMany(c => c.Products)
    .HasForeignKey(p => p.CategoryId);

// Many-to-Many (EF Core 5+):
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public ICollection<Student> Students { get; set; } = new List<Student>();
}

// EF Core auto-creates the join table!
```
""",
            BestPractices = """
- Always use `async` versions of EF Core methods (FindAsync, ToListAsync, SaveChangesAsync)
- Use migrations for every schema change — never modify the database manually in production
- Use `.Include()` for eager loading when you need related data to avoid N+1 queries
- Use projections with `.Select()` to retrieve only the columns you need
- Keep your DbContext scoped (one per HTTP request) — ASP.NET Core does this by default with `AddDbContext`
- Use Fluent API in `OnModelCreating` for complex configuration; Data Annotations for simple rules
""",
            VoiceSummary = "Entity Framework Core is an ORM that maps C sharp classes to database tables. You define entity models as plain classes with properties. The DbContext class manages database connections and exposes DbSet properties for each table. Use migrations to version your database schema. CRUD operations use Add, Find, Where, Remove, and SaveChangesAsync. Include handles eager loading of related entities.",
            Type = LessonType.Mixed,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 15 — Authentication & Security
        // ══════════════════════════════

        new Lesson
        {
            Id = 23,
            CourseModuleId = 15,
            Title = "JWT Authentication & API Security",
            Content = """
# JWT Authentication & API Security

## What is JWT?

**JSON Web Token (JWT)** is a compact, URL-safe token format used for securely transmitting claims between parties. A JWT has three parts separated by dots:

```
header.payload.signature
```

```
eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0In0.signature
```

- **Header** — algorithm and token type
- **Payload** — claims (user ID, role, expiry)
- **Signature** — ensures the token hasn't been tampered with

## Setting Up JWT in ASP.NET Core

### Install Package

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### Configure in Program.cs

```csharp
var key = builder.Configuration["Jwt:Key"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourApp",
            ValidAudience = "YourApp",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key))
        };
    });

// Don't forget the middleware ORDER matters:
app.UseAuthentication();   // Must come before
app.UseAuthorization();    // Must come after
```

### Generate Tokens

```csharp
public class TokenService
{
    private readonly string _key;

    public TokenService(IConfiguration config)
    {
        _key = config["Jwt:Key"]!;
    }

    public string GenerateToken(User user, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourApp",
            audience: "YourApp",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

## Protecting Endpoints

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    // Any authenticated user
    [HttpGet]
    [Authorize]
    public IActionResult GetMyOrders()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(new { UserId = userId });
    }

    // Only users with Admin role
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteOrder(int id)
    {
        return NoContent();
    }

    // Public endpoint — no authentication needed
    [HttpGet("public")]
    [AllowAnonymous]
    public IActionResult GetPublicInfo()
    {
        return Ok("Anyone can see this.");
    }
}
```

## Input Validation

### Data Annotations

```csharp
public class RegisterRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = "";

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FirstName { get; set; } = "";
}
```

### FluentValidation (Preferred for Complex Rules)

```csharp
// dotnet add package FluentValidation.AspNetCore

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Must contain an uppercase letter")
            .Matches("[0-9]").WithMessage("Must contain a digit");
    }
}
```

## Security Best Practices

```csharp
// 1. Never store secrets in code — use environment variables
var key = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

// 2. Hash passwords — never store plain text
var hashedPassword = BCrypt.Net.BCrypt.HashPassword("myPassword");
bool isValid = BCrypt.Net.BCrypt.Verify("myPassword", hashedPassword);

// 3. Use HTTPS in production
app.UseHttpsRedirection();

// 4. CORS — restrict which domains can call your API
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
```
""",
            BestPractices = """
- Never store JWT keys or secrets in source code — use environment variables or a secrets manager
- Always validate and sanitize all user input — never trust data from the client
- Set reasonable token expiry times (1-4 hours) and implement refresh token rotation
- Use HTTPS in production — never transmit tokens or credentials over HTTP
- Use [Authorize] by default and [AllowAnonymous] only on specific public endpoints
- Apply the principle of least privilege — grant only the minimum access needed for each role
""",
            VoiceSummary = "JWT authentication uses JSON Web Tokens to securely identify users without server-side sessions. The token contains claims like user ID and role, signed with a secret key. Configure JWT in Program dot cs with AddAuthentication and AddJwtBearer. Use the Authorize attribute to protect endpoints and the Roles parameter for role-based access. Always validate input with Data Annotations or FluentValidation, and never store secrets in code.",
            Type = LessonType.Mixed,
            DurationMinutes = 40,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 17 — Stacks & Queues
        // ══════════════════════════════

        new Lesson
        {
            Id = 24,
            CourseModuleId = 17,
            Title = "Stacks & Queues — LIFO and FIFO Data Structures",
            Content = """
# Stacks & Queues

## Stack — Last In, First Out (LIFO)

A **stack** is like a stack of plates — you add to the top and remove from the top. The last item added is the first removed.

### Using .NET's Stack<T>

```csharp
var stack = new Stack<string>();

// Push — add to top
stack.Push("First");
stack.Push("Second");
stack.Push("Third");

Console.WriteLine(stack.Peek());   // Third (look without removing)
Console.WriteLine(stack.Pop());    // Third (remove from top)
Console.WriteLine(stack.Pop());    // Second
Console.WriteLine(stack.Count);    // 1
```

### Implementing a Stack from Scratch

```csharp
public class MyStack<T>
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;
    public bool IsEmpty => _items.Count == 0;

    public void Push(T item) => _items.Add(item);

    public T Pop()
    {
        if (IsEmpty) throw new InvalidOperationException("Stack is empty.");
        var item = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Stack is empty.");
        return _items[^1];
    }
}
```

### Stack Applications

```csharp
// 1. Balanced Parentheses Checker
bool IsBalanced(string s)
{
    var stack = new Stack<char>();
    var pairs = new Dictionary<char, char>
    {
        { ')', '(' }, { ']', '[' }, { '}', '{' }
    };

    foreach (char c in s)
    {
        if ("([{".Contains(c))
            stack.Push(c);
        else if (")]}".Contains(c))
        {
            if (stack.Count == 0 || stack.Pop() != pairs[c])
                return false;
        }
    }
    return stack.Count == 0;
}

Console.WriteLine(IsBalanced("({[]})")); // True
Console.WriteLine(IsBalanced("({[})"));  // False

// 2. Reverse a String
string Reverse(string s)
{
    var stack = new Stack<char>(s);
    return new string(stack.ToArray());
}

// 3. Undo/Redo System
var undoStack = new Stack<string>();
var redoStack = new Stack<string>();
undoStack.Push("Type A");
undoStack.Push("Type B");
redoStack.Push(undoStack.Pop()); // Undo "Type B"
```

## Queue — First In, First Out (FIFO)

A **queue** is like a queue at a shop — first person in line is served first.

### Using .NET's Queue<T>

```csharp
var queue = new Queue<string>();

// Enqueue — add to back
queue.Enqueue("Alice");
queue.Enqueue("Bob");
queue.Enqueue("Charlie");

Console.WriteLine(queue.Peek());     // Alice (look without removing)
Console.WriteLine(queue.Dequeue());  // Alice (remove from front)
Console.WriteLine(queue.Dequeue());  // Bob
Console.WriteLine(queue.Count);      // 1
```

### Implementing a Queue from Scratch

```csharp
public class MyQueue<T>
{
    private readonly LinkedList<T> _items = new();

    public int Count => _items.Count;
    public bool IsEmpty => _items.Count == 0;

    public void Enqueue(T item) => _items.AddLast(item);

    public T Dequeue()
    {
        if (IsEmpty) throw new InvalidOperationException("Queue is empty.");
        var item = _items.First!.Value;
        _items.RemoveFirst();
        return item;
    }

    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Queue is empty.");
        return _items.First!.Value;
    }
}
```

### Queue Applications

```csharp
// 1. Print Queue
var printQueue = new Queue<string>();
printQueue.Enqueue("Document A");
printQueue.Enqueue("Document B");
printQueue.Enqueue("Document C");

while (printQueue.Count > 0)
    Console.WriteLine($"Printing: {printQueue.Dequeue()}");

// 2. BFS (Breadth-First Search) uses a queue
// 3. Task schedulers, message queues, buffering
```

## Complexity

| Operation | Stack | Queue |
|---|---|---|
| Push / Enqueue | O(1) | O(1) |
| Pop / Dequeue | O(1) | O(1) |
| Peek | O(1) | O(1) |
| Search | O(n) | O(n) |
""",
            BestPractices = """
- Use `Stack<T>` for LIFO behaviour — function call tracking, undo systems, expression parsing
- Use `Queue<T>` for FIFO behaviour — task scheduling, BFS, message processing
- Always check `Count > 0` or use `TryPeek`/`TryPop` before accessing elements to avoid exceptions
- Prefer .NET's built-in Stack<T> and Queue<T> unless you need custom behaviour
- Remember: stacks are ideal for recursion simulation; queues for level-order processing
""",
            VoiceSummary = "A stack is a last-in-first-out data structure with push, pop, and peek operations, all running in constant time. A queue is first-in-first-out with enqueue, dequeue, and peek. Use stacks for undo systems, balanced bracket checking, and evaluating expressions. Use queues for task scheduling, breadth-first search, and message processing. Both are available as generic types in dot NET.",
            Type = LessonType.Practice,
            DurationMinutes = 35,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 18 — Sorting Algorithms
        // ══════════════════════════════

        new Lesson
        {
            Id = 25,
            CourseModuleId = 18,
            Title = "Sorting Algorithms — From Bubble Sort to Merge Sort",
            Content = """
# Sorting Algorithms

## Why Study Sorting?

Sorting is one of the most fundamental operations in computer science. Understanding sorting algorithms teaches you algorithm design, complexity analysis, and trade-offs between time and space.

## Bubble Sort — O(n²)

The simplest sorting algorithm. Repeatedly swap adjacent elements if they're in the wrong order.

```csharp
void BubbleSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        bool swapped = false;
        for (int j = 0; j < n - 1 - i; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]); // Tuple swap
                swapped = true;
            }
        }
        if (!swapped) break; // Already sorted — early exit
    }
}
```

## Selection Sort — O(n²)

Find the minimum element in the unsorted portion and swap it to the front.

```csharp
void SelectionSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        int minIndex = i;
        for (int j = i + 1; j < n; j++)
        {
            if (arr[j] < arr[minIndex])
                minIndex = j;
        }
        if (minIndex != i)
            (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
    }
}
```

## Insertion Sort — O(n²) worst, O(n) best

Builds the sorted array one element at a time by inserting each into its correct position.

```csharp
void InsertionSort(int[] arr)
{
    for (int i = 1; i < arr.Length; i++)
    {
        int key = arr[i];
        int j = i - 1;

        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}
```

> **Insertion sort is fast for small or nearly-sorted arrays.** Many real-world sorting libraries use it for small partitions.

## Merge Sort — O(n log n)

Divide-and-conquer: recursively split the array in half, sort each half, then merge.

```csharp
void MergeSort(int[] arr, int left, int right)
{
    if (left >= right) return;

    int mid = left + (right - left) / 2;

    MergeSort(arr, left, mid);
    MergeSort(arr, mid + 1, right);
    Merge(arr, left, mid, right);
}

void Merge(int[] arr, int left, int mid, int right)
{
    int[] leftArr  = arr[left..(mid + 1)];
    int[] rightArr = arr[(mid + 1)..(right + 1)];

    int i = 0, j = 0, k = left;

    while (i < leftArr.Length && j < rightArr.Length)
    {
        arr[k++] = leftArr[i] <= rightArr[j]
            ? leftArr[i++]
            : rightArr[j++];
    }

    while (i < leftArr.Length) arr[k++] = leftArr[i++];
    while (j < rightArr.Length) arr[k++] = rightArr[j++];
}

// Usage:
int[] data = { 38, 27, 43, 3, 9, 82, 10 };
MergeSort(data, 0, data.Length - 1);
// Result: 3, 9, 10, 27, 38, 43, 82
```

## Quick Sort — O(n log n) average, O(n²) worst

Choose a pivot, partition elements around it, then sort each partition.

```csharp
void QuickSort(int[] arr, int low, int high)
{
    if (low >= high) return;

    int pivot = Partition(arr, low, high);
    QuickSort(arr, low, pivot - 1);
    QuickSort(arr, pivot + 1, high);
}

int Partition(int[] arr, int low, int high)
{
    int pivot = arr[high];
    int i = low - 1;

    for (int j = low; j < high; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
    (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
    return i + 1;
}
```

## Comparison Table

| Algorithm | Best | Average | Worst | Space | Stable? |
|---|---|---|---|---|---|
| Bubble Sort | O(n) | O(n²) | O(n²) | O(1) | Yes |
| Selection Sort | O(n²) | O(n²) | O(n²) | O(1) | No |
| Insertion Sort | O(n) | O(n²) | O(n²) | O(1) | Yes |
| Merge Sort | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes |
| Quick Sort | O(n log n) | O(n log n) | O(n²) | O(log n) | No |
""",
            BestPractices = """
- Use `Array.Sort()` or `List.Sort()` in production — they use an optimised IntroSort (Quick + Heap + Insertion)
- Merge Sort guarantees O(n log n) and is stable — ideal when stability matters
- Quick Sort is generally faster in practice due to cache efficiency, despite O(n²) worst case
- Use Insertion Sort for small arrays (< 20 elements) — the constant factor is very low
- Always analyse both time AND space complexity when choosing an algorithm
- A 'stable' sort preserves the relative order of equal elements — important for multi-key sorting
""",
            VoiceSummary = "Bubble sort, selection sort, and insertion sort are simple O of n squared algorithms good for learning. Merge sort uses divide-and-conquer to achieve O of n log n time guaranteed, at the cost of O of n extra space. Quick sort averages O of n log n and is fast in practice but can degrade to O of n squared on bad inputs. In production, use Array dot Sort which combines the best of quick sort, heap sort, and insertion sort.",
            Type = LessonType.Practice,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 19 — Trees & BSTs
        // ══════════════════════════════

        new Lesson
        {
            Id = 26,
            CourseModuleId = 19,
            Title = "Binary Trees & Binary Search Trees",
            Content = """
# Binary Trees & Binary Search Trees

## Tree Terminology

- **Node** — an element in the tree containing data
- **Root** — the topmost node (no parent)
- **Children** — nodes directly below a node
- **Leaf** — a node with no children
- **Height** — the longest path from root to a leaf
- **Depth** — the distance from the root to a node

## Binary Tree — Each node has at most 2 children

```csharp
public class TreeNode<T>
{
    public T Value { get; set; }
    public TreeNode<T>? Left { get; set; }
    public TreeNode<T>? Right { get; set; }

    public TreeNode(T value)
    {
        Value = value;
    }
}
```

## Tree Traversals

```csharp
public class BinaryTree<T>
{
    public TreeNode<T>? Root { get; set; }

    // In-order: Left → Root → Right (gives sorted order for BST)
    public void InOrder(TreeNode<T>? node)
    {
        if (node is null) return;
        InOrder(node.Left);
        Console.Write(node.Value + " ");
        InOrder(node.Right);
    }

    // Pre-order: Root → Left → Right (useful for copying/serialising)
    public void PreOrder(TreeNode<T>? node)
    {
        if (node is null) return;
        Console.Write(node.Value + " ");
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    // Post-order: Left → Right → Root (useful for deletion)
    public void PostOrder(TreeNode<T>? node)
    {
        if (node is null) return;
        PostOrder(node.Left);
        PostOrder(node.Right);
        Console.Write(node.Value + " ");
    }

    // Level-order (BFS): Level by level using a queue
    public void LevelOrder()
    {
        if (Root is null) return;
        var queue = new Queue<TreeNode<T>>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            Console.Write(node.Value + " ");
            if (node.Left is not null) queue.Enqueue(node.Left);
            if (node.Right is not null) queue.Enqueue(node.Right);
        }
    }
}
```

## Binary Search Tree (BST)

A BST enforces an ordering rule: for every node, all values in the left subtree are less, and all values in the right subtree are greater.

```csharp
public class BinarySearchTree
{
    private TreeNode<int>? _root;

    // INSERT
    public void Insert(int value)
    {
        _root = Insert(_root, value);
    }

    private TreeNode<int> Insert(TreeNode<int>? node, int value)
    {
        if (node is null) return new TreeNode<int>(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
        // Duplicate values are ignored

        return node;
    }

    // SEARCH
    public bool Search(int value)
    {
        return Search(_root, value);
    }

    private bool Search(TreeNode<int>? node, int value)
    {
        if (node is null) return false;
        if (value == node.Value) return true;
        return value < node.Value
            ? Search(node.Left, value)
            : Search(node.Right, value);
    }

    // FIND MIN / MAX
    public int FindMin()
    {
        var node = _root ?? throw new InvalidOperationException("Tree is empty.");
        while (node.Left is not null) node = node.Left;
        return node.Value;
    }

    public int FindMax()
    {
        var node = _root ?? throw new InvalidOperationException("Tree is empty.");
        while (node.Right is not null) node = node.Right;
        return node.Value;
    }

    // HEIGHT
    public int Height(TreeNode<int>? node)
    {
        if (node is null) return -1;
        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }
}

// Usage:
var bst = new BinarySearchTree();
int[] values = { 50, 30, 70, 20, 40, 60, 80 };
foreach (int v in values) bst.Insert(v);

//        50
//       /  \
//     30    70
//    / \   / \
//  20  40 60  80

Console.WriteLine(bst.Search(40));  // True
Console.WriteLine(bst.Search(45));  // False
Console.WriteLine(bst.FindMin());   // 20
Console.WriteLine(bst.FindMax());   // 80
```

## BST Complexity

| Operation | Average | Worst (unbalanced) |
|---|---|---|
| Search | O(log n) | O(n) |
| Insert | O(log n) | O(n) |
| Delete | O(log n) | O(n) |
| Traversal | O(n) | O(n) |

> When a BST becomes unbalanced (like a linked list), performance degrades to O(n). Self-balancing trees (AVL, Red-Black) maintain O(log n) by rebalancing after insertions and deletions.
""",
            BestPractices = """
- Always start tree algorithms recursively — iterative versions are optimisations for later
- Use In-Order traversal to get BST elements in sorted order
- Use Level-Order (BFS) traversal when you need to process nodes level by level
- Be aware that unbalanced BSTs degrade to O(n) — consider SortedSet<T> in .NET which uses a balanced tree
- .NET's SortedDictionary<TKey, TValue> and SortedSet<T> use Red-Black Trees internally
- Practice tree problems recursively — the base case is always `if (node is null) return`
""",
            VoiceSummary = "A binary tree is a hierarchical data structure where each node has at most two children. Tree traversals include in-order (left, root, right), pre-order (root, left, right), post-order (left, right, root), and level-order using a queue. A binary search tree enforces ordering so that left children are smaller and right children are larger. BST operations average O of log n time, but degrade to O of n when unbalanced.",
            Type = LessonType.Practice,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        },

        // ══════════════════════════════
        // MODULE 22 — Advanced LINQ
        // ══════════════════════════════

        new Lesson
        {
            Id = 27,
            CourseModuleId = 22,
            Title = "Advanced LINQ — Joins, Grouping & Aggregation",
            Content = """
# Advanced LINQ

## Setup Data

```csharp
public record Student(int Id, string Name, string Department);
public record Grade(int StudentId, string Subject, int Score);

var students = new List<Student>
{
    new(1, "Alice",   "CS"),
    new(2, "Bob",     "CS"),
    new(3, "Charlie", "Math"),
    new(4, "Diana",   "Math"),
    new(5, "Eve",     "CS")
};

var grades = new List<Grade>
{
    new(1, "C#",       95), new(1, "Algorithms", 88),
    new(2, "C#",       72), new(2, "Algorithms", 80),
    new(3, "Calculus",  90), new(3, "Algebra",   85),
    new(4, "Calculus",  78), new(5, "C#",        91)
};
```

## Inner Join

```csharp
// Method syntax
var results = students
    .Join(grades,
          s => s.Id,          // outer key
          g => g.StudentId,   // inner key
          (s, g) => new { s.Name, g.Subject, g.Score });

foreach (var r in results)
    Console.WriteLine($"{r.Name}: {r.Subject} = {r.Score}");

// Query syntax
var results2 =
    from s in students
    join g in grades on s.Id equals g.StudentId
    select new { s.Name, g.Subject, g.Score };
```

## Group Join (Left Join)

```csharp
// Students with ALL their grades (even students with no grades)
var withGrades = students
    .GroupJoin(grades,
              s => s.Id,
              g => g.StudentId,
              (student, studentGrades) => new
              {
                  student.Name,
                  Grades = studentGrades.ToList(),
                  Average = studentGrades.Any()
                      ? studentGrades.Average(g => g.Score)
                      : 0
              });

foreach (var s in withGrades)
    Console.WriteLine($"{s.Name}: avg = {s.Average:F1} ({s.Grades.Count} grades)");
```

## GroupBy

```csharp
// Group students by department
var departments = students
    .GroupBy(s => s.Department)
    .Select(g => new
    {
        Department = g.Key,
        Count = g.Count(),
        Students = g.Select(s => s.Name).ToList()
    });

foreach (var dept in departments)
    Console.WriteLine($"{dept.Department}: {string.Join(", ", dept.Students)}");
// CS: Alice, Bob, Eve
// Math: Charlie, Diana

// Group with aggregation
var deptStats = students
    .GroupJoin(grades, s => s.Id, g => g.StudentId, (s, gs) => new { s, gs })
    .SelectMany(x => x.gs.DefaultIfEmpty(), (x, g) => new { x.s.Department, Score = g?.Score ?? 0 })
    .GroupBy(x => x.Department)
    .Select(g => new
    {
        Department = g.Key,
        AvgScore = g.Average(x => x.Score),
        TopScore = g.Max(x => x.Score)
    });
```

## Aggregation Methods

```csharp
var scores = new[] { 95, 88, 72, 80, 90, 85, 78, 91 };

Console.WriteLine(scores.Sum());         // 679
Console.WriteLine(scores.Average());     // 84.875
Console.WriteLine(scores.Min());         // 72
Console.WriteLine(scores.Max());         // 95
Console.WriteLine(scores.Count());       // 8

// Aggregate — custom reduction
string csv = scores.Aggregate("", (acc, s) => acc + (acc == "" ? "" : ",") + s);
// "95,88,72,80,90,85,78,91"

int product = new[] { 2, 3, 4 }.Aggregate(1, (acc, n) => acc * n);
// 1 * 2 * 3 * 4 = 24
```

## SelectMany — Flatten Nested Collections

```csharp
var departments = new[]
{
    new { Name = "CS", Courses = new[] { "C#", "Algorithms", "Databases" } },
    new { Name = "Math", Courses = new[] { "Calculus", "Algebra" } }
};

// Flatten all courses into a single list
var allCourses = departments.SelectMany(d => d.Courses);
// ["C#", "Algorithms", "Databases", "Calculus", "Algebra"]

// With source reference
var detailed = departments
    .SelectMany(d => d.Courses, (dept, course) => new { dept.Name, Course = course });
// { CS, C# }, { CS, Algorithms }, { CS, Databases }, { Math, Calculus }, { Math, Algebra }
```

## Zip

```csharp
var names  = new[] { "Alice", "Bob", "Charlie" };
var scores2 = new[] { 95, 82, 88 };

var pairs = names.Zip(scores2, (name, score) => $"{name}: {score}");
// ["Alice: 95", "Bob: 82", "Charlie: 88"]
```

## Set Operations

```csharp
var a = new[] { 1, 2, 3, 4, 5 };
var b = new[] { 3, 4, 5, 6, 7 };

var union     = a.Union(b);         // { 1, 2, 3, 4, 5, 6, 7 }
var intersect = a.Intersect(b);     // { 3, 4, 5 }
var except    = a.Except(b);        // { 1, 2 }
var distinct  = new[] { 1, 1, 2, 2, 3 }.Distinct(); // { 1, 2, 3 }
```
""",
            BestPractices = """
- Use `Join` for inner joins and `GroupJoin` for left outer joins
- Always materialise GroupBy results with `ToList()` before re-enumerating to avoid multiple executions
- Use `SelectMany` to flatten nested collections — it's the LINQ equivalent of nested foreach loops
- Prefer `Aggregate` only for custom reductions — use `Sum`, `Min`, `Max`, `Average` for standard aggregates
- Use set operations (`Union`, `Intersect`, `Except`) instead of manual loops for set logic
- Remember that most LINQ operators use deferred execution — force with `ToList()` when you need a snapshot
""",
            VoiceSummary = "Advanced LINQ includes inner joins with Join, left outer joins with GroupJoin, and grouping with GroupBy. Aggregation methods like Sum, Average, Min, Max, and Count reduce collections to single values. SelectMany flattens nested collections. Zip combines two sequences element by element. Set operations like Union, Intersect, and Except perform mathematical set logic on collections. These operators let you write complex data transformations declaratively.",
            Type = LessonType.Practice,
            DurationMinutes = 45,
            Order = 1,
            CreatedAt = d
        }
    );

    // ───────────────────────────────────────────────
    // BADGES
    // ───────────────────────────────────────────────
    private static void SeedBadges(ModelBuilder mb, DateTime d) => mb.Entity<Badge>().HasData(
        new Badge { Id = 1,  Name = "First Steps",         Description = "Complete your first lesson",              ImageUrl = "/badges/first-steps.svg",      CreatedAt = d },
        new Badge { Id = 2,  Name = "On Fire",             Description = "Maintain a 3-day learning streak",        ImageUrl = "/badges/fire.svg",             CreatedAt = d },
        new Badge { Id = 3,  Name = "Unstoppable",         Description = "Maintain a 7-day learning streak",        ImageUrl = "/badges/streak-7.svg",         CreatedAt = d },
        new Badge { Id = 4,  Name = "Quick Learner",       Description = "Complete 3 lessons",                      ImageUrl = "/badges/quick-learner.svg",    CreatedAt = d },
        new Badge { Id = 5,  Name = "Dedicated Student",   Description = "Complete 10 lessons",                     ImageUrl = "/badges/dedicated.svg",        CreatedAt = d },
        new Badge { Id = 6,  Name = "Quiz Whiz",           Description = "Pass your first quiz with 80%+",          ImageUrl = "/badges/quiz-whiz.svg",        CreatedAt = d },
        new Badge { Id = 7,  Name = "Perfect Score",       Description = "Score 100% on any quiz",                  ImageUrl = "/badges/perfect.svg",          CreatedAt = d },
        new Badge { Id = 8,  Name = "Code Newbie",         Description = "Submit your first coding exercise",       ImageUrl = "/badges/code-newbie.svg",      CreatedAt = d },
        new Badge { Id = 9,  Name = "Problem Solver",      Description = "Solve 5 coding challenges",               ImageUrl = "/badges/problem-solver.svg",   CreatedAt = d },
        new Badge { Id = 10, Name = "Challenge Master",    Description = "Solve 15 coding challenges",              ImageUrl = "/badges/challenge-master.svg", CreatedAt = d },
        new Badge { Id = 11, Name = "C# Graduate",         Description = "Complete the C# Fundamentals course",     ImageUrl = "/badges/csharp-grad.svg",      CreatedAt = d },
        new Badge { Id = 12, Name = "OOP Pro",             Description = "Complete the OOP course",                 ImageUrl = "/badges/oop-pro.svg",          CreatedAt = d },
        new Badge { Id = 13, Name = "API Builder",         Description = "Complete the ASP.NET Core course",        ImageUrl = "/badges/api-builder.svg",      CreatedAt = d },
        new Badge { Id = 14, Name = "Algorithm Ace",       Description = "Complete the Data Structures course",     ImageUrl = "/badges/algo-ace.svg",         CreatedAt = d },
        new Badge { Id = 15, Name = "LINQ Ninja",          Description = "Complete the LINQ & Functional course",   ImageUrl = "/badges/linq-ninja.svg",       CreatedAt = d },
        new Badge { Id = 16, Name = "XP Hunter",           Description = "Earn 500 XP total",                       ImageUrl = "/badges/xp-500.svg",           CreatedAt = d },
        new Badge { Id = 17, Name = "XP Legend",           Description = "Earn 2000 XP total",                      ImageUrl = "/badges/xp-2000.svg",          CreatedAt = d }
    );

    // ───────────────────────────────────────────────
    // QUIZZES
    // ───────────────────────────────────────────────
    private static void SeedQuizzes(ModelBuilder mb, DateTime d) => mb.Entity<Quiz>().HasData(
        new Quiz { Id = 1,  LessonId = 1,  Title = "C# & .NET Basics Quiz",            CreatedAt = d },
        new Quiz { Id = 2,  LessonId = 3,  Title = "Hello World & Console I/O Quiz",   CreatedAt = d },
        new Quiz { Id = 3,  LessonId = 4,  Title = "Variables & Data Types Quiz",      CreatedAt = d },
        new Quiz { Id = 4,  LessonId = 5,  Title = "Operators Quiz",                   CreatedAt = d },
        new Quiz { Id = 5,  LessonId = 6,  Title = "Control Flow Quiz",                CreatedAt = d },
        new Quiz { Id = 6,  LessonId = 7,  Title = "Loops Quiz",                       CreatedAt = d },
        new Quiz { Id = 7,  LessonId = 8,  Title = "Methods Quiz",                     CreatedAt = d },
        new Quiz { Id = 8,  LessonId = 9,  Title = "Arrays Quiz",                      CreatedAt = d },
        new Quiz { Id = 9,  LessonId = 10, Title = "Collections Quiz",                 CreatedAt = d },
        new Quiz { Id = 10, LessonId = 11, Title = "String Manipulation Quiz",         CreatedAt = d },
        new Quiz { Id = 11, LessonId = 12, Title = "OOP Classes & Constructors Quiz",  CreatedAt = d },
        new Quiz { Id = 12, LessonId = 13, Title = "Inheritance & Polymorphism Quiz",  CreatedAt = d },
        new Quiz { Id = 13, LessonId = 14, Title = "Interfaces & Abstract Classes Quiz", CreatedAt = d },
        new Quiz { Id = 14, LessonId = 19, Title = "LINQ Essentials Quiz",             CreatedAt = d },
        new Quiz { Id = 15, LessonId = 20, Title = "Encapsulation & Properties Quiz",  CreatedAt = d },
        new Quiz { Id = 16, LessonId = 21, Title = "SOLID Principles Quiz",             CreatedAt = d },
        new Quiz { Id = 17, LessonId = 22, Title = "Entity Framework Core Quiz",       CreatedAt = d },
        new Quiz { Id = 18, LessonId = 23, Title = "Authentication & Security Quiz",   CreatedAt = d },
        new Quiz { Id = 19, LessonId = 24, Title = "Stacks & Queues Quiz",             CreatedAt = d },
        new Quiz { Id = 20, LessonId = 25, Title = "Sorting Algorithms Quiz",          CreatedAt = d },
        new Quiz { Id = 21, LessonId = 26, Title = "Trees & BSTs Quiz",                CreatedAt = d },
        new Quiz { Id = 22, LessonId = 27, Title = "Advanced LINQ Quiz",               CreatedAt = d }
    );

    // ───────────────────────────────────────────────
    // QUESTIONS & OPTIONS
    // ───────────────────────────────────────────────
    private static void SeedQuestions(ModelBuilder mb, DateTime d)
    {
        mb.Entity<Question>().HasData(

            // ── Quiz 1: C# & .NET Basics ──
            new Question { Id = 1,  QuizId = 1,  Text = "Which company developed C#?",                                                   Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 2,  QuizId = 1,  Text = "C# is a statically-typed language.",                                            Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 3,  QuizId = 1,  Text = "What does CLR stand for?",                                                      Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 4,  QuizId = 1,  Text = "The file extension for C# source files is ___",                                 Type = QuestionType.FillInTheBlank, CorrectAnswer = ".cs", CreatedAt = d },
            new Question { Id = 5,  QuizId = 1,  Text = "Which command runs a .NET console application from the terminal?",              Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 2: Hello World & Console I/O ──
            new Question { Id = 6,  QuizId = 2,  Text = "What is the output of: Console.WriteLine(2 + 3);",                             Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 7,  QuizId = 2,  Text = "Console.Write() adds a newline after the output.",                             Type = QuestionType.TrueFalse,       CreatedAt = d },
            new Question { Id = 8,  QuizId = 2,  Text = "Which symbol starts a string interpolation expression in C#?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 9,  QuizId = 2,  Text = "Which escape sequence represents a tab character?",                            Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 3: Variables & Data Types ──
            new Question { Id = 10, QuizId = 3,  Text = "Which data type should be used for currency/money calculations?",              Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 11, QuizId = 3,  Text = "var is a dynamically-typed keyword in C#.",                                    Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 12, QuizId = 3,  Text = "What is the default value of an uninitialized int variable?",                  Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 13, QuizId = 3,  Text = "To make an int nullable, you write int___",                                    Type = QuestionType.FillInTheBlank, CorrectAnswer = "?", CreatedAt = d },
            new Question { Id = 14, QuizId = 3,  Text = "Which keyword declares a compile-time constant?",                              Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 4: Operators ──
            new Question { Id = 15, QuizId = 4,  Text = "What is the output of: Console.WriteLine(17 % 5);",                           Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 16, QuizId = 4,  Text = "What is the output of: Console.WriteLine(17 / 5);",                           Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 17, QuizId = 4,  Text = "The && operator short-circuits when the left operand is true.",                Type = QuestionType.TrueFalse,       CreatedAt = d },
            new Question { Id = 18, QuizId = 4,  Text = "Which operator returns the remainder of integer division?",                    Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 5: Control Flow ──
            new Question { Id = 19, QuizId = 5,  Text = "In a switch expression, what symbol represents the default arm?",              Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 20, QuizId = 5,  Text = "The ternary operator requires three operands.",                                Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 21, QuizId = 5,  Text = "Which operator returns a default value when the left side is null?",           Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 22, QuizId = 5,  Text = "What is the output of: Console.WriteLine(5 > 3 ? \"Yes\" : \"No\");",         Type = QuestionType.OutputPrediction, CreatedAt = d },

            // ── Quiz 6: Loops ──
            new Question { Id = 23, QuizId = 6,  Text = "Which loop is best suited for iterating over a collection when you don't need the index?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 24, QuizId = 6,  Text = "A do-while loop always executes its body at least once.",                     Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 25, QuizId = 6,  Text = "Which keyword skips the rest of the current loop iteration?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 26, QuizId = 6,  Text = "What is the output of: for(int i=0;i<3;i++) Console.Write(i);",              Type = QuestionType.OutputPrediction, CreatedAt = d },

            // ── Quiz 7: Methods ──
            new Question { Id = 27, QuizId = 7,  Text = "Which keyword is used to pass multiple arguments as an array parameter?",     Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 28, QuizId = 7,  Text = "Method overloading requires different return types.",                          Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 29, QuizId = 7,  Text = "What is the output of: Console.WriteLine(Factorial(4)); where Factorial is n <= 1 ? 1 : n * Factorial(n-1)?", Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 30, QuizId = 7,  Text = "A method with return type void ___ return a value.",                          Type = QuestionType.FillInTheBlank, CorrectAnswer = "cannot", CreatedAt = d },

            // ── Quiz 8: Arrays ──
            new Question { Id = 31, QuizId = 8,  Text = "Arrays in C# are zero-indexed.",                                              Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 32, QuizId = 8,  Text = "What index accesses the last element of array arr using C# 8+ syntax?",      Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 33, QuizId = 8,  Text = "Which method sorts an array in place?",                                        Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 34, QuizId = 8,  Text = "What is the output of: int[] a = {3,1,2}; Array.Sort(a); Console.Write(a[0]);", Type = QuestionType.OutputPrediction, CreatedAt = d },

            // ── Quiz 9: Collections ──
            new Question { Id = 35, QuizId = 9,  Text = "Which collection type provides O(1) average lookup by key?",                  Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 36, QuizId = 9,  Text = "List<T> has a fixed size that cannot change after creation.",                 Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 37, QuizId = 9,  Text = "Which method should you use for safe dictionary key access?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 38, QuizId = 9,  Text = "HashSet<T> allows ___ elements.",                                              Type = QuestionType.FillInTheBlank, CorrectAnswer = "unique", CreatedAt = d },

            // ── Quiz 10: Strings ──
            new Question { Id = 39, QuizId = 10, Text = "Strings in C# are immutable.",                                                Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 40, QuizId = 10, Text = "Which class should you use to build a string in a loop for performance?",    Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 41, QuizId = 10, Text = "What is the output of: Console.WriteLine(\"hello\".ToUpper());",             Type = QuestionType.OutputPrediction, CreatedAt = d },
            new Question { Id = 42, QuizId = 10, Text = "Which method splits a string into an array by a delimiter?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 11: OOP Classes ──
            new Question { Id = 43, QuizId = 11, Text = "A constructor is called when an object is created with the new keyword.",     Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 44, QuizId = 11, Text = "Which access modifier makes a member accessible only within the same class?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 45, QuizId = 11, Text = "Static members belong to the ___ not to instances.",                          Type = QuestionType.FillInTheBlank, CorrectAnswer = "class", CreatedAt = d },
            new Question { Id = 46, QuizId = 11, Text = "Which method should you override to provide a string representation?",       Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 12: Inheritance ──
            new Question { Id = 47, QuizId = 12, Text = "C# supports multiple class inheritance (inheriting from multiple base classes).", Type = QuestionType.TrueFalse,   CreatedAt = d },
            new Question { Id = 48, QuizId = 12, Text = "Which keyword is used to call the base class constructor?",                   Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 49, QuizId = 12, Text = "A sealed class cannot be ___.",                                               Type = QuestionType.FillInTheBlank, CorrectAnswer = "inherited", CreatedAt = d },
            new Question { Id = 50, QuizId = 12, Text = "Which keyword marks a method as overridable in a base class?",               Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 13: Interfaces ──
            new Question { Id = 51, QuizId = 13, Text = "A class can implement multiple interfaces.",                                  Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 52, QuizId = 13, Text = "What prefix does C# convention use for interface names?",                    Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 53, QuizId = 13, Text = "An abstract class ___ be instantiated directly.",                             Type = QuestionType.FillInTheBlank, CorrectAnswer = "cannot", CreatedAt = d },
            new Question { Id = 54, QuizId = 13, Text = "Which can have a constructor: abstract class or interface?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },

            // ── Quiz 14: LINQ ──
            new Question { Id = 55, QuizId = 14, Text = "LINQ queries use deferred execution by default.",                             Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 56, QuizId = 14, Text = "Which LINQ method filters elements based on a predicate?",                   Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 57, QuizId = 14, Text = "Which method forces immediate execution of a LINQ query into a List?",       Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 58, QuizId = 14, Text = "Which LINQ method is preferred over Count() > 0 for checking existence?",   Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 15: Encapsulation ──
            new Question { Id = 59, QuizId = 15, Text = "Which keyword restricts a member to be accessible only within its own class?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 60, QuizId = 15, Text = "In C#, properties are essentially syntax sugar for getter and setter methods.", Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 61, QuizId = 15, Text = "Which C# feature allows setting a property value only during object initialization?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 16: SOLID ──
            new Question { Id = 62, QuizId = 16, Text = "What does the 'S' in SOLID stand for?",                                        Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 63, QuizId = 16, Text = "The Open/Closed principle states classes should be open for modification but closed for extension.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 64, QuizId = 16, Text = "Dependency Inversion suggests high-level modules should depend on ___ not concrete implementations.", Type = QuestionType.FillInTheBlank, CorrectAnswer = "abstractions", CreatedAt = d },
            
            // ── Quiz 17: EF Core ──
            new Question { Id = 65, QuizId = 17, Text = "What is the primary class used to coordinate EF Core functionality for a given data model?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 66, QuizId = 17, Text = "NuGet package Microsoft.EntityFrameworkCore.Design is required to run migrations.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 67, QuizId = 17, Text = "Which method applies migrations to the database at runtime?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 18: Authentication ──
            new Question { Id = 68, QuizId = 18, Text = "How many parts does a JSON Web Token (JWT) consist of?",                       Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 69, QuizId = 18, Text = "The [Authorize] attribute can be applied to both controllers and individual actions.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 70, QuizId = 18, Text = "Passwords should be stored as plain text for easy recovery.",                  Type = QuestionType.TrueFalse,      CreatedAt = d },
            
            // ── Quiz 19: Stacks & Queues ──
            new Question { Id = 71, QuizId = 19, Text = "A Stack follows the FIFO (First-In-First-Out) principle.",                       Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 72, QuizId = 19, Text = "Which method adds an item to the top of a Stack?",                               Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 73, QuizId = 19, Text = "Which method removes and returns the item from the front of a Queue?",           Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 20: Sorting ──
            new Question { Id = 74, QuizId = 20, Text = "Which sorting algorithm has a O(n log n) best, average, and worst-case time complexity?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 75, QuizId = 20, Text = "Bubble sort is generally the most efficient sorting algorithm for large datasets.", Type = QuestionType.TrueFalse, CreatedAt = d },
            new Question { Id = 76, QuizId = 20, Text = "Which sorting algorithm uses a 'pivot' element?",                             Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 21: Trees ──
            new Question { Id = 77, QuizId = 21, Text = "In a Binary Search Tree, the left child is always greater than its parent.",      Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 78, QuizId = 21, Text = "Which traversal visits the root first, then left, then right?",                 Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 79, QuizId = 21, Text = "What is the average time complexity for searching in a balanced Binary Search Tree?", Type = QuestionType.MultipleChoice, CreatedAt = d },
            
            // ── Quiz 22: Advanced LINQ ──
            new Question { Id = 80, QuizId = 22, Text = "Which LINQ method flattens a sequence of sequences into a single sequence?",   Type = QuestionType.MultipleChoice, CreatedAt = d },
            new Question { Id = 81, QuizId = 22, Text = "The GroupJoin operator is equivalent to a SQL Left Outer Join.",                 Type = QuestionType.TrueFalse,      CreatedAt = d },
            new Question { Id = 82, QuizId = 22, Text = "Which method merges two sequences by using a selector function to pair elements?", Type = QuestionType.MultipleChoice, CreatedAt = d }
        );

        mb.Entity<QuestionOption>().HasData(

            // Q1: Who developed C#?
            new QuestionOption { Id = 1,   QuestionId = 1,  Text = "Microsoft",           IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 2,   QuestionId = 1,  Text = "Google",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 3,   QuestionId = 1,  Text = "Oracle",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 4,   QuestionId = 1,  Text = "Apple",               IsCorrect = false, CreatedAt = d },

            // Q2: C# is statically-typed
            new QuestionOption { Id = 5,   QuestionId = 2,  Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 6,   QuestionId = 2,  Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q3: CLR stands for?
            new QuestionOption { Id = 7,   QuestionId = 3,  Text = "Common Language Runtime",    IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 8,   QuestionId = 3,  Text = "Common Logic Runtime",       IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 9,   QuestionId = 3,  Text = "Compiled Language Resource", IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 10,  QuestionId = 3,  Text = "Core Library Runtime",       IsCorrect = false, CreatedAt = d },

            // Q5: Which command runs the app?
            new QuestionOption { Id = 11,  QuestionId = 5,  Text = "dotnet run",          IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 12,  QuestionId = 5,  Text = "dotnet start",        IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 13,  QuestionId = 5,  Text = "csharp run",          IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 14,  QuestionId = 5,  Text = "dotnet execute",      IsCorrect = false, CreatedAt = d },

            // Q6: Output of Console.WriteLine(2 + 3)
            new QuestionOption { Id = 15,  QuestionId = 6,  Text = "5",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 16,  QuestionId = 6,  Text = "23",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 17,  QuestionId = 6,  Text = "2+3",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 18,  QuestionId = 6,  Text = "Error",               IsCorrect = false, CreatedAt = d },

            // Q7: Console.Write() adds newline?
            new QuestionOption { Id = 19,  QuestionId = 7,  Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 20,  QuestionId = 7,  Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q8: String interpolation symbol?
            new QuestionOption { Id = 21,  QuestionId = 8,  Text = "$",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 22,  QuestionId = 8,  Text = "@",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 23,  QuestionId = 8,  Text = "#",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 24,  QuestionId = 8,  Text = "%",                   IsCorrect = false, CreatedAt = d },

            // Q9: Tab escape sequence?
            new QuestionOption { Id = 25,  QuestionId = 9,  Text = "\\t",                 IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 26,  QuestionId = 9,  Text = "\\n",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 27,  QuestionId = 9,  Text = "\\r",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 28,  QuestionId = 9,  Text = "\\b",                 IsCorrect = false, CreatedAt = d },

            // Q10: Currency data type?
            new QuestionOption { Id = 29,  QuestionId = 10, Text = "decimal",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 30,  QuestionId = 10, Text = "double",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 31,  QuestionId = 10, Text = "float",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 32,  QuestionId = 10, Text = "long",                IsCorrect = false, CreatedAt = d },

            // Q11: var is dynamically-typed?
            new QuestionOption { Id = 33,  QuestionId = 11, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 34,  QuestionId = 11, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q12: Default value of int?
            new QuestionOption { Id = 35,  QuestionId = 12, Text = "0",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 36,  QuestionId = 12, Text = "null",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 37,  QuestionId = 12, Text = "-1",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 38,  QuestionId = 12, Text = "undefined",           IsCorrect = false, CreatedAt = d },

            // Q14: Compile-time constant keyword?
            new QuestionOption { Id = 39,  QuestionId = 14, Text = "const",               IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 40,  QuestionId = 14, Text = "readonly",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 41,  QuestionId = 14, Text = "static",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 42,  QuestionId = 14, Text = "fixed",               IsCorrect = false, CreatedAt = d },

            // Q15: 17 % 5
            new QuestionOption { Id = 43,  QuestionId = 15, Text = "2",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 44,  QuestionId = 15, Text = "3",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 45,  QuestionId = 15, Text = "0",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 46,  QuestionId = 15, Text = "5",                   IsCorrect = false, CreatedAt = d },

            // Q16: 17 / 5
            new QuestionOption { Id = 47,  QuestionId = 16, Text = "3",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 48,  QuestionId = 16, Text = "3.4",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 49,  QuestionId = 16, Text = "2",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 50,  QuestionId = 16, Text = "4",                   IsCorrect = false, CreatedAt = d },

            // Q17: && short-circuits on left false?
            new QuestionOption { Id = 51,  QuestionId = 17, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 52,  QuestionId = 17, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q18: Modulo operator?
            new QuestionOption { Id = 53,  QuestionId = 18, Text = "%",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 54,  QuestionId = 18, Text = "/",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 55,  QuestionId = 18, Text = "mod",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 56,  QuestionId = 18, Text = "\\",                  IsCorrect = false, CreatedAt = d },

            // Q19: Default arm in switch expression?
            new QuestionOption { Id = 57,  QuestionId = 19, Text = "_",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 58,  QuestionId = 19, Text = "default",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 59,  QuestionId = 19, Text = "*",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 60,  QuestionId = 19, Text = "else",                IsCorrect = false, CreatedAt = d },

            // Q20: Ternary needs 3 operands?
            new QuestionOption { Id = 61,  QuestionId = 20, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 62,  QuestionId = 20, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q21: Null-coalescing operator?
            new QuestionOption { Id = 63,  QuestionId = 21, Text = "??",                  IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 64,  QuestionId = 21, Text = "?.",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 65,  QuestionId = 21, Text = "||",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 66,  QuestionId = 21, Text = "!",                   IsCorrect = false, CreatedAt = d },

            // Q22: 5 > 3 ? "Yes" : "No"
            new QuestionOption { Id = 67,  QuestionId = 22, Text = "Yes",                 IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 68,  QuestionId = 22, Text = "No",                  IsCorrect = false, CreatedAt = d },

            // Q23: Best loop for collection?
            new QuestionOption { Id = 69,  QuestionId = 23, Text = "foreach",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 70,  QuestionId = 23, Text = "for",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 71,  QuestionId = 23, Text = "while",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 72,  QuestionId = 23, Text = "do-while",            IsCorrect = false, CreatedAt = d },

            // Q24: do-while always runs once?
            new QuestionOption { Id = 73,  QuestionId = 24, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 74,  QuestionId = 24, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q25: Skip iteration keyword?
            new QuestionOption { Id = 75,  QuestionId = 25, Text = "continue",            IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 76,  QuestionId = 25, Text = "break",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 77,  QuestionId = 25, Text = "skip",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 78,  QuestionId = 25, Text = "next",                IsCorrect = false, CreatedAt = d },

            // Q26: for(i=0;i<3;i++) Console.Write(i)
            new QuestionOption { Id = 79,  QuestionId = 26, Text = "012",                 IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 80,  QuestionId = 26, Text = "123",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 81,  QuestionId = 26, Text = "0 1 2",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 82,  QuestionId = 26, Text = "Error",               IsCorrect = false, CreatedAt = d },

            // Q27: params keyword?
            new QuestionOption { Id = 83,  QuestionId = 27, Text = "params",              IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 84,  QuestionId = 27, Text = "args",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 85,  QuestionId = 27, Text = "varargs",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 86,  QuestionId = 27, Text = "multiple",            IsCorrect = false, CreatedAt = d },

            // Q28: Overloading needs different return types?
            new QuestionOption { Id = 87,  QuestionId = 28, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 88,  QuestionId = 28, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q29: Factorial(4)?
            new QuestionOption { Id = 89,  QuestionId = 29, Text = "24",                  IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 90,  QuestionId = 29, Text = "12",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 91,  QuestionId = 29, Text = "16",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 92,  QuestionId = 29, Text = "120",                 IsCorrect = false, CreatedAt = d },

            // Q31: Arrays are zero-indexed?
            new QuestionOption { Id = 93,  QuestionId = 31, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 94,  QuestionId = 31, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q32: Last element index?
            new QuestionOption { Id = 95,  QuestionId = 32, Text = "arr[^1]",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 96,  QuestionId = 32, Text = "arr[-1]",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 97,  QuestionId = 32, Text = "arr[last]",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 98,  QuestionId = 32, Text = "arr.Last",            IsCorrect = false, CreatedAt = d },

            // Q33: In-place sort?
            new QuestionOption { Id = 99,  QuestionId = 33, Text = "Array.Sort(arr)",     IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 100, QuestionId = 33, Text = "arr.OrderBy(x=>x)",  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 101, QuestionId = 33, Text = "Array.Order(arr)",    IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 102, QuestionId = 33, Text = "arr.Sort()",          IsCorrect = false, CreatedAt = d },

            // Q34: Sort then a[0] of {3,1,2}?
            new QuestionOption { Id = 103, QuestionId = 34, Text = "1",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 104, QuestionId = 34, Text = "3",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 105, QuestionId = 34, Text = "2",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 106, QuestionId = 34, Text = "Error",               IsCorrect = false, CreatedAt = d },

            // Q35: O(1) key lookup?
            new QuestionOption { Id = 107, QuestionId = 35, Text = "Dictionary<TKey,TValue>", IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 108, QuestionId = 35, Text = "List<T>",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 109, QuestionId = 35, Text = "Array",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 110, QuestionId = 35, Text = "Stack<T>",            IsCorrect = false, CreatedAt = d },

            // Q36: List<T> fixed size?
            new QuestionOption { Id = 111, QuestionId = 36, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 112, QuestionId = 36, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q37: Safe dictionary access?
            new QuestionOption { Id = 113, QuestionId = 37, Text = "TryGetValue",         IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 114, QuestionId = 37, Text = "GetValue",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 115, QuestionId = 37, Text = "dict[key]",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 116, QuestionId = 37, Text = "Find",                IsCorrect = false, CreatedAt = d },

            // Q39: Strings immutable?
            new QuestionOption { Id = 117, QuestionId = 39, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 118, QuestionId = 39, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q40: Build string in loop?
            new QuestionOption { Id = 119, QuestionId = 40, Text = "StringBuilder",       IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 120, QuestionId = 40, Text = "StringBuffer",        IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 121, QuestionId = 40, Text = "StringWriter",        IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 122, QuestionId = 40, Text = "StringHelper",        IsCorrect = false, CreatedAt = d },

            // Q41: "hello".ToUpper()?
            new QuestionOption { Id = 123, QuestionId = 41, Text = "HELLO",               IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 124, QuestionId = 41, Text = "hello",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 125, QuestionId = 41, Text = "Hello",               IsCorrect = false, CreatedAt = d },

            // Q42: Split string?
            new QuestionOption { Id = 126, QuestionId = 42, Text = "Split",               IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 127, QuestionId = 42, Text = "Divide",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 128, QuestionId = 42, Text = "Separate",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 129, QuestionId = 42, Text = "Partition",           IsCorrect = false, CreatedAt = d },

            // Q43: Constructor called on new?
            new QuestionOption { Id = 130, QuestionId = 43, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 131, QuestionId = 43, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q44: Private access modifier?
            new QuestionOption { Id = 132, QuestionId = 44, Text = "private",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 133, QuestionId = 44, Text = "protected",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 134, QuestionId = 44, Text = "internal",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 135, QuestionId = 44, Text = "public",              IsCorrect = false, CreatedAt = d },

            // Q46: Override for string representation?
            new QuestionOption { Id = 136, QuestionId = 46, Text = "ToString()",          IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 137, QuestionId = 46, Text = "Print()",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 138, QuestionId = 46, Text = "Display()",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 139, QuestionId = 46, Text = "Represent()",         IsCorrect = false, CreatedAt = d },

            // Q47: Multiple class inheritance?
            new QuestionOption { Id = 140, QuestionId = 47, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 141, QuestionId = 47, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q48: Base constructor keyword?
            new QuestionOption { Id = 142, QuestionId = 48, Text = "base",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 143, QuestionId = 48, Text = "super",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 144, QuestionId = 48, Text = "parent",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 145, QuestionId = 48, Text = "this",                IsCorrect = false, CreatedAt = d },

            // Q50: Overridable method keyword in base?
            new QuestionOption { Id = 146, QuestionId = 50, Text = "virtual",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 147, QuestionId = 50, Text = "abstract",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 148, QuestionId = 50, Text = "override",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 149, QuestionId = 50, Text = "sealed",              IsCorrect = false, CreatedAt = d },

            // Q51: Multiple interfaces?
            new QuestionOption { Id = 150, QuestionId = 51, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 151, QuestionId = 51, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q52: Interface prefix convention?
            new QuestionOption { Id = 152, QuestionId = 52, Text = "I",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 153, QuestionId = 52, Text = "IF",                  IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 154, QuestionId = 52, Text = "Int",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 155, QuestionId = 52, Text = "Intf",                IsCorrect = false, CreatedAt = d },

            // Q54: Abstract class or interface can have constructor?
            new QuestionOption { Id = 156, QuestionId = 54, Text = "Abstract class",      IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 157, QuestionId = 54, Text = "Interface",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 158, QuestionId = 54, Text = "Both",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 159, QuestionId = 54, Text = "Neither",             IsCorrect = false, CreatedAt = d },

            // Q55: LINQ deferred execution?
            new QuestionOption { Id = 160, QuestionId = 55, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 161, QuestionId = 55, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q56: LINQ filter method?
            new QuestionOption { Id = 162, QuestionId = 56, Text = "Where",               IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 163, QuestionId = 56, Text = "Filter",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 164, QuestionId = 56, Text = "Select",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 165, QuestionId = 56, Text = "Find",                IsCorrect = false, CreatedAt = d },

            // Q57: Force immediate execution to List?
            new QuestionOption { Id = 166, QuestionId = 57, Text = "ToList()",            IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 167, QuestionId = 57, Text = "Execute()",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 168, QuestionId = 57, Text = "Run()",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 169, QuestionId = 57, Text = "Materialise()",       IsCorrect = false, CreatedAt = d },

            // Q58: Any() vs Count() > 0?
            new QuestionOption { Id = 170, QuestionId = 58, Text = "Any()",               IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 171, QuestionId = 58, Text = "Count() > 0",         IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 172, QuestionId = 58, Text = "Exists()",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 173, QuestionId = 58, Text = "Contains()",          IsCorrect = false, CreatedAt = d },

            // Q59: Access modifier for same class?
            new QuestionOption { Id = 174, QuestionId = 59, Text = "private",             IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 175, QuestionId = 59, Text = "protected",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 176, QuestionId = 59, Text = "internal",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 177, QuestionId = 59, Text = "public",              IsCorrect = false, CreatedAt = d },

            // Q60: Properties are syntax sugar?
            new QuestionOption { Id = 178, QuestionId = 60, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 179, QuestionId = 60, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q61: Init-only property?
            new QuestionOption { Id = 180, QuestionId = 61, Text = "init",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 181, QuestionId = 61, Text = "set",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 182, QuestionId = 61, Text = "get",                 IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 183, QuestionId = 61, Text = "static",              IsCorrect = false, CreatedAt = d },

            // Q62: S in SOLID?
            new QuestionOption { Id = 184, QuestionId = 62, Text = "Single Responsibility", IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 185, QuestionId = 62, Text = "Software Security",    IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 186, QuestionId = 62, Text = "Simple Routing",       IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 187, QuestionId = 62, Text = "Static Relationship",  IsCorrect = false, CreatedAt = d },

            // Q63: Open/Closed principle?
            new QuestionOption { Id = 188, QuestionId = 63, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 189, QuestionId = 63, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q65: Primary EF Core class?
            new QuestionOption { Id = 190, QuestionId = 65, Text = "DbContext",           IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 191, QuestionId = 65, Text = "DbSet",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 192, QuestionId = 65, Text = "Entity",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 193, QuestionId = 65, Text = "QueryBuilder",        IsCorrect = false, CreatedAt = d },

            // Q66: Migration design package?
            new QuestionOption { Id = 194, QuestionId = 66, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 195, QuestionId = 66, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q67: Database runtime migration?
            new QuestionOption { Id = 196, QuestionId = 67, Text = "Database.MigrateAsync()", IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 197, QuestionId = 67, Text = "Database.Update()",       IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 198, QuestionId = 67, Text = "DbContext.Deploy()",      IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 199, QuestionId = 67, Text = "EF.Apply()",              IsCorrect = false, CreatedAt = d },

            // Q68: JWT parts?
            new QuestionOption { Id = 200, QuestionId = 68, Text = "3",                   IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 201, QuestionId = 68, Text = "2",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 202, QuestionId = 68, Text = "4",                   IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 203, QuestionId = 68, Text = "1",                   IsCorrect = false, CreatedAt = d },

            // Q69: Authorize can be applied everywhere?
            new QuestionOption { Id = 204, QuestionId = 69, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 205, QuestionId = 69, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q70: Passwords plain text?
            new QuestionOption { Id = 206, QuestionId = 70, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 207, QuestionId = 70, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q71: Stack FIFO?
            new QuestionOption { Id = 208, QuestionId = 71, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 209, QuestionId = 71, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q72: Add to stack?
            new QuestionOption { Id = 210, QuestionId = 72, Text = "Push()",              IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 211, QuestionId = 72, Text = "Enqueue()",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 212, QuestionId = 72, Text = "Add()",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 213, QuestionId = 72, Text = "Pop()",               IsCorrect = false, CreatedAt = d },

            // Q73: Remove from Queue?
            new QuestionOption { Id = 214, QuestionId = 73, Text = "Dequeue()",           IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 215, QuestionId = 73, Text = "Pop()",               IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 216, QuestionId = 73, Text = "Remove()",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 217, QuestionId = 73, Text = "Poll()",              IsCorrect = false, CreatedAt = d },

            // Q74: O(n log n) sorting?
            new QuestionOption { Id = 218, QuestionId = 74, Text = "Merge Sort",          IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 219, QuestionId = 74, Text = "Bubble Sort",         IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 220, QuestionId = 74, Text = "Insertion Sort",      IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 221, QuestionId = 74, Text = "Selection Sort",      IsCorrect = false, CreatedAt = d },

            // Q75: Bubble sort efficient?
            new QuestionOption { Id = 222, QuestionId = 75, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 223, QuestionId = 75, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q76: Pivot element?
            new QuestionOption { Id = 224, QuestionId = 76, Text = "Quick Sort",          IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 225, QuestionId = 76, Text = "Merge Sort",          IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 226, QuestionId = 76, Text = "Heap Sort",           IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 227, QuestionId = 76, Text = "Shell Sort",          IsCorrect = false, CreatedAt = d },

            // Q77: BST left child greater?
            new QuestionOption { Id = 228, QuestionId = 77, Text = "True",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 229, QuestionId = 77, Text = "False",               IsCorrect = true,  CreatedAt = d },

            // Q78: Root -> Left -> Right?
            new QuestionOption { Id = 230, QuestionId = 78, Text = "Pre-order",           IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 231, QuestionId = 78, Text = "In-order",            IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 232, QuestionId = 78, Text = "Post-order",          IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 233, QuestionId = 78, Text = "Level-order",         IsCorrect = false, CreatedAt = d },

            // Q79: BST Search complexity?
            new QuestionOption { Id = 234, QuestionId = 79, Text = "O(log n)",            IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 235, QuestionId = 79, Text = "O(n)",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 236, QuestionId = 79, Text = "O(1)",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 237, QuestionId = 79, Text = "O(n log n)",          IsCorrect = false, CreatedAt = d },

            // Q80: Flatten sequence?
            new QuestionOption { Id = 238, QuestionId = 80, Text = "SelectMany",          IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 239, QuestionId = 80, Text = "GroupBy",             IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 240, QuestionId = 80, Text = "Join",                IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 241, QuestionId = 80, Text = "Flatten",             IsCorrect = false, CreatedAt = d },

            // Q81: GroupJoin equivalent to Left Join?
            new QuestionOption { Id = 242, QuestionId = 81, Text = "True",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 243, QuestionId = 81, Text = "False",               IsCorrect = false, CreatedAt = d },

            // Q82: Zip selector?
            new QuestionOption { Id = 244, QuestionId = 82, Text = "Zip",                IsCorrect = true,  CreatedAt = d },
            new QuestionOption { Id = 245, QuestionId = 82, Text = "Concatenate",        IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 246, QuestionId = 82, Text = "Merge",              IsCorrect = false, CreatedAt = d },
            new QuestionOption { Id = 247, QuestionId = 82, Text = "Combine",            IsCorrect = false, CreatedAt = d }
        );
    }

    // ───────────────────────────────────────────────
    // CODING EXERCISES
    // ───────────────────────────────────────────────
    private static void SeedExercises(ModelBuilder mb, DateTime d) => mb.Entity<CodingExercise>().HasData(

        // ── Lesson 3: Hello World ──
        new CodingExercise
        {
            Id = 1, LessonId = 3, Order = 1, Difficulty = 1,
            Title = "Hello, C# Academy!",
            Instructions = "Write a program that prints exactly: Hello, C# Academy!",
            StarterCode = "// Write your code below\n",
            ExpectedOutput = "Hello, C# Academy!",
            Hint = "Use Console.WriteLine() with the exact text.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 2, LessonId = 3, Order = 2, Difficulty = 1,
            Title = "Personal Introduction",
            Instructions = "Print your name on the first line, your age on the second line, and your country on the third line.",
            StarterCode = "string name = \"Alice\";\nint age = 25;\nstring country = \"Ghana\";\n// Print each on its own line",
            ExpectedOutput = "Alice\n25\nGhana",
            Hint = "Call Console.WriteLine() three times, once for each variable.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 3, LessonId = 3, Order = 3, Difficulty = 1,
            Title = "Formatted Output",
            Instructions = "Using string interpolation, print: My name is Alice and I am 25 years old.",
            StarterCode = "string name = \"Alice\";\nint age = 25;\n// Use string interpolation",
            ExpectedOutput = "My name is Alice and I am 25 years old.",
            Hint = "Use $\"...{variable}...\" syntax.",
            CreatedAt = d
        },

        // ── Lesson 4: Variables & Data Types ──
        new CodingExercise
        {
            Id = 4, LessonId = 4, Order = 1, Difficulty = 1,
            Title = "Add Two Numbers",
            Instructions = "Declare two integers (10 and 20), add them, and print the result.",
            StarterCode = "int a = 10;\nint b = 20;\n// Print a + b",
            ExpectedOutput = "30",
            Hint = "Console.WriteLine(a + b);",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 5, LessonId = 4, Order = 2, Difficulty = 1,
            Title = "Circle Area",
            Instructions = "Given radius = 7.0, calculate the area of a circle (π × r²) and print it rounded to 2 decimal places.",
            StarterCode = "double radius = 7.0;\n// Area = Math.PI * radius * radius",
            ExpectedOutput = "153.94",
            Hint = "Use Math.PI and Console.WriteLine($\"{area:F2}\");",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 6, LessonId = 4, Order = 3, Difficulty = 2,
            Title = "Temperature Converter",
            Instructions = "Convert 100 degrees Celsius to Fahrenheit using the formula F = (C * 9/5) + 32. Print the result.",
            StarterCode = "double celsius = 100;\n// Convert to Fahrenheit",
            ExpectedOutput = "212",
            Hint = "double fahrenheit = (celsius * 9.0 / 5.0) + 32;",
            CreatedAt = d
        },

        // ── Lesson 5: Operators ──
        new CodingExercise
        {
            Id = 7, LessonId = 5, Order = 1, Difficulty = 1,
            Title = "Even or Odd",
            Instructions = "Given n = 17, print 'Even' if n is even, 'Odd' if n is odd. Use the modulo operator.",
            StarterCode = "int n = 17;\n// Determine even or odd",
            ExpectedOutput = "Odd",
            Hint = "if (n % 2 == 0) ...",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 8, LessonId = 5, Order = 2, Difficulty = 2,
            Title = "Swap Variables",
            Instructions = "Swap the values of a and b without using a third variable. Print a then b after the swap.",
            StarterCode = "int a = 5;\nint b = 10;\n// Swap a and b\nConsole.WriteLine(a);\nConsole.WriteLine(b);",
            ExpectedOutput = "10\n5",
            Hint = "a = a + b; b = a - b; a = a - b;",
            CreatedAt = d
        },

        // ── Lesson 6: If-Else ──
        new CodingExercise
        {
            Id = 9, LessonId = 6, Order = 1, Difficulty = 1,
            Title = "Grade Calculator",
            Instructions = "Given score = 85, print 'A' for ≥90, 'B' for ≥80, 'C' for ≥70, 'D' for ≥60, 'F' otherwise.",
            StarterCode = "int score = 85;\n// Determine grade",
            ExpectedOutput = "B",
            Hint = "Use if / else if / else chain.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 10, LessonId = 6, Order = 2, Difficulty = 2,
            Title = "Triangle Classifier",
            Instructions = "Given sides a=3, b=4, c=5, print 'Equilateral', 'Isosceles', or 'Scalene'.",
            StarterCode = "int a = 3, b = 4, c = 5;\n// Classify the triangle",
            ExpectedOutput = "Scalene",
            Hint = "Check if all sides equal, two sides equal, or no sides equal.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 11, LessonId = 6, Order = 3, Difficulty = 2,
            Title = "Leap Year",
            Instructions = "Given year = 2024, print 'Leap Year' or 'Not a Leap Year'. A year is a leap year if divisible by 4, except centuries unless also divisible by 400.",
            StarterCode = "int year = 2024;\n// Is it a leap year?",
            ExpectedOutput = "Leap Year",
            Hint = "(year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)",
            CreatedAt = d
        },

        // ── Lesson 7: Loops ──
        new CodingExercise
        {
            Id = 12, LessonId = 7, Order = 1, Difficulty = 1,
            Title = "Count to 10",
            Instructions = "Use a loop to print numbers from 1 to 10, each on a new line.",
            StarterCode = "// Print 1 to 10",
            ExpectedOutput = "1\n2\n3\n4\n5\n6\n7\n8\n9\n10",
            Hint = "for (int i = 1; i <= 10; i++)",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 13, LessonId = 7, Order = 2, Difficulty = 1,
            Title = "Sum 1 to 100",
            Instructions = "Use a loop to calculate the sum of integers from 1 to 100 and print the result.",
            StarterCode = "int sum = 0;\n// Add 1 through 100 to sum",
            ExpectedOutput = "5050",
            Hint = "for (int i = 1; i <= 100; i++) sum += i;",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 14, LessonId = 7, Order = 3, Difficulty = 2,
            Title = "Multiplication Table",
            Instructions = "Print the 5 times table from 5×1 to 5×10, one per line in the format: 5 x 1 = 5",
            StarterCode = "// Print 5 times table",
            ExpectedOutput = "5 x 1 = 5\n5 x 2 = 10\n5 x 3 = 15\n5 x 4 = 20\n5 x 5 = 25\n5 x 6 = 30\n5 x 7 = 35\n5 x 8 = 40\n5 x 9 = 45\n5 x 10 = 50",
            Hint = "for (int i = 1; i <= 10; i++) Console.WriteLine($\"5 x {i} = {5*i}\");",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 15, LessonId = 7, Order = 4, Difficulty = 2,
            Title = "Stars Triangle",
            Instructions = "Print a right-angled triangle of stars with 5 rows (row 1 = 1 star, row 5 = 5 stars).",
            StarterCode = "// Print star triangle",
            ExpectedOutput = "*\n**\n***\n****\n*****",
            Hint = "Nested loops: outer for rows, inner prints stars.",
            CreatedAt = d
        },

        // ── Lesson 8: Methods ──
        new CodingExercise
        {
            Id = 16, LessonId = 8, Order = 1, Difficulty = 2,
            Title = "Max of Three",
            Instructions = "Write a method Max(int a, int b, int c) that returns the largest of three numbers. Call it with 12, 45, 27 and print the result.",
            StarterCode = "// Write the Max method, then call it\nConsole.WriteLine(Max(12, 45, 27));",
            ExpectedOutput = "45",
            Hint = "Return Math.Max(a, Math.Max(b, c));",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 17, LessonId = 8, Order = 2, Difficulty = 2,
            Title = "IsPrime Method",
            Instructions = "Write a method IsPrime(int n) that returns true if n is prime. Print 'Prime' for n=29, 'Not Prime' for n=15.",
            StarterCode = "Console.WriteLine(IsPrime(29) ? \"Prime\" : \"Not Prime\");\nConsole.WriteLine(IsPrime(15) ? \"Prime\" : \"Not Prime\");",
            ExpectedOutput = "Prime\nNot Prime",
            Hint = "Check divisibility from 2 to (int)Math.Sqrt(n).",
            CreatedAt = d
        },

        // ── Lesson 9: Arrays ──
        new CodingExercise
        {
            Id = 18, LessonId = 9, Order = 1, Difficulty = 1,
            Title = "Array Sum & Average",
            Instructions = "Given int[] scores = {90, 85, 72, 96, 88}, print the sum and average (2 decimal places) on separate lines.",
            StarterCode = "int[] scores = { 90, 85, 72, 96, 88 };\n// Print sum then average",
            ExpectedOutput = "431\n86.20",
            Hint = "Loop to sum, then divide by scores.Length.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 19, LessonId = 9, Order = 2, Difficulty = 2,
            Title = "Reverse Array",
            Instructions = "Reverse the array {1, 2, 3, 4, 5} and print elements separated by spaces.",
            StarterCode = "int[] arr = { 1, 2, 3, 4, 5 };\n// Reverse and print",
            ExpectedOutput = "5 4 3 2 1",
            Hint = "Use Array.Reverse(arr) then loop with Console.Write.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 20, LessonId = 9, Order = 3, Difficulty = 3,
            Title = "Find Second Largest",
            Instructions = "Find and print the second largest number in {5, 2, 8, 1, 9, 3, 7}.",
            StarterCode = "int[] nums = { 5, 2, 8, 1, 9, 3, 7 };\n// Find second largest",
            ExpectedOutput = "8",
            Hint = "Sort the array and access the second-to-last element.",
            CreatedAt = d
        },

        // ── Lesson 10: Collections ──
        new CodingExercise
        {
            Id = 21, LessonId = 10, Order = 1, Difficulty = 2,
            Title = "Word Frequency",
            Instructions = "Count the frequency of each word in \"apple banana apple cherry banana apple\" and print each word and count, sorted alphabetically.",
            StarterCode = "string text = \"apple banana apple cherry banana apple\";\n// Count word frequencies",
            ExpectedOutput = "apple: 3\nbanana: 2\ncherry: 1",
            Hint = "Split into words, use a Dictionary<string, int> to count.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 22, LessonId = 10, Order = 2, Difficulty = 2,
            Title = "Remove Duplicates",
            Instructions = "Remove duplicates from {1,2,2,3,4,4,5,5,5} and print unique values separated by spaces.",
            StarterCode = "int[] nums = { 1, 2, 2, 3, 4, 4, 5, 5, 5 };\n// Remove duplicates",
            ExpectedOutput = "1 2 3 4 5",
            Hint = "Use a HashSet<int> or List with Contains check.",
            CreatedAt = d
        },

        // ── Lesson 11: Strings ──
        new CodingExercise
        {
            Id = 23, LessonId = 11, Order = 1, Difficulty = 2,
            Title = "Reverse a String",
            Instructions = "Reverse the string \"Hello, World!\" and print it.",
            StarterCode = "string s = \"Hello, World!\";\n// Reverse s",
            ExpectedOutput = "!dlroW ,olleH",
            Hint = "Convert to char array with ToCharArray(), call Array.Reverse(), then new string(chars).",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 24, LessonId = 11, Order = 2, Difficulty = 2,
            Title = "Count Vowels",
            Instructions = "Count the number of vowels (a, e, i, o, u — case-insensitive) in \"Programming in C# is Fun!\" and print the count.",
            StarterCode = "string text = \"Programming in C# is Fun!\";\n// Count vowels",
            ExpectedOutput = "6",
            Hint = "Loop through each char, check if it's in \"aeiou\" using Contains or a switch.",
            CreatedAt = d
        },

        // ── Lesson 12: OOP Classes ──
        new CodingExercise
        {
            Id = 25, LessonId = 12, Order = 1, Difficulty = 2,
            Title = "Create a BankAccount",
            Instructions = "Create a BankAccount with owner \"Alice\" and balance 500. Deposit 200, then withdraw 100. Print the final balance.",
            StarterCode = "// Implement a BankAccount class with Deposit and Withdraw methods",
            ExpectedOutput = "600",
            Hint = "Class with decimal _balance. Deposit adds, Withdraw subtracts if sufficient funds.",
            CreatedAt = d
        },
        new CodingExercise
        {
            Id = 26, LessonId = 12, Order = 2, Difficulty = 3,
            Title = "Stack Implementation",
            Instructions = "Implement a simple Stack<int> class with Push, Pop, Peek, and IsEmpty. Push 1,2,3. Pop once. Print Peek result.",
            StarterCode = "// Implement Stack<int> class\n// Push(1); Push(2); Push(3); Pop(); Console.WriteLine(Peek());",
            ExpectedOutput = "2",
            Hint = "Use a List<int> internally. Peek returns last element without removing.",
            CreatedAt = d
        }
    );

    // ───────────────────────────────────────────────
    // TUTORIAL STEPS
    // ───────────────────────────────────────────────
    private static void SeedTutorials(ModelBuilder mb, DateTime d) => mb.Entity<TutorialStep>().HasData(

        // Lesson 1 — Intro to C#
        new TutorialStep { Id = 1,  LessonId = 1,  Order = 1,  Title = "What is a Programming Language?",       Content = "A programming language is a formal set of instructions used to communicate with a computer. C# is one such language that compiles into code the .NET runtime can execute.", CreatedAt = d },
        new TutorialStep { Id = 2,  LessonId = 1,  Order = 2,  Title = "The .NET Ecosystem",                    Content = "The .NET ecosystem includes the CLR (runtime), BCL (class libraries), SDK tools, and NuGet package ecosystem. Together they provide everything you need to build any kind of application.", CreatedAt = d },
        new TutorialStep { Id = 3,  LessonId = 1,  Order = 3,  Title = "Your First Line of Code",               Content = "Output text with Console.WriteLine:", CodeSample = "Console.WriteLine(\"Hello, World!\");", CreatedAt = d },
        new TutorialStep { Id = 4,  LessonId = 1,  Order = 4,  Title = "Comments in C#",                        Content = "Comments explain your code to other developers (including your future self) and are ignored by the compiler.", CodeSample = "// Single-line comment\n/* Multi-line\n   comment */", CreatedAt = d },

        // Lesson 3 — Hello World
        new TutorialStep { Id = 5,  LessonId = 3,  Order = 1,  Title = "The Console Class",                     Content = "Console is a built-in static class in the System namespace. It provides methods for reading input and writing output to the terminal.", CreatedAt = d },
        new TutorialStep { Id = 6,  LessonId = 3,  Order = 2,  Title = "WriteLine vs Write",                    Content = "WriteLine adds a newline after output; Write does not.", CodeSample = "Console.Write(\"A\");\nConsole.Write(\"B\");\nConsole.WriteLine();\nConsole.WriteLine(\"C\");\n// Output: AB\n// C", CreatedAt = d },
        new TutorialStep { Id = 7,  LessonId = 3,  Order = 3,  Title = "Reading User Input",                    Content = "Use Console.ReadLine() to capture a line of text from the user.", CodeSample = "Console.Write(\"Name: \");\nstring name = Console.ReadLine()!;\nConsole.WriteLine($\"Hello, {name}!\");", CreatedAt = d },

        // Lesson 4 — Variables
        new TutorialStep { Id = 8,  LessonId = 4,  Order = 1,  Title = "Declaring Variables",                   Content = "A variable declaration specifies the type and name. An initialiser sets the starting value.", CodeSample = "int age = 25;\nstring city = \"Kumasi\";\nbool isStudent = true;", CreatedAt = d },
        new TutorialStep { Id = 9,  LessonId = 4,  Order = 2,  Title = "Value Types",                           Content = "Value types store their data directly on the stack. Modifying one variable does not affect another.", CodeSample = "int a = 10;\nint b = a;  // b gets a copy\nb = 20;\nConsole.WriteLine(a); // still 10", CreatedAt = d },
        new TutorialStep { Id = 10, LessonId = 4,  Order = 3,  Title = "Type Inference with var",               Content = "The var keyword lets the compiler infer the type from the assignment. The type is still fixed at compile time.", CodeSample = "var name = \"Alice\";  // string\nvar age  = 30;       // int\nvar pi   = 3.14;     // double", CreatedAt = d },
        new TutorialStep { Id = 11, LessonId = 4,  Order = 4,  Title = "Constants",                             Content = "Use const for values that should never change. This communicates intent and prevents accidental modification.", CodeSample = "const double Pi = 3.14159265358979;\nconst int DaysInWeek = 7;", CreatedAt = d },

        // Lesson 6 — Control Flow
        new TutorialStep { Id = 12, LessonId = 6,  Order = 1,  Title = "If-Else Basics",                        Content = "An if statement runs a block of code only when its condition is true. The optional else block runs when the condition is false.", CodeSample = "int x = 10;\nif (x > 5)\n    Console.WriteLine(\"Big\");\nelse\n    Console.WriteLine(\"Small\");", CreatedAt = d },
        new TutorialStep { Id = 13, LessonId = 6,  Order = 2,  Title = "Switch Expression",                     Content = "Switch expressions (C# 8+) are concise and return a value directly. The _ arm is the default case.", CodeSample = "string result = dayOfWeek switch\n{\n    1 => \"Monday\",\n    2 => \"Tuesday\",\n    _ => \"Other\"\n};", CreatedAt = d },

        // Lesson 7 — Loops
        new TutorialStep { Id = 14, LessonId = 7,  Order = 1,  Title = "For Loop Anatomy",                      Content = "The for loop has three parts: initialiser, condition, and iterator.", CodeSample = "//  init   cond    iter\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);", CreatedAt = d },
        new TutorialStep { Id = 15, LessonId = 7,  Order = 2,  Title = "Foreach Loop",                          Content = "Foreach cleanly iterates every element in a collection without managing an index variable.", CodeSample = "string[] colors = { \"Red\", \"Green\", \"Blue\" };\nforeach (string color in colors)\n    Console.WriteLine(color);", CreatedAt = d },
        new TutorialStep { Id = 16, LessonId = 7,  Order = 3,  Title = "Break and Continue",                    Content = "break exits the loop immediately. continue skips the rest of the current iteration and moves to the next.", CodeSample = "for (int i = 0; i < 10; i++)\n{\n    if (i == 7) break;\n    if (i % 2 == 0) continue;\n    Console.Write(i + \" \"); // 1 3 5\n}", CreatedAt = d },

        // Lesson 8 — Methods
        new TutorialStep { Id = 17, LessonId = 8,  Order = 1,  Title = "Why Methods?",                          Content = "Methods allow you to write code once and reuse it. They make programs easier to read, test, and maintain.", CreatedAt = d },
        new TutorialStep { Id = 18, LessonId = 8,  Order = 2,  Title = "Expression-Bodied Methods",             Content = "For single-expression methods, use the => arrow for a concise one-liner.", CodeSample = "int Add(int a, int b) => a + b;\nbool IsEven(int n) => n % 2 == 0;\nstring Greet(string name) => $\"Hello, {name}!\";", CreatedAt = d },
        new TutorialStep { Id = 19, LessonId = 8,  Order = 3,  Title = "Recursion",                             Content = "A recursive method calls itself. Always define a base case to stop the recursion.", CodeSample = "int Factorial(int n)\n{\n    if (n <= 1) return 1; // base case\n    return n * Factorial(n - 1); // recursive call\n}", CreatedAt = d },

        // Lesson 12 — OOP Classes
        new TutorialStep { Id = 20, LessonId = 12, Order = 1,  Title = "Class vs Object",                       Content = "A class is the blueprint; an object is an instance of that blueprint. You can create many objects from one class.", CodeSample = "// Blueprint\npublic class Dog { public string Name; }\n\n// Instances\nvar rex = new Dog { Name = \"Rex\" };\nvar fido = new Dog { Name = \"Fido\" };", CreatedAt = d },
        new TutorialStep { Id = 21, LessonId = 12, Order = 2,  Title = "Encapsulation",                         Content = "Keep fields private and expose them through properties. This lets you add validation without breaking callers.", CodeSample = "public class Person\n{\n    private int _age;\n    public int Age\n    {\n        get => _age;\n        set => _age = value >= 0 ? value : throw new ArgumentException();\n    }\n}", CreatedAt = d },

        // Lesson 13 — Inheritance
        new TutorialStep { Id = 22, LessonId = 13, Order = 1,  Title = "The 'is-a' Relationship",               Content = "Inheritance models an 'is-a' relationship. A Dog is an Animal. A Manager is an Employee.", CreatedAt = d },
        new TutorialStep { Id = 23, LessonId = 13, Order = 2,  Title = "virtual and override",                  Content = "Mark a base class method virtual to allow overriding. In the derived class, use override to replace the implementation.", CodeSample = "public class Animal  { public virtual  string Speak() => \"...\"; }\npublic class Dog : Animal { public override string Speak() => \"Woof!\"; }", CreatedAt = d },
        new TutorialStep { Id = 24, LessonId = 13, Order = 3,  Title = "Polymorphism in Action",                Content = "A variable declared as a base type can hold any derived type. The correct override is called at runtime.", CodeSample = "Animal a = new Dog();\nConsole.WriteLine(a.Speak()); // \"Woof!\" — not \"...\"", CreatedAt = d },

        // Lesson 19 — LINQ
        new TutorialStep { Id = 25, LessonId = 19, Order = 1,  Title = "Why LINQ?",                             Content = "Without LINQ you write imperative loops. With LINQ you write declarative queries that express *what* you want, not *how* to get it.", CodeSample = "// Without LINQ\nvar result = new List<int>();\nforeach (int n in numbers)\n    if (n > 5) result.Add(n);\n\n// With LINQ\nvar result = numbers.Where(n => n > 5).ToList();", CreatedAt = d },
        new TutorialStep { Id = 26, LessonId = 19, Order = 2,  Title = "Chaining Operators",                    Content = "LINQ operators return IEnumerable<T> and can be chained together. Each operator adds a step to the pipeline.", CodeSample = "var result = numbers\n    .Where(n => n % 2 == 0)   // filter\n    .Select(n => n * n)        // transform\n    .OrderByDescending(n => n) // sort\n    .Take(3)                   // first 3\n    .ToList();", CreatedAt = d },
        new TutorialStep { Id = 27, LessonId = 19, Order = 3,  Title = "Deferred Execution",                    Content = "LINQ queries don't run until you enumerate them. Materialise with ToList() or ToArray() when you need a fixed snapshot.", CodeSample = "var q = numbers.Where(n => n > 3); // no execution yet\nnumbers.Add(99);                    // this IS included\nvar list = q.ToList();              // executes NOW — includes 99", CreatedAt = d },
        
        // Lesson 20 — Encapsulation
        new TutorialStep { Id = 28, LessonId = 20, Order = 1,  Title = "Why Private Fields?",                  Content = "Directly exposing fields allows invalid data (e.g. negative age). Private fields combined with properties prevent this.", CodeSample = "private int _xp;\npublic int XP \n{ \n    get => _xp; \n    set => _xp = value >= 0 ? value : 0; \n}", CreatedAt = d },
        new TutorialStep { Id = 29, LessonId = 20, Order = 2,  Title = "Auto-Implemented Properties",         Content = "If you don't need validation logic, use auto-properties. The compiler creates the private backing field for you.", CodeSample = "public string UserName { get; set; }", CreatedAt = d },
        
        // Lesson 21 — SOLID
        new TutorialStep { Id = 30, LessonId = 21, Order = 1,  Title = "Single Responsibility",               Content = "A class should have one, and only one, reason to change. Separate data, logic, and presentation.", CodeSample = "// Bad: handles data AND saves to file\n// Good: User class + UserRepository class", CreatedAt = d },
        new TutorialStep { Id = 31, LessonId = 21, Order = 2,  Title = "Dependency Inversion",                 Content = "Depend on interfaces, not concrete classes. This makes your code testable and flexible.", CodeSample = "public class OrderService(IDbContext db) { ... }", CreatedAt = d },
        
        // Lesson 22 — EF Core
        new TutorialStep { Id = 32, LessonId = 22, Order = 1,  Title = "The DbContext",                        Content = "DbContext is your gateway to the database. It tracks changes and handles connectivity.", CodeSample = "public class MyDbContext : DbContext \n{ \n    public DbSet<User> Users { get; set; } \n}", CreatedAt = d },
        new TutorialStep { Id = 33, LessonId = 22, Order = 2,  Title = "Migrations",                           Content = "Migrations evolve your database schema as your models change. Use 'dotnet ef migrations add' to create one.", CreatedAt = d },
        
        // Lesson 24 — Stacks & Queues
        new TutorialStep { Id = 34, LessonId = 24, Order = 1,  Title = "Stack (LIFO)",                         Content = "Last-In, First-Out. Think of a stack of plates. You add to the top and take from the top.", CodeSample = "var s = new Stack<int>();\ns.Push(1);\nint top = s.Pop(); // 1", CreatedAt = d },
        new TutorialStep { Id = 35, LessonId = 24, Order = 2,  Title = "Queue (FIFO)",                         Content = "First-In, First-Out. Think of a line at a store. The first person in is the first person served.", CodeSample = "var q = new Queue<string>();\nq.Enqueue(\"Alice\");\nstring first = q.Dequeue(); // \"Alice\"", CreatedAt = d },
        
        // Lesson 25 — Sorting
        new TutorialStep { Id = 36, LessonId = 25, Order = 1,  Title = "Quick Sort",                           Content = "A divide-and-conquer algorithm. It picks a 'pivot' and partitions the array into smaller and larger elements.", CreatedAt = d },
        
        // Lesson 26 — Trees
        new TutorialStep { Id = 37, LessonId = 26, Order = 1,  Title = "Binary Search Tree",                   Content = "A tree where each node has at most two children. Left child is smaller, right child is larger than parent.", CreatedAt = d },
        
        // Lesson 27 — Advanced LINQ
        new TutorialStep { Id = 38, LessonId = 27, Order = 1,  Title = "SelectMany",                           Content = "Flattens nested collections. If you have a list of Departments, each with a list of Courses, SelectMany gives you one list of all Courses.", CreatedAt = d },
        new TutorialStep { Id = 39, LessonId = 27, Order = 2,  Title = "Zip",                                  Content = "Combines two sequences into one by pairing elements at the same index.", CodeSample = "var result = names.Zip(ages, (n, a) => $\"{n} is {a}\");", CreatedAt = d }
    );

    // ───────────────────────────────────────────────
    // VIDEOS
    // ───────────────────────────────────────────────
    private static void SeedVideos(ModelBuilder mb, DateTime d) => mb.Entity<LessonVideo>().HasData(

        new LessonVideo { Id = 1,  LessonId = 1,  Title = "C# in 100 Seconds",                       VideoUrl = "https://youtu.be/ravLFzIguCM",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 2,  CreatedAt = d },
        new LessonVideo { Id = 2,  LessonId = 1,  Title = "C# Full Course for Beginners (freeCodeCamp)", VideoUrl = "https://www.youtube.com/watch?v=Wx2TKSol0I8", Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 60, CreatedAt = d },
        new LessonVideo { Id = 3,  LessonId = 1,  Title = "Every C# feature in 10 minutes",     VideoUrl = "https://youtu.be/J0FhV3dM80o",   Provider = VideoProvider.YouTube, Order = 3, DurationMinutes = 10, CreatedAt = d },

        new LessonVideo { Id = 4,  LessonId = 2,  Title = "Installing .NET SDK — Step by Step",      VideoUrl = "https://youtu.be/UX_So82fEAI",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 8,  CreatedAt = d },

        new LessonVideo { Id = 5,  LessonId = 4,  Title = "C# Variables & Data Types Explained",    VideoUrl = "https://youtu.be/YzfMqUa-Nx0",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 18, CreatedAt = d },
        new LessonVideo { Id = 6,  LessonId = 4,  Title = "Value Types vs Reference Types in C#",   VideoUrl = "https://youtu.be/95SkyJe3Fe0",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 12, CreatedAt = d },

        new LessonVideo { Id = 7,  LessonId = 6,  Title = "C# If-Else & Switch Statements",         VideoUrl = "https://youtu.be/IzzNzSXkCMM",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 14, CreatedAt = d },
        new LessonVideo { Id = 8,  LessonId = 6,  Title = "Pattern Matching in C# 8+",              VideoUrl = "https://youtu.be/ySd_-h_Dapc",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 15, CreatedAt = d },

        new LessonVideo { Id = 9,  LessonId = 7,  Title = "For, While, Foreach Loops in C#",        VideoUrl = "https://youtu.be/WhACXlObR8s",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 20, CreatedAt = d },

        new LessonVideo { Id = 10, LessonId = 8,  Title = "C# Methods — Parameters & Return Types", VideoUrl = "https://youtu.be/IPpEefuFiVM",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 22, CreatedAt = d },

        new LessonVideo { Id = 11, LessonId = 12, Title = "OOP in C# — Classes & Objects",          VideoUrl = "https://youtu.be/pTB0EiLXUC8",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 30, CreatedAt = d },
        new LessonVideo { Id = 12, LessonId = 12, Title = "Properties in C# Explained",             VideoUrl = "https://youtu.be/OoYJy1s4zMY",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 15, CreatedAt = d },

        new LessonVideo { Id = 13, LessonId = 13, Title = "Inheritance & Polymorphism in C#",       VideoUrl = "https://youtu.be/CClziU97Xeg",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 25, CreatedAt = d },
        new LessonVideo { Id = 14, LessonId = 13, Title = "Virtual, Override & Sealed in C#",       VideoUrl = "https://youtu.be/AeknkeEUDiI",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 18, CreatedAt = d },

        new LessonVideo { Id = 15, LessonId = 14, Title = "Interfaces vs Abstract Classes in C#",   VideoUrl = "https://youtu.be/5p415gz2KBY",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 8, CreatedAt = d },

        new LessonVideo { Id = 16, LessonId = 15, Title = "ASP.NET Core Crash Course",              VideoUrl = "https://youtu.be/s1bk-68aB1U",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 10, CreatedAt = d },
        new LessonVideo { Id = 17, LessonId = 15, Title = "Dependency Injection in ASP.NET Core",   VideoUrl = "https://youtu.be/Hhpq7oYcpGE",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 25, CreatedAt = d },

        new LessonVideo { Id = 18, LessonId = 16, Title = "REST API with ASP.NET Core 8",           VideoUrl = "https://youtu.be/Tj3qsKSNvMk",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 45, CreatedAt = d },

        new LessonVideo { Id = 19, LessonId = 17, Title = "Big O Notation for Beginners",           VideoUrl = "https://youtu.be/XMUe3zFhM5c",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 14, CreatedAt = d },
        new LessonVideo { Id = 20, LessonId = 17, Title = "Linked Lists in C#",                     VideoUrl = "https://youtu.be/0AO7OwNzd2Y",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 20, CreatedAt = d },

        new LessonVideo { Id = 21, LessonId = 18, Title = "Delegates and Lambda Expressions in C#", VideoUrl = "https://youtu.be/PBP5KI8OFI0",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 22, CreatedAt = d },
        new LessonVideo { Id = 22, LessonId = 18, Title = "Func, Action, Predicate Explained",      VideoUrl = "https://youtu.be/LlZpno4_ylw",   Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 18, CreatedAt = d },

        new LessonVideo { Id = 23, LessonId = 19, Title = "LINQ in C# — Full Tutorial",             VideoUrl = "https://youtu.be/Kf9YiRkj-m4",   Provider = VideoProvider.YouTube, Order = 1, DurationMinutes = 40, CreatedAt = d },
        new LessonVideo { Id = 24, LessonId = 19, Title = "LINQ Performance Tips & Deferred Execution", VideoUrl = "https://youtu.be/pgPs_lMAIE4", Provider = VideoProvider.YouTube, Order = 2, DurationMinutes = 15, CreatedAt = d }
    );

    // ───────────────────────────────────────────────
    // CODING CHALLENGES
    // ───────────────────────────────────────────────
    private static void SeedChallenges(ModelBuilder mb, DateTime d) => mb.Entity<CodingChallenge>().HasData(

        // ── Easy ──
        new CodingChallenge
        {
            Id = 1, Order = 1, Difficulty = "Easy", XpReward = 20,
            Title = "Hello, User!",
            Description = "Print 'Hello, World!' to the console.",
            StarterCode = "// Your code here",
            ExpectedOutput = "Hello, World!",
            Hint = "Console.WriteLine",
            Tags = "basics,output",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 2, Order = 2, Difficulty = "Easy", XpReward = 20,
            Title = "FizzBuzz",
            Description = "Print numbers 1–15. Multiples of 3: 'Fizz'. Multiples of 5: 'Buzz'. Multiples of both: 'FizzBuzz'.",
            StarterCode = "for (int i = 1; i <= 15; i++)\n{\n    // Your code here\n}",
            ExpectedOutput = "1\n2\nFizz\n4\nBuzz\nFizz\n7\n8\nFizz\nBuzz\n11\nFizz\n13\n14\nFizzBuzz",
            Hint = "Use % (modulo) — check divisibility by both 3 and 5 first.",
            Tags = "loops,conditionals",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 3, Order = 3, Difficulty = "Easy", XpReward = 20,
            Title = "Reverse a String",
            Description = "Reverse the string 'hello' and print it.",
            StarterCode = "string s = \"hello\";",
            ExpectedOutput = "olleh",
            Hint = "ToCharArray() → Array.Reverse() → new string(chars)",
            Tags = "strings",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 4, Order = 4, Difficulty = "Easy", XpReward = 20,
            Title = "Sum an Array",
            Description = "Print the sum of {1, 2, 3, 4, 5}.",
            StarterCode = "int[] numbers = { 1, 2, 3, 4, 5 };",
            ExpectedOutput = "15",
            Hint = "Use a loop or numbers.Sum() from LINQ.",
            Tags = "arrays",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 5, Order = 5, Difficulty = "Easy", XpReward = 20,
            Title = "Maximum Value",
            Description = "Find and print the maximum value in {4, 7, 2, 9, 1, 5}.",
            StarterCode = "int[] nums = { 4, 7, 2, 9, 1, 5 };",
            ExpectedOutput = "9",
            Hint = "Use a loop tracking max, or nums.Max() with LINQ.",
            Tags = "arrays",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 6, Order = 6, Difficulty = "Easy", XpReward = 20,
            Title = "Count Words",
            Description = "Count the number of words in 'The quick brown fox jumps over the lazy dog' and print the count.",
            StarterCode = "string sentence = \"The quick brown fox jumps over the lazy dog\";",
            ExpectedOutput = "9",
            Hint = "Split on ' ' and check Length.",
            Tags = "strings",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 7, Order = 7, Difficulty = "Easy", XpReward = 25,
            Title = "Celsius to Fahrenheit",
            Description = "Convert 0°C, 100°C, and -40°C to Fahrenheit. Print each result on a new line.",
            StarterCode = "int[] celsius = { 0, 100, -40 };\n// Convert each",
            ExpectedOutput = "32\n212\n-40",
            Hint = "F = (C * 9/5) + 32",
            Tags = "math,loops",
            CreatedAt = d
        },

        // ── Medium ──
        new CodingChallenge
        {
            Id = 8, Order = 8, Difficulty = "Medium", XpReward = 35,
            Title = "Palindrome Check",
            Description = "Check if 'racecar' is a palindrome. Print 'True' or 'False'.",
            StarterCode = "string word = \"racecar\";",
            ExpectedOutput = "True",
            Hint = "Compare the word to its reverse.",
            Tags = "strings",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 9, Order = 9, Difficulty = "Medium", XpReward = 35,
            Title = "Factorial",
            Description = "Calculate and print the factorial of 6 (6! = 720).",
            StarterCode = "int n = 6;",
            ExpectedOutput = "720",
            Hint = "Multiply 1×2×3×4×5×6 in a loop.",
            Tags = "math,loops",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 10, Order = 10, Difficulty = "Medium", XpReward = 35,
            Title = "Fibonacci Sequence",
            Description = "Print the first 10 Fibonacci numbers, space-separated (starting with 1 1).",
            StarterCode = "// 1 1 2 3 5 8 13 21 34 55",
            ExpectedOutput = "1 1 2 3 5 8 13 21 34 55",
            Hint = "Track previous two values: a=1, b=1. In the loop: next = a+b, a=b, b=next.",
            Tags = "math,loops",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 11, Order = 11, Difficulty = "Medium", XpReward = 35,
            Title = "Count Occurrences",
            Description = "Count how many times the number 3 appears in {1,3,3,2,3,4,5,3,1}. Print the count.",
            StarterCode = "int[] nums = { 1, 3, 3, 2, 3, 4, 5, 3, 1 };\nint target = 3;",
            ExpectedOutput = "4",
            Hint = "Loop and increment a counter when nums[i] == target.",
            Tags = "arrays,loops",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 12, Order = 12, Difficulty = "Medium", XpReward = 35,
            Title = "Two Sum",
            Description = "Find two numbers in {2, 7, 11, 15} that add up to 9. Print their indices (e.g., '0 1').",
            StarterCode = "int[] nums = { 2, 7, 11, 15 };\nint target = 9;",
            ExpectedOutput = "0 1",
            Hint = "Nested loops: check every pair, or use a Dictionary for O(n).",
            Tags = "arrays,algorithms",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 13, Order = 13, Difficulty = "Medium", XpReward = 40,
            Title = "Anagram Check",
            Description = "Check if 'listen' and 'silent' are anagrams. Print 'True' or 'False'.",
            StarterCode = "string a = \"listen\";\nstring b = \"silent\";",
            ExpectedOutput = "True",
            Hint = "Sort both char arrays and compare.",
            Tags = "strings,sorting",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 14, Order = 14, Difficulty = "Medium", XpReward = 40,
            Title = "Bubble Sort",
            Description = "Sort {64, 34, 25, 12, 22, 11, 90} using bubble sort. Print sorted values space-separated.",
            StarterCode = "int[] arr = { 64, 34, 25, 12, 22, 11, 90 };",
            ExpectedOutput = "11 12 22 25 34 64 90",
            Hint = "Nested loops: compare adjacent pairs, swap if out of order.",
            Tags = "sorting,algorithms",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 15, Order = 15, Difficulty = "Medium", XpReward = 40,
            Title = "Roman Numeral",
            Description = "Convert the integer 2024 to Roman numerals. Print the result.",
            StarterCode = "int number = 2024;",
            ExpectedOutput = "MMXXIV",
            Hint = "Map values to symbols. Subtract the largest fitting value in a loop.",
            Tags = "math,strings",
            CreatedAt = d
        },

        // ── Hard ──
        new CodingChallenge
        {
            Id = 16, Order = 16, Difficulty = "Hard", XpReward = 50,
            Title = "Prime Check",
            Description = "Print 'Prime' if 97 is prime, 'Not Prime' otherwise.",
            StarterCode = "int num = 97;",
            ExpectedOutput = "Prime",
            Hint = "Check divisibility from 2 to (int)Math.Sqrt(num).",
            Tags = "math,algorithms",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 17, Order = 17, Difficulty = "Hard", XpReward = 50,
            Title = "Binary Search",
            Description = "Implement binary search to find the index of 7 in sorted array {1,3,5,7,9,11,13}. Print the index.",
            StarterCode = "int[] arr = { 1, 3, 5, 7, 9, 11, 13 };\nint target = 7;",
            ExpectedOutput = "3",
            Hint = "Track lo, hi, mid. If arr[mid] == target, return mid.",
            Tags = "algorithms,searching",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 18, Order = 18, Difficulty = "Hard", XpReward = 50,
            Title = "Valid Parentheses",
            Description = "Check if the string '({[]})' has valid, balanced brackets. Print 'Valid' or 'Invalid'.",
            StarterCode = "string s = \"({[]})\";",
            ExpectedOutput = "Valid",
            Hint = "Use a Stack. Push opens, pop and match closes.",
            Tags = "strings,stack,algorithms",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 19, Order = 19, Difficulty = "Hard", XpReward = 60,
            Title = "Matrix Transpose",
            Description = "Transpose a 3×3 matrix {{1,2,3},{4,5,6},{7,8,9}}. Print each row space-separated.",
            StarterCode = "int[,] matrix = { { 1,2,3 }, { 4,5,6 }, { 7,8,9 } };",
            ExpectedOutput = "1 4 7\n2 5 8\n3 6 9",
            Hint = "Swap [i,j] with [j,i]. Iterate with nested loops.",
            Tags = "arrays,math",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 20, Order = 20, Difficulty = "Hard", XpReward = 60,
            Title = "Merge Sort",
            Description = "Implement merge sort on {38, 27, 43, 3, 9, 82, 10}. Print sorted values space-separated.",
            StarterCode = "int[] arr = { 38, 27, 43, 3, 9, 82, 10 };",
            ExpectedOutput = "3 9 10 27 38 43 82",
            Hint = "Recursively split into halves, merge by comparing heads of each half.",
            Tags = "sorting,algorithms,recursion",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 21, Order = 21, Difficulty = "Hard", XpReward = 70,
            Title = "Word Frequency Top 3",
            Description = "Find the top 3 most frequent words in 'the cat sat on the mat the cat sat' and print them one per line with counts.",
            StarterCode = "string text = \"the cat sat on the mat the cat sat\";",
            ExpectedOutput = "the: 3\ncat: 2\nsat: 2",
            Hint = "Dictionary for counts, then order by descending count and take 3.",
            Tags = "strings,dictionary,linq",
            CreatedAt = d
        },
        new CodingChallenge
        {
            Id = 22, Order = 22, Difficulty = "Hard", XpReward = 70,
            Title = "Sieve of Eratosthenes",
            Description = "Use the Sieve of Eratosthenes to print all prime numbers up to 50, space-separated.",
            StarterCode = "int limit = 50;",
            ExpectedOutput = "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47",
            Hint = "Boolean array — mark multiples of each prime as not prime.",
            Tags = "math,algorithms",
            CreatedAt = d
        }
    );
}