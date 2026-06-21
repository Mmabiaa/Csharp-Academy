# Architecture

C# Academy is built using **Clean Architecture** principles.  The strict dependency flow ensures the core business logic remains isolated from infrastructure concerns.

---

## 🏗️ Architecture Layers

```
┌─────────────────────────────────────────────────────┐
│              PRESENTATION LAYER                     │
│   React 18 SPA (Vite/TypeScript)  |  ASP.NET Core  │
│              Web API                                │
└───────────────────────┬─────────────────────────────┘
                        │ HTTP / JSON
┌───────────────────────▼─────────────────────────────┐
│              APPLICATION LAYER                      │
│    MediatR Handlers · DTOs · FluentValidation       │
│         Organized by Feature (CQRS)                 │
└───────────────────────┬─────────────────────────────┘
                        │ Interfaces only
┌───────────────────────▼─────────────────────────────┐
│               DOMAIN LAYER (CORE)                   │
│    Entities · Interfaces · Business Rules           │
│         (Zero external dependencies)                │
└───────────────────────┬─────────────────────────────┘
                        │ Implements interfaces
┌───────────────────────▼─────────────────────────────┐
│            INFRASTRUCTURE LAYER                     │
│  EF Core/MySQL · Gemini AI · Roslyn · QuestPDF      │
│  JWT · File Storage · GamificationService           │
└─────────────────────────────────────────────────────┘
```

**Dependency Rule:** Arrows only point inwards. Infrastructure implements what Domain and Application declare.

---

## 📂 Project Structure Tree

```
Csharp-Academy/
├── src/
│   ├── backend/
│   │   ├── CsharpAcademy.Api/
│   │   │   ├── Controllers/
│   │   │   │   ├── AuthController.cs
│   │   │   │   ├── CoursesController.cs
│   │   │   │   ├── LessonsController.cs
│   │   │   │   ├── PracticesController.cs
│   │   │   │   ├── ClassroomsController.cs
│   │   │   │   ├── UsersController.cs
│   │   │   │   ├── FeatureControllers.cs  (Playground, Assistant, Certificates, Leaderboard)
│   │   │   │   └── PlatformControllers.cs (Admin, Teacher, Assignments, Challenges)
│   │   │   ├── appsettings.json
│   │   │   └── Program.cs
│   │   ├── CsharpAcademy.Application/
│   │   │   ├── Admin/ · Analytics/ · Assignments/ · Assistant/
│   │   │   ├── Auth/ · Certificates/ · Challenges/ · Classrooms/
│   │   │   ├── Common/Interfaces/  (All service contracts)
│   │   │   ├── Courses/ · Enrollments/ · Leaderboard/ · Lessons/
│   │   │   ├── Playground/ · Practices/ · Progress/ · Quizzes/
│   │   │   └── Users/
│   │   ├── CsharpAcademy.Domain/
│   │   │   ├── Entities/  (21 entity files)
│   │   │   └── Interfaces/ (15 repository interfaces)
│   │   └── CsharpAcademy.Infrastructure/
│   │       ├── Data/  (ApplicationDbContext + 15+ Repositories)
│   │       ├── Migrations/
│   │       └── Services/
│   │           ├── AiAssistantService.cs
│   │           ├── AiQuizGenerationService.cs
│   │           ├── CertificatePdfService.cs
│   │           ├── FileService.cs
│   │           ├── GamificationService.cs
│   │           ├── GeminiClient.cs
│   │           ├── JwtTokenService.cs
│   │           └── RoslynCodeExecutionService.cs
│   └── frontend/
│       └── src/
│           ├── components/
│           │   ├── Layout.tsx · CodeEditor.tsx · ConsolePanel.tsx
│           │   ├── NotificationOverlay.tsx · UserAvatar.tsx · VideoPlayer.tsx
│           ├── context/
│           │   ├── AuthContext.tsx · SoundContext.tsx · NotificationContext.tsx
│           ├── hooks/
│           │   └── useVoiceNarration.ts
│           ├── lib/
│           │   └── api.ts  (All API calls, 876 lines)
│           └── pages/
│               ├── admin/   (AdminDashboard, AdminCourses, AdminChallenges, AdminPractices)
│               ├── shared/  (Home, Login, Register, CertificateVerify)
│               ├── student/ (Courses, CourseDetail, LessonPage, QuizPage, Playground,
│               │             Assistant, Challenges, Practices, Classrooms, Leaderboard,
│               │             Profile, ProgressDashboard)
│               └── teachers/ (TeacherPortal, Assignments, Analytics)
└── docs/  (This documentation)
```

---

## 🔒 Dependency Rules (enforced via project references)

| Layer | Can Reference |
|---|---|
| `Domain` | Nothing |
| `Application` | `Domain` |
| `Infrastructure` | `Application`, `Domain` |
| `API` | `Application`, `Infrastructure` |
