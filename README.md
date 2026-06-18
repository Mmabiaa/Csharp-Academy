# C# Academy

## Intelligent C# Learning Environment

### Final Year Project

**Author:** Boateng Prince Agyenim
**Technology Stack:** ASP.NET Core 10, MySQL, React + Vite + TypeScript + Tailwind, Google Gemini API

---

## Developer Quick Start

```powershell
# Backend (http://localhost:5000)
cd src/backend
dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
dotnet run --project CsharpAcademy.Api

# Frontend (http://localhost:5173)
cd src/frontend
npm install
npm run dev
```

Configure `src/backend/.env` with `CONNECTION_STRING` and `GEMINI_API_KEY`. See [docs/STATUS.md](docs/STATUS.md) for the full API reference.

| Role | Email | Password |
|------|-------|----------|
| Teacher | teacher@academy.com | Teacher123! |
| Admin | admin@academy.com | Admin123! |

**Platform features:** 5 courses (sections + topics), YouTube video lessons, coding challenges, assignments & grading, admin/teacher portals, progress dashboard, AI tutor (Gemini).

---

# 1. Project Overview

## Introduction

C# Academy is a web-based intelligent learning platform designed to help students learn C# programming through structured courses, quizzes, interactive coding exercises, progress tracking, and AI-powered learning assistance.

The platform provides a complete digital learning environment where students can study C# programming, teachers can manage classes and assessments, and administrators can oversee system operations.

The primary goal of the system is to improve programming education through personalization, automation, and interactive learning experiences.

---

# 2. Objectives

## General Objective

To develop an intelligent online learning platform that enhances the teaching and learning of C# programming.

## Specific Objectives

* Provide structured C# learning content.
* Enable teachers to create and manage classes.
* Allow students to take quizzes and assignments.
* Track student learning progress.
* Implement AI-assisted code explanations.
* Support gamification through streaks and rewards.
* Improve accessibility using voice-assisted learning.

---

# 3. Target Users

## Students

Students can:

* Register and log in.
* Enroll in courses.
* Study lessons.
* Watch tutorials.
* Take quizzes.
* Track progress.
* Earn badges and certificates.
* Use AI code assistance.
* Join classes.

## Teachers

Teachers can:

* Create classes.
* Upload lessons.
* Create quizzes.
* Assign coursework.
* Review student performance.
* Provide feedback.

## Administrators

Administrators can:

* Manage users.
* Manage courses.
* Monitor platform activity.
* Generate reports.
* Moderate content.

---

# 4. Functional Requirements

## Authentication Module

Features:

* User registration
* Login
* Logout
* Password reset
* Role-based authorization
* Profile management

Roles:

* Student
* Teacher
* Administrator

---

## Course Management Module

Courses include:

* Beginner C#
* Intermediate C#
* Advanced C#
* Object-Oriented Programming
* LINQ
* ASP.NET Core
* Entity Framework

Each course contains:

* Lessons
* Examples
* Videos
* Exercises
* Assessments

---

## Quiz Management Module

Supported Question Types:

### Multiple Choice

Example:

What keyword creates an object?

A. create
B. new
C. class
D. object

---

### True/False

Example:

C# is case-sensitive.

---

### Fill in Missing Code

Example:

_____ Console.WriteLine("Hello");

---

### Output Prediction

Example:

```csharp
int x = 5;
Console.WriteLine(x++);
```

Students predict the output.

---

## Classroom Management Module

Teachers can:

* Create classes
* Invite students
* Assign lessons
* Assign quizzes

Students can:

* Join classes
* Submit assignments
* View grades

---

## Progress Tracking Module

Track:

* Lessons completed
* Quiz scores
* Learning time
* XP earned
* Current course
* Overall completion rate

Dashboard statistics:

* Total courses completed
* Average quiz score
* Current streak
* Badges earned

---

## Gamification Module

Features:

### Learning Streaks

Daily learning activity increases streak count.

### XP System

| Activity        | XP  |
| --------------- | --- |
| Complete Lesson | 10  |
| Pass Quiz       | 20  |
| Finish Course   | 100 |

### Achievement Badges

Examples:

* First Lesson
* Quiz Master
* 7-Day Streak
* Course Champion

### Leaderboards

Students compete based on XP.

---

## Voice Learning Module

Text-to-Speech Features:

* Read lessons aloud
* Read explanations aloud
* Accessibility support

Voice Commands:

* Next lesson
* Repeat explanation
* Start quiz

---

## AI Code Assistant Module

Students can submit C# code and ask also with voice or interactive UI:

* Explain this code
* Find bugs
* Optimize code
* Add comments
* Explain line-by-line

Example:

```csharp
for(int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

The AI explains:

* Loop structure
* Initialization
* Condition checking
* Increment operation
* Output behavior

---

## Interactive Coding Playground

Students can:

* Write C# code
* Execute code
* View output
* Request AI explanations

Features:

* Syntax highlighting
* Error display
* Output console
* AI feedback

---

# 5. Non-Functional Requirements

## Security

* Password hashing
* Input validation
* JWT authentication
* HTTPS communication
* Role-based access control
* CSRF protection
* Secure API communication

## Performance

* Response time below 3 seconds
* Optimized database queries
* Efficient caching

## Scalability

* Modular architecture
* Service-based design
* API-ready structure

## Usability

* Responsive design
* Simple navigation
* Accessibility support

---

# 6. System Architecture

Presentation Layer

* ASP.NET MVC Views
* Bootstrap
* React + Vite with TypeScript

Business Layer

* Services
* Validation
* Business Rules

Data Access Layer

* Entity Framework Core
* SQL Server

External Services

* OpenAI API
* Speech Synthesis API

---

# 7. Database Design

## Users

* UserID
* Name
* Email
* PasswordHash
* Role
* CreatedAt

## Courses

* CourseID
* Title
* Description

## Lessons

* LessonID
* CourseID
* Title
* Content

## Quizzes

* QuizID
* LessonID

## Questions

* QuestionID
* QuizID
* QuestionText
* CorrectAnswer

## Classes

* ClassID
* TeacherID

## Enrollments

* EnrollmentID
* StudentID
* ClassID

## Progress

* ProgressID
* StudentID
* LessonID
* CompletionStatus

## Streaks

* StreakID
* StudentID
* CurrentStreak

---

# 8. Technology Stack

## Frontend

* React + Vite with TypeScript

## Backend

* C#
* ASP.NET Core MVC
* Entity Framework Core

## Database

* Microsoft SQL Server

## AI Integration

* OpenAI API

## Voice Services

* Browser Speech Synthesis API

## Authentication

* ASP.NET Identity

---

# 9. Software Engineering Principles

The project follows:

### SOLID Principles

* Single Responsibility Principle
* Open/Closed Principle
* Liskov Substitution Principle
* Interface Segregation Principle
* Dependency Inversion Principle

### Clean Architecture

Layers:

* Presentation
* Application
* Domain
* Infrastructure

### Separation of Concerns

Business logic is separated from UI and database code.

### DRY Principle

Avoid duplicate code.

### KISS Principle

Keep solutions simple and maintainable.

### Repository Pattern

Used for data access abstraction.

### Dependency Injection

Used throughout the application.

---

# 10. Development Roadmap

## Phase 1 (Minimum Viable Product: backend + frontend)

* Authentication
* Courses
* Lessons
* Quizzes
* Classroom management
* Progress tracking

## Phase 2

* Streaks
* XP system
* Badges
* Certificates
* Voice learning

## Phase 3

* AI code explanations
* AI quiz generation
* Interactive coding playground

---

# 11. Expected Outcomes

Upon completion, the system should:

* Improve programming education.
* Provide personalized learning experiences.
* Increase student engagement.
* Support teachers with classroom management.
* Demonstrate practical application of software engineering principles.

---

# 12. Future Enhancements

* Mobile Application
* AI Learning Recommendations
* Live Coding Challenges
* Discussion Forums
* Peer Code Reviews
* Video Conferencing Integration
* Multi-Language Programming Support

---

# Conclusion

C# Academy is an intelligent learning platform that combines education, artificial intelligence, gamification, and classroom management into a single system. The project demonstrates advanced software engineering concepts, database design, web development, and AI integration while providing practical value to students and educators.
