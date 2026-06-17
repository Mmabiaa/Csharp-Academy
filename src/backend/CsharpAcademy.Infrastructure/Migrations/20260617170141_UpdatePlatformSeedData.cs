using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CsharpAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlatformSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/badges/first-steps.svg");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Maintain a 3-day learning streak", "/badges/fire.svg" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Maintain a 7-day learning streak", "/badges/streak-7.svg", "Unstoppable" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Complete 3 lessons", "/badges/quick-learner.svg", "Quick Learner" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Complete 10 lessons", "/badges/dedicated.svg", "Dedicated Student" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Pass your first quiz with 80%+", "/badges/quiz-whiz.svg", "Quiz Whiz" });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Score 100% on any quiz", "/badges/perfect.svg", "Perfect Score", null },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Submit your first coding exercise", "/badges/code-newbie.svg", "Code Newbie", null },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Solve 5 coding challenges", "/badges/problem-solver.svg", "Problem Solver", null },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Solve 15 coding challenges", "/badges/challenge-master.svg", "Challenge Master", null },
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete the C# Fundamentals course", "/badges/csharp-grad.svg", "C# Graduate", null },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete the OOP course", "/badges/oop-pro.svg", "OOP Pro", null },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete the ASP.NET Core course", "/badges/api-builder.svg", "API Builder", null },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete the Data Structures course", "/badges/algo-ace.svg", "Algorithm Ace", null },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete the LINQ & Functional course", "/badges/linq-ninja.svg", "LINQ Ninja", null },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Earn 500 XP total", "/badges/xp-500.svg", "XP Hunter", null },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Earn 2000 XP total", "/badges/xp-2000.svg", "XP Legend", null }
                });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title" },
                values: new object[] { "Print 'Hello, World!' to the console.", "Hello, World!", "Console.WriteLine", "// Your code here", "basics,output", "Hello, User!" });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title" },
                values: new object[] { "Print numbers 1–15. Multiples of 3: 'Fizz'. Multiples of 5: 'Buzz'. Multiples of both: 'FizzBuzz'.", "1\n2\nFizz\n4\nBuzz\nFizz\n7\n8\nFizz\nBuzz\n11\nFizz\n13\n14\nFizzBuzz", "Use % (modulo) — check divisibility by both 3 and 5 first.", "for (int i = 1; i <= 15; i++)\n{\n    // Your code here\n}", "loops,conditionals", "FizzBuzz" });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Reverse the string 'hello' and print it.", "olleh", "ToCharArray() → Array.Reverse() → new string(chars)", "string s = \"hello\";", "strings", "Reverse a String", 20 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print the sum of {1, 2, 3, 4, 5}.", "Easy", "15", "Use a loop or numbers.Sum() from LINQ.", "int[] numbers = { 1, 2, 3, 4, 5 };", "arrays", "Sum an Array", 20 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Find and print the maximum value in {4, 7, 2, 9, 1, 5}.", "Easy", "9", "Use a loop tracking max, or nums.Max() with LINQ.", "int[] nums = { 4, 7, 2, 9, 1, 5 };", "arrays", "Maximum Value", 20 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Count the number of words in 'The quick brown fox jumps over the lazy dog' and print the count.", "Easy", "9", "Split on ' ' and check Length.", "string sentence = \"The quick brown fox jumps over the lazy dog\";", "strings", "Count Words", 20 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Convert 0°C, 100°C, and -40°C to Fahrenheit. Print each result on a new line.", "Easy", "32\n212\n-40", "F = (C * 9/5) + 32", "int[] celsius = { 0, 100, -40 };\n// Convert each", "math,loops", "Celsius to Fahrenheit", 25 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Check if 'racecar' is a palindrome. Print 'True' or 'False'.", "Medium", "True", "Compare the word to its reverse.", "string word = \"racecar\";", "strings", "Palindrome Check", 35 });

            migrationBuilder.InsertData(
                table: "CodingChallenges",
                columns: new[] { "Id", "CreatedAt", "Description", "Difficulty", "ExpectedOutput", "Hint", "IsPublished", "Order", "StarterCode", "Tags", "Title", "UpdatedAt", "XpReward" },
                values: new object[,]
                {
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Calculate and print the factorial of 6 (6! = 720).", "Medium", "720", "Multiply 1×2×3×4×5×6 in a loop.", true, 9, "int n = 6;", "math,loops", "Factorial", null, 35 },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print the first 10 Fibonacci numbers, space-separated (starting with 1 1).", "Medium", "1 1 2 3 5 8 13 21 34 55", "Track previous two values: a=1, b=1. In the loop: next = a+b, a=b, b=next.", true, 10, "// 1 1 2 3 5 8 13 21 34 55", "math,loops", "Fibonacci Sequence", null, 35 },
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Count how many times the number 3 appears in {1,3,3,2,3,4,5,3,1}. Print the count.", "Medium", "4", "Loop and increment a counter when nums[i] == target.", true, 11, "int[] nums = { 1, 3, 3, 2, 3, 4, 5, 3, 1 };\nint target = 3;", "arrays,loops", "Count Occurrences", null, 35 },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Find two numbers in {2, 7, 11, 15} that add up to 9. Print their indices (e.g., '0 1').", "Medium", "0 1", "Nested loops: check every pair, or use a Dictionary for O(n).", true, 12, "int[] nums = { 2, 7, 11, 15 };\nint target = 9;", "arrays,algorithms", "Two Sum", null, 35 },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Check if 'listen' and 'silent' are anagrams. Print 'True' or 'False'.", "Medium", "True", "Sort both char arrays and compare.", true, 13, "string a = \"listen\";\nstring b = \"silent\";", "strings,sorting", "Anagram Check", null, 40 },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sort {64, 34, 25, 12, 22, 11, 90} using bubble sort. Print sorted values space-separated.", "Medium", "11 12 22 25 34 64 90", "Nested loops: compare adjacent pairs, swap if out of order.", true, 14, "int[] arr = { 64, 34, 25, 12, 22, 11, 90 };", "sorting,algorithms", "Bubble Sort", null, 40 },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Convert the integer 2024 to Roman numerals. Print the result.", "Medium", "MMXXIV", "Map values to symbols. Subtract the largest fitting value in a loop.", true, 15, "int number = 2024;", "math,strings", "Roman Numeral", null, 40 },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Print 'Prime' if 97 is prime, 'Not Prime' otherwise.", "Hard", "Prime", "Check divisibility from 2 to (int)Math.Sqrt(num).", true, 16, "int num = 97;", "math,algorithms", "Prime Check", null, 50 },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Implement binary search to find the index of 7 in sorted array {1,3,5,7,9,11,13}. Print the index.", "Hard", "3", "Track lo, hi, mid. If arr[mid] == target, return mid.", true, 17, "int[] arr = { 1, 3, 5, 7, 9, 11, 13 };\nint target = 7;", "algorithms,searching", "Binary Search", null, 50 },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Check if the string '({[]})' has valid, balanced brackets. Print 'Valid' or 'Invalid'.", "Hard", "Valid", "Use a Stack. Push opens, pop and match closes.", true, 18, "string s = \"({[]})\";", "strings,stack,algorithms", "Valid Parentheses", null, 50 },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Transpose a 3×3 matrix {{1,2,3},{4,5,6},{7,8,9}}. Print each row space-separated.", "Hard", "1 4 7\n2 5 8\n3 6 9", "Swap [i,j] with [j,i]. Iterate with nested loops.", true, 19, "int[,] matrix = { { 1,2,3 }, { 4,5,6 }, { 7,8,9 } };", "arrays,math", "Matrix Transpose", null, 60 },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Implement merge sort on {38, 27, 43, 3, 9, 82, 10}. Print sorted values space-separated.", "Hard", "3 9 10 27 38 43 82", "Recursively split into halves, merge by comparing heads of each half.", true, 20, "int[] arr = { 38, 27, 43, 3, 9, 82, 10 };", "sorting,algorithms,recursion", "Merge Sort", null, 60 },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Find the top 3 most frequent words in 'the cat sat on the mat the cat sat' and print them one per line with counts.", "Hard", "the: 3\ncat: 2\nsat: 2", "Dictionary for counts, then order by descending count and take 3.", true, 21, "string text = \"the cat sat on the mat the cat sat\";", "strings,dictionary,linq", "Word Frequency Top 3", null, 70 },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Use the Sieve of Eratosthenes to print all prime numbers up to 50, space-separated.", "Hard", "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47", "Boolean array — mark multiples of each prime as not prime.", true, 22, "int limit = 50;", "math,algorithms", "Sieve of Eratosthenes", null, 70 }
                });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedOutput", "Hint", "Instructions", "LessonId", "StarterCode", "Title" },
                values: new object[] { "Hello, C# Academy!", "Use Console.WriteLine() with the exact text.", "Write a program that prints exactly: Hello, C# Academy!", 3, "// Write your code below\n", "Hello, C# Academy!" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedOutput", "Hint", "Instructions", "Order", "StarterCode", "Title" },
                values: new object[] { "Alice\n25\nGhana", "Call Console.WriteLine() three times, once for each variable.", "Print your name on the first line, your age on the second line, and your country on the third line.", 2, "string name = \"Alice\";\nint age = 25;\nstring country = \"Ghana\";\n// Print each on its own line", "Personal Introduction" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Difficulty", "ExpectedOutput", "Hint", "Instructions", "LessonId", "Order", "StarterCode", "Title" },
                values: new object[] { 1, "My name is Alice and I am 25 years old.", "Use $\"...{variable}...\" syntax.", "Using string interpolation, print: My name is Alice and I am 25 years old.", 3, 3, "string name = \"Alice\";\nint age = 25;\n// Use string interpolation", "Formatted Output" });

            migrationBuilder.InsertData(
                table: "CodingExercises",
                columns: new[] { "Id", "CreatedAt", "Difficulty", "ExpectedOutput", "Hint", "Instructions", "LessonId", "Order", "StarterCode", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "30", "Console.WriteLine(a + b);", "Declare two integers (10 and 20), add them, and print the result.", 4, 1, "int a = 10;\nint b = 20;\n// Print a + b", "Add Two Numbers", null },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "153.94", "Use Math.PI and Console.WriteLine($\"{area:F2}\");", "Given radius = 7.0, calculate the area of a circle (π × r²) and print it rounded to 2 decimal places.", 4, 2, "double radius = 7.0;\n// Area = Math.PI * radius * radius", "Circle Area", null },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "212", "double fahrenheit = (celsius * 9.0 / 5.0) + 32;", "Convert 100 degrees Celsius to Fahrenheit using the formula F = (C * 9/5) + 32. Print the result.", 4, 3, "double celsius = 100;\n// Convert to Fahrenheit", "Temperature Converter", null },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Odd", "if (n % 2 == 0) ...", "Given n = 17, print 'Even' if n is even, 'Odd' if n is odd. Use the modulo operator.", 5, 1, "int n = 17;\n// Determine even or odd", "Even or Odd", null },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "10\n5", "a = a + b; b = a - b; a = a - b;", "Swap the values of a and b without using a third variable. Print a then b after the swap.", 5, 2, "int a = 5;\nint b = 10;\n// Swap a and b\nConsole.WriteLine(a);\nConsole.WriteLine(b);", "Swap Variables", null },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "B", "Use if / else if / else chain.", "Given score = 85, print 'A' for ≥90, 'B' for ≥80, 'C' for ≥70, 'D' for ≥60, 'F' otherwise.", 6, 1, "int score = 85;\n// Determine grade", "Grade Calculator", null },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Scalene", "Check if all sides equal, two sides equal, or no sides equal.", "Given sides a=3, b=4, c=5, print 'Equilateral', 'Isosceles', or 'Scalene'.", 6, 2, "int a = 3, b = 4, c = 5;\n// Classify the triangle", "Triangle Classifier", null },
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Leap Year", "(year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)", "Given year = 2024, print 'Leap Year' or 'Not a Leap Year'. A year is a leap year if divisible by 4, except centuries unless also divisible by 400.", 6, 3, "int year = 2024;\n// Is it a leap year?", "Leap Year", null },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "1\n2\n3\n4\n5\n6\n7\n8\n9\n10", "for (int i = 1; i <= 10; i++)", "Use a loop to print numbers from 1 to 10, each on a new line.", 7, 1, "// Print 1 to 10", "Count to 10", null },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "5050", "for (int i = 1; i <= 100; i++) sum += i;", "Use a loop to calculate the sum of integers from 1 to 100 and print the result.", 7, 2, "int sum = 0;\n// Add 1 through 100 to sum", "Sum 1 to 100", null },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "5 x 1 = 5\n5 x 2 = 10\n5 x 3 = 15\n5 x 4 = 20\n5 x 5 = 25\n5 x 6 = 30\n5 x 7 = 35\n5 x 8 = 40\n5 x 9 = 45\n5 x 10 = 50", "for (int i = 1; i <= 10; i++) Console.WriteLine($\"5 x {i} = {5*i}\");", "Print the 5 times table from 5×1 to 5×10, one per line in the format: 5 x 1 = 5", 7, 3, "// Print 5 times table", "Multiplication Table", null },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "*\n**\n***\n****\n*****", "Nested loops: outer for rows, inner prints stars.", "Print a right-angled triangle of stars with 5 rows (row 1 = 1 star, row 5 = 5 stars).", 7, 4, "// Print star triangle", "Stars Triangle", null },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "45", "Return Math.Max(a, Math.Max(b, c));", "Write a method Max(int a, int b, int c) that returns the largest of three numbers. Call it with 12, 45, 27 and print the result.", 8, 1, "// Write the Max method, then call it\nConsole.WriteLine(Max(12, 45, 27));", "Max of Three", null },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Prime\nNot Prime", "Check divisibility from 2 to (int)Math.Sqrt(n).", "Write a method IsPrime(int n) that returns true if n is prime. Print 'Prime' for n=29, 'Not Prime' for n=15.", 8, 2, "Console.WriteLine(IsPrime(29) ? \"Prime\" : \"Not Prime\");\nConsole.WriteLine(IsPrime(15) ? \"Prime\" : \"Not Prime\");", "IsPrime Method", null },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "431\n86.20", "Loop to sum, then divide by scores.Length.", "Given int[] scores = {90, 85, 72, 96, 88}, print the sum and average (2 decimal places) on separate lines.", 9, 1, "int[] scores = { 90, 85, 72, 96, 88 };\n// Print sum then average", "Array Sum & Average", null },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "5 4 3 2 1", "Use Array.Reverse(arr) then loop with Console.Write.", "Reverse the array {1, 2, 3, 4, 5} and print elements separated by spaces.", 9, 2, "int[] arr = { 1, 2, 3, 4, 5 };\n// Reverse and print", "Reverse Array", null },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "8", "Sort the array and access the second-to-last element.", "Find and print the second largest number in {5, 2, 8, 1, 9, 3, 7}.", 9, 3, "int[] nums = { 5, 2, 8, 1, 9, 3, 7 };\n// Find second largest", "Find Second Largest", null },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "apple: 3\nbanana: 2\ncherry: 1", "Split into words, use a Dictionary<string, int> to count.", "Count the frequency of each word in \"apple banana apple cherry banana apple\" and print each word and count, sorted alphabetically.", 10, 1, "string text = \"apple banana apple cherry banana apple\";\n// Count word frequencies", "Word Frequency", null },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "1 2 3 4 5", "Use a HashSet<int> or List with Contains check.", "Remove duplicates from {1,2,2,3,4,4,5,5,5} and print unique values separated by spaces.", 10, 2, "int[] nums = { 1, 2, 2, 3, 4, 4, 5, 5, 5 };\n// Remove duplicates", "Remove Duplicates", null },
                    { 23, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "!dlroW ,olleH", "Convert to char array with ToCharArray(), call Array.Reverse(), then new string(chars).", "Reverse the string \"Hello, World!\" and print it.", 11, 1, "string s = \"Hello, World!\";\n// Reverse s", "Reverse a String", null },
                    { 24, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "6", "Loop through each char, check if it's in \"aeiou\" using Contains or a switch.", "Count the number of vowels (a, e, i, o, u — case-insensitive) in \"Programming in C# is Fun!\" and print the count.", 11, 2, "string text = \"Programming in C# is Fun!\";\n// Count vowels", "Count Vowels", null }
                });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Introduction to C#, the .NET ecosystem, and setting up your development environment.", "Understand C# history and purpose; Install the .NET SDK; Create and run your first console application; Understand the structure of a C# program", "Getting Started with C#" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Working with different data types, declaring variables, performing operations, and converting between types.", "Declare and initialise variables; Use value and reference types; Apply arithmetic, comparison, and logical operators; Perform type conversions and casting", "Variables, Data Types & Operators" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Conditionals, loops, switch statements, and branching to control program execution.", "Write if/else and switch statements; Use for, while, do-while, and foreach loops; Apply break, continue, and return; Use ternary and switch expressions", "Control Flow" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 1, "Creating reusable methods with parameters, return values, overloading, and recursion.", "Define and call methods; Use parameters and return types; Overload methods; Understand scope and recursion; Use optional and named parameters", 4, "Methods & Functions" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 1, "Working with arrays, multi-dimensional arrays, List<T>, Dictionary<TKey,TValue>, and other collection types.", "Declare and use arrays; Work with multi-dimensional arrays; Use List<T> and Dictionary<TKey,TValue>; Iterate collections with loops and foreach", 5, "Arrays & Collections" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 1, "String manipulation, formatting, parsing, and working with the StringBuilder class.", "Manipulate strings with built-in methods; Format strings with interpolation and format specifiers; Parse user input; Use StringBuilder for performance", 6, "Strings & Text Processing" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Title" },
                values: new object[] { 2, "The fundamentals of classes, objects, constructors, properties, and fields.", "Define classes with fields and properties; Create objects with constructors; Apply access modifiers; Understand this keyword and object lifetime", "Classes & Objects" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 2, "Data hiding, access modifiers, auto-properties, computed properties, and init-only setters.", "Apply public, private, protected, internal; Use auto-properties and backing fields; Create computed properties; Use init-only setters in records", 2, "Encapsulation & Properties" });

            migrationBuilder.InsertData(
                table: "CourseModules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "LearningObjectives", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Extending classes, method overriding, virtual/override/sealed, and polymorphic behaviour.", "Create inheritance hierarchies; Override and extend methods; Understand base class constructors; Apply polymorphism through references", 3, "Inheritance & Polymorphism", null },
                    { 10, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Defining contracts with interfaces, abstract base classes, and multiple interface implementation.", "Define and implement interfaces; Create abstract classes; Distinguish interface from abstract class; Apply dependency inversion principle", 4, "Interfaces & Abstract Classes", null },
                    { 11, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Understanding and applying SOLID design principles in real-world C# code.", "Apply Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion principles", 5, "SOLID Principles", null },
                    { 12, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Project structure, middleware pipeline, dependency injection, and configuration.", "Create and run ASP.NET Core projects; Understand the middleware pipeline; Register services with DI; Read from appsettings.json", 1, "ASP.NET Core Fundamentals", null },
                    { 13, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Creating controllers, handling HTTP verbs, routing, model binding, and returning proper responses.", "Create API controllers; Map HTTP verbs to actions; Use route attributes; Bind models from body, query, and route; Return correct status codes", 2, "Building RESTful Controllers", null },
                    { 14, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Database access with EF Core: DbContext, migrations, CRUD operations, and relationships.", "Configure DbContext; Define entity models; Create and apply migrations; Perform CRUD with EF Core; Map entity relationships", 3, "Entity Framework Core", null },
                    { 15, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "JWT authentication, authorisation, data validation, and API security best practices.", "Implement JWT bearer authentication; Apply [Authorize] policies; Validate input with data annotations and FluentValidation; Secure API endpoints", 4, "Authentication & Security", null },
                    { 16, 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Linear data structures: arrays, dynamic arrays, and singly/doubly linked lists.", "Implement arrays and dynamic arrays; Build singly and doubly linked lists; Analyse time and space complexity; Compare array vs linked list trade-offs", 1, "Arrays & Linked Lists", null },
                    { 17, 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Stack and queue implementations using arrays and linked lists, and their applications.", "Implement stacks with push/pop; Implement queues with enqueue/dequeue; Use Stack<T> and Queue<T> from .NET; Solve problems using stacks and queues", 2, "Stacks & Queues", null },
                    { 18, 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Classic sorting algorithms: Bubble, Selection, Insertion, Merge, Quick, and their complexities.", "Implement and compare sorting algorithms; Understand O(n²) vs O(n log n); Choose the right sort for the use case", 3, "Sorting Algorithms", null },
                    { 19, 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tree data structures, binary trees, BST operations, and tree traversal algorithms.", "Implement a binary tree; Perform in-order, pre-order, post-order traversal; Build a BST with insert, search, delete; Calculate tree height and balance", 4, "Trees & Binary Search Trees", null },
                    { 20, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Understanding delegates, Func<T>, Action<T>, and lambda expression syntax.", "Declare and invoke delegates; Use Func<T> and Action<T>; Write lambda expressions; Understand closures and captured variables", 1, "Delegates & Lambda Expressions", null },
                    { 21, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Query syntax vs method syntax, standard query operators, and deferred execution.", "Write LINQ in query and method syntax; Apply Where, Select, OrderBy, GroupBy, Join; Understand deferred vs immediate execution; Use ToList, ToArray, First, Single", 2, "LINQ Essentials", null },
                    { 22, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Projections, joins, grouping, aggregation, and writing efficient LINQ queries.", "Perform inner and group joins; Create custom projections with anonymous types; Aggregate with Sum, Count, Min, Max, Average; Chain multiple operators efficiently", 3, "Advanced LINQ", null }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EstimatedHours" },
                values: new object[] { "Master C# from absolute zero. Learn variables, data types, operators, control flow, methods, arrays, and core .NET basics. Perfect for complete beginners with no prior programming experience.", 20 });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Deep dive into OOP concepts: classes, objects, constructors, inheritance, polymorphism, encapsulation, interfaces, abstract classes, and SOLID design principles.", 22, "Object-Oriented Programming in C#" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Build professional RESTful APIs with ASP.NET Core 8, Entity Framework Core, JWT authentication, middleware, dependency injection, and clean architecture patterns.", 28, "ASP.NET Core Web APIs" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Master essential data structures (arrays, linked lists, stacks, queues, hash tables, trees, graphs) and algorithms (sorting, searching, dynamic programming, graph traversal).", 30, "Data Structures & Algorithms with C#" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "EstimatedHours" },
                values: new object[] { "Harness the full power of LINQ for data querying and transformation. Learn delegates, lambdas, Func/Action, expression trees, and functional programming patterns in C#.", 14 });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DurationMinutes", "Title" },
                values: new object[] { 60, "C# Full Course for Beginners (freeCodeCamp)" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DurationMinutes", "LessonId", "Order", "Title", "VideoUrl" },
                values: new object[] { 10, 1, 3, "Why C# is Amazing — The Big Picture", "https://www.youtube.com/watch?v=M5ugY7fWydE" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DurationMinutes", "LessonId", "Title", "VideoUrl" },
                values: new object[] { 8, 2, "Installing .NET SDK — Step by Step", "https://www.youtube.com/watch?v=p6bSpbqlNBI" });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[,]
                {
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 4, 1, 0, "C# Variables & Data Types Explained", null, "https://www.youtube.com/watch?v=nrkO3k_gbcM" },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 4, 2, 0, "Value Types vs Reference Types in C#", null, "https://www.youtube.com/watch?v=9glzO6CCZVI" },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, 6, 1, 0, "C# If-Else & Switch Statements", null, "https://www.youtube.com/watch?v=y8EZHx4LNDA" },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 6, 2, 0, "Pattern Matching in C# 8+", null, "https://www.youtube.com/watch?v=BQHF5VVB4Dc" },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 7, 1, 0, "For, While, Foreach Loops in C#", null, "https://www.youtube.com/watch?v=3b-VwetPStY" },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, 8, 1, 0, "C# Methods — Parameters & Return Types", null, "https://www.youtube.com/watch?v=l9gmFZaP9UE" }
                });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Title", "VoiceSummary" },
                values: new object[] { "- Use top-level statements for small programs and scripts (C# 9+)\n- Always use meaningful file and class names\n- Keep Program.cs minimal — delegate logic to separate classes\n- Use `dotnet --version` to confirm your SDK is installed before starting any project", "# Introduction to C# & the .NET Ecosystem\n\n## What is C#?\n\n**C#** (pronounced \"C-Sharp\") is a modern, general-purpose, statically-typed, object-oriented programming language developed by Microsoft and first released in 2002. It is designed for building a wide range of applications — from simple console programs to large-scale enterprise web services, mobile apps, games, and cloud workloads.\n\nC# is part of the **.NET ecosystem**, which provides:\n\n- **CLR (Common Language Runtime)** — the virtual machine that manages memory, handles exceptions, and runs your compiled code.\n- **BCL (Base Class Library)** — thousands of pre-built classes for I/O, networking, collections, threading, cryptography, and more.\n- **SDK & Tooling** — `dotnet` CLI, NuGet package manager, and first-class IDE support (Visual Studio, VS Code, Rider).\n\n## Why Learn C#?\n\n| Reason | Detail |\n|---|---|\n| Cross-platform | Runs on Windows, macOS, and Linux |\n| Versatile | Web APIs, desktop (WPF/WinForms/MAUI), games (Unity), mobile (MAUI), cloud (Azure) |\n| Modern Language | Records, pattern matching, nullable reference types, top-level programs |\n| Strong Job Market | Widely used in enterprise, gaming, and finance |\n| Performance | Near-native speed with AOT compilation in .NET 8+ |\n\n## A Brief History\n\n- **2002** — C# 1.0 ships with .NET Framework\n- **2007** — C# 3.0 introduces LINQ, lambdas, extension methods\n- **2017** — .NET Core 1.0 — truly cross-platform\n- **2020** — .NET 5 unifies .NET Core and .NET Framework\n- **2024** — C# 13 with .NET 9 (latest)\n\n## Your First Glimpse of C# Code\n\n```csharp\n// Program.cs — top-level statements (C# 9+)\nConsole.WriteLine(\"Hello, C# Academy!\");\n\n// Read user input\nConsole.Write(\"Enter your name: \");\nstring name = Console.ReadLine()!;\nConsole.WriteLine($\"Welcome, {name}!\");\n```\n\nThe `//` prefix starts a single-line comment — the compiler ignores it. `Console.WriteLine` writes a line of text to the terminal. `$\"...\"` is a **string interpolation** — we'll cover this fully in the Strings module.\n\n## The .NET Compilation Pipeline\n\n```\nYour .cs files\n     │\n     ▼  C# Compiler (Roslyn)\nIL (Intermediate Language) in .dll/.exe\n     │\n     ▼  JIT Compiler (at runtime)\nNative Machine Code\n     │\n     ▼  CPU executes\n```\n\nThis two-step process means C# code is portable across architectures, while still running at near-native speed.", 20, "Introduction to C# & the .NET Ecosystem", "C sharp is a modern, cross-platform, object-oriented language built by Microsoft. It runs on the dot NET ecosystem, which provides a runtime, class libraries, and tooling. You can use C sharp to build web APIs, desktop apps, mobile apps, games, and cloud services. The compiler turns your code into Intermediate Language, which the runtime then JIT-compiles to native machine code at runtime." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Title", "VoiceSummary" },
                values: new object[] { "- Pin your SDK version with a `global.json` file to ensure consistent builds across machines\n- Use `dotnet new gitignore` to generate a proper .gitignore for .NET projects\n- Never commit the `bin/` and `obj/` folders to version control\n- Enable nullable reference types (`<Nullable>enable</Nullable>`) from the start — it prevents null reference exceptions", "# Setting Up Your Development Environment\n\n## Step 1: Install the .NET SDK\n\nThe **.NET SDK** includes the compiler, runtime, and the `dotnet` CLI tool.\n\n1. Visit [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)\n2. Download the **latest LTS version** (e.g., .NET 8)\n3. Run the installer and follow the prompts\n\n**Verify the installation:**\n```bash\ndotnet --version\n# Expected output: 8.0.xxx\n```\n\n## Step 2: Choose an Editor\n\n| Editor | Best For | Cost |\n|---|---|---|\n| **Visual Studio 2022** | Full-featured Windows/Mac IDE | Free (Community) |\n| **VS Code + C# Dev Kit** | Lightweight, cross-platform | Free |\n| **JetBrains Rider** | Advanced IntelliJ-based IDE | Paid (free for students) |\n\n**Recommended for beginners:** VS Code with the **C# Dev Kit** extension.\n\nInstall VS Code extensions:\n- C# Dev Kit (`ms-dotnettools.csdevkit`)\n- .NET Install Tool (`ms-dotnettools.vscode-dotnet-runtime`)\n\n## Step 3: Create Your First Project\n\nOpen a terminal and run:\n\n```bash\n# Create a new console application\ndotnet new console -n HelloCSharp\n\n# Navigate into the project folder\ncd HelloCSharp\n\n# Open in VS Code\ncode .\n\n# Run the project\ndotnet run\n```\n\n**Expected output:**\n```\nHello, World!\n```\n\n## Understanding the Project Structure\n\n```\nHelloCSharp/\n├── HelloCSharp.csproj    ← Project file (XML config)\n├── Program.cs            ← Your main code file\n├── obj/                  ← Build intermediaries (ignore)\n└── bin/                  ← Compiled output\n```\n\n**HelloCSharp.csproj:**\n```xml\n<Project Sdk=\"Microsoft.NET.Sdk\">\n  <PropertyGroup>\n    <OutputType>Exe</OutputType>\n    <TargetFramework>net8.0</TargetFramework>\n    <Nullable>enable</Nullable>\n    <ImplicitUsings>enable</ImplicitUsings>\n  </PropertyGroup>\n</Project>\n```\n\n- `OutputType>Exe` — builds a runnable executable\n- `TargetFramework` — targets .NET 8\n- `Nullable>enable` — enables nullable reference type warnings\n- `ImplicitUsings>enable` — auto-imports common namespaces (`System`, `System.Collections.Generic`, etc.)\n\n## Useful dotnet CLI Commands\n\n```bash\ndotnet new console -n MyApp    # Create console app\ndotnet new webapi  -n MyApi    # Create Web API\ndotnet run                     # Build and run\ndotnet build                   # Build only\ndotnet test                    # Run tests\ndotnet add package Newtonsoft.Json  # Add NuGet package\ndotnet restore                 # Restore packages\n```", 20, "Setting Up Your Development Environment", "Install the dot NET SDK, verify it with dotnet --version, and choose an editor like VS Code or Visual Studio. Create projects with the dotnet new command, navigate into the folder, and run with dotnet run. The project file controls target framework, nullability, and implicit usings." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "- Use `Console.WriteLine` for output and `Console.ReadLine` for input in console apps\n- Prefer string interpolation `$\"...\"` over concatenation `+` for readability\n- Use verbatim strings `@\"...\"` for file paths and multi-line strings to avoid escape noise\n- Add XML doc comments (`///`) to public methods and classes from the start", "# Your First C# Program\n\n## The Classic Hello World\n\n```csharp\nConsole.WriteLine(\"Hello, World!\");\n```\n\nThat single line is a complete, runnable C# program (using top-level statements from C# 9+). Let's break it down:\n\n| Part | Meaning |\n|---|---|\n| `Console` | A built-in class in the `System` namespace for terminal I/O |\n| `.` | Member access operator — accesses a member of `Console` |\n| `WriteLine` | A method that outputs text followed by a newline |\n| `\"Hello, World!\"` | A **string literal** — text enclosed in double quotes |\n| `;` | Statement terminator — required at the end of every statement |\n\n## Writing vs Writing a Line\n\n```csharp\nConsole.Write(\"Hello, \");      // No newline\nConsole.Write(\"World!\");       // Continues on same line\nConsole.WriteLine();           // Prints an empty line\n\nConsole.WriteLine(\"New line\"); // Writes then moves to next line\n```\n\n**Output:**\n```\nHello, World!\n\nNew line\n```\n\n## Reading User Input\n\n```csharp\nConsole.Write(\"What is your name? \");\nstring name = Console.ReadLine()!;\nConsole.WriteLine($\"Hello, {name}! Welcome to C# Academy.\");\n```\n\n`Console.ReadLine()` returns a `string?` (nullable string). The `!` tells the compiler \"trust me, this won't be null\" — we'll cover nullability properly later.\n\n## Displaying Multiple Values\n\n```csharp\nstring firstName = \"Alice\";\nint age = 25;\ndouble gpa = 3.85;\n\nConsole.WriteLine($\"Name: {firstName}\");\nConsole.WriteLine($\"Age:  {age}\");\nConsole.WriteLine($\"GPA:  {gpa:F2}\"); // F2 = 2 decimal places\n\n// All on one line using string concatenation\nConsole.WriteLine(\"Name: \" + firstName + \", Age: \" + age);\n```\n\n## Escape Sequences\n\nInside strings, you can use escape sequences for special characters:\n\n```csharp\nConsole.WriteLine(\"Line 1\\nLine 2\");        // \\n = newline\nConsole.WriteLine(\"Column1\\tColumn2\");      // \\t = tab\nConsole.WriteLine(\"She said \\\"Hello!\\\"\");   // \\\" = literal quote\nConsole.WriteLine(\"C:\\\\Users\\\\Alice\");      // \\\\ = literal backslash\n\n// Verbatim strings avoid escape sequences:\nConsole.WriteLine(@\"C:\\Users\\Alice\");       // Same output, easier to read\n```\n\n## Comments\n\n```csharp\n// Single-line comment — ignored by compiler\n\n/*\n   Multi-line comment\n   Useful for longer explanations\n*/\n\n/// <summary>\n/// XML documentation comment — used by IDEs and doc generators\n/// </summary>\n```", 1, 20, 3, "Your First C# Program — Hello World", "Console dot WriteLine writes text to the terminal. Console dot ReadLine reads a line of input from the user. String interpolation lets you embed variables directly inside strings using the dollar sign prefix. Escape sequences like backslash n for newline and backslash t for tab control how text is formatted." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Always use `decimal` for money/currency — never `float` or `double`\n- Use `var` when the type is obvious from the right-hand side to reduce noise\n- Prefer `const` over magic numbers scattered through code (e.g., `const int MaxRetries = 3`)\n- Enable nullable reference types in the project file to catch null issues at compile time\n- Use digit separators `_` in large numeric literals for readability", "# Variables & Data Types\n\n## What is a Variable?\n\nA **variable** is a named storage location in memory that holds a value. In C#, every variable has a **type** that determines what kind of data it can store.\n\n```csharp\n// Syntax: type name = value;\nint age = 25;\nstring city = \"Accra\";\nbool isLoggedIn = true;\n```\n\n## Value Types vs Reference Types\n\n| Category | Types | Stored In |\n|---|---|---|\n| **Value types** | int, double, bool, char, struct, enum | Stack |\n| **Reference types** | string, class, array, interface | Heap |\n\nValue types store their data directly. Reference types store a *reference (pointer)* to where the data lives on the heap.\n\n## Integer Types\n\n```csharp\nbyte   b = 255;              // 8-bit,  0 to 255\nsbyte  sb = -128;            // 8-bit, -128 to 127\nshort  s = 32_000;           // 16-bit\nushort us = 65_535;          // 16-bit unsigned\nint    i = 2_147_483_647;    // 32-bit (most common)\nuint   ui = 4_294_967_295u;  // 32-bit unsigned\nlong   l = 9_223_372_036L;   // 64-bit\nulong  ul = 18_446_744_073uL;// 64-bit unsigned\n\n// Digit separators (_) improve readability — only cosmetic\nint population = 33_000_000;\n```\n\n**Rule of thumb:** Use `int` for whole numbers unless you have a specific reason for a different size.\n\n## Floating-Point Types\n\n```csharp\nfloat  f = 3.14f;        // 32-bit, ~7 digits precision  (suffix f)\ndouble d = 3.14159265;   // 64-bit, ~15 digits precision (default)\ndecimal m = 19.99m;      // 128-bit, 28–29 digits (suffix m)\n```\n\n> ⚠️ **Critical:** Use `decimal` for financial calculations (money). `float` and `double` have binary rounding errors — `0.1 + 0.2` is not exactly `0.3` in floating-point.\n\n```csharp\ndouble bad  = 0.1 + 0.2;    // 0.30000000000000004\ndecimal ok  = 0.1m + 0.2m;  // 0.3\n```\n\n## Text Types\n\n```csharp\nchar   c = 'A';             // Single character (single quotes)\nstring s = \"Hello, World!\"; // Sequence of characters (double quotes)\n\n// Strings are immutable — each operation creates a new string\nstring greeting = \"Hello\";\ngreeting = greeting + \", Alice!\"; // New string created in memory\n```\n\n## Boolean\n\n```csharp\nbool isActive  = true;\nbool isDeleted = false;\n\n// Common in conditions\nif (isActive && !isDeleted)\n{\n    Console.WriteLine(\"User is active.\");\n}\n```\n\n## Type Inference with `var`\n\nThe `var` keyword lets the compiler infer the type from the right-hand side:\n\n```csharp\nvar name    = \"Alice\";        // inferred as string\nvar age     = 30;             // inferred as int\nvar price   = 9.99m;         // inferred as decimal\nvar numbers = new[] { 1, 2, 3 }; // inferred as int[]\n```\n\n`var` is still **statically typed** — the type is fixed at compile time. It's not dynamic.\n\n## Constants & Readonly\n\n```csharp\n// const — value fixed at compile time\nconst double Pi = 3.14159265358979;\nconst int DaysInWeek = 7;\n\n// readonly — value fixed at runtime (can be set in constructor)\nreadonly DateTime CreatedAt = DateTime.UtcNow;\n```\n\n## Nullable Types\n\nValue types cannot normally be `null`. Add `?` to allow null:\n\n```csharp\nint  age      = 25;    // cannot be null\nint? maybeAge = null;  // nullable int\n\nif (maybeAge.HasValue)\n    Console.WriteLine(maybeAge.Value);\n\n// Null-coalescing operator: use default if null\nint display = maybeAge ?? 0;\n```", 30, 1, "Variables & Data Types", 4, "Variables are named memory locations with a fixed type. C sharp has value types like int, double, bool, and char stored on the stack, and reference types like string and class stored on the heap. Use decimal for money, int for whole numbers, and string for text. The var keyword lets the compiler infer the type automatically. Add a question mark to make value types nullable." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "- Use parentheses to clarify operator precedence — don't rely on memorising it\n- Always cast or use `Convert` when narrowing numeric types (e.g., double to int)\n- Prefer `int.TryParse` over `int.Parse` when dealing with user input — it won't throw\n- Use `%` (modulo) to check even/odd: `n % 2 == 0` means even\n- Avoid side effects in compound expressions with ++ and -- to prevent hard-to-spot bugs", "# Operators & Expressions\n\n## Arithmetic Operators\n\n```csharp\nint a = 17, b = 5;\n\nConsole.WriteLine(a + b);   // 22  — Addition\nConsole.WriteLine(a - b);   // 12  — Subtraction\nConsole.WriteLine(a * b);   // 85  — Multiplication\nConsole.WriteLine(a / b);   // 3   — Integer division (truncates!)\nConsole.WriteLine(a % b);   // 2   — Modulo (remainder)\n\n// For floating-point division:\ndouble result = (double)a / b;  // 3.4\n```\n\n> ⚠️ Integer division truncates: `17 / 5 = 3`, not 3.4. Cast to double first.\n\n## Increment & Decrement\n\n```csharp\nint x = 10;\nx++;    // Post-increment: x becomes 11\n++x;    // Pre-increment:  x becomes 12\nx--;    // Post-decrement: x becomes 11\n--x;    // Pre-decrement:  x becomes 10\n\n// Post vs pre matters in expressions:\nint a = 5;\nint b = a++;  // b = 5, a = 6  (old value returned)\nint c = ++a;  // c = 7, a = 7  (new value returned)\n```\n\n## Compound Assignment\n\n```csharp\nint n = 10;\nn += 5;   // n = 15  (same as n = n + 5)\nn -= 3;   // n = 12\nn *= 2;   // n = 24\nn /= 4;   // n = 6\nn %= 4;   // n = 2\n```\n\n## Comparison Operators\n\n```csharp\nint x = 10, y = 20;\n\nConsole.WriteLine(x == y);   // False — Equal to\nConsole.WriteLine(x != y);   // True  — Not equal to\nConsole.WriteLine(x < y);    // True  — Less than\nConsole.WriteLine(x > y);    // False — Greater than\nConsole.WriteLine(x <= y);   // True  — Less than or equal\nConsole.WriteLine(x >= y);   // False — Greater than or equal\n```\n\nComparison operators return a `bool` value.\n\n## Logical Operators\n\n```csharp\nbool a = true, b = false;\n\nConsole.WriteLine(a && b);   // False — AND: both must be true\nConsole.WriteLine(a || b);   // True  — OR:  at least one true\nConsole.WriteLine(!a);       // False — NOT: inverts the value\nConsole.WriteLine(a ^ b);    // True  — XOR: exactly one true\n```\n\n**Short-circuit evaluation:**\n- `&&` stops evaluating if the first operand is `false`\n- `||` stops evaluating if the first operand is `true`\n\n```csharp\nstring? s = null;\n// Safe — second part not evaluated if s is null\nif (s != null && s.Length > 0)\n    Console.WriteLine(s);\n```\n\n## Bitwise Operators (for advanced use)\n\n```csharp\nint a = 0b_1100;   // 12 in binary\nint b = 0b_1010;   // 10 in binary\n\nConsole.WriteLine(a & b);    // 0b_1000 = 8  — Bitwise AND\nConsole.WriteLine(a | b);    // 0b_1110 = 14 — Bitwise OR\nConsole.WriteLine(a ^ b);    // 0b_0110 = 6  — Bitwise XOR\nConsole.WriteLine(~a);       // Bitwise NOT (flips all bits)\nConsole.WriteLine(a << 1);   // 24 — Left shift (multiply by 2)\nConsole.WriteLine(a >> 1);   // 6  — Right shift (divide by 2)\n```\n\n## Operator Precedence\n\nFrom highest to lowest (simplified):\n\n```\n1. () — parentheses\n2. ++ -- ! ~ (unary)\n3. * / %\n4. + -\n5. < > <= >=\n6. == !=\n7. &&\n8. ||\n9. = += -= (assignment)\n```\n\n**Best practice: use parentheses** to make your intent clear rather than relying on precedence rules.\n\n```csharp\nint result = 2 + 3 * 4;        // 14 (not 20)\nint clear  = 2 + (3 * 4);     // 14 — explicit\nint also   = (2 + 3) * 4;     // 20 — different intent\n```\n\n## Type Conversion\n\n```csharp\n// Implicit conversion (no data loss)\nint i = 42;\nlong l = i;       // int fits in long — safe\ndouble d = i;     // int fits in double — safe\n\n// Explicit cast (may lose data)\ndouble pi = 3.14159;\nint truncated = (int)pi;   // 3 — fractional part lost!\n\n// Convert class\nstring s = \"42\";\nint n = Convert.ToInt32(s);    // 42\ndouble x = Convert.ToDouble(\"3.14\");\n\n// TryParse — safe conversion, no exception on failure\nif (int.TryParse(\"123abc\", out int result))\n    Console.WriteLine(result);\nelse\n    Console.WriteLine(\"Not a valid integer\");\n```", 2, 25, 2, "Operators & Expressions", "Operators perform arithmetic, comparison, logical, and assignment operations. Integer division truncates, so cast to double for decimal results. Logical operators short-circuit — and-and stops at the first false, or-or stops at the first true. Use TryParse for safe user-input conversion. Always use parentheses to make your intent explicit." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "- Prefer switch expressions over switch statements for value-returning logic\n- Use the ternary operator only for simple conditions — keep it on one line\n- Never leave out `break` in a switch statement (unlike C/C++ it won't fall through, but it's required)\n- Prefer pattern matching over explicit type checks (`obj is int n` rather than `(int)obj`)\n- Use `?.` (null-conditional) to safely access members of potentially null objects", "# If-Else Statements & Switch Expressions\n\n## Basic If-Else\n\n```csharp\nint score = 85;\n\nif (score >= 90)\n{\n    Console.WriteLine(\"Grade: A\");\n}\nelse if (score >= 80)\n{\n    Console.WriteLine(\"Grade: B\");\n}\nelse if (score >= 70)\n{\n    Console.WriteLine(\"Grade: C\");\n}\nelse if (score >= 60)\n{\n    Console.WriteLine(\"Grade: D\");\n}\nelse\n{\n    Console.WriteLine(\"Grade: F\");\n}\n```\n\n## Ternary Operator\n\nA concise one-line conditional:\n\n```csharp\nint age = 20;\nstring status = age >= 18 ? \"Adult\" : \"Minor\";\nConsole.WriteLine(status);  // Adult\n\n// Nested (avoid for readability — prefer if-else)\nstring label = score >= 90 ? \"A\" : score >= 80 ? \"B\" : \"C\";\n```\n\n## Null-Coalescing Operators\n\n```csharp\nstring? input = null;\n\n// ?? — use right side if left is null\nstring display = input ?? \"Default Value\";\nConsole.WriteLine(display);  // Default Value\n\n// ??= — assign only if currently null\ninput ??= \"Assigned\";\nConsole.WriteLine(input);   // Assigned\n\n// ?. — null-conditional: only call member if not null\nint? length = input?.Length;\n```\n\n## Switch Statement\n\n```csharp\nstring day = \"Monday\";\n\nswitch (day)\n{\n    case \"Monday\":\n    case \"Tuesday\":\n    case \"Wednesday\":\n    case \"Thursday\":\n    case \"Friday\":\n        Console.WriteLine(\"Weekday\");\n        break;\n    case \"Saturday\":\n    case \"Sunday\":\n        Console.WriteLine(\"Weekend\");\n        break;\n    default:\n        Console.WriteLine(\"Unknown day\");\n        break;\n}\n```\n\n## Switch Expression (C# 8+) — Preferred Modern Style\n\n```csharp\n// Returns a value directly\nstring dayType = day switch\n{\n    \"Saturday\" or \"Sunday\" => \"Weekend\",\n    \"Monday\" or \"Tuesday\" or \"Wednesday\" or \"Thursday\" or \"Friday\" => \"Weekday\",\n    _ => \"Unknown\"    // _ is the default arm\n};\nConsole.WriteLine(dayType);\n\n// With guard conditions\nint score2 = 75;\nstring grade = score2 switch\n{\n    >= 90 => \"A\",\n    >= 80 => \"B\",\n    >= 70 => \"C\",\n    >= 60 => \"D\",\n    _     => \"F\"\n};\nConsole.WriteLine(grade);  // C\n```\n\n## Pattern Matching\n\n```csharp\nobject value = 42;\n\n// Type pattern\nif (value is int n)\n    Console.WriteLine($\"It's an int: {n}\");\n\n// Switch expression with type patterns\nstring Describe(object obj) => obj switch\n{\n    int i    => $\"Integer: {i}\",\n    double d => $\"Double: {d:F2}\",\n    string s => $\"String of length {s.Length}\",\n    null     => \"It's null\",\n    _        => \"Unknown type\"\n};\n\nConsole.WriteLine(Describe(42));       // Integer: 42\nConsole.WriteLine(Describe(3.14));     // Double: 3.14\nConsole.WriteLine(Describe(\"hello\"));  // String of length 5\n```", 30, 1, "If-Else Statements & Switch Expressions", "If-else statements let your program make decisions based on conditions. The ternary operator is a compact one-line if-else. Switch expressions, introduced in C sharp 8, return a value directly and are more concise than traditional switch statements. Pattern matching in C sharp lets you branch on the type or value of an object using the is keyword and switch expressions." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Prefer `foreach` over `for` when you just need to iterate a collection without tracking an index\n- Use `for` when you need the index, or need to iterate backwards or in steps\n- Use `while` when the loop count depends on a runtime condition (user input, network, etc.)\n- Never modify a collection while iterating it with `foreach` — use `for` or iterate a copy instead\n- Avoid infinite loops — ensure your loop condition will eventually become false\n- Break complex nested loops into separate methods for readability", "# Loops\n\nLoops repeat a block of code while a condition is true, or for a specific number of iterations.\n\n## For Loop\n\nBest when you know how many iterations you need:\n\n```csharp\n// Basic: count up\nfor (int i = 0; i < 5; i++)\n{\n    Console.WriteLine($\"Iteration {i}\");\n}\n// Output: Iteration 0, 1, 2, 3, 4\n\n// Count down\nfor (int i = 10; i >= 1; i--)\n    Console.Write(i + \" \");\n// Output: 10 9 8 7 6 5 4 3 2 1\n\n// Step by 2\nfor (int i = 0; i <= 10; i += 2)\n    Console.Write(i + \" \");\n// Output: 0 2 4 6 8 10\n```\n\n## While Loop\n\nBest when you don't know the iteration count in advance:\n\n```csharp\nint n = 1;\nwhile (n <= 10)\n{\n    Console.Write(n + \" \");\n    n++;\n}\n// Output: 1 2 3 4 5 6 7 8 9 10\n\n// Reading input until valid\nstring? input;\ndo\n{\n    Console.Write(\"Enter 'quit' to exit: \");\n    input = Console.ReadLine();\n    Console.WriteLine($\"You entered: {input}\");\n} while (input != \"quit\");\n```\n\n## Do-While Loop\n\nGuarantees the body runs **at least once**:\n\n```csharp\nint attempt = 0;\ndo\n{\n    Console.WriteLine($\"Attempt #{attempt + 1}\");\n    attempt++;\n} while (attempt < 3);\n// Output: Attempt #1, Attempt #2, Attempt #3\n```\n\n## Foreach Loop\n\nBest for iterating over collections — clean and concise:\n\n```csharp\nstring[] fruits = { \"Apple\", \"Banana\", \"Cherry\", \"Date\" };\n\nforeach (string fruit in fruits)\n{\n    Console.WriteLine(fruit);\n}\n\n// Works with any IEnumerable\nvar numbers = new List<int> { 1, 2, 3, 4, 5 };\nforeach (var num in numbers)\n{\n    Console.Write(num * num + \" \");  // 1 4 9 16 25\n}\n```\n\n## Break & Continue\n\n```csharp\n// break — exits the loop entirely\nfor (int i = 0; i < 10; i++)\n{\n    if (i == 5)\n        break;               // stops at 5\n    Console.Write(i + \" \");  // 0 1 2 3 4\n}\n\n// continue — skips current iteration\nfor (int i = 0; i < 10; i++)\n{\n    if (i % 2 == 0)\n        continue;            // skip even numbers\n    Console.Write(i + \" \");  // 1 3 5 7 9\n}\n```\n\n## Nested Loops\n\n```csharp\n// Multiplication table\nfor (int row = 1; row <= 5; row++)\n{\n    for (int col = 1; col <= 5; col++)\n    {\n        Console.Write($\"{row * col,4}\");  // ,4 = right-align in 4 chars\n    }\n    Console.WriteLine();\n}\n```\n\n## Loop Best Practices & Common Patterns\n\n```csharp\n// Sum all numbers 1-100\nint sum = 0;\nfor (int i = 1; i <= 100; i++)\n    sum += i;\nConsole.WriteLine(sum);  // 5050\n\n// Find first element matching a condition\nint[] data = { 3, 7, 1, 9, 4, 6 };\nint target = -1;\nforeach (int x in data)\n{\n    if (x > 5)\n    {\n        target = x;\n        break;  // Stop once found\n    }\n}\nConsole.WriteLine(target);  // 7\n\n// Collecting results in a list\nvar evens = new List<int>();\nfor (int i = 1; i <= 20; i++)\n    if (i % 2 == 0)\n        evens.Add(i);\n// evens: 2, 4, 6, ..., 20\n```", 3, 30, 2, "Loops — For, While, Do-While & Foreach", 2, "C sharp has four main loop types. The for loop is used for a known number of iterations with an initialiser, condition, and increment. The while loop runs while a condition is true. The do-while loop runs at least once before checking its condition. The foreach loop cleanly iterates every element in a collection. Use break to exit a loop early and continue to skip to the next iteration." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Keep methods short and focused on a single task (Single Responsibility Principle)\n- Use expression-bodied methods for simple one-liners to improve readability\n- Limit the number of parameters to 3-4; if you need more, consider a parameter object\n- Prefer returning a value over using `out` parameters when possible\n- Name methods with a verb that describes what they do: `CalculateTax`, `GetUser`, `IsValid`\n- Avoid deep recursion on large inputs — use iteration instead to prevent stack overflow", "# Methods in C#\n\nA **method** is a named block of code that performs a specific task. Methods allow you to write code once and reuse it throughout your program.\n\n## Basic Method Syntax\n\n```csharp\n// returnType MethodName(parameters)\nvoid SayHello()\n{\n    Console.WriteLine(\"Hello!\");\n}\n\n// Call it:\nSayHello();  // Hello!\n```\n\n## Methods with Parameters\n\n```csharp\nvoid Greet(string name)\n{\n    Console.WriteLine($\"Hello, {name}!\");\n}\n\nGreet(\"Alice\");  // Hello, Alice!\nGreet(\"Bob\");    // Hello, Bob!\n```\n\nMultiple parameters:\n\n```csharp\nvoid PrintSum(int a, int b)\n{\n    Console.WriteLine($\"{a} + {b} = {a + b}\");\n}\n\nPrintSum(3, 5);   // 3 + 5 = 8\nPrintSum(10, 20); // 10 + 20 = 30\n```\n\n## Return Values\n\n```csharp\n// Return type before method name\nint Add(int a, int b)\n{\n    return a + b;\n}\n\ndouble CircleArea(double radius)\n{\n    return Math.PI * radius * radius;\n}\n\nint result = Add(5, 3);\nConsole.WriteLine(result);           // 8\nConsole.WriteLine(CircleArea(5.0));  // 78.539...\n```\n\n## Expression-Bodied Methods (C# 6+)\n\nFor single-expression methods, use the `=>` arrow:\n\n```csharp\nint Add(int a, int b) => a + b;\ndouble CircleArea(double r) => Math.PI * r * r;\nstring Greet(string name) => $\"Hello, {name}!\";\nbool IsEven(int n) => n % 2 == 0;\n```\n\n## Optional (Default) Parameters\n\n```csharp\nvoid CreateUser(string name, string role = \"User\", bool isActive = true)\n{\n    Console.WriteLine($\"Name: {name}, Role: {role}, Active: {isActive}\");\n}\n\nCreateUser(\"Alice\");                        // Alice, User, True\nCreateUser(\"Bob\", \"Admin\");                // Bob, Admin, True\nCreateUser(\"Charlie\", isActive: false);    // Named argument\n```\n\n## Named Arguments\n\n```csharp\nvoid DisplayInfo(string firstName, string lastName, int age)\n{\n    Console.WriteLine($\"{firstName} {lastName}, Age: {age}\");\n}\n\n// Use names to pass in any order\nDisplayInfo(lastName: \"Mensah\", age: 30, firstName: \"Ama\");\n```\n\n## Method Overloading\n\nMultiple methods with the same name but different parameter lists:\n\n```csharp\nint Multiply(int a, int b) => a * b;\ndouble Multiply(double a, double b) => a * b;\nint Multiply(int a, int b, int c) => a * b * c;\n\nConsole.WriteLine(Multiply(3, 4));         // 12\nConsole.WriteLine(Multiply(3.0, 4.5));    // 13.5\nConsole.WriteLine(Multiply(2, 3, 4));     // 24\n```\n\nThe compiler selects the correct overload based on the argument types.\n\n## Recursion\n\nA method that calls itself:\n\n```csharp\nint Factorial(int n)\n{\n    if (n <= 1) return 1;          // Base case\n    return n * Factorial(n - 1);   // Recursive call\n}\n\nConsole.WriteLine(Factorial(5));  // 120 (5*4*3*2*1)\nConsole.WriteLine(Factorial(10)); // 3628800\n```\n\n## Out Parameters\n\nReturn multiple values from a method:\n\n```csharp\nbool TryDivide(int a, int b, out double result)\n{\n    if (b == 0)\n    {\n        result = 0;\n        return false;\n    }\n    result = (double)a / b;\n    return true;\n}\n\nif (TryDivide(10, 3, out double answer))\n    Console.WriteLine($\"Result: {answer:F2}\");  // Result: 3.33\n```\n\n## Params — Variable Number of Arguments\n\n```csharp\nint Sum(params int[] numbers)\n{\n    int total = 0;\n    foreach (int n in numbers)\n        total += n;\n    return total;\n}\n\nConsole.WriteLine(Sum(1, 2, 3));           // 6\nConsole.WriteLine(Sum(10, 20, 30, 40));    // 100\nConsole.WriteLine(Sum());                  // 0\n```", 4, 35, "Defining & Calling Methods", 4, "Methods are named blocks of reusable code. They can take parameters as inputs and return values as outputs. Expression-bodied methods with the arrow syntax provide a concise one-line form. Method overloading lets you define multiple methods with the same name but different parameter types. Recursion is when a method calls itself — always define a base case to stop it. The params keyword lets a method accept a variable number of arguments." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Use `Array.Sort` before `Array.BinarySearch` — binary search requires sorted data\n- Prefer `List<T>` over arrays when the size may change at runtime\n- Use index-from-end syntax `[^1]` for the last element instead of `[array.Length - 1]`\n- Use range slices `[2..5]` for clean sub-array extraction\n- Always check `Length` before accessing elements to avoid `IndexOutOfRangeException`", "# Arrays in C#\n\nAn **array** is a fixed-size, ordered collection of elements of the same type.\n\n## Declaring & Initialising Arrays\n\n```csharp\n// Declare with size — elements initialised to default (0 for int)\nint[] scores = new int[5];\nscores[0] = 95;\nscores[1] = 87;\nscores[2] = 72;\n\n// Initialiser syntax — size inferred\nstring[] days = { \"Mon\", \"Tue\", \"Wed\", \"Thu\", \"Fri\" };\n\n// Combined syntax\ndouble[] prices = new double[] { 9.99, 14.99, 4.50 };\n\n// Implicit type\nvar names = new[] { \"Alice\", \"Bob\", \"Charlie\" };\n```\n\n## Accessing Elements\n\nArrays are **zero-indexed** — the first element is at index 0:\n\n```csharp\nstring[] fruits = { \"Apple\", \"Banana\", \"Cherry\" };\n\nConsole.WriteLine(fruits[0]);   // Apple\nConsole.WriteLine(fruits[2]);   // Cherry\nConsole.WriteLine(fruits[^1]);  // Cherry  — index from end (C# 8+)\nConsole.WriteLine(fruits[^2]);  // Banana\n\n// Array length\nConsole.WriteLine(fruits.Length);   // 3\n```\n\n## Iterating Arrays\n\n```csharp\nint[] numbers = { 10, 20, 30, 40, 50 };\n\n// For loop (use when you need the index)\nfor (int i = 0; i < numbers.Length; i++)\n{\n    Console.WriteLine($\"[{i}] = {numbers[i]}\");\n}\n\n// Foreach (use when you just need values)\nforeach (int n in numbers)\n    Console.Write(n + \" \");\n```\n\n## Common Array Operations\n\n```csharp\nint[] data = { 5, 2, 8, 1, 9, 3 };\n\n// Sort\nArray.Sort(data);\n// data: 1, 2, 3, 5, 8, 9\n\n// Reverse\nArray.Reverse(data);\n// data: 9, 8, 5, 3, 2, 1\n\n// Search (array must be sorted for BinarySearch)\nArray.Sort(data);\nint index = Array.BinarySearch(data, 5);\nConsole.WriteLine(index);  // index of 5\n\n// Find\nint firstOver5 = Array.Find(data, x => x > 5);\n\n// Copy\nint[] copy = new int[data.Length];\nArray.Copy(data, copy, data.Length);\n```\n\n## Multi-Dimensional Arrays\n\n```csharp\n// 2D array (matrix)\nint[,] matrix = new int[3, 3];\nmatrix[0, 0] = 1;\nmatrix[1, 1] = 5;\nmatrix[2, 2] = 9;\n\n// 2D initialiser\nint[,] grid = {\n    { 1, 2, 3 },\n    { 4, 5, 6 },\n    { 7, 8, 9 }\n};\n\n// Access element at row 1, col 2\nConsole.WriteLine(grid[1, 2]);  // 6\n\n// Iterate\nfor (int row = 0; row < 3; row++)\n{\n    for (int col = 0; col < 3; col++)\n        Console.Write($\"{grid[row, col]} \");\n    Console.WriteLine();\n}\n```\n\n## Jagged Arrays (Array of Arrays)\n\n```csharp\nint[][] jagged = new int[3][];\njagged[0] = new int[] { 1, 2 };\njagged[1] = new int[] { 3, 4, 5 };\njagged[2] = new int[] { 6 };\n\nforeach (var row in jagged)\n{\n    foreach (var val in row)\n        Console.Write(val + \" \");\n    Console.WriteLine();\n}\n```\n\n## Array Ranges & Slices (C# 8+)\n\n```csharp\nint[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };\n\nint[] slice1 = nums[2..5];    // { 2, 3, 4 }\nint[] slice2 = nums[..3];     // { 0, 1, 2 }\nint[] slice3 = nums[7..];     // { 7, 8, 9 }\nint[] last3  = nums[^3..];    // { 7, 8, 9 }\n```", 5, 30, "Arrays in C#", 2, "Arrays store fixed-size, ordered collections of the same type. They are zero-indexed, meaning the first element is at index zero. Use Array dot Sort and Array dot Reverse for in-place sorting. Multi-dimensional arrays represent matrices, and jagged arrays are arrays of arrays with varying row sizes. C sharp 8 added index-from-end syntax and range slices for cleaner element access." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Use `List<T>` as your default collection — it's flexible, typed, and has great LINQ support\n- Use `Dictionary<TKey, TValue>` for O(1) key lookups instead of linear search over a list\n- Always use `TryGetValue` instead of direct dictionary indexing when the key may not exist\n- Use `HashSet<T>` when you need unique elements and fast membership testing\n- Use `GetValueOrDefault(key, defaultValue)` for concise dictionary access with a fallback", "# List<T> & Dictionary<TKey, TValue>\n\n## List<T> — Dynamic Array\n\n`List<T>` is the most commonly used collection — a dynamically-resizable, ordered list of typed elements.\n\n```csharp\n// Create\nvar fruits = new List<string>();\n\n// Add elements\nfruits.Add(\"Apple\");\nfruits.Add(\"Banana\");\nfruits.Add(\"Cherry\");\nfruits.AddRange(new[] { \"Date\", \"Elderberry\" });\n\n// Access\nConsole.WriteLine(fruits[0]);        // Apple\nConsole.WriteLine(fruits.Count);     // 5\n\n// Insert at specific position\nfruits.Insert(1, \"Avocado\");  // Insert at index 1\n\n// Remove\nfruits.Remove(\"Banana\");             // Remove by value\nfruits.RemoveAt(0);                   // Remove by index\n\n// Contains\nbool hasApple = fruits.Contains(\"Apple\");  // true\n\n// Find\nstring? first = fruits.Find(f => f.StartsWith(\"C\"));  // Cherry\n\n// Sort\nfruits.Sort();                        // Alphabetical\nfruits.Sort((a, b) => b.CompareTo(a)); // Reverse alphabetical\n\n// Convert to array\nstring[] arr = fruits.ToArray();\n```\n\n## List<T> — Initialiser Syntax\n\n```csharp\nvar scores = new List<int> { 95, 87, 72, 91, 68 };\n\n// LINQ on List\nint max   = scores.Max();             // 95\nint min   = scores.Min();             // 68\ndouble avg = scores.Average();        // 82.6\nint sum   = scores.Sum();             // 413\n\n// Filter\nvar passing = scores.Where(s => s >= 70).ToList();\n\n// Iterate\nforeach (int s in scores)\n    Console.WriteLine(s);\n```\n\n## Dictionary<TKey, TValue> — Key-Value Lookup\n\nA `Dictionary` stores key-value pairs, providing O(1) average lookup by key.\n\n```csharp\n// Create\nvar capitals = new Dictionary<string, string>\n{\n    { \"Ghana\", \"Accra\" },\n    { \"Nigeria\", \"Abuja\" },\n    { \"Kenya\", \"Nairobi\" }\n};\n\n// Add\ncapitals[\"Egypt\"] = \"Cairo\";\ncapitals.Add(\"South Africa\", \"Pretoria\");\n\n// Access by key\nConsole.WriteLine(capitals[\"Ghana\"]);   // Accra\n\n// Safe access — avoid KeyNotFoundException\nif (capitals.TryGetValue(\"Uganda\", out string? capital))\n    Console.WriteLine(capital);\nelse\n    Console.WriteLine(\"Not found\");\n\n// Check existence\nbool hasNigeria = capitals.ContainsKey(\"Nigeria\");   // true\n\n// Remove\ncapitals.Remove(\"Egypt\");\n\n// Iterate\nforeach (var kvp in capitals)\n    Console.WriteLine($\"{kvp.Key}: {kvp.Value}\");\n\n// Iterate keys or values\nforeach (string country in capitals.Keys)\n    Console.WriteLine(country);\n\nforeach (string city in capitals.Values)\n    Console.WriteLine(city);\n```\n\n## Word Frequency — Common Dictionary Pattern\n\n```csharp\nstring text = \"the quick brown fox jumps over the lazy dog the fox\";\nvar words = text.Split(' ');\nvar freq = new Dictionary<string, int>();\n\nforeach (string word in words)\n{\n    if (freq.ContainsKey(word))\n        freq[word]++;\n    else\n        freq[word] = 1;\n    // Or: freq.TryGetValue(word, out int c); freq[word] = c + 1;\n    // Or (cleaner): freq[word] = freq.GetValueOrDefault(word, 0) + 1;\n}\n\nforeach (var kvp in freq.OrderByDescending(k => k.Value))\n    Console.WriteLine($\"{kvp.Key}: {kvp.Value}\");\n```\n\n## Other Useful Collections\n\n```csharp\n// HashSet<T> — unique elements, O(1) lookup\nvar unique = new HashSet<int> { 1, 2, 3 };\nunique.Add(2);  // Ignored — already exists\nunique.Add(4);\nConsole.WriteLine(unique.Count);  // 4\n\n// Stack<T> — LIFO\nvar stack = new Stack<string>();\nstack.Push(\"first\");\nstack.Push(\"second\");\nstack.Push(\"third\");\nConsole.WriteLine(stack.Pop());    // third\nConsole.WriteLine(stack.Peek());   // second (doesn't remove)\n\n// Queue<T> — FIFO\nvar queue = new Queue<string>();\nqueue.Enqueue(\"first\");\nqueue.Enqueue(\"second\");\nConsole.WriteLine(queue.Dequeue()); // first\nConsole.WriteLine(queue.Peek());    // second\n```", 5, 35, 2, "List<T> & Dictionary<TKey, TValue>", 4, "List of T is a dynamic array that grows automatically. Use Add, Remove, Contains, and Sort to manage elements. Dictionary of TKey TValue stores key-value pairs with O(1) average lookup. Always use TryGetValue for safe key access. HashSet provides unique element storage. Stack is last-in-first-out and Queue is first-in-first-out." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Always use string interpolation `$\"...\"` instead of concatenation with `+` for multi-variable strings\n- Use `StringBuilder` when concatenating in a loop (10+ iterations) for performance\n- Use `string.IsNullOrWhiteSpace` rather than `== null || == \"\"` for robust null/empty checks\n- Use `StringComparison.OrdinalIgnoreCase` for case-insensitive comparisons to be explicit and culture-safe\n- Use raw string literals `\"\"\"...\"\"\"` (C# 11+) for multi-line strings that contain quotes or backslashes", "# String Manipulation & Formatting\n\n## Strings are Immutable\n\nIn C#, strings are **immutable** — once created, they cannot be changed. Every string operation that \"modifies\" a string actually creates a new one.\n\n```csharp\nstring s = \"hello\";\ns.ToUpper();    // Creates a new string, but doesn't change s!\nConsole.WriteLine(s);          // hello (unchanged)\n\nstring upper = s.ToUpper();    // Capture the result\nConsole.WriteLine(upper);      // HELLO\n```\n\n## Common String Methods\n\n```csharp\nstring text = \"  Hello, C# Academy!  \";\n\n// Case\nConsole.WriteLine(text.ToUpper());           // \"  HELLO, C# ACADEMY!  \"\nConsole.WriteLine(text.ToLower());           // \"  hello, c# academy!  \"\n\n// Trim whitespace\nConsole.WriteLine(text.Trim());              // \"Hello, C# Academy!\"\nConsole.WriteLine(text.TrimStart());         // \"Hello, C# Academy!  \"\nConsole.WriteLine(text.TrimEnd());           // \"  Hello, C# Academy!\"\n\n// Contains, StartsWith, EndsWith\nConsole.WriteLine(text.Contains(\"Academy\")); // True\nConsole.WriteLine(text.StartsWith(\"  He\"));  // True\nConsole.WriteLine(text.EndsWith(\"!  \"));     // True\n\n// Length\nConsole.WriteLine(\"hello\".Length);           // 5\n\n// Replace\nstring replaced = text.Replace(\"Academy\", \"World\");\n\n// Split\nstring csv = \"Alice,Bob,Charlie,Dave\";\nstring[] names = csv.Split(',');\n// names: [\"Alice\", \"Bob\", \"Charlie\", \"Dave\"]\n\n// Join\nstring joined = string.Join(\" | \", names);\n// \"Alice | Bob | Charlie | Dave\"\n\n// Substring\nstring sub = \"Hello, World!\".Substring(7, 5);  // \"World\"\n// Or with range: \"Hello, World!\"[7..12] = \"World\"\n\n// IndexOf\nint pos = \"Hello, World!\".IndexOf('W');   // 7\n\n// Remove\nstring removed = \"Hello, World!\".Remove(5, 7); // \"Hello!\"\n```\n\n## String Interpolation\n\nThe preferred way to build strings with embedded values:\n\n```csharp\nstring name = \"Alice\";\nint age = 25;\ndouble score = 98.7654;\n\n// Basic interpolation\nConsole.WriteLine($\"Name: {name}, Age: {age}\");\n\n// Format specifiers inside {}\nConsole.WriteLine($\"Score: {score:F2}\");         // 98.77 (2 decimals)\nConsole.WriteLine($\"Score: {score:P1}\");         // 9,876.5% (percentage)\nConsole.WriteLine($\"Price: {9.99m:C}\");          // $9.99 (currency)\nConsole.WriteLine($\"Hex: {255:X}\");              // FF\nConsole.WriteLine($\"Date: {DateTime.Now:dd/MM/yyyy}\");\n\n// Padding and alignment\nConsole.WriteLine($\"{\"Left\",-10}|{\"Right\",10}\"); // Left-align, right-align\nConsole.WriteLine($\"{42,8}\");                    // Right-aligned in 8 chars\n```\n\n## Verbatim & Raw String Literals\n\n```csharp\n// Verbatim string — backslashes treated literally\nstring path = @\"C:\\Users\\Alice\\Documents\\file.txt\";\n\n// Multi-line verbatim\nstring json = @\"{\n    \"\"name\"\": \"\"Alice\"\",\n    \"\"age\"\": 25\n}\";\n\n// Raw string literal (C# 11+) — no escaping needed\nstring raw = \"\"\"\n    {\n        \"name\": \"Alice\",\n        \"age\": 25\n    }\n    \"\"\";\n```\n\n## String Comparison\n\n```csharp\nstring a = \"Hello\";\nstring b = \"hello\";\n\n// Case-sensitive (default)\nConsole.WriteLine(a == b);                    // False\nConsole.WriteLine(a.Equals(b));               // False\n\n// Case-insensitive\nConsole.WriteLine(a.Equals(b, StringComparison.OrdinalIgnoreCase));  // True\nConsole.WriteLine(string.Compare(a, b, ignoreCase: true));           // 0\n\n// Check null or empty\nstring? s = null;\nConsole.WriteLine(string.IsNullOrEmpty(s));        // True\nConsole.WriteLine(string.IsNullOrWhiteSpace(\"  \")); // True\n```\n\n## StringBuilder — For Performance\n\nWhen building strings in a loop, use `StringBuilder` to avoid creating many intermediate string objects:\n\n```csharp\nusing System.Text;\n\n// Slow — creates 1000 strings\nstring result = \"\";\nfor (int i = 0; i < 1000; i++)\n    result += i.ToString();\n\n// Fast — single buffer, mutates in place\nvar sb = new StringBuilder();\nfor (int i = 0; i < 1000; i++)\n    sb.Append(i);\n\nstring fastResult = sb.ToString();\n\n// Other StringBuilder methods\nsb.AppendLine(\"Next line\");\nsb.Insert(0, \"Start: \");\nsb.Replace(\"old\", \"new\");\n```", 6, 30, "String Manipulation & Formatting", 4, "Strings in C sharp are immutable, so every method returns a new string. Key methods include ToUpper, ToLower, Trim, Contains, Split, Join, Replace, and Substring. String interpolation with the dollar sign prefix embeds variables and format specifiers directly in strings. Use StringBuilder when building strings in loops to avoid creating many temporary string objects." });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "BestPractices", "Content", "CourseModuleId", "CreatedAt", "DurationMinutes", "Order", "Title", "Type", "UpdatedAt", "VoiceSummary" },
                values: new object[,]
                {
                    { 12, "- Keep fields `private` and expose data through properties — this is encapsulation\n- Use `readonly` fields or `init`-only properties for immutable data\n- Override `ToString()` on your classes to make debugging and logging cleaner\n- Use `: this(...)` constructor chaining to avoid duplicating initialisation logic\n- Validate inputs in constructors and setters to maintain object invariants\n- Use `nameof(paramName)` in exceptions instead of hard-coded strings — safer for refactoring", "# Classes, Objects & Constructors\n\n## What is a Class?\n\nA **class** is a blueprint for creating objects. It defines the data (fields/properties) and behaviour (methods) that objects of that class will have.\n\n```csharp\npublic class BankAccount\n{\n    // Fields (private data)\n    private string _owner;\n    private decimal _balance;\n\n    // Constructor — called when creating an object\n    public BankAccount(string owner, decimal initialBalance)\n    {\n        _owner = owner;\n        _balance = initialBalance;\n    }\n\n    // Property (public access to private data)\n    public string Owner => _owner;\n    public decimal Balance => _balance;\n\n    // Methods (behaviour)\n    public void Deposit(decimal amount)\n    {\n        if (amount <= 0)\n            throw new ArgumentException(\"Deposit amount must be positive.\");\n        _balance += amount;\n        Console.WriteLine($\"Deposited {amount:C}. Balance: {_balance:C}\");\n    }\n\n    public bool Withdraw(decimal amount)\n    {\n        if (amount > _balance)\n        {\n            Console.WriteLine(\"Insufficient funds.\");\n            return false;\n        }\n        _balance -= amount;\n        Console.WriteLine($\"Withdrew {amount:C}. Balance: {_balance:C}\");\n        return true;\n    }\n\n    public override string ToString() =>\n        $\"Account[{_owner}]: {_balance:C}\";\n}\n```\n\n## Creating & Using Objects\n\n```csharp\n// Instantiate with new keyword\nvar account = new BankAccount(\"Alice\", 1000m);\n\naccount.Deposit(500m);      // Deposited $500.00. Balance: $1,500.00\naccount.Withdraw(200m);     // Withdrew $200.00.  Balance: $1,300.00\naccount.Withdraw(2000m);    // Insufficient funds.\n\nConsole.WriteLine(account); // Account[Alice]: $1,300.00\nConsole.WriteLine(account.Balance);  // 1300\n```\n\n## Properties in Detail\n\nProperties expose data with optional logic in getters and setters:\n\n```csharp\npublic class Person\n{\n    // Auto-property — compiler generates backing field\n    public string FirstName { get; set; } = \"\";\n    public string LastName  { get; set; } = \"\";\n\n    // Computed property — no backing field\n    public string FullName => $\"{FirstName} {LastName}\";\n\n    // Property with validation in setter\n    private int _age;\n    public int Age\n    {\n        get => _age;\n        set\n        {\n            if (value < 0 || value > 150)\n                throw new ArgumentOutOfRangeException(nameof(value), \"Age must be 0-150.\");\n            _age = value;\n        }\n    }\n\n    // Init-only property (C# 9+) — set only in constructor or initialiser\n    public DateTime DateOfBirth { get; init; }\n}\n\n// Object initialiser syntax (requires parameterless constructor or init properties)\nvar person = new Person\n{\n    FirstName = \"Ama\",\n    LastName = \"Mensah\",\n    Age = 28,\n    DateOfBirth = new DateTime(1996, 3, 15)\n};\n\nConsole.WriteLine(person.FullName);     // Ama Mensah\n```\n\n## Multiple Constructors (Constructor Overloading)\n\n```csharp\npublic class Rectangle\n{\n    public double Width  { get; }\n    public double Height { get; }\n\n    // Parameterless constructor — square\n    public Rectangle() : this(1.0, 1.0) { }\n\n    // Single parameter — square with given side\n    public Rectangle(double side) : this(side, side) { }\n\n    // Full constructor\n    public Rectangle(double width, double height)\n    {\n        Width  = width;\n        Height = height;\n    }\n\n    public double Area()      => Width * Height;\n    public double Perimeter() => 2 * (Width + Height);\n\n    public override string ToString() =>\n        $\"Rectangle({Width}×{Height}) Area={Area()}\";\n}\n\nvar r1 = new Rectangle();           // 1×1\nvar r2 = new Rectangle(5.0);        // 5×5\nvar r3 = new Rectangle(4.0, 6.0);   // 4×6\n\nConsole.WriteLine(r3.Area());       // 24\nConsole.WriteLine(r3);              // Rectangle(4×6) Area=24\n```\n\n## Static Members\n\nStatic members belong to the class itself, not to any instance:\n\n```csharp\npublic class MathHelper\n{\n    public static double Pi = 3.14159265358979;\n\n    public static double CircleArea(double r) => Pi * r * r;\n    public static int Clamp(int value, int min, int max) =>\n        Math.Max(min, Math.Min(max, value));\n}\n\n// Call without creating an instance\nConsole.WriteLine(MathHelper.CircleArea(5)); // 78.539...\nConsole.WriteLine(MathHelper.Clamp(150, 0, 100)); // 100\n```", 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "Classes, Objects & Constructors", 4, null, "A class is a blueprint for objects, defining their data as properties and their behaviour as methods. The constructor runs when an object is created with the new keyword. Properties expose private fields with optional validation logic. Use init-only properties for immutable data. Static members belong to the class itself, not to individual instances. Override ToString to get useful text representations of your objects." },
                    { 20, "- Default to `private` for fields — expose only what's necessary through properties\n- Use auto-properties for simple get/set; full properties when you need validation\n- Use `init` setters for properties that should only be set during object creation\n- Prefer records for immutable data transfer objects (DTOs) and value objects\n- Never expose collection fields directly — return read-only views instead\n- Use `required` keyword (C# 11+) to enforce mandatory properties at compile time", "# Encapsulation, Properties & Access Modifiers\n\n## What is Encapsulation?\n\n**Encapsulation** is the OOP principle of bundling data (fields) and the methods that operate on that data into a single unit (class), while restricting direct access to the internal state. This protects data integrity and hides implementation details.\n\n```csharp\n// ❌ BAD — public fields, no control\npublic class BadBankAccount\n{\n    public decimal Balance;  // Anyone can set this to negative!\n}\n\n// ✅ GOOD — private field with controlled access\npublic class BankAccount\n{\n    private decimal _balance;\n\n    public decimal Balance => _balance;  // Read-only property\n\n    public void Deposit(decimal amount)\n    {\n        if (amount <= 0)\n            throw new ArgumentException(\"Deposit must be positive.\");\n        _balance += amount;\n    }\n\n    public bool Withdraw(decimal amount)\n    {\n        if (amount <= 0 || amount > _balance) return false;\n        _balance -= amount;\n        return true;\n    }\n}\n```\n\n## Access Modifiers\n\n| Modifier | Accessible From |\n|---|---|\n| `public` | Anywhere |\n| `private` | Same class only |\n| `protected` | Same class + derived classes |\n| `internal` | Same assembly (project) |\n| `protected internal` | Same assembly OR derived classes |\n| `private protected` | Same class + derived classes in same assembly |\n\n```csharp\npublic class Employee\n{\n    public   string Name { get; set; }        // Anywhere\n    private  decimal _salary;                 // This class only\n    protected int DepartmentId { get; set; }  // This + derived\n    internal string EmployeeCode { get; set; } // Same project\n}\n```\n\n**Rule of thumb:** Start with `private` and only widen access as needed.\n\n## Properties — The C# Way\n\nProperties look like fields but have getter/setter logic behind them.\n\n### Auto-Properties\n\n```csharp\npublic class Product\n{\n    // Auto-implemented — compiler generates the backing field\n    public string Name { get; set; } = string.Empty;\n    public decimal Price { get; set; }\n    public int Stock { get; private set; }  // Public get, private set\n\n    public void Restock(int quantity) => Stock += quantity;\n}\n```\n\n### Full Properties with Backing Fields\n\n```csharp\npublic class Temperature\n{\n    private double _celsius;\n\n    public double Celsius\n    {\n        get => _celsius;\n        set\n        {\n            if (value < -273.15)\n                throw new ArgumentException(\"Below absolute zero!\");\n            _celsius = value;\n        }\n    }\n\n    // Computed property — no backing field\n    public double Fahrenheit => (_celsius * 9.0 / 5.0) + 32;\n    public double Kelvin => _celsius + 273.15;\n}\n\nvar t = new Temperature { Celsius = 100 };\nConsole.WriteLine(t.Fahrenheit);  // 212\nConsole.WriteLine(t.Kelvin);     // 373.15\n```\n\n### Init-Only Properties (C# 9+)\n\n```csharp\npublic class User\n{\n    public int Id { get; init; }          // Can only be set during initialisation\n    public string Email { get; init; }\n    public string Name { get; set; }      // Can be changed later\n}\n\nvar user = new User { Id = 1, Email = \"alice@example.com\", Name = \"Alice\" };\nuser.Name = \"Alice M.\";    // ✅ OK\n// user.Id = 2;            // ❌ Compile error — init-only\n```\n\n### Required Properties (C# 11+)\n\n```csharp\npublic class Order\n{\n    public required string CustomerId { get; init; }\n    public required decimal Total { get; init; }\n    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;\n}\n\n// Must provide required properties or compiler error\nvar order = new Order { CustomerId = \"C001\", Total = 199.99m };\n```\n\n## Records — Immutable Data Types\n\n```csharp\n// Record class — immutable by default, value-based equality\npublic record Person(string FirstName, string LastName, int Age);\n\nvar p1 = new Person(\"Alice\", \"Mensah\", 25);\nvar p2 = new Person(\"Alice\", \"Mensah\", 25);\n\nConsole.WriteLine(p1 == p2);  // True — value equality\nConsole.WriteLine(p1);        // Person { FirstName = Alice, LastName = Mensah, Age = 25 }\n\n// With-expression — create a modified copy\nvar p3 = p1 with { Age = 26 };\nConsole.WriteLine(p3);        // Person { FirstName = Alice, LastName = Mensah, Age = 26 }\n```", 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 35, 1, "Encapsulation, Properties & Access Modifiers", 4, null, "Encapsulation means hiding internal data behind controlled access points. In C sharp, properties provide getter and setter methods wrapped in field-like syntax. Auto-properties let the compiler generate the backing field, while full properties give you validation control. Access modifiers like private, protected, internal, and public control visibility. Init-only and required properties enforce immutability and mandatory values at compile time." }
                });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] { false, 1, "Oracle" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 1, "Apple" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 2, "True" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 2, "False" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 3, "Common Language Runtime" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 3, "Common Logic Runtime" });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 3, "Compiled Language Resource", null },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 3, "Core Library Runtime", null },
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "dotnet run", null },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, "dotnet start", null },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, "csharp run", null },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, "dotnet execute", null }
                });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "Which company developed C#?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "C# is a statically-typed language.");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "QuizId", "Text" },
                values: new object[] { 1, "What does CLR stand for?" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CorrectAnswer", "QuizId", "Text" },
                values: new object[] { ".cs", 1, "The file extension for C# source files is ___" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Text", "Type" },
                values: new object[] { "Which command runs a .NET console application from the terminal?", 0 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "What is the output of: Console.WriteLine(2 + 3);", 3, null },
                    { 7, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Console.Write() adds a newline after the output.", 1, null },
                    { 8, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Which symbol starts a string interpolation expression in C#?", 0, null },
                    { 9, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Which escape sequence represents a tab character?", 0, null }
                });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "C# & .NET Basics Quiz");

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Hello World & Console I/O Quiz");

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "LessonId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Variables & Data Types Quiz", null },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Operators Quiz", null },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "Control Flow Quiz", null },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, "Loops Quiz", null },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "Methods Quiz", null },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Arrays Quiz", null },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "Collections Quiz", null },
                    { 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "String Manipulation Quiz", null }
                });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Title" },
                values: new object[] { "A programming language is a formal set of instructions used to communicate with a computer. C# is one such language that compiles into code the .NET runtime can execute.", "What is a Programming Language?" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CodeSample", "Content", "Title" },
                values: new object[] { null, "The .NET ecosystem includes the CLR (runtime), BCL (class libraries), SDK tools, and NuGet package ecosystem. Together they provide everything you need to build any kind of application.", "The .NET Ecosystem" });

            migrationBuilder.UpdateData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CodeSample", "Content", "LessonId", "Order", "Title" },
                values: new object[] { "Console.WriteLine(\"Hello, World!\");", "Output text with Console.WriteLine:", 1, 3, "Your First Line of Code" });

            migrationBuilder.InsertData(
                table: "TutorialSteps",
                columns: new[] { "Id", "CodeSample", "Content", "CreatedAt", "LessonId", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "// Single-line comment\n/* Multi-line\n   comment */", "Comments explain your code to other developers (including your future self) and are ignored by the compiler.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 4, "Comments in C#", null },
                    { 5, null, "Console is a built-in static class in the System namespace. It provides methods for reading input and writing output to the terminal.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1, "The Console Class", null },
                    { 6, "Console.Write(\"A\");\nConsole.Write(\"B\");\nConsole.WriteLine();\nConsole.WriteLine(\"C\");\n// Output: AB\n// C", "WriteLine adds a newline after output; Write does not.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, "WriteLine vs Write", null },
                    { 7, "Console.Write(\"Name: \");\nstring name = Console.ReadLine()!;\nConsole.WriteLine($\"Hello, {name}!\");", "Use Console.ReadLine() to capture a line of text from the user.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, "Reading User Input", null },
                    { 8, "int age = 25;\nstring city = \"Kumasi\";\nbool isStudent = true;", "A variable declaration specifies the type and name. An initialiser sets the starting value.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, "Declaring Variables", null },
                    { 9, "int a = 10;\nint b = a;  // b gets a copy\nb = 20;\nConsole.WriteLine(a); // still 10", "Value types store their data directly on the stack. Modifying one variable does not affect another.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2, "Value Types", null },
                    { 10, "var name = \"Alice\";  // string\nvar age  = 30;       // int\nvar pi   = 3.14;     // double", "The var keyword lets the compiler infer the type from the assignment. The type is still fixed at compile time.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 3, "Type Inference with var", null },
                    { 11, "const double Pi = 3.14159265358979;\nconst int DaysInWeek = 7;", "Use const for values that should never change. This communicates intent and prevents accidental modification.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 4, "Constants", null },
                    { 12, "int x = 10;\nif (x > 5)\n    Console.WriteLine(\"Big\");\nelse\n    Console.WriteLine(\"Small\");", "An if statement runs a block of code only when its condition is true. The optional else block runs when the condition is false.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, 1, "If-Else Basics", null },
                    { 13, "string result = dayOfWeek switch\n{\n    1 => \"Monday\",\n    2 => \"Tuesday\",\n    _ => \"Other\"\n};", "Switch expressions (C# 8+) are concise and return a value directly. The _ arm is the default case.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, 2, "Switch Expression", null },
                    { 14, "//  init   cond    iter\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);", "The for loop has three parts: initialiser, condition, and iterator.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, 1, "For Loop Anatomy", null },
                    { 15, "string[] colors = { \"Red\", \"Green\", \"Blue\" };\nforeach (string color in colors)\n    Console.WriteLine(color);", "Foreach cleanly iterates every element in a collection without managing an index variable.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, 2, "Foreach Loop", null },
                    { 16, "for (int i = 0; i < 10; i++)\n{\n    if (i == 7) break;\n    if (i % 2 == 0) continue;\n    Console.Write(i + \" \"); // 1 3 5\n}", "break exits the loop immediately. continue skips the rest of the current iteration and moves to the next.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, 3, "Break and Continue", null },
                    { 17, null, "Methods allow you to write code once and reuse it. They make programs easier to read, test, and maintain.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 1, "Why Methods?", null },
                    { 18, "int Add(int a, int b) => a + b;\nbool IsEven(int n) => n % 2 == 0;\nstring Greet(string name) => $\"Hello, {name}!\";", "For single-expression methods, use the => arrow for a concise one-liner.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 2, "Expression-Bodied Methods", null },
                    { 19, "int Factorial(int n)\n{\n    if (n <= 1) return 1; // base case\n    return n * Factorial(n - 1); // recursive call\n}", "A recursive method calls itself. Always define a base case to stop the recursion.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 3, "Recursion", null }
                });

            migrationBuilder.InsertData(
                table: "CodingExercises",
                columns: new[] { "Id", "CreatedAt", "Difficulty", "ExpectedOutput", "Hint", "Instructions", "LessonId", "Order", "StarterCode", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "600", "Class with decimal _balance. Deposit adds, Withdraw subtracts if sufficient funds.", "Create a BankAccount with owner \"Alice\" and balance 500. Deposit 200, then withdraw 100. Print the final balance.", 12, 1, "// Implement a BankAccount class with Deposit and Withdraw methods", "Create a BankAccount", null },
                    { 26, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "2", "Use a List<int> internally. Peek returns last element without removing.", "Implement a simple Stack<int> class with Push, Pop, Peek, and IsEmpty. Push 1,2,3. Pop once. Print Peek result.", 12, 2, "// Implement Stack<int> class\n// Push(1); Push(2); Push(3); Pop(); Console.WriteLine(Peek());", "Stack Implementation", null }
                });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[,]
                {
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30, 12, 1, 0, "OOP in C# — Classes & Objects", null, "https://www.youtube.com/watch?v=pTB0EiLXUC8" },
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 12, 2, 0, "Properties in C# Explained", null, "https://www.youtube.com/watch?v=PwDvHVQ8M0g" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "BestPractices", "Content", "CourseModuleId", "CreatedAt", "DurationMinutes", "Order", "Title", "Type", "UpdatedAt", "VoiceSummary" },
                values: new object[,]
                {
                    { 13, "- Favour composition over inheritance — \"has-a\" is often better than \"is-a\"\n- Only use inheritance when there is a genuine \"is-a\" relationship\n- Mark methods as `virtual` only if you intend them to be overridden\n- Use `sealed` on classes or methods to prevent unintended overriding\n- Prefer `is` pattern matching over `as` followed by null check — it's more concise\n- Use `base.Method()` when you want to extend (not fully replace) the base behaviour", "# Inheritance & Polymorphism\n\n## Inheritance\n\n**Inheritance** lets a class (derived/child) inherit members from another class (base/parent), promoting code reuse and establishing an \"is-a\" relationship.\n\n```csharp\n// Base class\npublic class Animal\n{\n    public string Name   { get; set; }\n    public int    Age    { get; set; }\n\n    public Animal(string name, int age)\n    {\n        Name = name;\n        Age  = age;\n    }\n\n    // virtual — can be overridden in derived classes\n    public virtual string Speak()\n    {\n        return \"...\";\n    }\n\n    public virtual void Describe()\n    {\n        Console.WriteLine($\"I am {Name}, a {GetType().Name} aged {Age}.\");\n    }\n}\n\n// Derived class\npublic class Dog : Animal\n{\n    public string Breed { get; set; }\n\n    public Dog(string name, int age, string breed)\n        : base(name, age)   // Call base constructor\n    {\n        Breed = breed;\n    }\n\n    // override — replaces the base implementation\n    public override string Speak() => \"Woof! Woof!\";\n\n    public override void Describe()\n    {\n        base.Describe();    // Call base method first\n        Console.WriteLine($\"I am a {Breed}.\");\n    }\n}\n\npublic class Cat : Animal\n{\n    public Cat(string name, int age) : base(name, age) { }\n\n    public override string Speak() => \"Meow!\";\n}\n```\n\n## Polymorphism\n\n**Polymorphism** means \"many forms\" — the same method call produces different behaviour depending on the actual runtime type:\n\n```csharp\n// All treated as Animals (base type)\nAnimal[] animals =\n{\n    new Dog(\"Rex\",   3, \"German Shepherd\"),\n    new Cat(\"Luna\",  2),\n    new Dog(\"Buddy\", 5, \"Labrador\"),\n    new Cat(\"Kitty\", 1)\n};\n\n// Same method call — different output per type\nforeach (Animal animal in animals)\n{\n    Console.WriteLine($\"{animal.Name} says: {animal.Speak()}\");\n}\n// Rex   says: Woof! Woof!\n// Luna  says: Meow!\n// Buddy says: Woof! Woof!\n// Kitty says: Meow!\n```\n\n## The `sealed` Keyword\n\nPrevent further inheritance with `sealed`:\n\n```csharp\npublic sealed class GoldenRetriever : Dog\n{\n    public GoldenRetriever(string name, int age)\n        : base(name, age, \"Golden Retriever\") { }\n\n    public override string Speak() => \"Bark! (very friendly)\";\n}\n\n// ERROR: cannot inherit from sealed class\n// public class GoldenRetrieverMix : GoldenRetriever { }\n```\n\n## Casting & Type Checking\n\n```csharp\nAnimal animal = new Dog(\"Rex\", 3, \"Husky\");\n\n// 'is' — type check\nif (animal is Dog)\n    Console.WriteLine(\"It's a dog!\");\n\n// 'is' with pattern — check and cast in one step\nif (animal is Dog dog)\n    Console.WriteLine($\"Breed: {dog.Breed}\");\n\n// 'as' — safe cast (returns null if fails, no exception)\nDog? d = animal as Dog;\nif (d != null)\n    Console.WriteLine(d.Breed);\n\n// Direct cast — throws InvalidCastException if type mismatch\nDog rex = (Dog)animal;\n```\n\n## Object — The Root of All Classes\n\nEvery class in C# implicitly inherits from `System.Object`, which provides:\n\n```csharp\nstring s = \"Hello\";\nConsole.WriteLine(s.ToString());       // Hello\nConsole.WriteLine(s.GetType());        // System.String\nConsole.WriteLine(s.Equals(\"Hello\")); // True\nConsole.WriteLine(s.GetHashCode());    // some int\n```", 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "Inheritance & Polymorphism", 1, null, "Inheritance lets a derived class reuse code from a base class. The colon syntax declares the parent, and the base keyword calls the parent's constructor or methods. Virtual methods in the base class can be overridden in derived classes. Polymorphism means a base class reference can point to any derived class object, and the correct overridden method is called at runtime. Use the is keyword for safe type checking and pattern matching." },
                    { 14, "- Program to interfaces, not implementations — depend on `ILogger`, not `ConsoleLogger`\n- Use abstract classes when related types share significant common behaviour\n- Use interfaces for contracts that unrelated types should satisfy (e.g., `IDisposable`, `IComparable`)\n- Keep interfaces small and focused — prefer many small interfaces over one fat interface (ISP)\n- Prefix interface names with `I` by convention: `IRepository`, `IPaymentService`", "# Interfaces & Abstract Classes\n\n## Abstract Classes\n\nAn **abstract class** cannot be instantiated directly. It defines a template with some methods implemented and some abstract (must be overridden):\n\n```csharp\npublic abstract class Shape\n{\n    public string Color { get; set; } = \"Black\";\n\n    // Abstract method — no implementation, MUST be overridden\n    public abstract double Area();\n    public abstract double Perimeter();\n\n    // Concrete method — has implementation, inherited as-is\n    public void PrintInfo()\n    {\n        Console.WriteLine($\"{GetType().Name}: Area={Area():F2}, Perimeter={Perimeter():F2}, Color={Color}\");\n    }\n}\n\npublic class Circle : Shape\n{\n    public double Radius { get; set; }\n    public Circle(double radius) => Radius = radius;\n\n    public override double Area()      => Math.PI * Radius * Radius;\n    public override double Perimeter() => 2 * Math.PI * Radius;\n}\n\npublic class Rectangle2 : Shape\n{\n    public double Width  { get; set; }\n    public double Height { get; set; }\n\n    public Rectangle2(double w, double h) { Width = w; Height = h; }\n\n    public override double Area()      => Width * Height;\n    public override double Perimeter() => 2 * (Width + Height);\n}\n\n// Usage\nShape[] shapes = { new Circle(5), new Rectangle2(4, 6) };\nforeach (var shape in shapes)\n    shape.PrintInfo();\n```\n\n## Interfaces\n\nAn **interface** defines a contract — a set of members that implementing classes must provide. Interfaces enable multiple \"type inheritance\" in C#.\n\n```csharp\npublic interface IDrawable\n{\n    void Draw();\n    string Color { get; set; }\n}\n\npublic interface IResizable\n{\n    void Resize(double factor);\n}\n\n// A class can implement multiple interfaces\npublic class GraphicCircle : IDrawable, IResizable\n{\n    public double Radius { get; private set; }\n    public string Color  { get; set; } = \"Red\";\n\n    public GraphicCircle(double radius) => Radius = radius;\n\n    public void Draw()\n    {\n        Console.WriteLine($\"Drawing a {Color} circle with radius {Radius}\");\n    }\n\n    public void Resize(double factor)\n    {\n        Radius *= factor;\n        Console.WriteLine($\"Resized to radius {Radius}\");\n    }\n}\n\nGraphicCircle gc = new GraphicCircle(5);\ngc.Draw();         // Drawing a Red circle with radius 5\ngc.Resize(2.0);    // Resized to radius 10\ngc.Draw();         // Drawing a Red circle with radius 10\n\n// Use interface as type — polymorphism\nIDrawable drawable = gc;\ndrawable.Draw();\n```\n\n## Interface Default Methods (C# 8+)\n\n```csharp\npublic interface ILogger\n{\n    void Log(string message);\n\n    // Default implementation — classes can optionally override\n    void LogError(string message) => Log($\"[ERROR] {message}\");\n    void LogInfo(string message)  => Log($\"[INFO]  {message}\");\n}\n\npublic class ConsoleLogger : ILogger\n{\n    public void Log(string message) => Console.WriteLine(message);\n    // LogError and LogInfo are inherited from the interface\n}\n\nvar logger = new ConsoleLogger();\nlogger.LogInfo(\"App started\");    // [INFO]  App started\nlogger.LogError(\"Crash!\");       // [ERROR] Crash!\n```\n\n## Interface vs Abstract Class\n\n| Feature | Abstract Class | Interface |\n|---|---|---|\n| Multiple inheritance | ❌ Single only | ✅ Multiple interfaces |\n| Constructor | ✅ Can have | ❌ Cannot have |\n| Fields | ✅ Can have | ❌ Properties only |\n| Default impl | ✅ Concrete methods | ✅ C# 8+ default methods |\n| Access modifiers | ✅ public/protected/private | ✅ All public by default |\n| Use when | Share code among related types | Define a contract for unrelated types |\n\n## Common Built-in Interfaces\n\n```csharp\n// IComparable<T> — defines natural ordering\npublic class Student : IComparable<Student>\n{\n    public string Name { get; set; }\n    public double GPA  { get; set; }\n\n    public int CompareTo(Student? other)\n    {\n        if (other is null) return 1;\n        return GPA.CompareTo(other.GPA); // Sort by GPA\n    }\n}\n\nvar students = new List<Student>\n{\n    new() { Name = \"Alice\", GPA = 3.8 },\n    new() { Name = \"Bob\",   GPA = 3.5 },\n    new() { Name = \"Carol\", GPA = 3.9 }\n};\nstudents.Sort();\n// Sorted: Bob (3.5), Alice (3.8), Carol (3.9)\n```", 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "Interfaces & Abstract Classes", 4, null, "Abstract classes define templates with some implemented and some abstract methods that derived classes must fill in. Interfaces define contracts — a set of members that any implementing class must provide. A class can implement multiple interfaces but only inherit from one class. Use abstract classes when related types share code, and interfaces when defining behaviour contracts across unrelated types." },
                    { 15, "- Always register dependencies in `Program.cs`, not scattered across the codebase\n- Prefer `Scoped` for database-related services, `Singleton` for stateless services\n- Never create `new SomeService()` directly — let the DI container manage object lifetimes\n- Use strongly-typed options (`IOptions<T>`) rather than raw `IConfiguration` access\n- Keep `Program.cs` clean — extract service registration to extension methods as the project grows", "# ASP.NET Core Project Structure & Dependency Injection\n\n## Creating a Web API Project\n\n```bash\ndotnet new webapi -n TodoApi --use-controllers\ncd TodoApi\ndotnet run\n```\n\nBrowse to `https://localhost:xxxx/swagger` to see the auto-generated API documentation.\n\n## Project Structure\n\n```\nTodoApi/\n├── Controllers/          ← API controllers (request handlers)\n├── Models/               ← Data models and DTOs\n├── Services/             ← Business logic\n├── Data/                 ← DbContext and migrations\n├── Program.cs            ← App bootstrap and DI registration\n├── appsettings.json      ← Configuration\n└── TodoApi.csproj        ← Project file\n```\n\n## Program.cs — The Composition Root\n\n```csharp\nvar builder = WebApplication.CreateBuilder(args);\n\n// ── Register services in the DI container ──\nbuilder.Services.AddControllers();\nbuilder.Services.AddEndpointsApiExplorer();\nbuilder.Services.AddSwaggerGen();\n\n// Register your own services\nbuilder.Services.AddScoped<ITodoService, TodoService>();\nbuilder.Services.AddSingleton<ILogger, ConsoleLogger>();\n\n// Add EF Core\nbuilder.Services.AddDbContext<AppDbContext>(options =>\n    options.UseSqlite(builder.Configuration.GetConnectionString(\"Default\")));\n\nvar app = builder.Build();\n\n// ── Configure the middleware pipeline ──\nif (app.Environment.IsDevelopment())\n{\n    app.UseSwagger();\n    app.UseSwaggerUI();\n}\n\napp.UseHttpsRedirection();\napp.UseAuthentication();\napp.UseAuthorization();\napp.MapControllers();\n\napp.Run();\n```\n\n## Dependency Injection\n\nDI is built into ASP.NET Core. Register services in `Program.cs`, then request them in constructors:\n\n```csharp\n// Service lifetimes:\n// Transient  — new instance per request to the DI container\n// Scoped     — new instance per HTTP request\n// Singleton  — one instance for the entire application\n\nbuilder.Services.AddTransient<IEmailSender, SmtpEmailSender>();\nbuilder.Services.AddScoped<IOrderService, OrderService>();\nbuilder.Services.AddSingleton<IConfiguration>(builder.Configuration);\n```\n\n```csharp\n// Controller receives its dependency via constructor injection\n[ApiController]\n[Route(\"api/[controller]\")]\npublic class TodosController : ControllerBase\n{\n    private readonly ITodoService _service;\n\n    public TodosController(ITodoService service)\n    {\n        _service = service;  // Injected by the DI container\n    }\n\n    [HttpGet]\n    public async Task<IActionResult> GetAll()\n    {\n        var todos = await _service.GetAllAsync();\n        return Ok(todos);\n    }\n}\n```\n\n## Configuration\n\n```json\n// appsettings.json\n{\n  \"ConnectionStrings\": {\n    \"Default\": \"Data Source=todo.db\"\n  },\n  \"JwtSettings\": {\n    \"SecretKey\": \"your-secret-key\",\n    \"Issuer\": \"TodoApi\",\n    \"ExpiryMinutes\": 60\n  },\n  \"Logging\": {\n    \"LogLevel\": {\n      \"Default\": \"Information\"\n    }\n  }\n}\n```\n\n```csharp\n// Reading config in a service\npublic class JwtService\n{\n    private readonly IConfiguration _config;\n\n    public JwtService(IConfiguration config)\n    {\n        _config = config;\n    }\n\n    public string GetIssuer() => _config[\"JwtSettings:Issuer\"]!;\n}\n\n// Strongly-typed options (preferred)\npublic class JwtSettings\n{\n    public string SecretKey    { get; set; } = \"\";\n    public string Issuer       { get; set; } = \"\";\n    public int    ExpiryMinutes { get; set; }\n}\n\n// In Program.cs:\nbuilder.Services.Configure<JwtSettings>(\n    builder.Configuration.GetSection(\"JwtSettings\"));\n\n// In a class:\npublic class TokenService\n{\n    private readonly JwtSettings _jwt;\n\n    public TokenService(IOptions<JwtSettings> opts)\n    {\n        _jwt = opts.Value;\n    }\n}\n```", 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 35, 1, "ASP.NET Core Project Structure & Dependency Injection", 0, null, "ASP.NET Core apps are bootstrapped in Program dot cs, where you register services with the DI container and configure the middleware pipeline. Dependency injection is a first-class citizen — register your services as Transient, Scoped, or Singleton, then request them via constructor parameters. Configuration is read from appsettings dot json and exposed via IConfiguration or strongly-typed IOptions." },
                    { 16, "- Always return typed `ActionResult<T>` from GET actions — provides better Swagger documentation\n- Use `CreatedAtAction` on POST — it sets the `Location` header to the new resource's URL\n- Return `NoContent()` (204) from PUT/DELETE, not `Ok()` with an empty body\n- Let `[ApiController]` handle model validation automatically — don't repeat validation logic\n- Use route constraints like `{id:int}` to prevent invalid routes reaching your controller\n- Never return internal exception details in production responses — use problem details", "# Building RESTful Controllers\n\n## REST Conventions\n\n| HTTP Verb | Route | Action | Success Code |\n|---|---|---|---|\n| GET | /api/items | List all items | 200 OK |\n| GET | /api/items/{id} | Get one item | 200 OK |\n| POST | /api/items | Create item | 201 Created |\n| PUT | /api/items/{id} | Replace item | 200 OK / 204 No Content |\n| PATCH | /api/items/{id} | Partial update | 200 OK |\n| DELETE | /api/items/{id} | Delete item | 204 No Content |\n\n## Complete CRUD Controller\n\n```csharp\nusing Microsoft.AspNetCore.Mvc;\n\n[ApiController]\n[Route(\"api/[controller]\")]\npublic class ProductsController : ControllerBase\n{\n    private readonly IProductService _service;\n\n    public ProductsController(IProductService service)\n        => _service = service;\n\n    // GET api/products\n    [HttpGet]\n    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()\n    {\n        var products = await _service.GetAllAsync();\n        return Ok(products);\n    }\n\n    // GET api/products/5\n    [HttpGet(\"{id:int}\")]\n    public async Task<ActionResult<ProductDto>> GetById(int id)\n    {\n        var product = await _service.GetByIdAsync(id);\n        if (product is null)\n            return NotFound(new { message = $\"Product {id} not found.\" });\n        return Ok(product);\n    }\n\n    // POST api/products\n    [HttpPost]\n    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)\n    {\n        var created = await _service.CreateAsync(dto);\n        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);\n    }\n\n    // PUT api/products/5\n    [HttpPut(\"{id:int}\")]\n    public async Task<IActionResult> Update(int id, UpdateProductDto dto)\n    {\n        if (!await _service.ExistsAsync(id))\n            return NotFound();\n\n        await _service.UpdateAsync(id, dto);\n        return NoContent();\n    }\n\n    // DELETE api/products/5\n    [HttpDelete(\"{id:int}\")]\n    public async Task<IActionResult> Delete(int id)\n    {\n        if (!await _service.ExistsAsync(id))\n            return NotFound();\n\n        await _service.DeleteAsync(id);\n        return NoContent();\n    }\n}\n```\n\n## Model Binding & Validation\n\n```csharp\npublic class CreateProductDto\n{\n    [Required]\n    [StringLength(100, MinimumLength = 2)]\n    public string Name { get; set; } = \"\";\n\n    [Required]\n    [Range(0.01, 100_000)]\n    public decimal Price { get; set; }\n\n    [StringLength(500)]\n    public string? Description { get; set; }\n\n    [Range(0, int.MaxValue)]\n    public int StockQuantity { get; set; }\n}\n```\n\nWhen `[ApiController]` is applied, model validation runs automatically before the action method executes — returning a 400 Bad Request with validation errors if any.\n\n## Route Constraints\n\n```csharp\n[HttpGet(\"{id:int}\")]          // Only matches integers\n[HttpGet(\"{name:alpha}\")]      // Only matches alphabetic strings\n[HttpGet(\"{id:int:min(1)}\")]   // Only matches int >= 1\n[HttpGet(\"{slug:regex(^[a-z0-9-]+$)}\")]  // Regex constraint\n```\n\n## Returning Proper Responses\n\n```csharp\nreturn Ok(data);                              // 200 with body\nreturn Created(uri, data);                    // 201 with location header\nreturn CreatedAtAction(nameof(Get), new { id }, data); // 201 with action link\nreturn NoContent();                           // 204 no body\nreturn BadRequest(new { error = \"msg\" });     // 400 with details\nreturn Unauthorized();                        // 401\nreturn Forbid();                              // 403\nreturn NotFound(new { message = \"...\" });    // 404 with details\nreturn Conflict(new { message = \"...\" });    // 409\nreturn StatusCode(500, new { message = \"...\" }); // 500\n```", 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "Building RESTful Controllers", 4, null, "RESTful controllers map HTTP verbs to CRUD actions using attributes like HttpGet, HttpPost, HttpPut, and HttpDelete. Return typed ActionResult of T for clear Swagger documentation. Use 200 OK for successful GET, 201 Created for POST, 204 No Content for PUT and DELETE, and 404 Not Found when a resource doesn't exist. The ApiController attribute enables automatic model validation." },
                    { 17, "- Always think about the Big O of your chosen data structure before writing code\n- Use arrays or `List<T>` when you need O(1) index access\n- Use a linked list when you need frequent O(1) insertions/deletions at the front\n- Always sort before binary searching — `Array.Sort` uses an O(n log n) algorithm\n- Avoid O(n²) algorithms on large inputs — they become unusable quickly", "# Big O Notation & Algorithm Complexity\n\n## Why Complexity Matters\n\nTwo programs can produce the same result but with very different performance characteristics. **Big O notation** describes how the runtime or memory usage of an algorithm scales with input size `n`.\n\n## Common Complexities (Best to Worst)\n\n| Notation | Name | Example | n=100 ops |\n|---|---|---|---|\n| O(1) | Constant | Array index access | 1 |\n| O(log n) | Logarithmic | Binary search | 7 |\n| O(n) | Linear | Linear search | 100 |\n| O(n log n) | Linearithmic | Merge sort | 664 |\n| O(n²) | Quadratic | Bubble sort | 10,000 |\n| O(2ⁿ) | Exponential | Recursive Fibonacci | 2^100 |\n\n## Array Operations & Complexity\n\n```csharp\nint[] arr = { 10, 20, 30, 40, 50 };\n\n// O(1) — Constant time: direct index access\nint first = arr[0];                    // Always 1 operation\nint last  = arr[arr.Length - 1];      // Always 1 operation\n\n// O(n) — Linear time: iterate entire array\nint sum = 0;\nforeach (int x in arr) sum += x;      // Scales with size\n\n// O(n) — Linear search (unsorted array)\nint LinearSearch(int[] a, int target)\n{\n    for (int i = 0; i < a.Length; i++)\n        if (a[i] == target) return i;\n    return -1;\n}\n\n// O(log n) — Binary search (sorted array only)\nint BinarySearch(int[] a, int target)\n{\n    int lo = 0, hi = a.Length - 1;\n    while (lo <= hi)\n    {\n        int mid = lo + (hi - lo) / 2;\n        if (a[mid] == target) return mid;\n        if (a[mid] < target)  lo = mid + 1;\n        else                  hi = mid - 1;\n    }\n    return -1;\n}\n```\n\n## Linked List Implementation\n\nA **singly linked list** consists of nodes, where each node holds a value and a reference to the next node.\n\n```csharp\npublic class Node<T>\n{\n    public T     Data { get; set; }\n    public Node<T>? Next { get; set; }\n\n    public Node(T data) => Data = data;\n}\n\npublic class LinkedList<T>\n{\n    private Node<T>? _head;\n    public int Count { get; private set; }\n\n    // O(1) — Insert at front\n    public void AddFirst(T data)\n    {\n        var newNode = new Node<T>(data);\n        newNode.Next = _head;\n        _head = newNode;\n        Count++;\n    }\n\n    // O(n) — Insert at end\n    public void AddLast(T data)\n    {\n        var newNode = new Node<T>(data);\n        if (_head is null)\n        {\n            _head = newNode;\n        }\n        else\n        {\n            var current = _head;\n            while (current.Next is not null)\n                current = current.Next;\n            current.Next = newNode;\n        }\n        Count++;\n    }\n\n    // O(n) — Search\n    public bool Contains(T data)\n    {\n        var current = _head;\n        while (current is not null)\n        {\n            if (EqualityComparer<T>.Default.Equals(current.Data, data))\n                return true;\n            current = current.Next;\n        }\n        return false;\n    }\n\n    // O(n) — Print all\n    public void Print()\n    {\n        var current = _head;\n        while (current is not null)\n        {\n            Console.Write(current.Data + \" → \");\n            current = current.Next;\n        }\n        Console.WriteLine(\"null\");\n    }\n}\n\n// Usage\nvar list = new LinkedList<int>();\nlist.AddFirst(30);\nlist.AddFirst(20);\nlist.AddFirst(10);\nlist.AddLast(40);\nlist.Print();           // 10 → 20 → 30 → 40 → null\nConsole.WriteLine(list.Contains(20));  // True\n```\n\n## Array vs Linked List\n\n| Operation | Array | Linked List |\n|---|---|---|\n| Access by index | O(1) | O(n) |\n| Search | O(n) | O(n) |\n| Insert at start | O(n) | O(1) |\n| Insert at end | O(1)* | O(n) |\n| Delete at start | O(n) | O(1) |\n| Memory | Contiguous | Scattered with pointers |\n\n*O(1) amortised for dynamic arrays like `List<T>`", 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "Big O Notation & Array Complexity", 4, null, "Big O notation describes how an algorithm's runtime scales with input size. O of 1 is constant, O of log n is logarithmic like binary search, O of n is linear like a simple loop, and O of n squared is quadratic like nested loops. Arrays offer O of 1 index access but O of n insertion at the start. Linked lists offer O of 1 insertion at the front but O of n access by index." },
                    { 18, "- Use `Func<T>` and `Action<T>` instead of declaring custom delegates in most cases\n- Be careful with closures in loops — capture loop variables explicitly if needed\n- Keep lambda bodies short — if the body is more than 2-3 lines, extract it to a named method\n- Use `Predicate<T>` for filter predicates to communicate intent clearly\n- Multicast delegates call handlers in the order they were added — be aware of side effects", "# Delegates, Func<T>, Action<T> & Lambda Expressions\n\n## What is a Delegate?\n\nA **delegate** is a type that holds a reference to a method. It lets you treat methods as first-class values — pass them as arguments, store them in variables, and invoke them later.\n\n```csharp\n// Declare a delegate type\ndelegate int MathOperation(int a, int b);\n\n// Methods that match the signature\nint Add(int a, int b) => a + b;\nint Multiply(int a, int b) => a * b;\n\n// Assign and invoke\nMathOperation op = Add;\nConsole.WriteLine(op(5, 3));   // 8\n\nop = Multiply;\nConsole.WriteLine(op(5, 3));   // 15\n```\n\n## Multicast Delegates\n\n```csharp\ndelegate void Notify(string message);\n\nvoid OnEmail(string msg) => Console.WriteLine($\"Email: {msg}\");\nvoid OnSms(string msg)   => Console.WriteLine($\"SMS:   {msg}\");\n\nNotify notify = OnEmail;\nnotify += OnSms;           // Add another handler\nnotify(\"Server is down\");  // Both methods called\n\nnotify -= OnEmail;         // Remove a handler\nnotify(\"All clear\");       // Only SMS now\n```\n\n## Built-in Delegates: Func & Action\n\nC# provides generic delegate types so you rarely need to declare your own:\n\n```csharp\n// Func<TResult> — no parameters, returns TResult\nFunc<int> getRandom = () => new Random().Next(1, 100);\n\n// Func<T, TResult> — one parameter\nFunc<int, bool>   isEven    = n => n % 2 == 0;\nFunc<string, int> strLength = s => s.Length;\n\n// Func<T1, T2, TResult> — two parameters\nFunc<int, int, int>    add     = (a, b) => a + b;\nFunc<string, string, string> concat = (a, b) => a + b;\n\nConsole.WriteLine(isEven(4));        // True\nConsole.WriteLine(strLength(\"Hi\")); // 2\nConsole.WriteLine(add(3, 7));       // 10\n\n// Action<T> — has parameters, returns void\nAction<string> print  = msg => Console.WriteLine(msg);\nAction<int, int> printSum = (a, b) => Console.WriteLine(a + b);\n\nprint(\"Hello!\");      // Hello!\nprintSum(5, 3);       // 8\n\n// Predicate<T> — shorthand for Func<T, bool>\nPredicate<int> isPositive = n => n > 0;\nConsole.WriteLine(isPositive(-5));  // False\n```\n\n## Lambda Expressions\n\nA **lambda expression** is an anonymous method written inline:\n\n```csharp\n// Syntax: (parameters) => expression\n// One liner:\nFunc<int, int> square = x => x * x;\n\n// Multi-line (statement lambda):\nFunc<int, int> factorial = n =>\n{\n    int result = 1;\n    for (int i = 2; i <= n; i++)\n        result *= i;\n    return result;\n};\n\nConsole.WriteLine(square(5));      // 25\nConsole.WriteLine(factorial(5));   // 120\n```\n\n## Closures — Capturing Variables\n\nLambdas can capture variables from their enclosing scope:\n\n```csharp\nint multiplier = 3;\nFunc<int, int> triple = x => x * multiplier;\n\nConsole.WriteLine(triple(5));  // 15\n\nmultiplier = 10;\nConsole.WriteLine(triple(5));  // 50  ← uses current value, not original!\n```\n\n> ⚠️ Closures capture the **variable**, not the value. Changes to the captured variable are reflected.\n\n## Higher-Order Functions\n\nFunctions that take other functions as parameters or return functions:\n\n```csharp\n// Apply a function to each element\nvoid ForEach(int[] numbers, Action<int> action)\n{\n    foreach (int n in numbers)\n        action(n);\n}\n\nint[] nums = { 1, 2, 3, 4, 5 };\nForEach(nums, n => Console.Write(n * n + \" \"));  // 1 4 9 16 25\n\n// Filter elements using a predicate\nint[] Filter(int[] numbers, Func<int, bool> predicate)\n{\n    var result = new List<int>();\n    foreach (int n in numbers)\n        if (predicate(n))\n            result.Add(n);\n    return result.ToArray();\n}\n\nint[] evens = Filter(nums, n => n % 2 == 0);\n// evens: 2, 4\n```\n\nThis is essentially what LINQ's `Where` method does — and we'll explore that next.", 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "Delegates, Func<T>, Action<T> & Lambdas", 4, null, "Delegates are types that hold references to methods, letting you pass methods as values. Func of T is a built-in delegate that takes parameters and returns a value. Action of T takes parameters but returns nothing. Lambda expressions are anonymous inline methods using the arrow syntax. Closures let lambdas capture variables from the surrounding scope, but they capture the variable itself, not its value at the time of capture." },
                    { 19, "- Prefer method syntax — it chains naturally and is more composable than query syntax\n- Always call `ToList()` or `ToArray()` to materialise a query when you need a snapshot\n- Never enumerate a LINQ query multiple times without materialising — it re-executes each time\n- Use `FirstOrDefault` instead of `First` to avoid exceptions when elements may not exist\n- Avoid LINQ in tight inner loops — the overhead of lambda calls can matter at scale\n- Use `Any()` instead of `Count() > 0` — it short-circuits on the first match", "# LINQ — Language Integrated Query\n\n**LINQ** lets you query and transform data using a consistent, expressive syntax directly in C# — no matter whether the data is in a list, array, database, or XML.\n\n## Method Syntax vs Query Syntax\n\n```csharp\nint[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };\n\n// ── Method syntax (preferred, more composable) ──\nvar evens = numbers\n    .Where(n => n % 2 == 0)   // Filter\n    .OrderByDescending(n => n) // Sort\n    .ToList();                 // Execute\n// [10, 8, 6, 4, 2]\n\n// ── Query syntax (SQL-like, good for complex joins) ──\nvar evensQuery =\n    (from n in numbers\n     where n % 2 == 0\n     orderby n descending\n     select n)\n    .ToList();\n// Same result\n```\n\n## Core LINQ Operators\n\n```csharp\nvar nums = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };\n\n// WHERE — filter\nvar big = nums.Where(n => n > 4);           // 5, 9, 6, 5, 5\n\n// SELECT — project/transform\nvar doubled = nums.Select(n => n * 2);      // 6, 2, 8, 2, 10, ...\n\n// ORDER BY\nvar sorted  = nums.OrderBy(n => n);         // ascending\nvar desc    = nums.OrderByDescending(n => n); // descending\n\n// DISTINCT — remove duplicates\nvar unique  = nums.Distinct();              // 3, 1, 4, 5, 9, 2, 6\n\n// TAKE / SKIP — paging\nvar first3  = nums.Take(3);                 // 3, 1, 4\nvar skip3   = nums.Skip(3);                 // 1, 5, 9, ...\nvar page2   = nums.Skip(3).Take(3);         // 1, 5, 9\n\n// AGGREGATE functions\nint  sum    = nums.Sum();                   // 44\nint  min    = nums.Min();                   // 1\nint  max    = nums.Max();                   // 9\ndouble avg  = nums.Average();               // 4.0\nint  count  = nums.Count();                 // 11\nint  countB = nums.Count(n => n > 4);       // 4\n```\n\n## Working with Objects\n\n```csharp\npublic record Product(int Id, string Name, string Category, decimal Price, int Stock);\n\nvar products = new List<Product>\n{\n    new(1, \"Laptop\",     \"Electronics\", 999.99m,  50),\n    new(2, \"Mouse\",      \"Electronics\", 29.99m,  200),\n    new(3, \"Desk\",       \"Furniture\",   349.00m,  15),\n    new(4, \"Chair\",      \"Furniture\",   249.00m,  30),\n    new(5, \"Monitor\",    \"Electronics\", 399.00m,  75),\n    new(6, \"Keyboard\",   \"Electronics\", 79.99m,  150),\n    new(7, \"Bookshelf\",  \"Furniture\",   199.00m,  20),\n};\n\n// Filter + Order\nvar expensive = products\n    .Where(p => p.Price > 200)\n    .OrderBy(p => p.Price)\n    .ToList();\n\n// Project — select specific fields\nvar names = products\n    .Select(p => new { p.Name, p.Price })\n    .OrderBy(x => x.Price);\n\n// GroupBy\nvar byCategory = products\n    .GroupBy(p => p.Category)\n    .Select(g => new\n    {\n        Category = g.Key,\n        Count    = g.Count(),\n        TotalValue = g.Sum(p => p.Price * p.Stock)\n    });\n\nforeach (var cat in byCategory)\n    Console.WriteLine($\"{cat.Category}: {cat.Count} items, ${cat.TotalValue:N0} total value\");\n\n// First, Single, Any, All\nvar cheapest = products.MinBy(p => p.Price);\nvar first    = products.First(p => p.Category == \"Electronics\");\nbool anyOut  = products.Any(p => p.Stock == 0);\nbool allIn   = products.All(p => p.Stock > 0);\n```\n\n## Deferred Execution\n\nLINQ queries are **lazy** — they don't run until you enumerate them:\n\n```csharp\nvar data = new List<int> { 1, 2, 3, 4, 5 };\n\n// This builds a query but does NOT execute yet\nvar query = data.Where(n => n > 2);\n\ndata.Add(10);  // Add element AFTER defining query\n\n// Execution happens here — includes 10!\nforeach (int n in query)\n    Console.Write(n + \" \");\n// Output: 3 4 5 10\n\n// Force immediate execution:\nvar snapshot = data.Where(n => n > 2).ToList();  // Executes NOW\ndata.Add(99);  // Does NOT affect snapshot\n```\n\n## Joining Collections\n\n```csharp\nvar orders = new[]\n{\n    new { OrderId = 1, ProductId = 1, Qty = 2 },\n    new { OrderId = 2, ProductId = 3, Qty = 1 },\n    new { OrderId = 3, ProductId = 1, Qty = 3 },\n};\n\nvar orderDetails =\n    from o in orders\n    join p in products on o.ProductId equals p.Id\n    select new { o.OrderId, p.Name, o.Qty, Total = p.Price * o.Qty };\n\nforeach (var detail in orderDetails)\n    Console.WriteLine($\"Order {detail.OrderId}: {detail.Name} x{detail.Qty} = ${detail.Total:N2}\");\n```", 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "LINQ — Language Integrated Query", 2, null, "LINQ provides a consistent way to query and transform data using method or query syntax. Core operators include Where for filtering, Select for projection, OrderBy for sorting, GroupBy for grouping, and aggregate methods like Sum, Min, Max, and Average. LINQ uses deferred execution — queries don't run until you enumerate them. Call ToList or ToArray to materialise results immediately." },
                    { 21, "- SRP: If you can't describe a class's purpose in one sentence, it's doing too much\n- OCP: Use interfaces and polymorphism instead of switch/if-else chains on types\n- LSP: A derived class must honour the promises (contract) of the base class\n- ISP: Keep interfaces small and focused — prefer many specific interfaces over one large one\n- DIP: Always inject dependencies via constructors — never create them with `new` inside a class\n- Apply SOLID pragmatically — not every class needs all five. Start with SRP and DIP.", "# SOLID Design Principles\n\nSOLID is a set of five design principles that make software easier to understand, maintain, and extend.\n\n## S — Single Responsibility Principle (SRP)\n\n> A class should have only **one reason to change**.\n\n```csharp\n// ❌ BAD — one class doing everything\npublic class UserService\n{\n    public void Register(string email, string password) { /* ... */ }\n    public void SendWelcomeEmail(string email) { /* ... */ }\n    public void LogActivity(string action) { /* ... */ }\n}\n\n// ✅ GOOD — separated responsibilities\npublic class UserService\n{\n    public void Register(string email, string password) { /* ... */ }\n}\n\npublic class EmailService\n{\n    public void SendWelcomeEmail(string email) { /* ... */ }\n}\n\npublic class ActivityLogger\n{\n    public void LogActivity(string action) { /* ... */ }\n}\n```\n\n## O — Open/Closed Principle (OCP)\n\n> Classes should be **open for extension** but **closed for modification**.\n\n```csharp\n// ❌ BAD — must modify class to add new discount types\npublic class DiscountCalculator\n{\n    public decimal Calculate(string type, decimal price)\n    {\n        if (type == \"Regular\") return price * 0.1m;\n        if (type == \"Premium\") return price * 0.2m;\n        // Must edit this method for every new type!\n        return 0;\n    }\n}\n\n// ✅ GOOD — extend via new classes, never modify existing code\npublic interface IDiscount\n{\n    decimal Calculate(decimal price);\n}\n\npublic class RegularDiscount : IDiscount\n{\n    public decimal Calculate(decimal price) => price * 0.1m;\n}\n\npublic class PremiumDiscount : IDiscount\n{\n    public decimal Calculate(decimal price) => price * 0.2m;\n}\n\n// Just add a new class for a new discount type — no existing code changes\npublic class StudentDiscount : IDiscount\n{\n    public decimal Calculate(decimal price) => price * 0.15m;\n}\n```\n\n## L — Liskov Substitution Principle (LSP)\n\n> Objects of a base class should be **replaceable** with objects of derived classes **without breaking** the program.\n\n```csharp\n// ❌ BAD — Square breaks Rectangle's contract\npublic class Rectangle\n{\n    public virtual int Width  { get; set; }\n    public virtual int Height { get; set; }\n    public int Area() => Width * Height;\n}\n\npublic class Square : Rectangle\n{\n    public override int Width\n    {\n        set { base.Width = value; base.Height = value; }\n    }\n    public override int Height\n    {\n        set { base.Width = value; base.Height = value; }\n    }\n}\n\n// ✅ GOOD — use an abstraction instead\npublic interface IShape\n{\n    double Area();\n}\n\npublic class Rectangle : IShape\n{\n    public int Width { get; set; }\n    public int Height { get; set; }\n    public double Area() => Width * Height;\n}\n\npublic class Square : IShape\n{\n    public int Side { get; set; }\n    public double Area() => Side * Side;\n}\n```\n\n## I — Interface Segregation Principle (ISP)\n\n> No client should be forced to depend on methods it **does not use**.\n\n```csharp\n// ❌ BAD — fat interface\npublic interface IWorker\n{\n    void Work();\n    void Eat();\n    void Sleep();\n}\n\n// A robot worker doesn't eat or sleep!\n\n// ✅ GOOD — small, specific interfaces\npublic interface IWorkable { void Work(); }\npublic interface IFeedable { void Eat(); }\npublic interface ISleepable { void Sleep(); }\n\npublic class Human : IWorkable, IFeedable, ISleepable\n{\n    public void Work()  { /* ... */ }\n    public void Eat()   { /* ... */ }\n    public void Sleep() { /* ... */ }\n}\n\npublic class Robot : IWorkable\n{\n    public void Work() { /* ... */ }\n}\n```\n\n## D — Dependency Inversion Principle (DIP)\n\n> High-level modules should not depend on low-level modules. Both should depend on **abstractions**.\n\n```csharp\n// ❌ BAD — directly coupled to a specific database\npublic class OrderService\n{\n    private readonly SqlDatabase _db = new SqlDatabase();\n\n    public void Save(Order order) => _db.Insert(order);\n}\n\n// ✅ GOOD — depends on an interface, not a concrete class\npublic interface IOrderRepository\n{\n    void Save(Order order);\n    Order? GetById(int id);\n}\n\npublic class SqlOrderRepository : IOrderRepository\n{\n    public void Save(Order order) { /* SQL logic */ }\n    public Order? GetById(int id) { /* SQL logic */ return null; }\n}\n\npublic class OrderService\n{\n    private readonly IOrderRepository _repo;\n\n    // Inject the dependency — this is DIP + DI together\n    public OrderService(IOrderRepository repo)\n    {\n        _repo = repo;\n    }\n\n    public void PlaceOrder(Order order) => _repo.Save(order);\n}\n```", 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "SOLID Design Principles in C#", 0, null, "SOLID is five design principles. Single Responsibility means each class has one job. Open-Closed means you extend classes through new code rather than modifying existing code. Liskov Substitution means derived classes must honour the base class contract. Interface Segregation means interfaces should be small and specific. Dependency Inversion means high-level code should depend on abstractions, not concrete implementations. Together they produce flexible, maintainable code." },
                    { 22, "- Always use `async` versions of EF Core methods (FindAsync, ToListAsync, SaveChangesAsync)\n- Use migrations for every schema change — never modify the database manually in production\n- Use `.Include()` for eager loading when you need related data to avoid N+1 queries\n- Use projections with `.Select()` to retrieve only the columns you need\n- Keep your DbContext scoped (one per HTTP request) — ASP.NET Core does this by default with `AddDbContext`\n- Use Fluent API in `OnModelCreating` for complex configuration; Data Annotations for simple rules", "# Entity Framework Core\n\n**Entity Framework Core (EF Core)** is an Object-Relational Mapper (ORM) that lets you work with databases using C# objects instead of raw SQL.\n\n## Setup\n\n```bash\ndotnet add package Microsoft.EntityFrameworkCore.SqlServer\ndotnet add package Microsoft.EntityFrameworkCore.Tools\ndotnet add package Microsoft.EntityFrameworkCore.Design\n```\n\n## Define Entity Models\n\n```csharp\npublic class Product\n{\n    public int Id { get; set; }         // Convention: Id = primary key\n    public string Name { get; set; } = \"\";\n    public decimal Price { get; set; }\n    public int CategoryId { get; set; } // Foreign key\n\n    // Navigation property\n    public Category Category { get; set; } = null!;\n}\n\npublic class Category\n{\n    public int Id { get; set; }\n    public string Name { get; set; } = \"\";\n\n    // Collection navigation\n    public ICollection<Product> Products { get; set; } = new List<Product>();\n}\n```\n\n## Create the DbContext\n\n```csharp\npublic class AppDbContext : DbContext\n{\n    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }\n\n    public DbSet<Product> Products { get; set; }\n    public DbSet<Category> Categories { get; set; }\n\n    protected override void OnModelCreating(ModelBuilder modelBuilder)\n    {\n        // Fluent API configuration\n        modelBuilder.Entity<Product>(entity =>\n        {\n            entity.Property(p => p.Name).HasMaxLength(200).IsRequired();\n            entity.Property(p => p.Price).HasPrecision(18, 2);\n            entity.HasOne(p => p.Category)\n                  .WithMany(c => c.Products)\n                  .HasForeignKey(p => p.CategoryId)\n                  .OnDelete(DeleteBehavior.Cascade);\n        });\n    }\n}\n```\n\n## Migrations\n\n```bash\n# Create a migration\ndotnet ef migrations add InitialCreate\n\n# Apply to database\ndotnet ef database update\n\n# Remove last migration (if not applied)\ndotnet ef migrations remove\n```\n\n## CRUD Operations\n\n### Create\n\n```csharp\nvar category = new Category { Name = \"Electronics\" };\ncontext.Categories.Add(category);\nawait context.SaveChangesAsync();\n\nvar product = new Product\n{\n    Name = \"Laptop\",\n    Price = 999.99m,\n    CategoryId = category.Id\n};\ncontext.Products.Add(product);\nawait context.SaveChangesAsync();\n```\n\n### Read\n\n```csharp\n// Get all\nvar allProducts = await context.Products.ToListAsync();\n\n// Get by ID\nvar product = await context.Products.FindAsync(1);\n\n// Query with filtering\nvar expensive = await context.Products\n    .Where(p => p.Price > 500)\n    .OrderBy(p => p.Name)\n    .ToListAsync();\n\n// Include related data (eager loading)\nvar productsWithCategory = await context.Products\n    .Include(p => p.Category)\n    .ToListAsync();\n\n// Projection\nvar summary = await context.Products\n    .Select(p => new { p.Name, p.Price, Category = p.Category.Name })\n    .ToListAsync();\n```\n\n### Update\n\n```csharp\nvar product = await context.Products.FindAsync(1);\nif (product is not null)\n{\n    product.Price = 899.99m;\n    await context.SaveChangesAsync();\n}\n```\n\n### Delete\n\n```csharp\nvar product = await context.Products.FindAsync(1);\nif (product is not null)\n{\n    context.Products.Remove(product);\n    await context.SaveChangesAsync();\n}\n```\n\n## Relationships\n\n```csharp\n// One-to-Many: A Category has many Products\nmodelBuilder.Entity<Product>()\n    .HasOne(p => p.Category)\n    .WithMany(c => c.Products)\n    .HasForeignKey(p => p.CategoryId);\n\n// Many-to-Many (EF Core 5+):\npublic class Student\n{\n    public int Id { get; set; }\n    public string Name { get; set; } = \"\";\n    public ICollection<Course> Courses { get; set; } = new List<Course>();\n}\n\npublic class Course\n{\n    public int Id { get; set; }\n    public string Title { get; set; } = \"\";\n    public ICollection<Student> Students { get; set; } = new List<Student>();\n}\n\n// EF Core auto-creates the join table!\n```", 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "Entity Framework Core — Database Access in C#", 4, null, "Entity Framework Core is an ORM that maps C sharp classes to database tables. You define entity models as plain classes with properties. The DbContext class manages database connections and exposes DbSet properties for each table. Use migrations to version your database schema. CRUD operations use Add, Find, Where, Remove, and SaveChangesAsync. Include handles eager loading of related entities." },
                    { 23, "- Never store JWT keys or secrets in source code — use environment variables or a secrets manager\n- Always validate and sanitize all user input — never trust data from the client\n- Set reasonable token expiry times (1-4 hours) and implement refresh token rotation\n- Use HTTPS in production — never transmit tokens or credentials over HTTP\n- Use [Authorize] by default and [AllowAnonymous] only on specific public endpoints\n- Apply the principle of least privilege — grant only the minimum access needed for each role", "# JWT Authentication & API Security\n\n## What is JWT?\n\n**JSON Web Token (JWT)** is a compact, URL-safe token format used for securely transmitting claims between parties. A JWT has three parts separated by dots:\n\n```\nheader.payload.signature\n```\n\n```\neyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0In0.signature\n```\n\n- **Header** — algorithm and token type\n- **Payload** — claims (user ID, role, expiry)\n- **Signature** — ensures the token hasn't been tampered with\n\n## Setting Up JWT in ASP.NET Core\n\n### Install Package\n\n```bash\ndotnet add package Microsoft.AspNetCore.Authentication.JwtBearer\n```\n\n### Configure in Program.cs\n\n```csharp\nvar key = builder.Configuration[\"Jwt:Key\"]!;\n\nbuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)\n    .AddJwtBearer(options =>\n    {\n        options.TokenValidationParameters = new TokenValidationParameters\n        {\n            ValidateIssuer = true,\n            ValidateAudience = true,\n            ValidateLifetime = true,\n            ValidateIssuerSigningKey = true,\n            ValidIssuer = \"YourApp\",\n            ValidAudience = \"YourApp\",\n            IssuerSigningKey = new SymmetricSecurityKey(\n                Encoding.UTF8.GetBytes(key))\n        };\n    });\n\n// Don't forget the middleware ORDER matters:\napp.UseAuthentication();   // Must come before\napp.UseAuthorization();    // Must come after\n```\n\n### Generate Tokens\n\n```csharp\npublic class TokenService\n{\n    private readonly string _key;\n\n    public TokenService(IConfiguration config)\n    {\n        _key = config[\"Jwt:Key\"]!;\n    }\n\n    public string GenerateToken(User user, string role)\n    {\n        var claims = new[]\n        {\n            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),\n            new Claim(ClaimTypes.Email, user.Email!),\n            new Claim(ClaimTypes.Role, role)\n        };\n\n        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));\n        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);\n\n        var token = new JwtSecurityToken(\n            issuer: \"YourApp\",\n            audience: \"YourApp\",\n            claims: claims,\n            expires: DateTime.UtcNow.AddHours(2),\n            signingCredentials: creds);\n\n        return new JwtSecurityTokenHandler().WriteToken(token);\n    }\n}\n```\n\n## Protecting Endpoints\n\n```csharp\n[ApiController]\n[Route(\"api/[controller]\")]\npublic class OrdersController : ControllerBase\n{\n    // Any authenticated user\n    [HttpGet]\n    [Authorize]\n    public IActionResult GetMyOrders()\n    {\n        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;\n        return Ok(new { UserId = userId });\n    }\n\n    // Only users with Admin role\n    [HttpDelete(\"{id}\")]\n    [Authorize(Roles = \"Admin\")]\n    public IActionResult DeleteOrder(int id)\n    {\n        return NoContent();\n    }\n\n    // Public endpoint — no authentication needed\n    [HttpGet(\"public\")]\n    [AllowAnonymous]\n    public IActionResult GetPublicInfo()\n    {\n        return Ok(\"Anyone can see this.\");\n    }\n}\n```\n\n## Input Validation\n\n### Data Annotations\n\n```csharp\npublic class RegisterRequest\n{\n    [Required(ErrorMessage = \"Email is required\")]\n    [EmailAddress(ErrorMessage = \"Invalid email format\")]\n    public string Email { get; set; } = \"\";\n\n    [Required]\n    [MinLength(6, ErrorMessage = \"Password must be at least 6 characters\")]\n    public string Password { get; set; } = \"\";\n\n    [Required]\n    [StringLength(50, MinimumLength = 2)]\n    public string FirstName { get; set; } = \"\";\n}\n```\n\n### FluentValidation (Preferred for Complex Rules)\n\n```csharp\n// dotnet add package FluentValidation.AspNetCore\n\npublic class RegisterRequestValidator : AbstractValidator<RegisterRequest>\n{\n    public RegisterRequestValidator()\n    {\n        RuleFor(x => x.Email)\n            .NotEmpty().WithMessage(\"Email is required\")\n            .EmailAddress().WithMessage(\"Invalid email format\");\n\n        RuleFor(x => x.Password)\n            .NotEmpty()\n            .MinimumLength(6)\n            .Matches(\"[A-Z]\").WithMessage(\"Must contain an uppercase letter\")\n            .Matches(\"[0-9]\").WithMessage(\"Must contain a digit\");\n    }\n}\n```\n\n## Security Best Practices\n\n```csharp\n// 1. Never store secrets in code — use environment variables\nvar key = Environment.GetEnvironmentVariable(\"JWT_SECRET_KEY\");\n\n// 2. Hash passwords — never store plain text\nvar hashedPassword = BCrypt.Net.BCrypt.HashPassword(\"myPassword\");\nbool isValid = BCrypt.Net.BCrypt.Verify(\"myPassword\", hashedPassword);\n\n// 3. Use HTTPS in production\napp.UseHttpsRedirection();\n\n// 4. CORS — restrict which domains can call your API\nbuilder.Services.AddCors(options =>\n{\n    options.AddPolicy(\"Production\", policy =>\n        policy.WithOrigins(\"https://yourdomain.com\")\n              .AllowAnyHeader()\n              .AllowAnyMethod());\n});\n```", 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 1, "JWT Authentication & API Security", 4, null, "JWT authentication uses JSON Web Tokens to securely identify users without server-side sessions. The token contains claims like user ID and role, signed with a secret key. Configure JWT in Program dot cs with AddAuthentication and AddJwtBearer. Use the Authorize attribute to protect endpoints and the Roles parameter for role-based access. Always validate input with Data Annotations or FluentValidation, and never store secrets in code." },
                    { 24, "- Use `Stack<T>` for LIFO behaviour — function call tracking, undo systems, expression parsing\n- Use `Queue<T>` for FIFO behaviour — task scheduling, BFS, message processing\n- Always check `Count > 0` or use `TryPeek`/`TryPop` before accessing elements to avoid exceptions\n- Prefer .NET's built-in Stack<T> and Queue<T> unless you need custom behaviour\n- Remember: stacks are ideal for recursion simulation; queues for level-order processing", "# Stacks & Queues\n\n## Stack — Last In, First Out (LIFO)\n\nA **stack** is like a stack of plates — you add to the top and remove from the top. The last item added is the first removed.\n\n### Using .NET's Stack<T>\n\n```csharp\nvar stack = new Stack<string>();\n\n// Push — add to top\nstack.Push(\"First\");\nstack.Push(\"Second\");\nstack.Push(\"Third\");\n\nConsole.WriteLine(stack.Peek());   // Third (look without removing)\nConsole.WriteLine(stack.Pop());    // Third (remove from top)\nConsole.WriteLine(stack.Pop());    // Second\nConsole.WriteLine(stack.Count);    // 1\n```\n\n### Implementing a Stack from Scratch\n\n```csharp\npublic class MyStack<T>\n{\n    private readonly List<T> _items = new();\n\n    public int Count => _items.Count;\n    public bool IsEmpty => _items.Count == 0;\n\n    public void Push(T item) => _items.Add(item);\n\n    public T Pop()\n    {\n        if (IsEmpty) throw new InvalidOperationException(\"Stack is empty.\");\n        var item = _items[^1];\n        _items.RemoveAt(_items.Count - 1);\n        return item;\n    }\n\n    public T Peek()\n    {\n        if (IsEmpty) throw new InvalidOperationException(\"Stack is empty.\");\n        return _items[^1];\n    }\n}\n```\n\n### Stack Applications\n\n```csharp\n// 1. Balanced Parentheses Checker\nbool IsBalanced(string s)\n{\n    var stack = new Stack<char>();\n    var pairs = new Dictionary<char, char>\n    {\n        { ')', '(' }, { ']', '[' }, { '}', '{' }\n    };\n\n    foreach (char c in s)\n    {\n        if (\"([{\".Contains(c))\n            stack.Push(c);\n        else if (\")]}\".Contains(c))\n        {\n            if (stack.Count == 0 || stack.Pop() != pairs[c])\n                return false;\n        }\n    }\n    return stack.Count == 0;\n}\n\nConsole.WriteLine(IsBalanced(\"({[]})\")); // True\nConsole.WriteLine(IsBalanced(\"({[})\"));  // False\n\n// 2. Reverse a String\nstring Reverse(string s)\n{\n    var stack = new Stack<char>(s);\n    return new string(stack.ToArray());\n}\n\n// 3. Undo/Redo System\nvar undoStack = new Stack<string>();\nvar redoStack = new Stack<string>();\nundoStack.Push(\"Type A\");\nundoStack.Push(\"Type B\");\nredoStack.Push(undoStack.Pop()); // Undo \"Type B\"\n```\n\n## Queue — First In, First Out (FIFO)\n\nA **queue** is like a queue at a shop — first person in line is served first.\n\n### Using .NET's Queue<T>\n\n```csharp\nvar queue = new Queue<string>();\n\n// Enqueue — add to back\nqueue.Enqueue(\"Alice\");\nqueue.Enqueue(\"Bob\");\nqueue.Enqueue(\"Charlie\");\n\nConsole.WriteLine(queue.Peek());     // Alice (look without removing)\nConsole.WriteLine(queue.Dequeue());  // Alice (remove from front)\nConsole.WriteLine(queue.Dequeue());  // Bob\nConsole.WriteLine(queue.Count);      // 1\n```\n\n### Implementing a Queue from Scratch\n\n```csharp\npublic class MyQueue<T>\n{\n    private readonly LinkedList<T> _items = new();\n\n    public int Count => _items.Count;\n    public bool IsEmpty => _items.Count == 0;\n\n    public void Enqueue(T item) => _items.AddLast(item);\n\n    public T Dequeue()\n    {\n        if (IsEmpty) throw new InvalidOperationException(\"Queue is empty.\");\n        var item = _items.First!.Value;\n        _items.RemoveFirst();\n        return item;\n    }\n\n    public T Peek()\n    {\n        if (IsEmpty) throw new InvalidOperationException(\"Queue is empty.\");\n        return _items.First!.Value;\n    }\n}\n```\n\n### Queue Applications\n\n```csharp\n// 1. Print Queue\nvar printQueue = new Queue<string>();\nprintQueue.Enqueue(\"Document A\");\nprintQueue.Enqueue(\"Document B\");\nprintQueue.Enqueue(\"Document C\");\n\nwhile (printQueue.Count > 0)\n    Console.WriteLine($\"Printing: {printQueue.Dequeue()}\");\n\n// 2. BFS (Breadth-First Search) uses a queue\n// 3. Task schedulers, message queues, buffering\n```\n\n## Complexity\n\n| Operation | Stack | Queue |\n|---|---|---|\n| Push / Enqueue | O(1) | O(1) |\n| Pop / Dequeue | O(1) | O(1) |\n| Peek | O(1) | O(1) |\n| Search | O(n) | O(n) |", 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 35, 1, "Stacks & Queues — LIFO and FIFO Data Structures", 2, null, "A stack is a last-in-first-out data structure with push, pop, and peek operations, all running in constant time. A queue is first-in-first-out with enqueue, dequeue, and peek. Use stacks for undo systems, balanced bracket checking, and evaluating expressions. Use queues for task scheduling, breadth-first search, and message processing. Both are available as generic types in dot NET." },
                    { 25, "- Use `Array.Sort()` or `List.Sort()` in production — they use an optimised IntroSort (Quick + Heap + Insertion)\n- Merge Sort guarantees O(n log n) and is stable — ideal when stability matters\n- Quick Sort is generally faster in practice due to cache efficiency, despite O(n²) worst case\n- Use Insertion Sort for small arrays (< 20 elements) — the constant factor is very low\n- Always analyse both time AND space complexity when choosing an algorithm\n- A 'stable' sort preserves the relative order of equal elements — important for multi-key sorting", "# Sorting Algorithms\n\n## Why Study Sorting?\n\nSorting is one of the most fundamental operations in computer science. Understanding sorting algorithms teaches you algorithm design, complexity analysis, and trade-offs between time and space.\n\n## Bubble Sort — O(n²)\n\nThe simplest sorting algorithm. Repeatedly swap adjacent elements if they're in the wrong order.\n\n```csharp\nvoid BubbleSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = 0; i < n - 1; i++)\n    {\n        bool swapped = false;\n        for (int j = 0; j < n - 1 - i; j++)\n        {\n            if (arr[j] > arr[j + 1])\n            {\n                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]); // Tuple swap\n                swapped = true;\n            }\n        }\n        if (!swapped) break; // Already sorted — early exit\n    }\n}\n```\n\n## Selection Sort — O(n²)\n\nFind the minimum element in the unsorted portion and swap it to the front.\n\n```csharp\nvoid SelectionSort(int[] arr)\n{\n    int n = arr.Length;\n    for (int i = 0; i < n - 1; i++)\n    {\n        int minIndex = i;\n        for (int j = i + 1; j < n; j++)\n        {\n            if (arr[j] < arr[minIndex])\n                minIndex = j;\n        }\n        if (minIndex != i)\n            (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);\n    }\n}\n```\n\n## Insertion Sort — O(n²) worst, O(n) best\n\nBuilds the sorted array one element at a time by inserting each into its correct position.\n\n```csharp\nvoid InsertionSort(int[] arr)\n{\n    for (int i = 1; i < arr.Length; i++)\n    {\n        int key = arr[i];\n        int j = i - 1;\n\n        while (j >= 0 && arr[j] > key)\n        {\n            arr[j + 1] = arr[j];\n            j--;\n        }\n        arr[j + 1] = key;\n    }\n}\n```\n\n> **Insertion sort is fast for small or nearly-sorted arrays.** Many real-world sorting libraries use it for small partitions.\n\n## Merge Sort — O(n log n)\n\nDivide-and-conquer: recursively split the array in half, sort each half, then merge.\n\n```csharp\nvoid MergeSort(int[] arr, int left, int right)\n{\n    if (left >= right) return;\n\n    int mid = left + (right - left) / 2;\n\n    MergeSort(arr, left, mid);\n    MergeSort(arr, mid + 1, right);\n    Merge(arr, left, mid, right);\n}\n\nvoid Merge(int[] arr, int left, int mid, int right)\n{\n    int[] leftArr  = arr[left..(mid + 1)];\n    int[] rightArr = arr[(mid + 1)..(right + 1)];\n\n    int i = 0, j = 0, k = left;\n\n    while (i < leftArr.Length && j < rightArr.Length)\n    {\n        arr[k++] = leftArr[i] <= rightArr[j]\n            ? leftArr[i++]\n            : rightArr[j++];\n    }\n\n    while (i < leftArr.Length) arr[k++] = leftArr[i++];\n    while (j < rightArr.Length) arr[k++] = rightArr[j++];\n}\n\n// Usage:\nint[] data = { 38, 27, 43, 3, 9, 82, 10 };\nMergeSort(data, 0, data.Length - 1);\n// Result: 3, 9, 10, 27, 38, 43, 82\n```\n\n## Quick Sort — O(n log n) average, O(n²) worst\n\nChoose a pivot, partition elements around it, then sort each partition.\n\n```csharp\nvoid QuickSort(int[] arr, int low, int high)\n{\n    if (low >= high) return;\n\n    int pivot = Partition(arr, low, high);\n    QuickSort(arr, low, pivot - 1);\n    QuickSort(arr, pivot + 1, high);\n}\n\nint Partition(int[] arr, int low, int high)\n{\n    int pivot = arr[high];\n    int i = low - 1;\n\n    for (int j = low; j < high; j++)\n    {\n        if (arr[j] < pivot)\n        {\n            i++;\n            (arr[i], arr[j]) = (arr[j], arr[i]);\n        }\n    }\n    (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);\n    return i + 1;\n}\n```\n\n## Comparison Table\n\n| Algorithm | Best | Average | Worst | Space | Stable? |\n|---|---|---|---|---|---|\n| Bubble Sort | O(n) | O(n²) | O(n²) | O(1) | Yes |\n| Selection Sort | O(n²) | O(n²) | O(n²) | O(1) | No |\n| Insertion Sort | O(n) | O(n²) | O(n²) | O(1) | Yes |\n| Merge Sort | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes |\n| Quick Sort | O(n log n) | O(n log n) | O(n²) | O(log n) | No |", 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "Sorting Algorithms — From Bubble Sort to Merge Sort", 2, null, "Bubble sort, selection sort, and insertion sort are simple O of n squared algorithms good for learning. Merge sort uses divide-and-conquer to achieve O of n log n time guaranteed, at the cost of O of n extra space. Quick sort averages O of n log n and is fast in practice but can degrade to O of n squared on bad inputs. In production, use Array dot Sort which combines the best of quick sort, heap sort, and insertion sort." },
                    { 26, "- Always start tree algorithms recursively — iterative versions are optimisations for later\n- Use In-Order traversal to get BST elements in sorted order\n- Use Level-Order (BFS) traversal when you need to process nodes level by level\n- Be aware that unbalanced BSTs degrade to O(n) — consider SortedSet<T> in .NET which uses a balanced tree\n- .NET's SortedDictionary<TKey, TValue> and SortedSet<T> use Red-Black Trees internally\n- Practice tree problems recursively — the base case is always `if (node is null) return`", "# Binary Trees & Binary Search Trees\n\n## Tree Terminology\n\n- **Node** — an element in the tree containing data\n- **Root** — the topmost node (no parent)\n- **Children** — nodes directly below a node\n- **Leaf** — a node with no children\n- **Height** — the longest path from root to a leaf\n- **Depth** — the distance from the root to a node\n\n## Binary Tree — Each node has at most 2 children\n\n```csharp\npublic class TreeNode<T>\n{\n    public T Value { get; set; }\n    public TreeNode<T>? Left { get; set; }\n    public TreeNode<T>? Right { get; set; }\n\n    public TreeNode(T value)\n    {\n        Value = value;\n    }\n}\n```\n\n## Tree Traversals\n\n```csharp\npublic class BinaryTree<T>\n{\n    public TreeNode<T>? Root { get; set; }\n\n    // In-order: Left → Root → Right (gives sorted order for BST)\n    public void InOrder(TreeNode<T>? node)\n    {\n        if (node is null) return;\n        InOrder(node.Left);\n        Console.Write(node.Value + \" \");\n        InOrder(node.Right);\n    }\n\n    // Pre-order: Root → Left → Right (useful for copying/serialising)\n    public void PreOrder(TreeNode<T>? node)\n    {\n        if (node is null) return;\n        Console.Write(node.Value + \" \");\n        PreOrder(node.Left);\n        PreOrder(node.Right);\n    }\n\n    // Post-order: Left → Right → Root (useful for deletion)\n    public void PostOrder(TreeNode<T>? node)\n    {\n        if (node is null) return;\n        PostOrder(node.Left);\n        PostOrder(node.Right);\n        Console.Write(node.Value + \" \");\n    }\n\n    // Level-order (BFS): Level by level using a queue\n    public void LevelOrder()\n    {\n        if (Root is null) return;\n        var queue = new Queue<TreeNode<T>>();\n        queue.Enqueue(Root);\n\n        while (queue.Count > 0)\n        {\n            var node = queue.Dequeue();\n            Console.Write(node.Value + \" \");\n            if (node.Left is not null) queue.Enqueue(node.Left);\n            if (node.Right is not null) queue.Enqueue(node.Right);\n        }\n    }\n}\n```\n\n## Binary Search Tree (BST)\n\nA BST enforces an ordering rule: for every node, all values in the left subtree are less, and all values in the right subtree are greater.\n\n```csharp\npublic class BinarySearchTree\n{\n    private TreeNode<int>? _root;\n\n    // INSERT\n    public void Insert(int value)\n    {\n        _root = Insert(_root, value);\n    }\n\n    private TreeNode<int> Insert(TreeNode<int>? node, int value)\n    {\n        if (node is null) return new TreeNode<int>(value);\n\n        if (value < node.Value)\n            node.Left = Insert(node.Left, value);\n        else if (value > node.Value)\n            node.Right = Insert(node.Right, value);\n        // Duplicate values are ignored\n\n        return node;\n    }\n\n    // SEARCH\n    public bool Search(int value)\n    {\n        return Search(_root, value);\n    }\n\n    private bool Search(TreeNode<int>? node, int value)\n    {\n        if (node is null) return false;\n        if (value == node.Value) return true;\n        return value < node.Value\n            ? Search(node.Left, value)\n            : Search(node.Right, value);\n    }\n\n    // FIND MIN / MAX\n    public int FindMin()\n    {\n        var node = _root ?? throw new InvalidOperationException(\"Tree is empty.\");\n        while (node.Left is not null) node = node.Left;\n        return node.Value;\n    }\n\n    public int FindMax()\n    {\n        var node = _root ?? throw new InvalidOperationException(\"Tree is empty.\");\n        while (node.Right is not null) node = node.Right;\n        return node.Value;\n    }\n\n    // HEIGHT\n    public int Height(TreeNode<int>? node)\n    {\n        if (node is null) return -1;\n        return 1 + Math.Max(Height(node.Left), Height(node.Right));\n    }\n}\n\n// Usage:\nvar bst = new BinarySearchTree();\nint[] values = { 50, 30, 70, 20, 40, 60, 80 };\nforeach (int v in values) bst.Insert(v);\n\n//        50\n//       /  \\\n//     30    70\n//    / \\   / \\\n//  20  40 60  80\n\nConsole.WriteLine(bst.Search(40));  // True\nConsole.WriteLine(bst.Search(45));  // False\nConsole.WriteLine(bst.FindMin());   // 20\nConsole.WriteLine(bst.FindMax());   // 80\n```\n\n## BST Complexity\n\n| Operation | Average | Worst (unbalanced) |\n|---|---|---|\n| Search | O(log n) | O(n) |\n| Insert | O(log n) | O(n) |\n| Delete | O(log n) | O(n) |\n| Traversal | O(n) | O(n) |\n\n> When a BST becomes unbalanced (like a linked list), performance degrades to O(n). Self-balancing trees (AVL, Red-Black) maintain O(log n) by rebalancing after insertions and deletions.", 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "Binary Trees & Binary Search Trees", 2, null, "A binary tree is a hierarchical data structure where each node has at most two children. Tree traversals include in-order (left, root, right), pre-order (root, left, right), post-order (left, right, root), and level-order using a queue. A binary search tree enforces ordering so that left children are smaller and right children are larger. BST operations average O of log n time, but degrade to O of n when unbalanced." },
                    { 27, "- Use `Join` for inner joins and `GroupJoin` for left outer joins\n- Always materialise GroupBy results with `ToList()` before re-enumerating to avoid multiple executions\n- Use `SelectMany` to flatten nested collections — it's the LINQ equivalent of nested foreach loops\n- Prefer `Aggregate` only for custom reductions — use `Sum`, `Min`, `Max`, `Average` for standard aggregates\n- Use set operations (`Union`, `Intersect`, `Except`) instead of manual loops for set logic\n- Remember that most LINQ operators use deferred execution — force with `ToList()` when you need a snapshot", "# Advanced LINQ\n\n## Setup Data\n\n```csharp\npublic record Student(int Id, string Name, string Department);\npublic record Grade(int StudentId, string Subject, int Score);\n\nvar students = new List<Student>\n{\n    new(1, \"Alice\",   \"CS\"),\n    new(2, \"Bob\",     \"CS\"),\n    new(3, \"Charlie\", \"Math\"),\n    new(4, \"Diana\",   \"Math\"),\n    new(5, \"Eve\",     \"CS\")\n};\n\nvar grades = new List<Grade>\n{\n    new(1, \"C#\",       95), new(1, \"Algorithms\", 88),\n    new(2, \"C#\",       72), new(2, \"Algorithms\", 80),\n    new(3, \"Calculus\",  90), new(3, \"Algebra\",   85),\n    new(4, \"Calculus\",  78), new(5, \"C#\",        91)\n};\n```\n\n## Inner Join\n\n```csharp\n// Method syntax\nvar results = students\n    .Join(grades,\n          s => s.Id,          // outer key\n          g => g.StudentId,   // inner key\n          (s, g) => new { s.Name, g.Subject, g.Score });\n\nforeach (var r in results)\n    Console.WriteLine($\"{r.Name}: {r.Subject} = {r.Score}\");\n\n// Query syntax\nvar results2 =\n    from s in students\n    join g in grades on s.Id equals g.StudentId\n    select new { s.Name, g.Subject, g.Score };\n```\n\n## Group Join (Left Join)\n\n```csharp\n// Students with ALL their grades (even students with no grades)\nvar withGrades = students\n    .GroupJoin(grades,\n              s => s.Id,\n              g => g.StudentId,\n              (student, studentGrades) => new\n              {\n                  student.Name,\n                  Grades = studentGrades.ToList(),\n                  Average = studentGrades.Any()\n                      ? studentGrades.Average(g => g.Score)\n                      : 0\n              });\n\nforeach (var s in withGrades)\n    Console.WriteLine($\"{s.Name}: avg = {s.Average:F1} ({s.Grades.Count} grades)\");\n```\n\n## GroupBy\n\n```csharp\n// Group students by department\nvar departments = students\n    .GroupBy(s => s.Department)\n    .Select(g => new\n    {\n        Department = g.Key,\n        Count = g.Count(),\n        Students = g.Select(s => s.Name).ToList()\n    });\n\nforeach (var dept in departments)\n    Console.WriteLine($\"{dept.Department}: {string.Join(\", \", dept.Students)}\");\n// CS: Alice, Bob, Eve\n// Math: Charlie, Diana\n\n// Group with aggregation\nvar deptStats = students\n    .GroupJoin(grades, s => s.Id, g => g.StudentId, (s, gs) => new { s, gs })\n    .SelectMany(x => x.gs.DefaultIfEmpty(), (x, g) => new { x.s.Department, Score = g?.Score ?? 0 })\n    .GroupBy(x => x.Department)\n    .Select(g => new\n    {\n        Department = g.Key,\n        AvgScore = g.Average(x => x.Score),\n        TopScore = g.Max(x => x.Score)\n    });\n```\n\n## Aggregation Methods\n\n```csharp\nvar scores = new[] { 95, 88, 72, 80, 90, 85, 78, 91 };\n\nConsole.WriteLine(scores.Sum());         // 679\nConsole.WriteLine(scores.Average());     // 84.875\nConsole.WriteLine(scores.Min());         // 72\nConsole.WriteLine(scores.Max());         // 95\nConsole.WriteLine(scores.Count());       // 8\n\n// Aggregate — custom reduction\nstring csv = scores.Aggregate(\"\", (acc, s) => acc + (acc == \"\" ? \"\" : \",\") + s);\n// \"95,88,72,80,90,85,78,91\"\n\nint product = new[] { 2, 3, 4 }.Aggregate(1, (acc, n) => acc * n);\n// 1 * 2 * 3 * 4 = 24\n```\n\n## SelectMany — Flatten Nested Collections\n\n```csharp\nvar departments = new[]\n{\n    new { Name = \"CS\", Courses = new[] { \"C#\", \"Algorithms\", \"Databases\" } },\n    new { Name = \"Math\", Courses = new[] { \"Calculus\", \"Algebra\" } }\n};\n\n// Flatten all courses into a single list\nvar allCourses = departments.SelectMany(d => d.Courses);\n// [\"C#\", \"Algorithms\", \"Databases\", \"Calculus\", \"Algebra\"]\n\n// With source reference\nvar detailed = departments\n    .SelectMany(d => d.Courses, (dept, course) => new { dept.Name, Course = course });\n// { CS, C# }, { CS, Algorithms }, { CS, Databases }, { Math, Calculus }, { Math, Algebra }\n```\n\n## Zip\n\n```csharp\nvar names  = new[] { \"Alice\", \"Bob\", \"Charlie\" };\nvar scores2 = new[] { 95, 82, 88 };\n\nvar pairs = names.Zip(scores2, (name, score) => $\"{name}: {score}\");\n// [\"Alice: 95\", \"Bob: 82\", \"Charlie: 88\"]\n```\n\n## Set Operations\n\n```csharp\nvar a = new[] { 1, 2, 3, 4, 5 };\nvar b = new[] { 3, 4, 5, 6, 7 };\n\nvar union     = a.Union(b);         // { 1, 2, 3, 4, 5, 6, 7 }\nvar intersect = a.Intersect(b);     // { 3, 4, 5 }\nvar except    = a.Except(b);        // { 1, 2 }\nvar distinct  = new[] { 1, 1, 2, 2, 3 }.Distinct(); // { 1, 2, 3 }\n```", 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 1, "Advanced LINQ — Joins, Grouping & Aggregation", 2, null, "Advanced LINQ includes inner joins with Join, left outer joins with GroupJoin, and grouping with GroupBy. Aggregation methods like Sum, Average, Min, Max, and Count reduce collections to single values. SelectMany flattens nested collections. Zip combines two sequences element by element. Set operations like Union, Intersect, and Except perform mathematical set logic on collections. These operators let you write complex data transformations declaratively." }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "5", null },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 6, "23", null },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 6, "2+3", null },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 6, "Error", null },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 7, "True", null },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 7, "False", null },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "$", null },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 8, "@", null },
                    { 23, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 8, "#", null },
                    { 24, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 8, "%", null },
                    { 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "\\t", null },
                    { 26, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 9, "\\n", null },
                    { 27, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 9, "\\r", null },
                    { 28, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 9, "\\b", null }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 10, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Which data type should be used for currency/money calculations?", 0, null },
                    { 11, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "var is a dynamically-typed keyword in C#.", 1, null },
                    { 12, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "What is the default value of an uninitialized int variable?", 0, null },
                    { 13, "?", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "To make an int nullable, you write int___", 2, null },
                    { 14, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Which keyword declares a compile-time constant?", 0, null },
                    { 15, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "What is the output of: Console.WriteLine(17 % 5);", 3, null },
                    { 16, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "What is the output of: Console.WriteLine(17 / 5);", 3, null },
                    { 17, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "The && operator short-circuits when the left operand is true.", 1, null },
                    { 18, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Which operator returns the remainder of integer division?", 0, null },
                    { 19, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "In a switch expression, what symbol represents the default arm?", 0, null },
                    { 20, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "The ternary operator requires three operands.", 1, null },
                    { 21, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Which operator returns a default value when the left side is null?", 0, null },
                    { 22, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "What is the output of: Console.WriteLine(5 > 3 ? \"Yes\" : \"No\");", 3, null },
                    { 23, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "Which loop is best suited for iterating over a collection when you don't need the index?", 0, null },
                    { 24, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "A do-while loop always executes its body at least once.", 1, null },
                    { 25, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "Which keyword skips the rest of the current loop iteration?", 0, null },
                    { 26, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "What is the output of: for(int i=0;i<3;i++) Console.Write(i);", 3, null },
                    { 27, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, "Which keyword is used to pass multiple arguments as an array parameter?", 0, null },
                    { 28, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, "Method overloading requires different return types.", 1, null },
                    { 29, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, "What is the output of: Console.WriteLine(Factorial(4)); where Factorial is n <= 1 ? 1 : n * Factorial(n-1)?", 3, null },
                    { 30, "cannot", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, "A method with return type void ___ return a value.", 2, null },
                    { 31, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "Arrays in C# are zero-indexed.", 1, null },
                    { 32, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "What index accesses the last element of array arr using C# 8+ syntax?", 0, null },
                    { 33, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "Which method sorts an array in place?", 0, null },
                    { 34, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "What is the output of: int[] a = {3,1,2}; Array.Sort(a); Console.Write(a[0]);", 3, null },
                    { 35, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Which collection type provides O(1) average lookup by key?", 0, null },
                    { 36, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "List<T> has a fixed size that cannot change after creation.", 1, null },
                    { 37, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Which method should you use for safe dictionary key access?", 0, null },
                    { 38, "unique", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "HashSet<T> allows ___ elements.", 2, null },
                    { 39, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "Strings in C# are immutable.", 1, null },
                    { 40, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "Which class should you use to build a string in a loop for performance?", 0, null },
                    { 41, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "What is the output of: Console.WriteLine(\"hello\".ToUpper());", 3, null },
                    { 42, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "Which method splits a string into an array by a delimiter?", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "LessonId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 11, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "OOP Classes & Constructors Quiz", null },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "Encapsulation & Properties Quiz", null }
                });

            migrationBuilder.InsertData(
                table: "TutorialSteps",
                columns: new[] { "Id", "CodeSample", "Content", "CreatedAt", "LessonId", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 20, "// Blueprint\npublic class Dog { public string Name; }\n\n// Instances\nvar rex = new Dog { Name = \"Rex\" };\nvar fido = new Dog { Name = \"Fido\" };", "A class is the blueprint; an object is an instance of that blueprint. You can create many objects from one class.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 1, "Class vs Object", null },
                    { 21, "public class Person\n{\n    private int _age;\n    public int Age\n    {\n        get => _age;\n        set => _age = value >= 0 ? value : throw new ArgumentException();\n    }\n}", "Keep fields private and expose them through properties. This lets you add validation without breaking callers.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 2, "Encapsulation", null },
                    { 28, "private int _xp;\npublic int XP \n{ \n    get => _xp; \n    set => _xp = value >= 0 ? value : 0; \n}", "Directly exposing fields allows invalid data (e.g. negative age). Private fields combined with properties prevent this.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 1, "Why Private Fields?", null },
                    { 29, "public string UserName { get; set; }", "If you don't need validation logic, use auto-properties. The compiler creates the private backing field for you.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 2, "Auto-Implemented Properties", null }
                });

            migrationBuilder.InsertData(
                table: "LessonVideos",
                columns: new[] { "Id", "CreatedAt", "DurationMinutes", "LessonId", "Order", "Provider", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[,]
                {
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, 13, 1, 0, "Inheritance & Polymorphism in C#", null, "https://www.youtube.com/watch?v=wqUfllZyeq4" },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 13, 2, 0, "Virtual, Override & Sealed in C#", null, "https://www.youtube.com/watch?v=rHMsEL90VNs" },
                    { 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 14, 1, 0, "Interfaces vs Abstract Classes in C#", null, "https://www.youtube.com/watch?v=C-NDfCKwv0I" },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 60, 15, 1, 0, "ASP.NET Core Crash Course", null, "https://www.youtube.com/watch?v=AhAxLiGC7Pc" },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, 15, 2, 0, "Dependency Injection in ASP.NET Core", null, "https://www.youtube.com/watch?v=Hhpq7oYcpyE" },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45, 16, 1, 0, "REST API with ASP.NET Core 8", null, "https://www.youtube.com/watch?v=7PNzEL60YVA" },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, 17, 1, 0, "Big O Notation for Beginners", null, "https://www.youtube.com/watch?v=v4cd1O4zkGw" },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 17, 2, 0, "Linked Lists in C#", null, "https://www.youtube.com/watch?v=WwfhLC16bis" },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, 18, 1, 0, "Delegates and Lambda Expressions in C#", null, "https://www.youtube.com/watch?v=R8Blt5c-Vi4" },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 18, 2, 0, "Func, Action, Predicate Explained", null, "https://www.youtube.com/watch?v=eTuFMr7KZGY" },
                    { 23, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40, 19, 1, 0, "LINQ in C# — Full Tutorial", null, "https://www.youtube.com/watch?v=gwD9awr3NNo" },
                    { 24, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 19, 2, 0, "LINQ Performance Tips & Deferred Execution", null, "https://www.youtube.com/watch?v=yh2nGaZvA1o" }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 29, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 10, "decimal", null },
                    { 30, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 10, "double", null },
                    { 31, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 10, "float", null },
                    { 32, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 10, "long", null },
                    { 33, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 11, "True", null },
                    { 34, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 11, "False", null },
                    { 35, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 12, "0", null },
                    { 36, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 12, "null", null },
                    { 37, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 12, "-1", null },
                    { 38, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 12, "undefined", null },
                    { 39, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 14, "const", null },
                    { 40, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 14, "readonly", null },
                    { 41, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 14, "static", null },
                    { 42, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 14, "fixed", null },
                    { 43, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 15, "2", null },
                    { 44, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 15, "3", null },
                    { 45, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 15, "0", null },
                    { 46, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 15, "5", null },
                    { 47, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 16, "3", null },
                    { 48, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 16, "3.4", null },
                    { 49, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 16, "2", null },
                    { 50, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 16, "4", null },
                    { 51, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 17, "True", null },
                    { 52, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 17, "False", null },
                    { 53, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 18, "%", null },
                    { 54, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 18, "/", null },
                    { 55, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 18, "mod", null },
                    { 56, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 18, "\\", null },
                    { 57, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 19, "_", null },
                    { 58, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 19, "default", null },
                    { 59, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 19, "*", null },
                    { 60, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 19, "else", null },
                    { 61, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 20, "True", null },
                    { 62, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 20, "False", null },
                    { 63, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 21, "??", null },
                    { 64, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 21, "?.", null },
                    { 65, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 21, "||", null },
                    { 66, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 21, "!", null },
                    { 67, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 22, "Yes", null },
                    { 68, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 22, "No", null },
                    { 69, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 23, "foreach", null },
                    { 70, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 23, "for", null },
                    { 71, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 23, "while", null },
                    { 72, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 23, "do-while", null },
                    { 73, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 24, "True", null },
                    { 74, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 24, "False", null },
                    { 75, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 25, "continue", null },
                    { 76, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 25, "break", null },
                    { 77, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 25, "skip", null },
                    { 78, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 25, "next", null },
                    { 79, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 26, "012", null },
                    { 80, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 26, "123", null },
                    { 81, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 26, "0 1 2", null },
                    { 82, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 26, "Error", null },
                    { 83, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 27, "params", null },
                    { 84, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 27, "args", null },
                    { 85, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 27, "varargs", null },
                    { 86, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 27, "multiple", null },
                    { 87, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 28, "True", null },
                    { 88, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 28, "False", null },
                    { 89, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 29, "24", null },
                    { 90, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 29, "12", null },
                    { 91, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 29, "16", null },
                    { 92, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 29, "120", null },
                    { 93, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 31, "True", null },
                    { 94, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 31, "False", null },
                    { 95, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 32, "arr[^1]", null },
                    { 96, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 32, "arr[-1]", null },
                    { 97, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 32, "arr[last]", null },
                    { 98, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 32, "arr.Last", null },
                    { 99, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 33, "Array.Sort(arr)", null },
                    { 100, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 33, "arr.OrderBy(x=>x)", null },
                    { 101, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 33, "Array.Order(arr)", null },
                    { 102, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 33, "arr.Sort()", null },
                    { 103, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 34, "1", null },
                    { 104, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 34, "3", null },
                    { 105, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 34, "2", null },
                    { 106, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 34, "Error", null },
                    { 107, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 35, "Dictionary<TKey,TValue>", null },
                    { 108, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 35, "List<T>", null },
                    { 109, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 35, "Array", null },
                    { 110, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 35, "Stack<T>", null },
                    { 111, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 36, "True", null },
                    { 112, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 36, "False", null },
                    { 113, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 37, "TryGetValue", null },
                    { 114, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 37, "GetValue", null },
                    { 115, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 37, "dict[key]", null },
                    { 116, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 37, "Find", null },
                    { 117, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 39, "True", null },
                    { 118, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 39, "False", null },
                    { 119, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 40, "StringBuilder", null },
                    { 120, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 40, "StringBuffer", null },
                    { 121, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 40, "StringWriter", null },
                    { 122, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 40, "StringHelper", null },
                    { 123, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 41, "HELLO", null },
                    { 124, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 41, "hello", null },
                    { 125, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 41, "Hello", null },
                    { 126, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 42, "Split", null },
                    { 127, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 42, "Divide", null },
                    { 128, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 42, "Separate", null },
                    { 129, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 42, "Partition", null }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 43, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "A constructor is called when an object is created with the new keyword.", 1, null },
                    { 44, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "Which access modifier makes a member accessible only within the same class?", 0, null },
                    { 45, "class", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "Static members belong to the ___ not to instances.", 2, null },
                    { 46, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "Which method should you override to provide a string representation?", 0, null },
                    { 59, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, "Which keyword restricts a member to be accessible only within its own class?", 0, null },
                    { 60, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, "In C#, properties are essentially syntax sugar for getter and setter methods.", 1, null },
                    { 61, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, "Which C# feature allows setting a property value only during object initialization?", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "LessonId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, "Inheritance & Polymorphism Quiz", null },
                    { 13, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, "Interfaces & Abstract Classes Quiz", null },
                    { 14, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, "LINQ Essentials Quiz", null },
                    { 16, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, "SOLID Principles Quiz", null },
                    { 17, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, "Entity Framework Core Quiz", null },
                    { 18, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 23, "Authentication & Security Quiz", null },
                    { 19, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24, "Stacks & Queues Quiz", null },
                    { 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, "Sorting Algorithms Quiz", null },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 26, "Trees & BSTs Quiz", null },
                    { 22, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 27, "Advanced LINQ Quiz", null }
                });

            migrationBuilder.InsertData(
                table: "TutorialSteps",
                columns: new[] { "Id", "CodeSample", "Content", "CreatedAt", "LessonId", "Order", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 22, null, "Inheritance models an 'is-a' relationship. A Dog is an Animal. A Manager is an Employee.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, 1, "The 'is-a' Relationship", null },
                    { 23, "public class Animal  { public virtual  string Speak() => \"...\"; }\npublic class Dog : Animal { public override string Speak() => \"Woof!\"; }", "Mark a base class method virtual to allow overriding. In the derived class, use override to replace the implementation.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, 2, "virtual and override", null },
                    { 24, "Animal a = new Dog();\nConsole.WriteLine(a.Speak()); // \"Woof!\" — not \"...\"", "A variable declared as a base type can hold any derived type. The correct override is called at runtime.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, 3, "Polymorphism in Action", null },
                    { 25, "// Without LINQ\nvar result = new List<int>();\nforeach (int n in numbers)\n    if (n > 5) result.Add(n);\n\n// With LINQ\nvar result = numbers.Where(n => n > 5).ToList();", "Without LINQ you write imperative loops. With LINQ you write declarative queries that express *what* you want, not *how* to get it.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 1, "Why LINQ?", null },
                    { 26, "var result = numbers\n    .Where(n => n % 2 == 0)   // filter\n    .Select(n => n * n)        // transform\n    .OrderByDescending(n => n) // sort\n    .Take(3)                   // first 3\n    .ToList();", "LINQ operators return IEnumerable<T> and can be chained together. Each operator adds a step to the pipeline.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 2, "Chaining Operators", null },
                    { 27, "var q = numbers.Where(n => n > 3); // no execution yet\nnumbers.Add(99);                    // this IS included\nvar list = q.ToList();              // executes NOW — includes 99", "LINQ queries don't run until you enumerate them. Materialise with ToList() or ToArray() when you need a fixed snapshot.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 3, "Deferred Execution", null },
                    { 30, "// Bad: handles data AND saves to file\n// Good: User class + UserRepository class", "A class should have one, and only one, reason to change. Separate data, logic, and presentation.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, 1, "Single Responsibility", null },
                    { 31, "public class OrderService(IDbContext db) { ... }", "Depend on interfaces, not concrete classes. This makes your code testable and flexible.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, 2, "Dependency Inversion", null },
                    { 32, "public class MyDbContext : DbContext \n{ \n    public DbSet<User> Users { get; set; } \n}", "DbContext is your gateway to the database. It tracks changes and handles connectivity.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, 1, "The DbContext", null },
                    { 33, null, "Migrations evolve your database schema as your models change. Use 'dotnet ef migrations add' to create one.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, 2, "Migrations", null },
                    { 34, "var s = new Stack<int>();\ns.Push(1);\nint top = s.Pop(); // 1", "Last-In, First-Out. Think of a stack of plates. You add to the top and take from the top.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24, 1, "Stack (LIFO)", null },
                    { 35, "var q = new Queue<string>();\nq.Enqueue(\"Alice\");\nstring first = q.Dequeue(); // \"Alice\"", "First-In, First-Out. Think of a line at a store. The first person in is the first person served.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24, 2, "Queue (FIFO)", null },
                    { 36, null, "A divide-and-conquer algorithm. It picks a 'pivot' and partitions the array into smaller and larger elements.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, 1, "Quick Sort", null },
                    { 37, null, "A tree where each node has at most two children. Left child is smaller, right child is larger than parent.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 26, 1, "Binary Search Tree", null },
                    { 38, null, "Flattens nested collections. If you have a list of Departments, each with a list of Courses, SelectMany gives you one list of all Courses.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 27, 1, "SelectMany", null },
                    { 39, "var result = names.Zip(ages, (n, a) => $\"{n} is {a}\");", "Combines two sequences into one by pairing elements at the same index.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 27, 2, "Zip", null }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 130, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 43, "True", null },
                    { 131, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 43, "False", null },
                    { 132, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 44, "private", null },
                    { 133, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 44, "protected", null },
                    { 134, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 44, "internal", null },
                    { 135, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 44, "public", null },
                    { 136, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 46, "ToString()", null },
                    { 137, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 46, "Print()", null },
                    { 138, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 46, "Display()", null },
                    { 139, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 46, "Represent()", null },
                    { 174, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 59, "private", null },
                    { 175, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 59, "protected", null },
                    { 176, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 59, "internal", null },
                    { 177, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 59, "public", null },
                    { 178, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 60, "True", null },
                    { 179, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 60, "False", null },
                    { 180, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 61, "init", null },
                    { 181, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 61, "set", null },
                    { 182, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 61, "get", null },
                    { 183, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 61, "static", null }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 47, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "C# supports multiple class inheritance (inheriting from multiple base classes).", 1, null },
                    { 48, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "Which keyword is used to call the base class constructor?", 0, null },
                    { 49, "inherited", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "A sealed class cannot be ___.", 2, null },
                    { 50, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "Which keyword marks a method as overridable in a base class?", 0, null },
                    { 51, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, "A class can implement multiple interfaces.", 1, null },
                    { 52, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, "What prefix does C# convention use for interface names?", 0, null },
                    { 53, "cannot", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, "An abstract class ___ be instantiated directly.", 2, null },
                    { 54, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, "Which can have a constructor: abstract class or interface?", 0, null },
                    { 55, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, "LINQ queries use deferred execution by default.", 1, null },
                    { 56, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, "Which LINQ method filters elements based on a predicate?", 0, null },
                    { 57, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, "Which method forces immediate execution of a LINQ query into a List?", 0, null },
                    { 58, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, "Which LINQ method is preferred over Count() > 0 for checking existence?", 0, null },
                    { 62, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, "What does the 'S' in SOLID stand for?", 0, null },
                    { 63, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, "The Open/Closed principle states classes should be open for modification but closed for extension.", 1, null },
                    { 64, "abstractions", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, "Dependency Inversion suggests high-level modules should depend on ___ not concrete implementations.", 2, null },
                    { 65, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17, "What is the primary class used to coordinate EF Core functionality for a given data model?", 0, null },
                    { 66, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17, "NuGet package Microsoft.EntityFrameworkCore.Design is required to run migrations.", 1, null },
                    { 67, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17, "Which method applies migrations to the database at runtime?", 0, null },
                    { 68, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, "How many parts does a JSON Web Token (JWT) consist of?", 0, null },
                    { 69, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, "The [Authorize] attribute can be applied to both controllers and individual actions.", 1, null },
                    { 70, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, "Passwords should be stored as plain text for easy recovery.", 1, null },
                    { 71, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, "A Stack follows the FIFO (First-In-First-Out) principle.", 1, null },
                    { 72, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, "Which method adds an item to the top of a Stack?", 0, null },
                    { 73, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, "Which method removes and returns the item from the front of a Queue?", 0, null },
                    { 74, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "Which sorting algorithm has a O(n log n) best, average, and worst-case time complexity?", 0, null },
                    { 75, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "Bubble sort is generally the most efficient sorting algorithm for large datasets.", 1, null },
                    { 76, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "Which sorting algorithm uses a 'pivot' element?", 0, null },
                    { 77, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, "In a Binary Search Tree, the left child is always greater than its parent.", 1, null },
                    { 78, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, "Which traversal visits the root first, then left, then right?", 0, null },
                    { 79, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, "What is the average time complexity for searching in a balanced Binary Search Tree?", 0, null },
                    { 80, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, "Which LINQ method flattens a sequence of sequences into a single sequence?", 0, null },
                    { 81, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, "The GroupJoin operator is equivalent to a SQL Left Outer Join.", 1, null },
                    { 82, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22, "Which method merges two sequences by using a selector function to pair elements?", 0, null }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 140, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 47, "True", null },
                    { 141, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 47, "False", null },
                    { 142, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 48, "base", null },
                    { 143, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 48, "super", null },
                    { 144, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 48, "parent", null },
                    { 145, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 48, "this", null },
                    { 146, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 50, "virtual", null },
                    { 147, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 50, "abstract", null },
                    { 148, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 50, "override", null },
                    { 149, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 50, "sealed", null },
                    { 150, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 51, "True", null },
                    { 151, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 51, "False", null },
                    { 152, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 52, "I", null },
                    { 153, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 52, "IF", null },
                    { 154, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 52, "Int", null },
                    { 155, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 52, "Intf", null },
                    { 156, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 54, "Abstract class", null },
                    { 157, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 54, "Interface", null },
                    { 158, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 54, "Both", null },
                    { 159, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 54, "Neither", null },
                    { 160, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 55, "True", null },
                    { 161, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 55, "False", null },
                    { 162, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 56, "Where", null },
                    { 163, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 56, "Filter", null },
                    { 164, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 56, "Select", null },
                    { 165, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 56, "Find", null },
                    { 166, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 57, "ToList()", null },
                    { 167, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 57, "Execute()", null },
                    { 168, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 57, "Run()", null },
                    { 169, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 57, "Materialise()", null },
                    { 170, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 58, "Any()", null },
                    { 171, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 58, "Count() > 0", null },
                    { 172, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 58, "Exists()", null },
                    { 173, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 58, "Contains()", null },
                    { 184, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 62, "Single Responsibility", null },
                    { 185, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 62, "Software Security", null },
                    { 186, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 62, "Simple Routing", null },
                    { 187, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 62, "Static Relationship", null },
                    { 188, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 63, "True", null },
                    { 189, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 63, "False", null },
                    { 190, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 65, "DbContext", null },
                    { 191, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 65, "DbSet", null },
                    { 192, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 65, "Entity", null },
                    { 193, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 65, "QueryBuilder", null },
                    { 194, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 66, "True", null },
                    { 195, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 66, "False", null },
                    { 196, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 67, "Database.MigrateAsync()", null },
                    { 197, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 67, "Database.Update()", null },
                    { 198, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 67, "DbContext.Deploy()", null },
                    { 199, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 67, "EF.Apply()", null },
                    { 200, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 68, "3", null },
                    { 201, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 68, "2", null },
                    { 202, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 68, "4", null },
                    { 203, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 68, "1", null },
                    { 204, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 69, "True", null },
                    { 205, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 69, "False", null },
                    { 206, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 70, "True", null },
                    { 207, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 70, "False", null },
                    { 208, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 71, "True", null },
                    { 209, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 71, "False", null },
                    { 210, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 72, "Push()", null },
                    { 211, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 72, "Enqueue()", null },
                    { 212, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 72, "Add()", null },
                    { 213, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 72, "Pop()", null },
                    { 214, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 73, "Dequeue()", null },
                    { 215, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 73, "Pop()", null },
                    { 216, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 73, "Remove()", null },
                    { 217, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 73, "Poll()", null },
                    { 218, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 74, "Merge Sort", null },
                    { 219, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 74, "Bubble Sort", null },
                    { 220, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 74, "Insertion Sort", null },
                    { 221, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 74, "Selection Sort", null },
                    { 222, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 75, "True", null },
                    { 223, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 75, "False", null },
                    { 224, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 76, "Quick Sort", null },
                    { 225, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 76, "Merge Sort", null },
                    { 226, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 76, "Heap Sort", null },
                    { 227, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 76, "Shell Sort", null },
                    { 228, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 77, "True", null },
                    { 229, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 77, "False", null },
                    { 230, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 78, "Pre-order", null },
                    { 231, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 78, "In-order", null },
                    { 232, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 78, "Post-order", null },
                    { 233, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 78, "Level-order", null },
                    { 234, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 79, "O(log n)", null },
                    { 235, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 79, "O(n)", null },
                    { 236, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 79, "O(1)", null },
                    { 237, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 79, "O(n log n)", null },
                    { 238, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 80, "SelectMany", null },
                    { 239, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 80, "GroupBy", null },
                    { 240, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 80, "Join", null },
                    { 241, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 80, "Flatten", null },
                    { 242, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 81, "True", null },
                    { 243, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 81, "False", null },
                    { 244, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 82, "Zip", null },
                    { 245, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 82, "Concatenate", null },
                    { 246, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 82, "Merge", null },
                    { 247, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 82, "Combine", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 24);

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
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 64);

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

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "TutorialSteps",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "3-day streak", "" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Complete 3 lessons", "", "Quick Learner" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Pass your first quiz", "", "Quiz Whiz" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Complete a course", "", "Graduate" });

            migrationBuilder.UpdateData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Solve 5 coding challenges", "", "Challenge Master" });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title" },
                values: new object[] { "Print numbers 1-5, replacing multiples of 3 with Fizz.", "1\n2\nFizz\n4\nFizz", "Use modulo operator %", "for(int i=1;i<=5;i++){}", "loops,conditionals", "FizzBuzz" });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title" },
                values: new object[] { "Reverse the string \"hello\" and print it.", "olleh", "Use new string(s.Reverse().ToArray())", "string s=\"hello\";", "strings", "Reverse String" });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Sum the array [1,2,3,4,5] and print result.", "15", "Use a loop or a.Sum()", "int[] a={1,2,3,4,5};", "arrays", "Sum Array", 25 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print factorial of 5 (120).", "Medium", "120", "Multiply 1*2*3*4*5", "// compute 5!", "math,loops", "Factorial", 30 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print True if \"racecar\" is palindrome.", "Medium", "True", "Compare with reversed string", "string w=\"racecar\";", "strings", "Palindrome Check", 30 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print the 7th Fibonacci number (13).", "Medium", "13", "Iterate with two variables", "// fib sequence: 1,1,2,3,5,8,13", "math", "Fibonacci", 35 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print True if 17 is prime.", "Hard", "True", "Check divisors up to sqrt(n)", "int n=17;", "math", "Prime Check", 40 });

            migrationBuilder.UpdateData(
                table: "CodingChallenges",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Difficulty", "ExpectedOutput", "Hint", "StarterCode", "Tags", "Title", "XpReward" },
                values: new object[] { "Print index of 7 in sorted array [1,3,5,7,9].", "Hard", "3", "Classic binary search", "int[] a={1,3,5,7,9}; int target=7;", "algorithms", "Binary Search", 50 });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedOutput", "Hint", "Instructions", "LessonId", "StarterCode", "Title" },
                values: new object[] { "Hello, C#!", "Console.WriteLine(\"Hello, C#!\");", "Print: Hello, C#!", 1, "", "Hello, C#!" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedOutput", "Hint", "Instructions", "Order", "StarterCode", "Title" },
                values: new object[] { "30", "Console.WriteLine(a+b);", "Print sum of 10 and 20.", 1, "int a=10;int b=20;", "Add Numbers" });

            migrationBuilder.UpdateData(
                table: "CodingExercises",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Difficulty", "ExpectedOutput", "Hint", "Instructions", "LessonId", "Order", "StarterCode", "Title" },
                values: new object[] { 2, "Bob", "Console.WriteLine(p.Name);", "Print name Bob.", 7, 1, "public class Person{public string Name{get;set;}}", "Create Person" });

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
                columns: new[] { "Description", "LearningObjectives", "Title" },
                values: new object[] { "Conditionals and loops.", "Write if/else; Use for and while loops", "Section 3: Control Flow" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 2, "OOP foundations.", "Define classes; Create objects; Use properties", 1, "Section 1: Classes & Objects" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 2, "Extending and reusing code.", "Use inheritance; Override methods; Apply polymorphism", 2, "Section 2: Inheritance" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 3, "REST APIs with ASP.NET Core.", "Create controllers; Handle HTTP verbs; Return JSON", 1, "Section 1: Web API Basics" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Title" },
                values: new object[] { 4, "Arrays and linked lists.", "Implement arrays; Work with List<T>", "Section 1: Linear Structures" });

            migrationBuilder.UpdateData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CourseId", "Description", "LearningObjectives", "Order", "Title" },
                values: new object[] { 5, "Query syntax and method syntax.", "Write LINQ queries; Use lambdas", 1, "Section 1: LINQ Essentials" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EstimatedHours" },
                values: new object[] { "Master C# from zero — variables, control flow, methods, and the .NET ecosystem. Perfect for beginners.", 8 });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Classes, inheritance, polymorphism, interfaces, and design principles in C#.", 10, "Object-Oriented Programming" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Build modern web APIs and MVC apps with ASP.NET Core, EF Core, and REST best practices.", 12, "ASP.NET Core Web Development" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "EstimatedHours", "Title" },
                values: new object[] { "Arrays, lists, stacks, queues, sorting, and searching — implemented in C#.", 15, "Data Structures & Algorithms" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "EstimatedHours" },
                values: new object[] { "Query data with LINQ, lambdas, delegates, and functional patterns.", 6 });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DurationMinutes", "Title" },
                values: new object[] { 15, "Introduction to C#" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DurationMinutes", "LessonId", "Order", "Title", "VideoUrl" },
                values: new object[] { 20, 8, 1, "OOP in C#", "https://www.youtube.com/watch?v=wqUfllZyeq4" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DurationMinutes", "LessonId", "Title", "VideoUrl" },
                values: new object[] { 30, 9, "ASP.NET Core Tutorial", "https://www.youtube.com/watch?v=AhAxLiGC7Pc" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Title", "VoiceSummary" },
                values: new object[] { "- Follow naming conventions\n- Use meaningful names", "C# is a modern, object-oriented language by Microsoft for the .NET platform.", 10, "What is C#?", "C sharp is a modern language for dot NET." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Title", "VoiceSummary" },
                values: new object[] { "- Pin SDK with global.json", "Install the .NET SDK from dotnet.microsoft.com and verify with `dotnet --version`.", 15, "Setting Up .NET", "Install dot NET SDK to start coding." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "- Initialize on declaration", "```csharp\nint age = 25;\nstring name = \"Alice\";\n```", 2, 12, 1, "Declaring Variables", "Variables store typed data." });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Arithmetic: +, -, *, /. Comparison: ==, !=, <, >.", 10, 2, "Operators", 0, "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "", "```csharp\nif (score >= 70) Console.WriteLine(\"Pass\");\n```", 3, 8, 1, "If Statements", "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BestPractices", "Content", "DurationMinutes", "Order", "Title", "VoiceSummary" },
                values: new object[] { "", "for, while, and foreach loops control repetition.", 15, 2, "Loops", "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "- Use properties not public fields", "```csharp\npublic class Person { public string Name { get; set; } }\n```", 4, 20, 1, "Creating Classes", 4, "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Derive classes with `: BaseClass` syntax.", 5, 18, "Inheritance", 1, "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Create a Web API project with `dotnet new webapi`.", 6, 25, "Your First API", 0, "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Order", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Fixed-size collections: `int[] nums = new int[5];`", 7, 15, 1, "Arrays in C#", 2, "" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BestPractices", "Content", "CourseModuleId", "DurationMinutes", "Title", "Type", "VoiceSummary" },
                values: new object[] { "", "Query collections: `items.Where(x => x > 5)`", 8, 12, "Introduction to LINQ", 0, "" });

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
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 2, "False" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 3, "int" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 3, "string" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 5, "5" });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "QuestionId", "Text" },
                values: new object[] { 5, "23" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "Who developed C#?");

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
                columns: new[] { "QuizId", "Text" },
                values: new object[] { 2, "Which keyword declares int?" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CorrectAnswer", "QuizId", "Text" },
                values: new object[] { "string", 2, "Fill in: text type is ___." });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Text", "Type" },
                values: new object[] { "Output of Console.WriteLine(2+3);", 3 });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "C# Basics Quiz");

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Variables Quiz");

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
        }
    }
}
