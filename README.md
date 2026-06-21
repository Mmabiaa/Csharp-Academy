# C# Academy

[![Status](https://img.shields.io/badge/status-v1.1.0--latest-brightgreen)](docs/1.%20Project%20Overview/CHANGELOG.md)
[![Architecture](https://img.shields.io/badge/architecture-clean-blue)](docs/2.%20Architecture/ARCHITECTURE.md)
[![License](https://img.shields.io/badge/license-MIT-lightgrey)](LICENSE)

**C# Academy** is an intelligent, gamified learning platform designed to bridge the gap between theory and practice for aspiring C# developers. Featuring a deep integration with **Google Gemini** for AI tutoring and a sandboxed **Roslyn execution environment**, it provides a complete ecosystem for students, teachers, and administrators.

---

## 🚀 Key Features

*   **Interactive C# Playground**: Run real C#/.NET code in the browser with Roslyn-powered sandboxing and security.
*   **AI Learning Assistant**: Context-aware C# tutor powered by **Google Gemini**, featuring adaptive explanations and a rule-based offline fallback.
*   **Structured Curriculum**: Comprehensive courses, from absolute beginner to advanced design patterns.
*   **3D High-Gloss UI**: A premium, bouncy Duolingo-inspired interface built for accessibility and high engagement.
*   **Gamification Engine**: Earn XP, maintain daily streaks (with Max Streak tracking), and unlock achievement badges.
*   **Teacher & Admin Portals**: Manage classrooms, assign tasks, grade submissions, and view deep learning analytics.
*   **Sound & Notifications**: Immersive auditory feedback system for successes, failures, and gamification events.
*   **Voice Narration**: Narrative audio summaries for lessons using the browser's native Speech API.
*   **Certifications**: Automated PDF certificate generation via QuestPDF with digital verification codes.

---

## 📂 Documentation

A comprehensive documentation suite is available in the [`/docs`](./docs) folder, organized by domain:

1.  [**Project Overview**](./docs/1.%20Project%20Overview) - Project Brief, Roadmap, and the accurate [Changelog](./docs/1.%20Project%20Overview/CHANGELOG.md).
2.  [**Architecture**](./docs/2.%20Architecture) - Clean Architecture diagrams, Database Schema, and documented Design Patterns.
3.  [**Backend**](./docs/3.%20Backend) - Technical dives into Roslyn Sandboxing, Gemini AI Engine, and Identity/Auth.
4.  [**Frontend**](./docs/4.%20Frontend) - Design System tokens (3D styles), State Management, and Component library.
5.  [**Features**](./docs/5.%20Features) - Detailed guides on the Course hierarchy, Quiz engine, and Classroom workflows.
6.  [**SE Principles**](./docs/6.%20Software%20Engineering%20Principles/SE_PRINCIPLES.md) - **The definitive report on SOLID, DRY, and Security for semester submission.**
7.  [**Developer Guide**](./docs/7.%20Developer%20Guide) - Step-by-step Setup, Testing strategy, and Environment variables.
8.  [**Deployment**](./docs/8.%20Deployment) - Guide on building for production and Docker containerization.

---

## 🛠️ Technology Stack

| Feature | Technology |
|---|---|
| **Frontend** | React 18, Vite, TypeScript, Tailwind CSS, Lucide |
| **Backend** | ASP.NET Core 10 (Web API), MediatR (CQRS) |
| **Database** | MySQL 8.0, EF Core (Pomelo Provider) |
| **Sandbox** | Roslyn (Microsoft.CodeAnalysis.CSharp.Scripting) |
| **AI Engine** | Google Gemini (Models: 1.5-Flash / 2.5-Flash) |
| **Reporting** | QuestPDF |
| **State** | TanStack Query v5 + React Context API |

---

## 🛠 Developer Setup

### Prerequisites
- .NET 9 or 10 SDK
- Node.js (v18+)
- MySQL Server (v8.0+)
- Google Gemini API Key ([Get one here](https://aistudio.google.com/apikey))

### Quick Start
1. **Database**: Create a MySQL db and configure `CONNECTION_STRING` in `src/backend/.env`.
2. **Migrations**: `dotnet ef database update --project src/backend/CsharpAcademy.Infrastructure`
3. **Launch Backend**: `dotnet run --project src/backend/CsharpAcademy.Api`
4. **Launch Frontend**: `cd src/frontend && npm install && npm run dev`

---

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

**Author:** Boateng Prince Agyenim  
*C# Semester Project - 2026*
