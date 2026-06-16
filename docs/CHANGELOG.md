# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Teacher/classroom management: create classrooms, join codes, member lists, optional course linking
- AI quiz generation from lesson content (`POST /api/lessons/{id}/quiz/generate`)
- Analytics dashboard for teachers (users, enrollments, quiz pass rate, course stats)
- PDF certificate export via QuestPDF (`GET /api/certificates/{code}/pdf`)
- New question types: fill-in-the-blank and output prediction
- Teacher registration role with JWT role claims
- Frontend: Classrooms page, Analytics dashboard, quiz type UI, PDF download links
- EF migration: `AddClassroomsAndQuestionTypes`
- AI Learning Assistant with OpenAI integration and offline C# tutor fallback
- Interactive C# Playground using Roslyn script execution (`POST /api/playground/run`)
- Certificate system: auto-issue on course completion, verification endpoint, Graduate badge
- Leaderboard API and page (top learners by XP)
- Lesson viewer with markdown rendering (`react-markdown`)
- Progress tracking with course completion percentage
- Quiz taking, scoring, and `QuizAttempt` persistence
- Gamification: XP rewards, daily streaks, 5 badges, badge checks on milestones
- APIs: lessons, progress, quizzes, users/profile, certificates, leaderboard, assistant, playground
- Frontend pages: Lesson, Quiz, Profile, Playground, AI Tutor, Leaderboard, Certificate Verify
- EF migrations: `InitialCreate`, `AddQuizzesAndGamification`, `AddCertificatesAndFeatures`
- Fixed `.env` loading with `Env.TraversePath().Load()` for reliable MySQL connection

### Changed
- Course detail shows progress bar and completed lesson indicators
- Lesson page links to AI Tutor with lesson context
- Profile page shows certificates, badges, XP, and enrollments
- Navigation expanded with Playground, AI Tutor, Leaderboard links

### Fixed
- MySQL connection failures caused by `.env` not being found at runtime
- Disabled HTTPS redirect in Development to avoid port warnings

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
