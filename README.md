# C# Academy

[![Status](https://img.shields.io/badge/status-v1.0.0--stable-brightgreen)](docs/STATUS.md)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](build_output.txt)
[![Architecture](https://img.shields.io/badge/architecture-clean-blue)](docs/ARCHITECTURE.md)

**C# Academy** is an intelligent, gamified learning platform designed to bridge the gap between theory and practice for aspiring C# developers. Featuring a deep integration with Google Gemini for AI tutoring and a sandboxed Roslyn execution environment, it provides a complete ecosystem for students, teachers, and administrators.

---

## 🚀 Key Features

### 🎓 For Students
- **Structured Curriculum**: 5 foundational courses covering everything from Basics to ASP.NET Core.
- **Interactive Playground**: Write and execute C# code directly in the browser with real-time feedback.
- **AI Learning Assistant**: Integrated "Gemini-1.5-Flash" for line-by-line code explanations and debugging.
- **Gamified Experience**: Earn XP, maintain daily streaks, and unlock achievement badges.
- **Certification**: Automated PDF certificates of completion for verified skills.

### 👩‍🏫 For Teachers
- **Classroom Management**: Create virtual classrooms and manage student enrollments.
- **Assignment System**: Deploy coding or essay-based assignments with automated delivery.
- **Grading & Feedback**: Review student code submissions and provide detailed performance reviews.
- **Analytics Dashboard**: Monitor class-wide progress and identify students needing assistance.

### 🛠 For Administrators
- **Platform Oversight**: Full control over user accounts, course content, and global metrics.
- **Course Authoring**: Create and publish new modules, lessons, and assessments.
- **System Monitoring**: Track platform health and user engagement stats.

---

## 🏗 System Architecture

The project is built following **Clean Architecture** principles, ensuring a decoupled, testable, and maintainable codebase.

- **Presentation**: React 18 + Vite + TypeScript (SPA) / ASP.NET Core Web API
- **Application**: MediatR Use Cases, FluentValidation, AutoMapper
- **Domain**: Pure POCO Entities, Value Objects, Domain Exceptions
- **Infrastructure**: Entity Framework Core 9 (MySQL), Identity, AI Services (Gemini), PDF Generation (QuestPDF)

For more details, see the [Architecture Documentation](docs/ARCHITECTURE.md).

---

## 💻 Technology Stack

| Layer | Technologies |
|---|---|
| **Backend** | .NET 10 (Preview), ASP.NET Core API |
| **Logic** | MediatR, C# 13, Roslyn (Code Execution) |
| **Frontend** | React 18, Tailwind CSS, Shadcn/UI, TanStack Query |
| **Database** | MySQL (Pomelo Provider) |
| **AI/ML** | Google Gemini API (AI Tutor & Quiz Generation) |
| **Tooling** | Vite, Prettier, ESLint, Git |

---

## 🛠 Developer Setup

### Prerequisites
- .NET 9 or 10 SDK
- Node.js (v18+)
- MySQL Server (v8.0+)
- Google Gemini API Key

### Backend Setup
1. Navigate to `src/backend`.
2. Create a `.env` file based on `.env.example`.
3. Configure your `CONNECTION_STRING` and `GEMINI_API_KEY`.
4. Apply migrations:
   ```powershell
   dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
   ```
5. Launch the API:
   ```powershell
   dotnet run --project CsharpAcademy.Api
   ```

### Frontend Setup
1. Navigate to `src/frontend`.
2. Install dependencies:
   ```bash
   npm install
   ```
3. Launch the dev server:
   ```bash
   npm run dev
   ```

---

## 🔑 Access Credentials

| User Role | Email | Password |
|---|---|---|
| **Administrator** | `admin@academy.com` | `Admin123!` |
| **Teacher** | `teacher@academy.com` | `Teacher123!` |

---

## 🗺 Documentation
- [Release Notes & Changelog](docs/CHANGELOG.md)
- [Detailed Project Status](docs/STATUS.md)
- [Architecture Overview](docs/ARCHITECTURE.md)
- [Product Roadmap](docs/ROADMAP.md)

---

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

**Author:** Boateng Prince Agyenim  
*C# Semester Project - 2026*
