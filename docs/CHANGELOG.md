# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
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
