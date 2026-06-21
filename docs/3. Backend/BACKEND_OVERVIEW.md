# Backend Overview

The C# Academy backend is a robust API built on **ASP.NET Core 10**, designed with a focus on modularity and high performance.

---

## 🏗️ Startup Pipeline (Program.cs)

The application bootstrap follows the modern Minimal API style (configured in `CsharpAcademy.Api/Program.cs`):

1. **Service Registration**:
   - Identity & JWT Auth setup.
   - MediatR scanning of the `Application` assembly.
   - EF Core with MySQL Pomelo provider.
   - `DotNetEnv` loading of `.env` variables into `IConfiguration`.
   - Infrastructure services (AI, Roslyn, QuestPDF, Gamification).
2. **Middleware Hierarchy**:
   - **CORS**: Configured for the Vite frontend.
   - **Authentication/Authorization**: Standard pipeline.
   - **ExceptionHandler**: Custom middleware for JSON error responses.
   - **Swagger**: Enabled for API documentation (dev only).

---

## 📁 Layer Breakdown

### Application Layer
- **Requests & Handlers**: All business logic is encapsulated in MediatR handlers. No logic lives in Controllers.
- **FluentValidation**: Pipelines automatically validate incoming data before the handler executes.
- **DTOs**: Clean contract separation between Domain and Frontend.

### Domain Layer (Core)
- **Entities**: Pure C# classes representing the database schema.
- **Interfaces**: Definitions for repositories and cross-cutting services.
- **Common**: Enums and shared value objects.

### Infrastructure Layer
- **Persistence**: `ApplicationDbContext` and repository implementations.
- **Services**: Heavyweight logic like AI API calls and code compilation.
- **Data Initialization**: `PlatformSeedData` for automatic environment setup.

---

## 🚀 Performance
- **Asynchronous throughout**: All DB and external API calls use `async/await` to avoid thread exhaustion.
- **Service Lifetimes**: Most repositories are `Scoped` (per-request), while `GeminiClient` (wrapped in a service) is also `Scoped` to handle configuration-specific instances correctly.
