# Technical Architecture

C# Academy is built using **Clean Architecture** principles, often referred to as Onion Architecture. This design pattern enforces a strict dependency flow, ensuring the core business logic (Domain) remains isolated from external concerns like databases, UI frameworks, and third-party APIs.

---

## 🏛 Architectural Layers

### 1. Domain Layer (Core)
The center of the onion. It is entirely self-contained and holds the fundamental logic of the application.
- **Entities**: Core business objects (e.g., `Course`, `Lesson`, `Badge`).
- **Value Objects**: Data structures without identity (e.g., `Address`, `Points`).
- **Enums**: Domain-specific constants (e.g., `LessonType`, `Difficulty`).
- **Interfaces**: Abstractions for data persistence and external services.

### 2. Application Layer
Contains the application's use cases and orchestrates the flow of data.
- **Commands & Queries**: Implemented using the **MediatR** pattern for CQRS separation.
- **DTOs**: Data Transfer Objects for communication with the API.
- **Behaviors**: Cross-cutting concerns like Validation (FluentValidation) and Logging.
- **Services**: Advanced logic that doesn't fit into a single entity.

### 3. Infrastructure Layer
Handles data persistence and communication with external systems.
- **Persistence**: EF Core implementations of Domain repositories using MySQL.
- **External Services**: Integrations with Gemini AI, QuestPDF, and text-to-speech providers.
- **Security**: JWT token generation and identity management.

### 4. Presentation Layer (API & Frontend)
The entry point for users and consumers.
- **Web API**: ASP.NET Core controllers exposing RESTful endpoints.
- **Frontend**: A modern React-based Single Page Application (SPA).
- **Documentation**: Swagger/OpenAPI integration for API exploration.

---

## 📡 Technology Stack

### Backend Ecosystem
- **Framework**: ASP.NET Core 10 (Targeting .NET 10 Preview)
- **Database**: MySQL 8.0 via Pomelo Entity Framework Core
- **Patterns**: MediatR (Mediator), Repository Pattern, Dependency Injection
- **Validation**: FluentValidation
- **Execution**: Roslyn Scripting API for sandboxed C# execution

### Frontend Ecosystem
- **Framework**: React 18 + Vite (for lightning-fast builds)
- **State Management**: TanStack Query (React Query)
- **Styling**: Tailwind CSS + shadcn/ui components
- **Navigation**: React Router 6
- **Type Safety**: TypeScript 5+

---

## 🔒 Security Principles
- **Authentication**: Stateless JWT Bearer tokens.
- **Authorization**: Role-based access control (RBAC) across Student, Teacher, and Admin tiers.
- **Audit**: Automated timestamps on all trackable entities.
- **Sandbox**: Code execution is restricted via Roslyn and monitored with timeouts.
