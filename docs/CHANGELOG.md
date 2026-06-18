# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-06-18

### Added
- **Intelligent C# Playground**: Integrated Roslyn-based script execution with standard output capture.
- **Platform upgrade**: Full suite of features including Admin panel, teacher portal, assignments & grading, and coding challenges.
- **Course structure**: 5 comprehensive courses with 8 sections and 11 lessons, featuring levels, durations, and learning objectives.
- **Video tutoring**: YouTube embed support per lesson with a dedicated media viewer.
- **Coding challenges**: 8 standalone puzzles (from FizzBuzz to Binary Search) with interactive validation and XP rewards.
- **Assignments**: End-to-end workflow for creation, submission, and grading with automated demo seeding.
- **Progress dashboard**: Real-time tracking of learner progress, completion rates, and achievement history.
- **Seed data**: Robust platform seeding via `PlatformSeedData.cs` and runtime demo accounts.
- **Frontend UI overhaul**: Premium dark theme, responsive Layout system, and role-based navigation.
- **New pages**: AdminDashboard, TeacherPortal, Assignments, Challenges, and ProgressDashboard.

### Changed
- **AI Integration**: Switched to Google Gemini API for faster and more accurate code explanations and quiz generation.
- **Course Metadata**: Enhanced DTOs to include difficulty levels, estimated hours, and detailed learning objectives.
- **Analytics**: Comprehensive dashboard for educators to monitor student performance and engagement metrics.
- **Assessment Engine**: Support for multiple question types (MC, T/F, Fill-in-the-blank, Output Prediction) and AI-powered generation.
- **Certification**: Automated PDF certificate generation via QuestPDF upon course completion.
- **Gamification**: Deeply integrated XP system, daily streaks, achievement badges, and global leaderboards.

### Fixed
- **Code Execution**: Fixed an issue in `RoslynCodeExecutionService` where `Console.WriteLine` output was not correctly captured.
- **Thread Safety**: Implemented `SemaphoreSlim` in the execution service to ensure safe concurrent code evaluation.
- **Environment Loading**: Improved `.env` discovery to ensure reliable database connections across different environments.
- **Connection Stability**: Resolved MySQL connectivity issues by normalizing environment path traversal.

## [0.1.0] - 2026-06-16

### Added
- Project initialization with Clean Architecture structure
- JWT authentication (register/login) with ASP.NET Core Identity
- Courses API with modules, lessons, and enrollment
- React frontend with Vite, TypeScript, Tailwind CSS, React Router
- MySQL support with Pomelo.EntityFrameworkCore.MySql
- Seed data: 2 courses, 3 modules, 4 lessons
- Environment variable support using DotNetEnv
- CORS configuration and Swagger docs

### Changed
- Target framework net10.0 with EF Core 9 for Pomelo compatibility
