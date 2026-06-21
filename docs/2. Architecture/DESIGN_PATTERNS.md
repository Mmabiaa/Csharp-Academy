# Design Patterns

C# Academy employs several industry-standard design patterns to ensure scalability, maintainability, and clean separation of concerns.

---

## 1. Architectural Patterns

### Clean Architecture (Onion Architecture)
The core business logic (Domain) sits at the center, with Application, Infrastructure, and Presentation layers wrapped around it. Dependencies only flow inwards, keeping the heart of the system independent of external frameworks like EF Core or React.

### CQRS (Command Query Responsibility Segregation)
Implemented via the **MediatR** library. We separate read operations (Queries) from write operations (Commands).
- **Commands**: `RegisterCommand`, `SubmitChallengeCommand`, `CreateAssignmentCommand`.
- **Queries**: `GetUserProfileQuery`, `GetCoursesQuery`, `GetLeaderboardQuery`.
*Benefits*: Each handler has a single responsibility, making the code highly testable and preventing "Fat Controllers."

---

## 2. Structural & Creational Patterns

### Repository Pattern
We use generic and specialized repositories (e.g., `ICourseRepository`, `IUserRepository`) to abstract the data access layer.
- **Interfaces**: Defined in `CsharpAcademy.Domain/Interfaces`.
- **Implementation**: Handled in `CsharpAcademy.Infrastructure/Data`.
*Benefits*: Allows switching from MySQL to another database without touching business logic.

### Dependency Injection (DI)
The foundation of the entire backend. We use constructor injection to provide services and repositories to controllers and handlers.
*Benefits*: Decouples high-level modules from low-level ones, following the Dependency Inversion Principle (SOLID).

### Data Transfer Objects (DTO)
Used to shape data for the frontend and hide sensitive internal entity details (e.g., password hashes).
*Examples*: `UserProfileDto`, `CourseDetailDto`.

---

## 3. Behavioral Patterns

### Strategy Pattern (AI Fallback)
Implemented in `AiAssistantService` and `AiQuizGenerationService`.
- **Online Strategy**: Uses `GeminiClient` to hit the Google Gemini API.
- **Offline Strategy**: Uses a rule-based/pre-defined keyword matching system if the API key is missing or the service is unreachable.
*Benefits*: Ensures "Graceful Degradation"—the app remains functional even without internet or API credits.

### Context Pattern (React)
The frontend uses the Context API as a global state provider.
- `AuthContext`: Manages JWT, user roles, and profile state.
- `SoundContext`: Orchestrates 3D audio feedback across the UI.
- `NotificationContext`: Manages the queue of gamification toasts and achievements.

### Observer Pattern (Event-Driven UI)
While not using a traditional "Subject/Observer" class, the platform follows observer principles:
- **Gamification**: When a lesson is completed, the `GamificationService` awards XP, which triggers a state update in the `AuthContext`, which in turn triggers a re-render of the XP balance in the Navbar and a "Level Up" toast in the `NotificationContext`.

---

## 4. Cross-Cutting Concerns

### Middleware Chain
Standard ASP.NET Core middleware handles logging, CORS, authentication, and exception handling in a pipeline fashion.

### Fluent Validation
Decorators/Validators (via `FluentValidation`) are used in the Application layer to ensure data integrity before commands reach the handlers.
