# C# Academy

[![Status](https://img.shields.io/badge/status-v1.0.0--stable-brightgreen)](docs/1.%20Project%20Overview/CHANGELOG.md)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](build_output.txt)
[![Architecture](https://img.shields.io/badge/architecture-clean-blue)](docs/2.%20Architecture/ARCHITECTURE.md)

**C# Academy** is an intelligent, gamified learning platform designed to bridge the gap between theory and practice for aspiring C# developers. Featuring a deep integration with Google Gemini for AI tutoring and a sandboxed Roslyn execution environment, it provides a complete ecosystem for students, teachers, and administrators.

---

## 🚀 Key Features

### 🎓 For Students
- **Structured Curriculum**: 5 foundational courses covering everything from Basics to ASP.NET Core.
- **Interactive Playground**: Write and execute C# code directly in the browser with real-time feedback.
- **AI Learning Assistant**: Integrated "Gemini-1.5-Flash" for line-by-line code explanations and debugging.
- **Gamified Experience**: Earn XP, maintain daily streaks, and unlock achievement badges.
- **Certification**: Automated PDF certificates of completion for verified skills.
- **File Attachments**: Allow students to submit files as assignments.


### 👩‍🏫 For Teachers
- **Classroom Management**: Create virtual classrooms and manage student enrollments.
- **Assignment System**: Deploy coding or essay-based assignments with automated delivery.
- **Grading & Feedback**: Review student code submissions and provide detailed performance reviews.
- **Analytics Dashboard**: Monitor class-wide progress and identify students needing assistance.
- **File Attachments**: Allow teachers to upload files to assignments.

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

For more details, see the [Architecture Documentation](docs/2.%20Architecture/ARCHITECTURE.md).

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

Detailed setup instructions can be found in [SETUP.md](docs/7.%20Developer%20Guide/SETUP.md).

### Quick Start
1. **Backend**: Navigate to `src/backend`, install dependencies, and run `dotnet run`.
2. **Frontend**: Navigate to `src/frontend`, run `npm install` and `npm run dev`.

---

## 🔑 Access Credentials

| User Role | Email | Password |
|---|---|---|
| **Administrator** | `admin@academy.com` | `Admin123!` |
| **Teacher** | `teacher@academy.com` | `Teacher123!` |

---

## 🗺 Documentation Structure
Detailed documentation is organized as follows:
- 📁 **[1. Project Overview](docs/1.%20Project%20Overview)**: Brief, Changelog, Roadmap
- 📁 **[2. Architecture](docs/2.%20Architecture)**: Patterns, Schema, API Design
- 📁 **[3. Backend](docs/3.%20Backend)**: MediatR, Domain, Auth, AI, Roslyn
- 📁 **[4. Frontend](docs/4.%20Frontend)**: React, Design System, State, Components
- 📁 **[5. Features](docs/5.%20Features)**: Courses, Quizzes, Certification
- 📁 **[6. Principles](docs/6.%20Software%20Engineering%20Principles)**: SOLID, Clean Code
- 📁 **[7. Dev Guide](docs/7.%20Developer%20Guide)**: Setup, Contributing, Testing
- 📁 **[8. Deployment](docs/8.%20Deployment)**: Docker, CI/CD

---

## 📄 License
This project is licensed under the MIT License.

**Author:** Boateng Prince Agyenim  
*C# Semester Project - 2026*
