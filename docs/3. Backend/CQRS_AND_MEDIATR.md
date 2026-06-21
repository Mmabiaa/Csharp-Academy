# CQRS and MediatR

C# Academy uses the **MediatR** library to implement a clean version of the Command Query Responsibility Segregation (CQRS) pattern.

---

## 🏗️ Structure

The Application layer is organized by Feature. Every feature folder (e.g., `Challenges`, `Courses`) contains:
- **Commands**: Requests that change state (e.g., `SubmitPracticeCommand`).
- **Queries**: Requests that only read data (e.g., `GetCourseDetailQuery`).
- **Handlers**: The actual logic executioners.
- **Validators**: `FluentValidation` rules for that specific request.

---

## 🛠️ The Request Pipeline

When an API call hits a controller, the following process occurs:

1. **Controller**: Receives the DTO and sends a MediatR Request: `_mediator.Send(command)`.
2. **Validation (Middleware)**: A pre-processor scans for a matching `AbstractValidator`. If input is invalid (e.g., empty password), it throws before reaching the handler.
3. **Handler**: 
   - Retrieves entities from Repositories.
   - Performs business logic (e.g., calculation of XP).
   - Saves changes.
   - Returns a result (usually a DTO).
4. **Controller**: Returns the result as `Ok(result)`.

---

## ⚖️ Why this approach?

- **Decoupling**: Controllers are "thin." They don't know *how* to register a user; they just know who to ask.
- **Parallel Development**: Developers can work on different handlers without touching shared service files.
- **Testing**: We can unit test a `Handler` without ever spinning up a web server or a real controller.
- **Traceability**: Every action in the system is represented by a single Request file, making it easy to audit "what can a user do."
