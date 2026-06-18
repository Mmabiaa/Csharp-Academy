using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CsharpAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorialsPracticesAndVoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BestPractices",
                table: "Lessons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VoiceSummary",
                table: "Lessons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CodingExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instructions = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StarterCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpectedOutput = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodingExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TutorialSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeSample = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialSteps_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PracticeCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeCompletions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticeCompletions_CodingExercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "CodingExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CodingExercises",
                columns: new[] { "Id", "CreatedAt", "Difficulty", "ExpectedOutput", "Hint", "Instructions", "LessonId", "Order", "StarterCode", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Hello, C#!", "Use Console.WriteLine(\"Hello, C#!\");", "Use Console.WriteLine to print exactly: Hello, C#!", 1, 1, "// Print your message below\n", "Hello, C#!", null },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "30", "Use Console.WriteLine(a + b);", "Declare two integers (10 and 20) and print their sum using Console.WriteLine.", 3, 1, "int a = 10;\nint b = 20;\n// Print the sum\n", "Add Two Numbers", null },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Bob", "Use Console.WriteLine(p.Name);", "Create a Person class with Name property, instantiate it with name \"Bob\", and print the name.", 4, 1, "public class Person { public string Name { get; set; } }\nvar p = new Person { Name = \"Bob\" };\n// Print p.Name\n", "Create a Person", null }
                });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestPractices", "VoiceSummary" },
                values: new object[] { "- Use meaningful names for variables and methods\n- Follow C# naming conventions (PascalCase for types/methods, camelCase for locals)\n- Prefer `var` when the type is obvious from the right-hand side\n- Keep methods small and focused on one task", "C# is a modern object-oriented language from Microsoft. It runs on the .NET platform and is used for web, desktop, and mobile apps. In this lesson you'll learn what makes C# popular and where it's used." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestPractices", "VoiceSummary" },
                values: new object[] { "- Pin your SDK version in a `global.json` for team projects\n- Use VS Code with the C# Dev Kit extension for a lightweight setup\n- Run `dotnet --info` to verify SDK and runtime versions\n- Create projects with `dotnet new` rather than copying folders", "To start coding in C sharp, install the dot NET SDK and a code editor like Visual Studio Code. Download from dot net dot microsoft dot com and verify with dotnet dash dash version in your terminal." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestPractices", "VoiceSummary" },
                values: new object[] { "- Initialize variables when you declare them when possible\n- Use `const` for values that never change\n- Choose the smallest appropriate type (`int` vs `long`)\n- Avoid magic numbers — use named constants instead", "Variables store data in C sharp. Declare them with a type and name, like int age equals twenty five, or string name equals Alice. C sharp is statically typed, so the compiler checks types at build time." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestPractices", "VoiceSummary" },
                values: new object[] { "- Use properties instead of public fields\n- Apply encapsulation — expose only what's needed\n- Name classes with nouns (Person, OrderService)\n- Keep one responsibility per class (Single Responsibility Principle)", "A class is a blueprint for objects in C sharp. Define properties like Name and Age, then create instances with the new keyword. Classes are the foundation of object-oriented programming." });

            migrationBuilder.InsertData(
                table: "TutorialSteps",
                columns: new[] { "Id", "CodeSample", "Content", "CreatedAt", "LessonId", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, "A programming language lets you give instructions to a computer. C# is designed to be readable and powerful.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, "What is a programming language?", null },
                    { 2, null, "Microsoft created C# together with the .NET platform. It's open-source and cross-platform today.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, "Who makes C#?", null },
                    { 3, "Console.WriteLine(\"Hello, World!\");", "Every C# program can output text to the console:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 3, "Your first line of code", null },
                    { 4, null, "Variables label memory locations so you can reuse and update values throughout your program.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1, "Why variables?", null },
                    { 5, "int count = 0;", "Use `int` for whole numbers:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, "Declaring an integer", null },
                    { 6, "string greeting = \"Hello\";", "Use `string` for text. Strings use double quotes:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, "Declaring a string", null },
                    { 7, null, "A class defines structure; an object is a specific instance created from that class.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, "Classes vs objects", null },
                    { 8, "public class Car { public string Model { get; set; } }", "Use the class keyword and add properties:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2, "Defining a class", null },
                    { 9, "var car = new Car { Model = \"Sedan\" };", "Use `new` to create an instance:", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 3, "Creating an object", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingExercises_LessonId",
                table: "CodingExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeCompletions_ExerciseId",
                table: "PracticeCompletions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeCompletions_UserId_ExerciseId",
                table: "PracticeCompletions",
                columns: new[] { "UserId", "ExerciseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorialSteps_LessonId",
                table: "TutorialSteps",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracticeCompletions");

            migrationBuilder.DropTable(
                name: "TutorialSteps");

            migrationBuilder.DropTable(
                name: "CodingExercises");

            migrationBuilder.DropColumn(
                name: "BestPractices",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "VoiceSummary",
                table: "Lessons");
        }
    }
}
