# Project Status

## Current State
- Backend: ASP.NET Core API on http://localhost:5000
- Frontend: React + Vite on http://localhost:5173
- Database: MySQL (`csharpacademy`) with EF Core 9 + Pomelo 9 on .NET 10
- Architecture: Clean Architecture (Domain → Application → Infrastructure → Api)
- Authentication: JWT + ASP.NET Core Identity (Student / Teacher / Admin roles)
- API docs: http://localhost:5000/swagger

## Completed Features

### Foundation
- [x] Clean Architecture project structure
- [x] Domain entities and EF Core migrations
- [x] JWT authentication (register/login) with role selection
- [x] MySQL connection via `.env` with `Env.TraversePath().Load()`
- [x] Auto-migration and role seeding on startup

### Learning Core
- [x] Courses, modules, and lessons (CRUD read APIs + seed data)
- [x] Course enrollment
- [x] Lesson viewer with markdown rendering
- [x] Progress tracking (mark lessons complete, course %)

### Assessment
- [x] Quizzes with multiple choice, true/false, fill-in-the-blank, and output prediction
- [x] Quiz submission, scoring, and attempt persistence
- [x] Pass threshold (70%) with XP rewards
- [x] AI quiz generation from lesson content (OpenAI + fallback)

### Gamification
- [x] XP system (lessons +10, quizzes +25)
- [x] Daily streak tracking
- [x] Badges (First Steps, On Fire, Quick Learner, Quiz Whiz, Graduate)
- [x] Leaderboard (top learners by XP)

### Teacher & Classroom
- [x] Teacher registration role
- [x] Classroom creation with join codes
- [x] Student join-by-code enrollment
- [x] Optional course linking per classroom
- [x] Analytics dashboard (users, enrollments, quiz stats, course performance)

### Advanced Features
- [x] AI Learning Assistant (OpenAI when configured, offline tutor fallback)
- [x] Interactive C# Playground (Roslyn script execution)
- [x] Certificates (auto-issued at 100% course completion, public verification)
- [x] PDF certificate export (QuestPDF)

### Learning Experience
- [x] Step-by-step guided tutorials per lesson
- [x] Hands-on coding practices with validation and XP
- [x] Best practices tips per lesson
- [x] Voice narration (browser text-to-speech) on lessons
- [x] Practices hub page listing all exercises

### Frontend
- [x] Home, Courses, Course Detail, Lesson, Quiz pages
- [x] Login, Register (Student/Teacher), Profile with XP/badges/enrollments
- [x] Playground, AI Tutor, Leaderboard, Certificate verification
- [x] Classrooms page, Analytics dashboard (teachers), PDF download links

## Setup Instructions

### 1. Configure Environment
Copy `src/backend/.env.example` to `src/backend/.env`:
```
CONNECTION_STRING=Server=localhost;Port=3306;Database=csharpacademy;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;

# Optional: enable AI assistant and quiz generation (Google Gemini)
GEMINI_API_KEY=your-key-here
GEMINI_MODEL=gemini-2.0-flash
```

### 2. Apply Migrations
```powershell
cd src/backend
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
| GET | `/api/courses` | No | List courses |
| GET | `/api/courses/{id}` | No | Course detail |
| POST | `/api/courses/{id}/enroll` | Yes | Enroll in course |
| GET | `/api/courses/{id}/progress` | Yes | User progress |
| GET | `/api/lessons/{id}` | Optional | Lesson detail |
| POST | `/api/lessons/{id}/complete` | Yes | Mark lesson complete |
| GET | `/api/lessons/{id}/quiz` | No | Get quiz |
| POST | `/api/lessons/{id}/quiz/submit` | Yes | Submit quiz |
| POST | `/api/lessons/{id}/quiz/generate` | Teacher | AI-generate quiz |
| POST | `/api/classrooms` | Teacher | Create classroom |
| GET | `/api/classrooms` | Yes | List my classrooms |
| POST | `/api/classrooms/join` | Yes | Join by code |
| GET | `/api/analytics/dashboard` | Teacher | Analytics snapshot |
| POST | `/api/playground/run` | No | Run C# code |
| POST | `/api/assistant/chat` | No | AI tutor chat |
| GET | `/api/certificates` | Yes | User certificates |
| GET | `/api/certificates/verify/{code}` | No | Verify certificate |
| GET | `/api/certificates/{code}/pdf` | No | Download certificate PDF |
| GET | `/api/practices` | No | List all coding exercises |
| GET | `/api/practices/lessons/{id}` | No | Exercises for a lesson |
| POST | `/api/practices/{id}/submit` | Yes | Submit practice solution |
| GET | `/api/lessons/{id}/tutorial` | No | Guided tutorial steps |
| GET | `/api/leaderboard` | No | XP leaderboard |
| GET | `/api/users/me` | Yes | User profile |
| POST | `/api/auth/register` | No | Register (optional role) |
| POST | `/api/auth/login` | No | Login |

## Known Issues
- Pomelo EF Core 10 not yet released; project uses EF Core 9 (compatible with .NET 10 runtime)
- Playground blocks file/network access; 5-second execution timeout
- AI features use offline fallback unless `GEMINI_API_KEY` is set

## Next Features to Implement
1. Admin panel for course/content management
2. Real-time notifications
3. Assignment submissions and grading
4. OAuth social login
5. Mobile-responsive PWA
