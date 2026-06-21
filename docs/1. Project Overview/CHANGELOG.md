# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.1.0] - 2026-06-21

### Added
- **Sound Management System**: Full `SoundContext` implementation in React with 7 distinct sound effects: `click`, `success`, `error`, `notification`, `complete`, `enrolled`, and `quiz`. Persistence of user sound preferences via `localStorage`.
- **Notification System**: `NotificationContext` providing typed gamification overlays (`congrats`, `achievement`, `streak`, `info`). Sound is auto-triggered (`notification.mp3`) whenever a notification fires. Info-type notifications auto-dismiss after 5 seconds.
- **Voice Narration**: `useVoiceNarration` hook using the browser's native `Web Speech API` (`SpeechSynthesisUtterance`) with configurable rate and pitch, allowing lesson text to be read aloud.
- **Google OAuth Login**: `POST /api/auth/google-login` endpoint and `GoogleLoginCommand` handler added. Frontend uses `@react-oauth/google` and the `GoogleOAuthProvider` wrapper. Requires `VITE_GOOGLE_CLIENT_ID` in the frontend `.env`.
- **Progress Tracking – Challenge Completions**: `solvedChallengeIds` and `solvedPracticeIds` are now returned by `GET /api/users/me/progress` (`UserProgressSummaryDto`). Frontend `Challenges.tsx` and `Practices.tsx` read this data to display completion state.
- **Admin Content Management**: Full CRUD endpoints under `api/admin` for Courses, Modules, Lessons, Practices, Challenges, and Videos. Three dedicated admin pages: `AdminCourses`, `AdminChallenges`, `AdminPractices`.
- **File Attachments**: Upload/delete file attachments (`POST/DELETE /api/assignments/attachments`) for classrooms, assignments, and submissions via `FileService`.
- **Analytics Endpoints**: `GET /api/analytics/dashboard` (Admin) and `GET /api/analytics/teaching` (Teacher) providing platform-wide and class-specific metrics.
- **Leaderboard**: `GET /api/leaderboard?top=N` returns top-N users ranked by XP, including display name and current streak.
- **Profile Update**: `PUT /api/users/me` allows updating first/last name, email, profile image URL, and password.

### Changed
- **Gemini Model**: The configured active model is now `gemini-2.5-flash` (set in `src/backend/.env`). The default fallback in code is `gemini-1.5-flash`.
- **AI Token Limit**: The assistant chat endpoint uses up to **1800 tokens** per request.
- **JWT Expiry**: Tokens are configured to expire after **1440 minutes** (24 hours) via `appsettings.json`.
- **Streak Logic**: Streak now resets if `LastActiveDate` was not today or yesterday (strict calendar-day comparison, not 48-hour window).
- **User Entity**: Added `MaxStreak` property alongside `CurrentStreak` to preserve a user's best streak on the `User` entity and `UserProfile` DTO.

### Fixed
- **Role Preservation on Profile Refresh**: `AuthContext.refreshUser` now merges existing roles from state when the profile API response returns an empty roles array, preventing role-stripping on page reload.
- **Challenge Solved State**: Backend now correctly records `ChallengeCompletion` and surfaces `solvedChallengeIds` in the progress summary, fixing the UI not reflecting already-solved challenges.
- **Sound Feedback Loop**: `NotificationContext` consumes `useSound` correctly so that notification sound is played exactly once per event.

---

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
- **Frontend UI overhaul**: Premium light theme with Duolingo-inspired design tokens, responsive layout, and role-based navigation.
- **New pages**: AdminDashboard, TeacherPortal, Assignments, Challenges, and ProgressDashboard.

### Changed
- **AI Integration**: Switched to Google Gemini API for faster and more accurate code explanations and quiz generation.
- **Course Metadata**: Enhanced DTOs to include difficulty levels, estimated hours, and detailed learning objectives.
- **Analytics**: Comprehensive dashboard for educators to monitor student performance and engagement metrics.
- **Assessment Engine**: Support for multiple question types (MultipleChoice, TrueFalse, FillInTheBlank, OutputPrediction) and AI-powered generation.
- **Certification**: Automated PDF certificate generation via QuestPDF upon course completion.
- **Gamification**: Deeply integrated XP system, daily streaks, achievement badges, and global leaderboards.

### Fixed
- **Code Execution**: Fixed an issue in `RoslynCodeExecutionService` where `Console.WriteLine` output was not correctly captured.
- **Thread Safety**: Implemented `SemaphoreSlim` in the execution service to ensure safe concurrent code evaluation.
- **Environment Loading**: Improved `.env` discovery to ensure reliable database connections across different environments.
- **Connection Stability**: Resolved MySQL connectivity issues by normalizing environment path traversal.

---

## [0.1.0] - 2026-06-16

### Added
- Project initialization with Clean Architecture structure.
- JWT authentication (register/login) with ASP.NET Core Identity.
- Courses API with modules, lessons, and enrollment.
- React frontend with Vite, TypeScript, Tailwind CSS, React Router.
- MySQL support with `Pomelo.EntityFrameworkCore.MySql`.
- Seed data: 2 courses, 3 modules, 4 lessons.
- Environment variable support using `DotNetEnv`.
- CORS configuration and Swagger docs.

### Changed
- Target framework `net10.0` with EF Core 9 for Pomelo compatibility.
