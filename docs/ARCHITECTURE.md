# Architecture

This document describes the Clean Architecture principles and structure of the C# Academy platform.

## Clean Architecture Layers

### Domain Layer
- Contains core entities and value objects
- Defines repository interfaces
- Contains domain services and business rules
- No dependencies on other layers

### Application Layer
- Contains use cases (commands and queries via MediatR)
- Defines DTOs and validators (FluentValidation)
- Depends on Domain layer only

### Infrastructure Layer
- Implements repository interfaces using EF Core
- Handles external services (OpenAI API, email, etc.)
- Depends on Domain and Application layers

### Presentation Layer
- ASP.NET Core Web API
- React + TypeScript frontend
- Depends on Application layer only

## Dependency Flow

```
Presentation → Application → Domain
Infrastructure → Domain
Infrastructure → Application
```

## Technology Stack

### Backend
- ASP.NET Core
- C# 12
- Entity Framework Core
- MediatR
- FluentValidation
- ASP.NET Identity
- SQL Server

### Frontend
- React 18
- TypeScript
- Vite
- React Router
- TanStack Query
- Tailwind CSS
- shadcn/ui
