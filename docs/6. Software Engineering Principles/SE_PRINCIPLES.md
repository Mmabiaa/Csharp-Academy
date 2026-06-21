# Software Engineering Principles Report

This document outlines the core engineering principles applied during the development of C# Academy. This serves as the technical justification for the platform's architecture and implementation details for the **Semester Project Submission**.

---

## 1. SOLID Principles

### **S**ingle Responsibility Principle (SRP)
Each class and method has a single reason to change. 
- **Example**: `RoslynCodeExecutionService` is only responsible for sandboxing code. It doesn't care about the user's XP or database records. `GamificationService` handles the logic for rewards, separated from the business logic of `LessonCompletion`.

### **O**pen/Closed Principle (OCP)
The system is open for extension but closed for modification.
- **Example**: The `AiAssistantService` uses an interface `IAiAssistantService`. If we want to switch from Google Gemini to OpenAI, we create a new implementation of the interface and swap the injection in `Program.cs` without changing a single line in the `AssistantController`.

### **L**iskov Substitution Principle (LSP)
Subtypes must be substitutable for their base types.
- **Example**: Every entity in the system inherits from the base `Entity` class. Repositories consume these entities uniformly, ensuring that any domain object can be handled by base repository logic (like setting `CreatedAt` timestamps).

### **I**nterface Segregation Principle (ISP)
Clients should not be forced to depend on methods they do not use.
- **Example**: Instead of one giant `IDatabaseService`, we have segregated interfaces like `ICourseRepository`, `IUserRepository`, and `IQuizRepository`. The `CoursesController` only depends on what it needs.

### **D**ependency Inversion Principle (DIP)
High-level modules should not depend on low-level modules; both should depend on abstractions.
- **Example**: The Application layer (High-level) depends on `ICertificatePdfService` (Abstraction). The actual `QuestPDF` implementation (Low-level/Infrastructure) is injected at runtime.

---

## 2. DRY (Don't Repeat Yourself) & Separation of Concerns

### Shared Logic
- **Base Handlers**: Shared logic for user identification and permission checking is encapsulated in MediatR pipeline behaviors and base controller methods.
- **Common DTOs**: Standardized response envelopes ensure the frontend parses errors and successes consistently.

### Separation of Layers
The frontend and backend are completely decoupled via a REST API. Inside the frontend:
- **UI vs Logic**: Components handle rendering, while **React Context** (Auth, Sound, Notifications) handles the complex state logic. **TanStack Query** handles cache synchronization, keeping the UI "dumb" and predictable.

---

## 3. Clean Code & Naming Conventions

- **Meaningful Names**: Methods are named as actions (`AwardXpAndUpdateStreakAsync`), and variables clearly describe their content (`solvedChallengeIds`).
- **Small Functions**: Handlers in the Application layer are strictly focused on one use-case, typically staying under 50 lines.
- **Self-Documenting Code**: Explicit typing and XML documentation on controllers reduce the need for external manuals.

---

## 4. Error Handling & Graceful Degradation

### Global Exception Handling
A middleware in the backend catches unhandled exceptions, logs them with `ILogger`, and returns a clean JSON error response, preventing stack trace leaks to the client.

### Graceful Degradation (AI)
If the **Gemini AI** service fails due to invalid API keys or rate limits, the system detects the exception and automatically switches to the `OfflineFallback` strategy. The user is notified of "Degraded" status but can still receive basic help.

---

## 5. Security Principles

- **Sandboxed Execution**: Student code is run via Roslyn with a custom `BlockedPatterns` list (restricting System.IO, System.Reflection, etc.) and a 10-second hard timeout.
- **Stateless Auth**: JWT (JSON Web Tokens) are used for secure, scalable authentication. Tokens are signed with a 256-bit Hmac key.
- **Input Validation**: `FluentValidation` ensures that Malformed data never reaches the Domain layer.

---

## 6. Performance Considerations

- **Audio Caching**: The `SoundContext` only loads audio assets once, preventing redundant network requests.
- **Server-State Caching**: `TanStack Query` caches course lists and user profiles, significantly reducing API load and providing an "instant" feel to the application.
- **Eager Loading**: `Include()` is used strategically in EF Core to prevent N+1 query problems in complex entity graphs (e.g., fetching a Course with its Modules and Lessons).
