namespace CsharpAcademy.Application.Lessons.Queries;

internal static class LessonCompanionKnowledgeBase
{
    public static readonly List<string> Greetings = new()
    {
        "Hey there! Ready to learn some C#? 📚",
        "Welcome back! Let's dive right in! 🌊",
        "Hi! I'm here to help you with anything you need. 😊",
        "Ready to expand your C# knowledge? Let's go! 🧠",
    };

    public static readonly List<string> Encouragements = new()
    {
        "You're doing great — keep going! 💪",
        "Nice work on that practice! 🎯",
        "I can tell you're focused today. Awesome! ✨",
        "Every line of code makes you better. Keep it up! 🌟",
        "Mistakes are just learning opportunities. You've got this! 🚀",
        "You're halfway there — push through! 💫",
    };

    public static readonly List<string> Celebrations = new()
    {
        "Lesson complete! You're leveling up fast! 🎉🎊",
        "Woohoo! That's another one in the books! 🏆",
        "Amazing! You crushed this lesson! ⭐",
        "High five! You're becoming a real C# developer! 🖐️",
        "That's my student! Excellent work! 💚",
    };

    // Keys use pipe to denote OR matching. First pipe-segment becomes the user-facing dictionary key.
    public static readonly Dictionary<string, string> ConceptExplanations = new()
    {
        ["variable|int|string|data type"] =
            "A **variable** is like a labeled box that stores data. You declare what type of data goes in the box, give it a name, and optionally put a value inside.\n\n```csharp\n// Type  Name   Value\n  int   age  = 25;\n```\n\nCommon types in C#:\n- `int` → whole numbers\n- `double` → decimals\n- `string` → text\n- `bool` → true/false",
        ["class|object|oop"] =
            "A **class** is a blueprint for creating objects. It defines what data (fields/properties) and actions (methods) an object of that type will have.\n\n```csharp\npublic class Car\n{\n    public string Model { get; set; }   // data\n    public int Speed { get; set; }\n\n    public void Accelerate()            // action\n    {\n        Speed += 10;\n    }\n}\n\n// Create an object from the blueprint:\nvar myCar = new Car { Model = \"Tesla\", Speed = 0 };\nmyCar.Accelerate();  // Speed is now 10\n```",
        ["loop|for|while|foreach|iteration"] =
            "**Loops** repeat a block of code until a condition is met. C# has 4 main kinds:\n\n```csharp\n// 1. for loop — repeat a known number of times\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);  // 0, 1, 2, 3, 4\n\n// 2. while loop — repeat while condition is true\nint count = 3;\nwhile (count > 0)\n    Console.WriteLine(count--);\n\n// 3. foreach — iterate over a collection\nforeach (var name in names)\n    Console.WriteLine(name);\n\n// 4. do-while — runs at least once, then checks condition\n```\n\n**Rule of thumb:** Use `for` when you know the count, `foreach` for lists, `while` for unknown repetition.",
        ["method|function"] =
            "A **method** (or function) is a reusable block of code that does one job. Methods take *inputs* (parameters) and optionally return an *output*.\n\n```csharp\n// ReturnType Name  (Parameters)\n    int       Add   (int a, int b)\n    {\n        return a + b;   // ← output\n    }\n\nvar result = Add(3, 5);  // result = 8\n```\n\nGood methods:\n- Do one thing well (Single Responsibility)\n- Have descriptive names\n- Are small enough to read at a glance",
        ["if|conditional|else"] =
            "**Conditional logic** lets your code make decisions using `if`, `else if`, and `else`.\n\n```csharp\nint score = 85;\n\nif (score >= 90)\n    Console.WriteLine(\"A grade!\");\nelse if (score >= 80)\n    Console.WriteLine(\"B grade!\");\nelse\n    Console.WriteLine(\"Keep practicing!\");\n```\n\nComparison operators: `==`, `!=`, `>`, `<`, `>=`, `<=`  \nLogical operators: `&&` (AND), `||` (OR), `!` (NOT)",
        ["array|collection"] =
            "An **array** stores a fixed-size collection of items of the same type, indexed starting at 0.\n\n```csharp\nstring[] fruits = { \"apple\", \"banana\", \"cherry\" };\n\nConsole.WriteLine(fruits[0]);   // apple\nConsole.WriteLine(fruits[1]);   // banana\nConsole.WriteLine(fruits.Length);  // 3\n\n// Loop through with for or foreach:\nforeach (var fruit in fruits)\n    Console.WriteLine(fruit);\n```\n\nFor *resizable* collections, use `List<T>` instead!",
        ["list|list<t>|generic collection"] =
            "`List<T>` is C#'s **resizable array** — the most commonly used collection.\n\n```csharp\nvar numbers = new List<int> { 1, 2, 3 };\n\nnumbers.Add(4);           // [1,2,3,4]\nnumbers.Remove(2);        // [1,3,4]\nConsole.WriteLine(numbers.Count);   // 3\n\nif (numbers.Contains(3))\n    Console.WriteLine(\"Found 3!\");\n\n// LINQ magic:\nvar bigOnes = numbers.Where(n => n > 2).ToList();  // [3,4]\n```\n\n💡 Use `List<T>` instead of arrays whenever the size might change!",
        ["property|get|set"] =
            "A **property** is a smart field — it looks like data from the outside, but uses methods (getters/setters) internally.\n\n```csharp\npublic class Person\n{\n    // Auto-property (most common)\n    public string Name { get; set; }\n\n    // Property with logic and a backing field\n    private int _age;\n    public int Age\n    {\n        get => _age;\n        set\n        {\n            if (value < 0) throw new ArgumentException();\n            _age = value;\n        }\n    }\n\n    // Read-only computed property\n    public bool IsAdult => Age >= 18;\n}\n```",
        ["async|await|task"] =
            "**`async`/`await`** lets your program do other work while waiting for slow operations (like network calls or file reads).\n\n```csharp\npublic async Task<string> GetUserAsync(int id)\n{\n    // await pauses this method but NOT the whole program\n    var user = await _db.Users.FindAsync(id);\n    return user.Name;\n}\n```\n\n**Analogy:** Instead of staring at a microwave while food heats up, you set it and do other tasks. When it beeps, you come back for the food.",
        ["generic|generics"] =
            "**Generics** let you write code that works with *any* type, without rewriting it for `int`, `string`, `Customer`, etc.\n\n```csharp\n// A single method that works with ANY type\npublic T Echo<T>(T value)\n{\n    return value;\n}\n\nvar num = Echo(42);              // T = int\nvar msg = Echo(\"hello\");        // T = string\n```\n\nGenerics give you:\n- ✅ Code reuse\n- ✅ Type safety (no casting)\n- ✅ Performance (no boxing)\n\n`List<T>`, `Dictionary<TKey,TValue>`, and `IEnumerable<T>` are all generics you already use!",
        ["string|text|interpolation"] =
            "C# **strings** are sequences of characters enclosed in quotes. They are *immutable* — once created, they cannot be changed.\n\n```csharp\nstring name = \"Ada Lovelace\";\n\nConsole.WriteLine(name.Length);         // 12\nConsole.WriteLine(name.ToUpper());      // ADA LOVELACE\nConsole.WriteLine(name.Contains(\"Ace\")); // true\n\n// String interpolation with $\"...{expr}...\"\nConsole.WriteLine($\"Hello, {name}!\");\n\n// Verbatim strings with @\"...\" ignore escape characters:\nstring path = @\"C:\\Users\\Ada\\Code.cs\";\n```",
        ["namespace|using"] =
            "A **namespace** is like a folder for your code — it groups related classes and prevents naming conflicts.\n\n```csharp\n// Organize code into logical groups:\nnamespace CsharpAcademy.Utilities\n{\n    public class Logger  // this is CsharpAcademy.Utilities.Logger\n    {\n        public void Log(string msg) => Console.WriteLine(msg);\n    }\n}\n\n// Elsewhere, \"use\" the namespace to avoid typing the full path:\nusing CsharpAcademy.Utilities;\n\nvar logger = new Logger();  // ✓ Works, thanks to 'using'\n```\n\n**Why they matter:** Two libraries can both have a class called `Logger` — putting them in different namespaces (`Amazon.Logger` vs `Microsoft.Logger`) prevents collisions.",
        ["value type|reference type|stack|heap"] =
            "This is one of the *most important* concepts in C#! 🔥\n\n| Value types | Reference types |\n|---|---|\n| Stores the **actual value** | Stores a **reference (pointer)** to the value |\n| Lives on the **stack** | Lives on the **heap** |\n| Copy = duplicate the value | Copy = duplicate the reference (same data!) |\n| Examples: `int`, `double`, `bool`, `enum`, `struct` | Examples: `string`, `class`, `array`, `List<T>` |\n\n```csharp\n// VALUE TYPE — independent copies\nint a = 10;\nint b = a;   // b is a COPY\nb = 20;\nConsole.WriteLine(a);  // 10 (unchanged)\n\n// REFERENCE TYPE — share the same data\nvar listA = new List<int> { 1, 2, 3 };\nvar listB = listA;     // listB points to the SAME list\nlistB.Add(4);\nConsole.WriteLine(listA.Count);  // 4 (changed!)\n```",
        ["static|static keyword"] =
            "**`static`** means something belongs to the *class itself*, not to individual objects (instances) of that class.\n\n```csharp\npublic class MathUtils\n{\n    // Static method — call on the CLASS:\n    public static int Add(int a, int b) => a + b;\n\n    // Instance method — call on an OBJECT:\n    public int Multiply(int a, int b) => a * b;\n}\n\n// Static: no 'new' needed!\nvar sum = MathUtils.Add(2, 3);   // ✓\n\n// Instance: need an object first\nvar utils = new MathUtils();\nvar product = utils.Multiply(2, 3);  // ✓\n```\n\n**When to use static:** Utility methods (like `Console.WriteLine`, `Math.Sqrt`), constants, and singleton patterns. Don't overuse it — static code is harder to test!",
        ["inheritance|polymorphism|parent class"] =
            "**Inheritance** (one of the 4 OOP pillars) lets a child class reuse code from a parent class:\n\n```csharp\npublic class Animal  // parent\n{\n    public string Name { get; set; }\n    public virtual void Speak() => Console.WriteLine(\"...\");\n}\n\npublic class Dog : Animal  // child\n{\n    public override void Speak() => Console.WriteLine(\"Woof!\");\n}\n\npublic class Cat : Animal  // child\n{\n    public override void Speak() => Console.WriteLine(\"Meow!\");\n}\n\n// Polymorphism: one interface, many implementations\nAnimal pet = new Dog();\npet.Speak();  // Woof!  (calls Dog's version at runtime)\n```",
    };

    public static readonly List<QaEntry> QuestionAnswers = new()
    {
        new QaEntry(
            Keywords: new() { "what is c#", "about c#", "c# language", "what c#" },
            Question: "What is C#?",
            Answer:
                "**C#** (pronounced \"C-sharp\") is a modern, type-safe, object-oriented programming language created by Microsoft in 2000 as part of the .NET platform.\n\n**What makes it special:**\n- 💼 Used for enterprise apps, games (Unity), web (Blazor/ASP.NET), mobile (MAUI), and cloud (Azure)\n- 🎯 Garbage-collected (no manual memory management!)\n- 🧬 Full OOP support + functional features (LINQ, lambdas)\n- 🛡️ Strong typing catches bugs at compile time\n- 👥 Massive ecosystem + Microsoft backing"),
        new QaEntry(
            Keywords: new() { "difference between class and object", "class vs object", "object instance", "instance vs class" },
            Question: "What's the difference between a class and an object?",
            Answer:
                "Great question! Think of it like architecture vs. real buildings:\n\n| Class | Object |\n|---|---|\n| 📐 Blueprint | 🏠 Actual house |\n| Defines the structure | Is a concrete instance |\n| One class definition | Unlimited objects from it |\n| Doesn't occupy memory | Uses memory |\n\n```csharp\n// CLASS — the blueprint\npublic class Cookie\n{\n    public string Flavor { get; set; }\n    public bool IsBaked { get; set; }\n}\n\n// OBJECTS — actual cookies made from the blueprint\nvar chocoChip = new Cookie { Flavor = \"Chocolate Chip\", IsBaked = true };\nvar oatmeal = new Cookie { Flavor = \"Oatmeal\", IsBaked = false };\n```\n\n**Bottom line:** A class is the *recipe*, an object is the *actual cookie* you eat. 🍪"),
        new QaEntry(
            Keywords: new() { "what is namespace", "namespace for", "using namespace", "namespace purpose" },
            Question: "What is a namespace?",
            Answer:
                "A **namespace** is like a folder for your code — it groups related classes and prevents naming conflicts.\n\n```csharp\n// Organize code into logical groups:\nnamespace CsharpAcademy.Utilities\n{\n    public class Logger  // this is CsharpAcademy.Utilities.Logger\n    {\n        public void Log(string msg) => Console.WriteLine(msg);\n    }\n}\n\n// Elsewhere, \"use\" the namespace to avoid typing the full path:\nusing CsharpAcademy.Utilities;\n\nvar logger = new Logger();  // ✓ Works, thanks to 'using'\n```\n\n**Why they matter:** Two libraries can both have a class called `Logger` — putting them in different namespaces (`Amazon.Logger` vs `Microsoft.Logger`) prevents collisions."),
        new QaEntry(
            Keywords: new() { "value type reference type", "difference value reference", "stack heap", "pass by value reference" },
            Question: "Value types vs. reference types — what's the difference?",
            Answer:
                "This is one of the *most important* concepts in C#! 🔥\n\n| Value types | Reference types |\n|---|---|\n| Stores the **actual value** | Stores a **reference (pointer)** to the value |\n| Lives on the **stack** | Lives on the **heap** |\n| Copy = duplicate the value | Copy = duplicate the reference (same data!) |\n| Examples: `int`, `double`, `bool`, `enum`, `struct` | Examples: `string`, `class`, `array`, `List<T>` |\n\n```csharp\n// VALUE TYPE — independent copies\nint a = 10;\nint b = a;   // b is a COPY\nb = 20;\nConsole.WriteLine(a);  // 10 (unchanged)\n\n// REFERENCE TYPE — share the same data\nvar listA = new List<int> { 1, 2, 3 };\nvar listB = listA;     // listB points to the SAME list\nlistB.Add(4);\nConsole.WriteLine(listA.Count);  // 4 (changed!)\n```"),
        new QaEntry(
            Keywords: new() { "static meaning", "what is static", "static keyword", "when use static" },
            Question: "What does the 'static' keyword do?",
            Answer:
                "**`static`** means something belongs to the *class itself*, not to individual objects (instances) of that class.\n\n```csharp\npublic class MathUtils\n{\n    // Static method — call on the CLASS:\n    public static int Add(int a, int b) => a + b;\n\n    // Instance method — call on an OBJECT:\n    public int Multiply(int a, int b) => a * b;\n}\n\n// Static: no 'new' needed!\nvar sum = MathUtils.Add(2, 3);   // ✓\n\n// Instance: need an object first\nvar utils = new MathUtils();\nvar product = utils.Multiply(2, 3);  // ✓\n```\n\n**When to use static:** Utility methods (like `Console.WriteLine`, `Math.Sqrt`), constants, and singleton patterns. Don't overuse it — static code is harder to test!"),
        new QaEntry(
            Keywords: new() { "practice stuck", "stuck practice", "help practice", "cant solve", "can't solve", "code not working" },
            Question: "I'm stuck on the practice — what should I do?",
            Answer:
                "Don't worry — getting stuck is *normal* and it means you're learning! 💡 Try this approach:\n\n1. **Re-read the instructions** out loud — sometimes hearing it helps 🗣️\n2. **Break it into tiny steps** — solve just one part at a time\n3. **Check the starter code** for any clues or structure\n4. **Click the Hint button** — I wrote them specifically for this problem!\n5. **Run the code even if it's incomplete** — the error message might tell you exactly what's wrong\n6. **Draw it on paper** — a quick sketch of the logic works wonders ✏️\n\nIf all else fails, ask me a *specific* question like: *\"How do I check if a number is even?\"* — and I'll guide you without giving away the answer!"),
        new QaEntry(
            Keywords: new() { "difference for foreach", "foreach vs for", "when to use foreach", "for foreach when" },
            Question: "When should I use for vs. foreach?",
            Answer:
                "Use this simple rule of thumb:\n\n👉 **Use `foreach`** when you want to *look at every item* in a collection and you don't need the index number:\n\n```csharp\nforeach (var student in students)\n    Console.WriteLine(student.Name);\n```\n\n👉 **Use `for`** when you need the *index*, or you need to skip/modify items:\n\n```csharp\n// Need index? → for\nfor (int i = 0; i < students.Count; i++)\n    Console.WriteLine($\"Student #{i + 1}: {students[i].Name}\");\n\n// Modify list while iterating? → for (backwards!)\nfor (int i = list.Count - 1; i >= 0; i--)\n    if (list[i] < 0) list.RemoveAt(i);\n```\n\n**Summary:** If in doubt and it's a simple read-only loop → `foreach`. If you need the index or want to change the collection → `for`."),
        new QaEntry(
            Keywords: new() { "linq what is", "what is linq", "linq meaning", "explain linq" },
            Question: "What is LINQ?",
            Answer:
                "**LINQ** (*Language-Integrated Query*) adds query capabilities directly into C# so you can filter/sort/project data with readable method chains instead of writing raw loops.\n\n```csharp\n// Instead of this:\nvar bigNums = new List<int>();\nforeach (var n in numbers)\n    if (n > 100) bigNums.Add(n * 2);\n\n// Write this (LINQ methods + lambdas):\nvar bigNums = numbers\n    .Where(n => n > 100)\n    .Select(n => n * 2)\n    .OrderBy(n => n)\n    .ToList();\n```\n\nCommon LINQ methods:\n- `.Where(...)` — filter items\n- `.Select(...)` — transform/project\n- `.OrderBy(...)` / `.OrderByDescending(...)` — sort\n- `.First(...)` / `.FirstOrDefault(...)` — take first\n- `.Any(...)` / `.All(...)` — boolean checks\n- `.GroupBy(...)` — aggregate\n- `.Sum(...)`, `.Average(...)`, `.Count(...)` — math aggregates"),
    };

    public static readonly List<ExampleEntry> Examples = new()
    {
        new ExampleEntry(
            Concept: "If-else decision making",
            Analogy: "It's like a choose-your-own-adventure book — based on what's true, you turn to a different page.",
            CodeSample: "int age = 15;\n\nif (age >= 18)\n    Console.WriteLine(\"You can vote!\");\nelse if (age >= 16)\n    Console.WriteLine(\"You can drive!\");\nelse\n    Console.WriteLine(\"Still growing — keep learning!\");"),
        new ExampleEntry(
            Concept: "List<T> in action",
            Analogy: "A shopping list — you add items, remove them, and check if something is already on the list.",
            CodeSample: "var shopping = new List<string>();\nshopping.Add(\"Milk\");\nshopping.Add(\"Bread\");\nshopping.Add(\"Eggs\");\n\nshopping.Remove(\"Bread\");  // already have some\n\nif (shopping.Contains(\"Eggs\"))\n    Console.WriteLine(\"Eggs are on the list\");\n\nConsole.WriteLine($\"{shopping.Count} items total\");"),
        new ExampleEntry(
            Concept: "Method with return value",
            Analogy: "A soda machine — you put in coins (input) and get back a soda (output).",
            CodeSample: "decimal CalculateTotal(decimal price, int quantity, decimal discount)\n{\n    var subtotal = price * quantity;\n    return subtotal - discount;\n}\n\nvar total = CalculateTotal(9.99m, 2, 2.00m);\nConsole.WriteLine($\"Total: ${total}\");  // $17.98"),
        new ExampleEntry(
            Concept: "Constructor initialization",
            Analogy: "When you bake a cookie from the recipe — you set the flavor and bake-status right when it comes out of the oven.",
            CodeSample: "public class Cookie\n{\n    public string Flavor { get; }\n    public bool IsBaked { get; private set; }\n\n    // Constructor runs when you 'new' up a Cookie\n    public Cookie(string flavor)\n    {\n        Flavor = flavor;\n        IsBaked = false;\n    }\n\n    public void Bake() => IsBaked = true;\n}\n\nvar snack = new Cookie(\"Chocolate Chip\");\nsnack.Bake();"),
        new ExampleEntry(
            Concept: "Dictionary for lookups",
            Analogy: "A real dictionary — you look up a *word* (key) and get the *definition* (value) in O(1) time.",
            CodeSample: "var capitals = new Dictionary<string, string>\n{\n    [\"France\"] = \"Paris\",\n    [\"Japan\"] = \"Tokyo\",\n    [\"Ghana\"] = \"Accra\",\n};\n\nif (capitals.TryGetValue(\"Japan\", out var tokyo))\n    Console.WriteLine($\"Japan's capital is {tokyo}.\");\n\nforeach (var (country, city) in capitals)\n    Console.WriteLine($\"{country} → {city}\");"),
    };

    public static readonly List<QuizEntry> MiniQuizzes = new()
    {
        new QuizEntry(
            Topics: new() { "const", "readonly", "immutable", "variable", "keyword" },
            Question: "Which keyword declares a variable that CANNOT be changed after assignment?",
            Options: new List<string> { "dynamic", "const", "var", "static" },
            CorrectIndex: 1,
            Explanation:
                "✅ `const` — its value is set at compile time and is permanently fixed.\n\n`readonly` is similar but allows assignment in the constructor. The other options: `var` lets the compiler infer the type, `dynamic` bypasses type checking, and `static` means belongs-to-class, not about mutability."),
        new QuizEntry(
            Topics: new() { "operator", "concatenation", "string", "addition" },
            Question: "What is the output of: `Console.WriteLine(5 + 5 + \"5\");`",
            Options: new List<string> { "15", "\"555\"", "\"105\"", "Compiler error" },
            CorrectIndex: 2,
            Explanation:
                "✅ `\"105\"` — C# evaluates left-to-right.\n\n1. `5 + 5` → both ints → **10**  \n2. `10 + \"5\"` → int + string → **\"105\"** (the int is converted to string and concatenated)\n\nIf it were `\"5\" + 5 + 5`, the answer would be `\"555\"`!"),
        new QuizEntry(
            Topics: new() { "dictionary", "collection", "lookup", "keyvalue" },
            Question: "Which data structure would you use for a key → value lookup?",
            Options: new List<string> { "List<T>", "Queue<T>", "Dictionary<TKey, TValue>", "HashSet<T>" },
            CorrectIndex: 2,
            Explanation:
                "✅ `Dictionary<TKey, TValue>` — super fast lookups by key (O(1) average).\n\n- `List<T>` → ordered by index, no built-in key\n- `Queue<T>` → First-In-First-Out ordering\n- `HashSet<T>` → unique items, no values attached, just membership check"),
        new QuizEntry(
            Topics: new() { "access modifier", "private", "encapsulation", "public internal" },
            Question: "In C#, what does access modifier 'private' mean?",
            Options: new List<string>
            {
                "Only code in the same namespace can access it",
                "Only code in the same class/struct can access it",
                "Only derived classes can access it",
                "Any code anywhere can access it",
            },
            CorrectIndex: 1,
            Explanation:
                "✅ `private` = accessible *only* inside the class or struct that declared it.\n\nIt's the *most restrictive* access level and the default for class members. The full set (from most → least restrictive):  \n`private` → `protected` → `internal` → `protected internal` → `public`"),
        new QuizEntry(
            Topics: new() { "linq", "where", "select", "orderby", "projection" },
            Question: "What does LINQ's `.Where()` do?",
            Options: new List<string>
            {
                "Projects each item into a new form",
                "Filters a collection, keeping only items that match a condition",
                "Sorts the collection in descending order",
                "Returns the first item in the collection",
            },
            CorrectIndex: 1,
            Explanation:
                "✅ `.Where()` filters — keeps only items where the condition is `true`.\n\nThink of it like a coffee filter: grounds stay behind, only the liquid passes through ☕\n\nFor reference:\n- `.Select()` → projects/transforms\n- `.OrderByDescending()` → sorts desc\n- `.First()` → gets first item"),
        new QuizEntry(
            Topics: new() { "exception", "try catch", "error handling", "throw" },
            Question: "Which pair of keywords lets you gracefully handle runtime errors?",
            Options: new List<string> { "if / else", "try / catch", "lock / unlock", "async / await" },
            CorrectIndex: 1,
            Explanation:
                "✅ `try { } catch { }` — code that may throw goes in `try`, the recovery/handling logic goes in `catch`.\n\n```csharp\ntry\n{\n    int zero = 0;\n    int x = 10 / zero;  // boom 💥\n}\ncatch (DivideByZeroException)\n{\n    Console.WriteLine(\"Cannot divide by zero!\");\n}\n```\n\nAdd a `finally { }` block for cleanup code that *must* run either way."),
        new QuizEntry(
            Topics: new() { "constructor", "initializer", "class object", "ctor" },
            Question: "When is a class constructor executed?",
            Options: new List<string>
            {
                "Every time a method on the class is called",
                "Once per application, when the app starts",
                "When you create a new instance of the class with 'new'",
                "When the garbage collector runs",
            },
            CorrectIndex: 2,
            Explanation:
                "✅ Constructors run **once per object** at the moment you call `new YourClass(...)`.\n\nUse them to enforce required arguments, initialize state, and ensure an object is always valid before anyone uses it."),
    };

    public sealed record QaEntry(List<string> Keywords, string Question, string Answer);
    public sealed record ExampleEntry(string Concept, string Analogy, string CodeSample);
    public sealed record QuizEntry(
        List<string> Topics,
        string Question,
        List<string> Options,
        int CorrectIndex,
        string Explanation);
}
