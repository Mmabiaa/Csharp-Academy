# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- JWT authentication (register/login) with ASP.NET Core Identity
- Auth API endpoints (`/api/auth/register`, `/api/auth/login`)
- Course detail endpoint with modules and lessons (`GET /api/courses/{id}`)
- Course enrollment endpoint (`POST /api/courses/{id}/enroll`)
- Enrollment repository and command handler
- JwtTokenService for token generation
- DatabaseInitializer for auto-migration and role seeding on startup
- EF Core `InitialCreate` migration
- Seed data for modules and lessons
- Frontend auth context, Login and Register pages
- Frontend Course Detail page with enrollment
- Frontend `.env` with API URL configuration
- ApplicationDbContextFactory for design-time migrations

### Changed
- Downgraded EF Core packages to 9.0.0 for Pomelo MySQL provider compatibility on .NET 10
- Enhanced ApplicationDbContext with entity relationships and indexes
- Updated Courses page with links to course detail
- Fixed frontend API base URL default to `http://localhost:5000/api`

### Added (previous)
- Project initialization with Clean Architecture structure
- Documentation files (README.md, ARCHITECTURE.md, CHANGELOG.md, ROADMAP.md)
- Domain entities (Course, CourseModule, Lesson, Quiz, Question, QuestionOption, User, Enrollment, Progress, Badge, UserBadge)
- Application layer with MediatR and CQRS pattern (GetCoursesQuery)
- Infrastructure layer with EF Core and Identity
- Web API with controllers for Courses and WeatherForecast
- Seed data for initial courses
- React frontend with Vite, TypeScript, Tailwind CSS, React Router
- Home and Courses pages with responsive design
- .gitignore file
- Environment variable support using DotNetEnv
- EF Core tools manifest
- CORS configuration
- global.json for SDK version pinning
- MySQL support with Pomelo.EntityFrameworkCore.MySql
- Microsoft.EntityFrameworkCore.Design to API project for EF Core tools
- .env.example template for environment variables

### Changed
- Switched target framework to net10.0 (matches installed SDK)
- Updated all NuGet packages to .NET 10 compatible versions
- Moved Identity dependencies out of Domain layer to maintain Clean Architecture

### Fixed
- MediatR NuGet package errors (removed obsolete MediatR.Extensions.Microsoft.DependencyInjection)
- Fixed IdentityUser dependency issues by using Microsoft.AspNetCore.Identity.EntityFrameworkCore in Infrastructure layer
- Added Microsoft.EntityFrameworkCore.Design to API project for EF migrations
- Package version mismatches (Microsoft.Extensions.DependencyInjection)
