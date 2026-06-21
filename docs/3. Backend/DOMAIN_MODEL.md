# Domain Model

The Domain Layer is the "Brain" of the platform. It contains the fundamental entities and business rules, completely isolated from infrastructure concerns.

---

## 📐 Core Entities

All entities inherit from a base `Entity` class which provides:
- `Id` (Int, PK)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime, Nullable)

### 👤 User Aggregate
- **User**: The primary actor. Contains gamification state (`Xp`, `CurrentStreak`, `MaxStreak`) and relationship to course enrollments.
- **Badge**: Value-object-like entity defining achievements.

### 📚 Curriculum Aggregate
- **Course**: The root of the learning tree.
- **CourseModule**: Logical groupings (e.g., "Basics", "Advanced").
- **Lesson**: The primary delivery unit. Logic here involves order-management and content types.

### 📝 Assessment Aggregate
- **Quiz**: Container for questions.
- **QuizQuestion**: Defines behavior for different types (Multiple Choice vs. Fill-in-the-blank).
- **CodingExercise**: Defines the "Contract" for practice—what code is provided and what output is expected.

---

## 🛡️ Business Rules

The system enforces several domain-level rules:

1. **Sequential Progress**: A student cannot start Lesson 3 if Lesson 2 is not completed (Enforced in Application Layer via Progress check).
2. **Streak Continuity**: Streaks only count if active today or yesterday. Missed days reset the `CurrentStreak` but preserve the `MaxStreak`.
3. **Badge Unlocking**: Badges are only awarded once. The logic in `GamificationService` checks for existence before adding a redundant record.
4. **Certificate Eligibility**: A certificate record is only generated when `ProgressSummary.CompletionPercentage == 100`.
5. **Classroom Privacy**: Classrooms are only joinable via a valid 6-character `JoinCode`.

---

## 🚫 Domain Exceptions

We use specialized exceptions to handle business logic violations:
- `NotFoundException`: Resource doesn't exist.
- `ValidationException`: Domain rules (not just syntax) violated.
- `UnauthorizedException`: Action attempted without sufficient role or ownership.
