# Database Schema

The platform uses **MySQL 8.0** managed via **EF Core (Pomelo Provider)**. The schema is designed to handle hierarchical course content, complex gamification logic, and classroom management.

---

## 🗺️ Entity Relationship Overview

### 1. User & Gamification
- **Users**: Inherits from `IdentityUser<int>`. Stores `Xp`, `CurrentStreak`, `MaxStreak`, `LastActiveDate`, and `ProfileImageUrl`.
- **Badges**: Definitions of achievements (id, name, description).
- **UserBadges**: Many-to-many join table between Users and Badges.

### 2. Course Content Hierarchy
- **Courses**: The top-level container (`Level`, `EstimatedHours`).
- **CourseModules**: Groups of lessons within a course.
- **Lessons**: Individual learning units. Contains `Content` (Markdown), `BestPractices`, `VoiceSummary`.
- **LessonVideos**: Links to YouTube/Provider videos per lesson.
- **TutorialSteps**: Sequential instruction steps for guided learning.

### 3. Assessment & Progress
- **Quizzes**: Linked to a Lesson.
- **QuizQuestions**: MultipleChoice, TrueFalse, FillInTheBlank, OutputPrediction.
- **QuizOptions**: Answers for choice-based questions.
- **CodingExercises**: Practice tasks with `StarterCode` and `ExpectedOutput`.
- **CodingChallenges**: High-XP puzzles not tied to a specific lesson.
- **Enrollments**: Tracks which User is in which Course.
- **Progress**: Tracks completed lessons and quiz scores per user/course.
- **ChallengeCompletions**: Tracks solved standalone challenges.

### 4. Classroom & Collaborative
- **Classrooms**: Groups created by Teachers. Has a unique `JoinCode`.
- **ClassroomMembers**: Users joined to a classroom.
- **Assignments**: Tasks created by Teachers (linked to a course/lesson).
- **Submissions**: Student work for an assignment (Status: Pending, Graded).
- **Attachments**: Generic file metadata for uploads (Classroom/Assignment/Submission context).

---

## 🗄️ Table Definitions (Core)

| Table | Primary Key | Key Foreign Keys | Purpose |
|---|---|---|---|
| `AspNetUsers` | `Id` | - | User profile & gamification stats |
| `Courses` | `Id` | - | Platform curriculum |
| `Lessons` | `Id` | `ModuleId` | Learning content & metadata |
| `Enrollments` | `Id` | `UserId`, `CourseId` | Student course access |
| `Progress` | `Id` | `UserId`, `LessonId` | Completion tracking |
| `Assignments` | `Id` | `TeacherId`, `ClassroomId` | Teacher-led tasks |
| `Submissions` | `Id` | `AssignmentId`, `UserId` | Student task solutions |
| `Certificates` | `Id` | `UserId`, `CourseId` | Verified completion proof |

---

## ⚡ Performance Optimization
- **Indexes**: Clustered indexes on all `Id` columns. Non-clustered indexes on `UserId` in Progress/Enrollment tables for fast dashboard lookups.
- **Soft Deletes**: Not implemented in v1.1.0; deletions are permanent but protected by role guards.
- **Seed Data**: Automated seeding via `PlatformSeedData.cs` ensures a ready-to-use environment for examiners.
