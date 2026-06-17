# Project Status

## Current State
- Backend: ASP.NET Core API on http://localhost:5000
- Frontend: React + Vite on http://localhost:5173
- Database: MySQL (`csharpacademy`) with EF Core 9 + Pomelo 9 on .NET 10
- Architecture: Clean Architecture (Domain → Application → Infrastructure → Api)
- Authentication: JWT + ASP.NET Core Identity (Student / Teacher / Admin roles)
- AI: Google Gemini (`GEMINI_API_KEY`, `GEMINI_MODEL`)
- API docs: http://localhost:5000/swagger

## Completed Features

### Foundation
- [x] Clean Architecture project structure
- [x] Domain entities and EF Core migrations
- [x] JWT authentication (register/login) with role selection
- [x] MySQL connection via `.env` with `Env.TraversePath().Load()`
- [x] Auto-migration, role seeding, and demo users on startup

### Learning Core
- [x] 5 courses with sections (modules), topics (lessons), levels, and estimated hours
- [x] Lesson types: Reading, Video, Practice, Interactive
- [x] Course enrollment and progress tracking
- [x] Lesson viewer with markdown, tutorials, practices, best practices
- [x] YouTube video tutoring (`GET /api/lessons/{id}/videos`)

### Assessment & Challenges
- [x] Quizzes (MC, T/F, fill-in-the-blank, output prediction)
- [x] AI quiz generation (Gemini)
- [x] 8 standalone coding challenges with XP rewards
- [x] Hands-on lesson practices with validation

### Assignments & Grading
- [x] Teachers create assignments (code or essay)
- [x] Students submit assignments
- [x] Teachers grade submissions with feedback
- [x] Demo assignments seeded on startup

### Admin & Teacher Portals
- [x] Admin dashboard (users, courses, enrollments, assignments, challenges)
- [x] Admin course creation
- [x] Teacher portal (classrooms, assignments, pending grading)
- [x] Student progress summary dashboard

### Gamification
- [x] XP, streaks, badges, leaderboard, certificates + PDF

### Frontend UI
- [x] Dark professional theme (Microsoft Learn / Codecademy inspired)
- [x] Layout with role-based navigation
- [x] Pages: Admin, Teacher, Assignments, Challenges, Progress
- [x] Course cards with level badges, curriculum accordion
- [x] Lesson video tab with YouTube embeds

## Demo Accounts

| Role | Email | Password |
|------|-------|----------|
| Teacher | teacher@academy.com | Teacher123! |
| Admin | admin@academy.com | Admin123! |

## Setup Instructions

### 1. Configure Environment
Copy `src/backend/.env.example` to `src/backend/.env`:
```
CONNECTION_STRING=Server=localhost;Port=3306;Database=csharpacademy;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;

GEMINI_API_KEY=your-key-here
GEMINI_MODEL=gemini-1.5-flash
```

### 2. Apply Migrations
```powershell
cd src/backend
dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
```

If a migration fails mid-way, drop and recreate:
```powershell
dotnet ef database drop --force --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
```

### 3. Run
```powershell
# Backend
cd src/backend
dotnet run --project CsharpAcademy.Api

# Frontend
cd src/frontend
npm install
npm run dev
```

## API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/courses` | No | List courses (level, hours, lesson count) |
| GET | `/api/courses/{id}` | No | Course detail with sections/topics |
| POST | `/api/courses/{id}/enroll` | Yes | Enroll in course |
| GET | `/api/courses/{id}/progress` | Yes | User progress |
| GET | `/api/lessons/{id}` | Optional | Lesson detail |
| GET | `/api/lessons/{id}/videos` | No | YouTube/embed video list |
| POST | `/api/lessons/{id}/complete` | Yes | Mark lesson complete |
| GET | `/api/challenges` | No | List coding challenges |
| POST | `/api/challenges/{id}/submit` | Yes | Submit challenge solution |
| GET | `/api/assignments/my` | Yes | Student assignments |
| GET | `/api/assignments/teaching` | Teacher | Teacher assignments |
| POST | `/api/assignments` | Teacher | Create assignment |
| POST | `/api/assignments/{id}/submit` | Yes | Submit assignment |
| GET | `/api/assignments/{id}/submissions` | Teacher | View submissions |
| POST | `/api/assignments/submissions/{id}/grade` | Teacher | Grade submission |
| GET | `/api/admin/dashboard` | Admin | Platform stats |
| POST | `/api/admin/courses` | Admin | Create course |
| GET | `/api/teacher/dashboard` | Teacher | Teacher stats |
| GET | `/api/users/me/progress` | Yes | Progress summary |
| POST | `/api/assistant/chat` | No | AI tutor (Gemini) |
| POST | `/api/playground/run` | No | Run C# code |

See Swagger for the full API list.

## Migrations

| Migration | Description |
|-----------|-------------|
| `InitialCreate` | Core schema |
| `AddQuizzesAndGamification` | Quizzes, XP, badges |
| `AddCertificatesAndFeatures` | Certificates |
| `AddClassroomsAndQuestionTypes` | Classrooms, question types |
| `AddTutorialsPracticesAndVoice` | Tutorials, practices |
| `AddPlatformFeatures` | Assignments, challenges, videos, course metadata |

## Known Issues
- Pomelo EF Core 10 not yet released; project uses EF Core 9
- Playground blocks file/network access; 5-second execution timeout
- AI features require valid `GEMINI_API_KEY` (429 = quota exceeded)

## Next Features
1. Real-time notifications
2. OAuth social login
3. Mobile PWA
4. Inline code explanation (click-to-explain)
5. Discussion forums
