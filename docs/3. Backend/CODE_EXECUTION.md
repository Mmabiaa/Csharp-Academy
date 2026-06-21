# Sandboxed Code Execution

C# Academy allows students to run real C# code in the browser. This is powered by **Roslyn (The .NET Compiler Platform)** on the backend, wrapped in a multi-layered security sandbox.

---

## 🛠️ Implementation Detail

The service is implemented in `RoslynCodeExecutionService.cs`. 

### The Execution Pipeline:
1. **Source Normalization**: Wrap the student's code snippet into a valid C# script context.
2. **IO Redirection**: Redirect `Console.Out` and `Console.Error` to a `StringWriter` to capture output without affecting the server's own console.
3. **Compilation**: Use `CSharpScript.EvaluateAsync` to compile and execute the code in memory.
4. **Result Capture**: Return the captured output or any compilation/runtime errors to the frontend.

---

## 🛡️ Security Constraints

Executing user-submitted code is inherently dangerous. We mitigate this through several layers:

### 1. Namespace Blacklisting
Before execution, the code is scanned for "Blocked Patterns". Any code containing these keywords is rejected before compilation:
- `System.IO` (Prevents file system access)
- `System.Reflection` (Prevents bypassing private access)
- `System.Net` (Prevents network attacks)
- `Process.Start` (Prevents executing OS commands)
- `Environment.Exit` (Prevents crashing the server)

### 2. Timeouts
Each execution is bounded by a **10-second CancellationToken**. If the user writes an infinite loop (`while(true)`), the task is force-terminated to preserve server resources.

### 3. Resource Limits
Code is executed asynchronously using a `SemaphoreSlim`. This prevents a "Denial of Service" attack where many users try to exhaust the server's CPU by running heavy calculations simultaneously.

---

## 💻 Playground Features

- **Standard Libraries**: Students have access to `System`, `System.Linq`, `System.Collections.Generic`, and `System.Text`.
- **Error Reporting**: Compilation errors (e.g., missing semicolons) are returned with line numbers and diagnostic messages, helping students learn from their mistakes.
- **Implicit Usings**: Common namespaces are pre-referenced to allow for cleaner, "script-like" code (e.g., `Console.WriteLine("Hello")` works without a `using System;` or `Main` method).
