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
- .NET 9.0 (LTS) support
- Environment variable support using DotNetEnv
- EF Core tools manifest
- CORS configuration
- global.json for SDK version pinning
- MySQL support with Pomelo.EntityFrameworkCore.MySql

### Fixed
- MediatR NuGet package errors (removed obsolete MediatR.Extensions.Microsoft.DependencyInjection)
- Pomelo.EntityFrameworkCore.MySql version compatibility (switched to net9.0)
