# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

## feat: AI Learning Companion (Lesson Page)
 
## Overview
 
Introduced an AI-powered learning companion: a persistent, animated avatar fixed at the bottom-right of the lesson page that acts as a context-aware tutor for the current lesson. The feature uses predefined, lesson-specific data (no third-party AI integration yet) and is scoped exclusively to the lesson page — no other pages, routes, or shared components were modified.
 
**Type:** Feature (frontend-only)
**Scope:** `LessonPage` only
**Backend changes:** None
**New dependencies:** None (reuses `react-markdown`, `remark-gfm`, `lucide-react`)
 
---
 
## Added
 
### `src/frontend/src/sections/lesson-companion/companionData.ts`
Predefined, lesson-specific content store:
- 12 C# concept explanations
- 8 Q&A pairs
- 3 worked examples
- 5 mini-quizzes (multiple choice, with markdown explanations)
- 6 encouragement messages, 5 celebration messages, 4 greetings
- `matchLessonData()` — keyword detection against lesson content to auto-filter relevant topics
- `getAnswerFromData()` — ranked matching pipeline: greeting → encouragement/motivation → lesson Q&A → concept explanation → generic fallback → help menu
- `getSuggestedQuestions()` — surfaces lesson-relevant suggested questions in the chat empty state
### `src/frontend/src/sections/lesson-companion/useLectureNarration.ts`
Enhanced text-to-speech hook built on top of the browser Speech Synthesis API:
- Play / pause / stop / replay / jump-to-segment controls
- Adjustable playback speed: 0.75× / 1× / 1.25× / 1.5×, with seamless mid-playback restart
- Markdown-aware cleanup (strips code blocks, backticks, links, images, headings) before segmenting narration text
- Sentence-level segmentation with per-segment status (done / active / pending) and progress tracking
### `src/frontend/src/sections/lesson-companion/LessonCompanion.tsx`
Main companion component:
- Floating avatar (owl, SVG) fixed bottom-right, with 7 mood states: `idle`, `wave`, `thinking`, `happy`, `celebrate`, `concerned`, `speaking`
- Auto-triggered, clickable speech bubbles for greetings, encouragement, struggle detection, tutorial-midpoint checkpoints, and lesson-completion celebrations
- Expandable panel with three tabs:
  - **Discussion (Chat):** predefined Q&A/explanations, markdown + code rendering, typing indicator, per-session XP ticker
  - **Lecture (Listen):** narration transcript with clickable segments, play/pause/stop/replay, speed selector, progress bar
  - **Assessment (Quiz):** 5-question mini-quiz with instant feedback, score tracking, hearts, and a retake option
- Voice mute/unmute toggle with persisted preference (`localStorage`, per-browser)
- All UI built with the app's existing 3D button system (`duo-btn3d`) and Duolingo-style color tokens (`--duo-green`, `--duo-blue`, `--duo-yellow`, `--duo-purple`, etc.) for design consistency
- Styles scoped under `.duo-companion-*` class names
---
 
## Changed
 
### `src/frontend/src/pages/student/LessonPage.tsx`
- Imported and mounted `<LessonCompanion key={lessonId} ... />` at the bottom of the page (component resets its internal state on lesson navigation)
- Added local state: `practiceFailCount`, `practiceSuccessCount`, `lessonCompletedTrigger`
- Wired practice submission results to increment fail/success counters (drives the companion's "struggling" detection and encouragement bubbles)
- Wired `completeMutation.onSuccess` to trigger the companion's celebration mood
- No other pages, layouts, or shared components were touched
---
 
## Fixed
 
- **Malformed SVG transform:** The owl avatar's right eyebrow used an invalid `scale(1 1 17.5 10)` transform (SVG's `scale()` accepts 1–2 arguments, not 4). Replaced with the correct `translate → scale → translate` three-step pattern to mirror the shape about its pivot point, eliminating a console warning.
- **White-on-white inactive tab buttons:** The companion's injected `<style>` block defined a global, unscoped base rule (`.duo-btn3d { color: white; }`). Because this stylesheet is injected into the document by `LessonCompanion` — which mounts on the same page as the lesson tab buttons (`Lesson` / `Video` / `Tutorial` / `Practice` / `Best practices`, styled with the shared `duo-btn3d-white` class from `index.css`) — it leaked outside the companion widget and overrode the intended dark text color (`--duo-eel`) on those buttons, making their labels invisible on a white background. Fix: removed the redundant `color: white;` line from the companion's base `.duo-btn3d` rule (color is already set individually on `.duo-btn3d-green` / `.duo-btn3d-blue` within the widget), restoring correct contrast on page-level white buttons without altering the companion's own styling.
---
 
## Notes / Follow-ups
 
- Content is fully predefined/static per lesson; no LLM or third-party AI call is made in this iteration. The existing `/assistant` AI chat endpoint remains a separate, untouched feature that could later serve as a fallback for unmatched questions.
- Gamification (XP for interactions, avatar cosmetic unlocks, dedicated badges) was scoped out of this pass and remains a suggested next step.
- Because the companion's `<style>` tag is unscoped, any future class names added inside `LessonCompanion.tsx` should be prefixed (e.g. `.duo-companion-*`) to avoid repeating the global-leak issue fixed above.

### Added
- **Gmail SMTP Integration**: Implemented production-ready email service using MailKit 4.17.0 for sending password reset OTPs via Gmail SMTP.
- **Professional Email Template**: Created responsive HTML email template for password reset OTPs with gradient header, styled OTP display box, expiration warnings, and branded footer.
- **Email Configuration**: Added SMTP settings to `.env` and `.env.example` with instructions for generating Google App Passwords.

### Changed
- **EmailService**: Upgraded from placeholder logging to full SMTP implementation with both HTML and plain text email support.
- **ResetPasswordCommandHandler**: Refactored to use `UserManager<User>` for secure password updates instead of direct hash manipulation.
- **MailKit Package**: Updated to version 4.17.0 to address security vulnerabilities (previously 4.9.0 had moderate severity issues).

### Security
- **Password Reset Security**: Password changes now use ASP.NET Identity's `UserManager` for proper validation and secure hashing.
- **SMTP Credentials**: Email credentials properly isolated in environment variables with fallback to development logging when unconfigured.

---

## [1.1.1] - 2026-06-21

### Added
- **Dark Mode**: Added dark mode support for the frontend. 
- **Full API Documentation**: Completed comprehensive Swagger/OpenAPI documentation for 100% of the API. Every endpoint now includes `<summary>`, `<param>`, `<returns>`, and `<response code>` tags.
- **Dockerization**: Full multi-container support with `docker-compose.yml`. Includes persistent MySQL volumes, automated backend builds, and a "Zero Install" frontend build that reuses existing `node_modules` for speed.
- **Visual Overview**: Expanded the `README.md` with a high-fidelity 8-screenshot grid showcasing the Home Dashboard, Course Catalog, AI Assistant, and Code Playground.
- **Secure Configuration Management**: Added a global `docker.env` management system (git-ignored) to centralize secrets.
- **.dockerignore**: Implemented strict exclusion rules to keep images thin and secrets safe.

### Changed
- **Security Hardening**: Replaced hardcoded JWT and Database secrets in `appsettings.json` with secure environment variable overrides (`JWT_KEY`, `JWT_ISSUER`, etc.).
- **Code Refactoring**: Updated `Program.cs` and `JwtTokenService` to prioritize environment-based configuration for enhanced security.
- **Swagger Optimization**: Integrated explicit `[ProducesResponseType]` attributes across all controllers to generate accurate DTO schemas in the Swagger UI.

## Fixed
- **Challenges List Card Height Consistency**: Fixed an issue where the challenges list card would grow indefinitely, pushing the code editor and console down the page. The list now maintains a consistent height, ensuring the code editor and console remain visible without excessive scrolling.

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
