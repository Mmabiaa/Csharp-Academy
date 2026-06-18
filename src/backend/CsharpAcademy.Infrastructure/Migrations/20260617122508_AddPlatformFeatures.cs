using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CsharpAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatformFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedHours",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Courses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Courses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Courses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LearningObjectives",
                table: "CourseModules",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instructions = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    ClassroomId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MaxPoints = table.Column<int>(type: "int", nullable: false),
                    RequiresCode = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assignments_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CodingChallenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Difficulty = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StarterCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpectedOutput = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tags = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    XpReward = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingChallenges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Provider = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonVideos_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssignmentSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    Feedback = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GradedById = table.Column<int>(type: "int", nullable: true),
                    GradedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChallengeCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeCompletions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeCompletions_CodingChallenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "CodingChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "3-day streak");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Complete a course");

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[] { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Solve 5 coding challenges", "", "Challenge Master", null });

            migrationBuilder.InsertData(
                table: "CodingChallenges",
                columns: new[] { "Id", "CreatedAt", "Description", "Difficulty", "ExpectedOutput", "Hint", "IsPublished", "Order", "StarterCode", "Tags", "Title", "UpdatedAt", "XpReward" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print numbers 1-5, replacing multiples of 3 with Fizz.", "Easy", "1\n2\nFizz\n4\nFizz", "Use modulo operator %", true, 1, "for(int i=1;i<=5;i++){}", "loops,conditionals", "FizzBuzz", null, 20 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Reverse the string \"hello\" and print it.", "Easy", "olleh", "Use new string(s.Reverse().ToArray())", true, 2, "string s=\"hello\";", "strings", "Reverse String", null, 20 },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sum the array [1,2,3,4,5] and print result.", "Easy", "15", "Use a loop or a.Sum()", true, 3, "int[] a={1,2,3,4,5};", "arrays", "Sum Array", null, 25 },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print factorial of 5 (120).", "Medium", "120", "Multiply 1*2*3*4*5", true, 4, "// compute 5!", "math,loops", "Factorial", null, 30 },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print True if \"racecar\" is palindrome.", "Medium", "True", "Compare with reversed string", true, 5, "string w=\"racecar\";", "strings", "Palindrome Check", null, 30 },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print the 7th Fibonacci number (13).", "Medium", "13", "Iterate with two variables", true, 6, "// fib sequence: 1,1,2,3,5,8,13", "math", "Fibonacci", null, 35 },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print True if 17 is prime.", "Hard", "True", "Check divisors up to sqrt(n)", true, 7, "int n=17;", "math", "Prime Check", null, 40 },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print index of 7 in sorted array [1,3,5,7,9].", "Hard", "3", "Classic binary search", true, 8, "int[] a={1,3,5,7,9}; int target=7;", "algorithms", "Binary Search", null, 50 }
                });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hint", "Instructions", "StarterCode" },
                values: new object[] { "Console.WriteLine(\"Hello, C#!\");", "Print: Hello, C#!", "" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hint", "Instructions", "StarterCode", "Title" },
                values: new object[] { "Console.WriteLine(a+b);", "Print sum of 10 and 20.", "int a=10;int b=20;", "Add Numbers" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Introduction to C# and your dev environment.", "Understand C# history; Set up .NET SDK; Run first program", "Section 1: Getting Started" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Data types, variables, and operators.", "Declare variables; Use operators; Convert types", "Section 2: Variables & Types" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 1, "Conditionals and loops.", "Write if/else; Use for and while loops", 3, "Section 3: Control Flow" });

            migrationBuilder.InsertData(
                table: "CourseModules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "LearningObjectives", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "OOP foundations.", "Define classes; Create objects; Use properties", 1, "Section 1: Classes & Objects", null },
                    { 5, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Extending and reusing code.", "Use inheritance; Override methods; Apply polymorphism", 2, "Section 2: Inheritance", null }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EstimatedHours", "IsPublished", "Level", "ThumbnailUrl", "Title" },
                values: new object[] { "Master C# from zero — variables, control flow, methods, and the .NET ecosystem. Perfect for beginners.", 8, true, "Beginner", "", "C# Fundamentals" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EstimatedHours", "IsPublished", "Level", "ThumbnailUrl", "Title" },
                values: new object[] { "Classes, inheritance, polymorphism, interfaces, and design principles in C#.", 10, true, "Intermediate", "", "Object-Oriented Programming" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "EstimatedHours", "IsPublished", "Level", "ThumbnailUrl", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Build modern web APIs and MVC apps with ASP.NET Core, EF Core, and REST best practices.", 12, true, "Intermediate", "", "ASP.NET Core Web Development", null },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arrays, lists, stacks, queues, sorting, and searching — implemented in C#.", 15, true, "Advanced", "", "Data Structures & Algorithms", null },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Query data with LINQ, lambdas, delegates, and functional patterns.", 6, true, "Intermediate", "", "LINQ & Functional C#", null }
                });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1, 1, 0, "C# in 100 Seconds", null, "https://www.youtube.com/watch?v=ravLFzWr5H4" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 1, 2, 0, "Introduction to C#", null, "https://www.youtube.com/watch?v=Wx2TKSol0I8" }
                });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Type", "VoiceSummary" },
                values: new object[] { "- Follow naming conventions\n- Use meaningful names", "C# is a modern, object-oriented language by Microsoft for the .NET platform.", 10, 4, "C sharp is a modern language for dot NET." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Pin SDK with global.json", "Install the .NET SDK from dotnet.microsoft.com and verify with `dotnet --version`.", 15, "Setting Up .NET", 0, "Install dot NET SDK to start coding." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Type", "VoiceSummary" },
                values: new object[] { "- Initialize on declaration", "```csharp\nint age = 25;\nstring name = \"Alice\";\n```", 12, 2, "Variables store typed data." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Arithmetic: +, -, *, /. Comparison: ==, !=, <, >.", 2, 10, 2, "Operators", 0, "" });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "BestPractices", "Content", "CourseModuleId", "CreatedAt", "DurationMinutes", "Order", "Title", "Type", "UpdatedAt", "VoiceSummary" },
                values: new object[,]
                {
                    { 5, "", "```csharp\nif (score >= 70) Console.WriteLine(\"Pass\");\n```", 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 1, "If Statements", 0, null, "" },
                    { 6, "", "for, while, and foreach loops control repetition.", 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 2, "Loops", 2, null, "" }
                });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { true, 2, "True" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsCorrect", "Text" },
                values: new object[] { false, "False" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { true, 3, "int" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsCorrect", "Text" },
                values: new object[] { false, "string" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { true, 5, "5" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 5, "23" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "C# runs on .NET.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Which keyword declares int?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: "Fill in: text type is ___.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
                value: "Output of Console.WriteLine(2+3);");

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Title" },
                values: new object[] { "Languages give instructions to computers.", "What is a language?" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CodeSample", "Content", "Title" },
                values: new object[] { "Console.WriteLine(\"Hello!\");", "Output text:", "First code" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CodeSample", "Content", "LessonId", "Order", "Title" },
                values: new object[] { "int x = 5;", "Store values with types.", 3, 1, "Variables" });

            migrationBuilder.InsertData(
                table: "CourseModules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "LearningObjectives", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "REST APIs with ASP.NET Core.", "Create controllers; Handle HTTP verbs; Return JSON", 1, "Section 1: Web API Basics", null },
                    { 7, 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arrays and linked lists.", "Implement arrays; Work with List<T>", 1, "Section 1: Linear Structures", null },
                    { 8, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Query syntax and method syntax.", "Write LINQ queries; Use lambdas", 1, "Section 1: LINQ Essentials", null }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "BestPractices", "Content", "CourseModuleId", "CreatedAt", "DurationMinutes", "Order", "Title", "Type", "UpdatedAt", "VoiceSummary" },
                values: new object[,]
                {
                    { 7, "- Use properties not public fields", "```csharp\npublic class Person { public string Name { get; set; } }\n```", 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 1, "Creating Classes", 4, null, "" },
                    { 8, "", "Derive classes with `: BaseClass` syntax.", 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 1, "Inheritance", 1, null, "" }
                });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Hint", "Instructions", "LessonId", "StarterCode", "Title" },
                values: new object[] { "Console.WriteLine(p.Name);", "Print name Bob.", 7, "public class Person{public string Name{get;set;}}", "Create Person" });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 8, 1, 0, "OOP in C#", null, "https://www.youtube.com/watch?v=wqUfllZyeq4" });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "BestPractices", "Content", "CourseModuleId", "CreatedAt", "DurationMinutes", "Order", "Title", "Type", "UpdatedAt", "VoiceSummary" },
                values: new object[,]
                {
                    { 9, "", "Create a Web API project with `dotnet new webapi`.", 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, 1, "Your First API", 0, null, "" },
                    { 10, "", "Fixed-size collections: `int[] nums = new int[5];`", 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 1, "Arrays in C#", 2, null, "" },
                    { 11, "", "Query collections: `items.Where(x => x > 5)`", 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 1, "Introduction to LINQ", 0, null, "" }
                });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30, 9, 1, 0, "ASP.NET Core Tutorial", null, "https://www.youtube.com/watch?v=AhAxLiGC7Pc" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ClassroomId",
                table: "Assignments",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CreatedById",
                table: "Assignments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LessonId",
                table: "Assignments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_AssignmentId_UserId",
                table: "AssignmentSubmissions",
                columns: new[] { "AssignmentId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_UserId",
                table: "AssignmentSubmissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeCompletions_ChallengeId",
                table: "ChallengeCompletions",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeCompletions_UserId_ChallengeId",
                table: "ChallengeCompletions",
                columns: new[] { "UserId", "ChallengeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonVideos_LessonId",
                table: "LessonVideos",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSubmissions");

            migrationBuilder.DropTable(
                name: "ChallengeCompletions");

            migrationBuilder.DropTable(
                name: "LessonVideos");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "CodingChallenges");

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "EstimatedHours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LearningObjectives",
                table: "CourseModules");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Maintain a 3-day learning streak");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Complete an entire course");

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hint", "Instructions", "StarterCode" },
                values: new object[] { "Use Console.WriteLine(\"Hello, C#!\");", "Use Console.WriteLine to print exactly: Hello, C#!", "// Print your message below\n" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hint", "Instructions", "StarterCode", "Title" },
                values: new object[] { "Use Console.WriteLine(a + b);", "Declare two integers (10 and 20) and print their sum using Console.WriteLine.", "int a = 10;\nint b = 20;\n// Print the sum\n", "Add Two Numbers" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Hint", "Instructions", "LessonId", "StarterCode", "Title" },
                values: new object[] { "Use Console.WriteLine(p.Name);", "Create a Person class with Name property, instantiate it with name \"Bob\", and print the name.", 4, "public class Person { public string Name { get; set; } }\nvar p = new Person { Name = \"Bob\" };\n// Print p.Name\n", "Create a Person" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Introduction to C# and your development environment.", "Getting Started" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Learn about variables, constants, and basic data types in C#.", "Variables and Data Types" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CourseId", "Description", "Order", "Title" },
                values: new object[] { 2, "Understand the building blocks of object-oriented programming.", 1, "Classes and Objects" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Learn the basics of C# programming language, including variables, loops, and functions.", "C# Fundamentals for Beginners" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Master OOP concepts like classes, inheritance, and polymorphism in C#.", "Object-Oriented Programming with C#" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestPractices", "Content", "VoiceSummary" },
                values: new object[] { "- Use meaningful names for variables and methods\n- Follow C# naming conventions (PascalCase for types/methods, camelCase for locals)\n- Prefer `var` when the type is obvious from the right-hand side\n- Keep methods small and focused on one task", "C# is a modern, object-oriented programming language developed by Microsoft. It runs on the .NET platform and is widely used for web, desktop, and mobile applications.", "C# is a modern object-oriented language from Microsoft. It runs on the .NET platform and is used for web, desktop, and mobile apps. In this lesson you'll learn what makes C# popular and where it's used." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestPractices", "Content", "Title", "VoiceSummary" },
                values: new object[] { "- Pin your SDK version in a `global.json` for team projects\n- Use VS Code with the C# Dev Kit extension for a lightweight setup\n- Run `dotnet --info` to verify SDK and runtime versions\n- Create projects with `dotnet new` rather than copying folders", "To start coding in C#, you need the .NET SDK and a code editor like Visual Studio or VS Code. Download the SDK from dotnet.microsoft.com and verify installation with `dotnet --version`.", "Setting Up Your Environment", "To start coding in C sharp, install the dot NET SDK and a code editor like Visual Studio Code. Download from dot net dot microsoft dot com and verify with dotnet dash dash version in your terminal." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestPractices", "Content", "VoiceSummary" },
                values: new object[] { "- Initialize variables when you declare them when possible\n- Use `const` for values that never change\n- Choose the smallest appropriate type (`int` vs `long`)\n- Avoid magic numbers — use named constants instead", "Variables store data values. In C#, you declare a variable with a type and name:\n\n```csharp\nint age = 25;\nstring name = \"Alice\";\ndouble price = 19.99;\n```", "Variables store data in C sharp. Declare them with a type and name, like int age equals twenty five, or string name equals Alice. C sharp is statically typed, so the compiler checks types at build time." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "Order", "Title", "VoiceSummary" },
                values: new object[] { "- Use properties instead of public fields\n- Apply encapsulation — expose only what's needed\n- Name classes with nouns (Person, OrderService)\n- Keep one responsibility per class (Single Responsibility Principle)", "A class is a blueprint for creating objects. It defines properties and methods:\n\n```csharp\npublic class Person\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n}\n```", 3, 1, "Creating Classes", "A class is a blueprint for objects in C sharp. Define properties like Name and Age, then create instances with the new keyword. Classes are the foundation of object-oriented programming." });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { false, 1, "Apple" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsCorrect", "Text" },
                values: new object[] { true, "True" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { false, 2, "False" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsCorrect", "Text" },
                values: new object[] { true, "int" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { false, 3, "string" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 3, "bool" });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "5", null },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, "23", null },
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, "Error", null }
                });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "C# runs on the .NET platform.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Which keyword declares an integer variable?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: "Fill in the blank: The keyword for text/strings in C# is ___.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
                value: "What is the output of: Console.WriteLine(2 + 3);");

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Title" },
                values: new object[] { "A programming language lets you give instructions to a computer. C# is designed to be readable and powerful.", "What is a programming language?" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CodeSample", "Content", "Title" },
                values: new object[] { null, "Microsoft created C# together with the .NET platform. It's open-source and cross-platform today.", "Who makes C#?" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CodeSample", "Content", "LessonId", "Order", "Title" },
                values: new object[] { "Console.WriteLine(\"Hello, World!\");", "Every C# program can output text to the console:", 1, 3, "Your first line of code" });

            migrationBuilder.InsertData(
                table: "TutorialSteps",
                columns: new[] { "Id", "CodeSample", "Content", "CreatedAt", "LessonId", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, null, "Variables label memory locations so you can reuse and update values throughout your program.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1, "Why variables?", null },
                    { 5, "int count = 0;", "Use `int` for whole numbers:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, "Declaring an integer", null },
                    { 6, "string greeting = \"Hello\";", "Use `string` for text. Strings use double quotes:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, "Declaring a string", null },
                    { 7, null, "A class defines structure; an object is a specific instance created from that class.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, "Classes vs objects", null },
                    { 8, "public class Car { public string Model { get; set; } }", "Use the class keyword and add properties:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2, "Defining a class", null },
                    { 9, "var car = new Car { Model = \"Sedan\" };", "Use `new` to create an instance:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 3, "Creating an object", null }
                });
        }
    }
}
