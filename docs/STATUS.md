# Project Status

## Current State
- Backend: ASP.NET Core API on http://localhost:5000
- Frontend: React + Vite on http://localhost:5173
- Database: MySQL with EF Core 9 (compatible with Pomelo 9 on .NET 10)
- Architecture: Clean Architecture (Domain → Application → Infrastructure → Api)
- Authentication: JWT-based auth with ASP.NET Core Identity
- API: Swagger docs at http://localhost:5000/swagger

## Completed Features
- [x] Clean Architecture project structure
- [x] Domain entities (Course, Module, Lesson, Quiz, User, Enrollment, Progress, Badge)
- [x] EF Core migrations (`InitialCreate`)
- [x] Seed data (2 courses, 3 modules, 4 lessons)
- [x] JWT authentication (register/login)
- [x] Courses API (list, detail with modules/lessons)
- [x] Course enrollment API
- [x] Frontend: Home, Courses, Course Detail, Login, Register pages
- [x] Frontend auth context with token persistence

## Setup Instructions

### 1. Configure Database
Copy `src/backend/.env.example` to `src/backend/.env` and set your MySQL password:
```
CONNECTION_STRING=Server=localhost;Port=3306;Database=csharpacademy;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;
```

### 2. Apply Migrations
```powershell
cd src/backend
dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
```
Migrations also run automatically on API startup via `DatabaseInitializer`.

### 3. Run Backend
```powershell
cd src/backend
dotnet run --project CsharpAcademy.Api
```

### 4. Run Frontend
```powershell
cd src/frontend
npm install
npm run dev
```

## API Endpoints
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/courses` | No | List all courses |
| GET | `/api/courses/{id}` | No | Course detail with modules/lessons |
| POST | `/api/courses/{id}/enroll` | Yes | Enroll in a course |
| POST | `/api/auth/register` | No | Create account |
| POST | `/api/auth/login` | No | Sign in |

## Known Issues
- Pomelo.EntityFrameworkCore.MySql 10 is not yet released; project uses EF Core 9 with Pomelo 9 (compatible with .NET 10 runtime)
- MySQL must be running with valid credentials in `.env` for the API to seed/migrate on startup

## Next Features to Implement
1. Lesson viewer with content rendering
2. Progress tracking (mark lessons complete)
3. Quiz taking and scoring
4. Gamification (XP, Badges, Streaks)
5. AI Assistant Integration
6. Interactive Coding Playground
7. Certificates
