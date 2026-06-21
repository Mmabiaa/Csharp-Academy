# API Design

The C# Academy backend is a **RESTful Web API** built with ASP.NET Core 10. It follows resource-based naming conventions and uses standard HTTP verbs.

---

## 🚦 General Conventions

- **Base URL**: `http://localhost:5000/api`
- **Versioning**: Not explicitly in URL segment; currently defaults to v1 behavior.
- **Format**: All requests and responses use `application/json`.
- **Naming**: `PascalCase` properties in C# are serialized to `camelCase` in JSON for frontend compatibility.

---

## 📬 Response Envelope Format

We return status codes directly for success/fail. Common success responses return the resource directly or a list. Error responses follow this shape:

```json
{
  "message": "Detailed error message describing what went wrong."
}
```

---

## 🛠️ Main Endpoint Groups

### 1. Authentication (`/api/auth`)
- `POST /register`: Create new Student/Teacher.
- `POST /login`: Standard credential auth.
- `POST /google-login`: OAuth2 flow using ID Token.

### 2. Courses & Curriculum (`/api/courses`, `/api/lessons`)
- `GET /courses`: List all published courses.
- `GET /courses/{id}`: Detailed course with modules and lessons.
- `POST /courses/{id}/enroll`: Join a course.
- `GET /lessons/{id}`: Fetch lesson content (requires auth for progress check).
- `POST /lessons/{id}/complete`: Mark lesson as done.
- `GET /lessons/{id}/quiz`: Fetch quiz for the lesson.
- `POST /lessons/{id}/quiz/submit`: Submit answers for grading.

### 3. Interactive Features (`/api/playground`, `/api/assistant`)
- `POST /playground/run`: Sandbox execution of C# code chunks.
- `POST /assistant/chat`: Contextual learning chat.

### 4. Gamification (`/api/users`, `/api/leaderboard`)
- `GET /users/me`: Current user profile (roles, XP, badges).
- `GET /users/me/progress`: Overall platform progress summary.
- `GET /leaderboard?top=10`: Global XP rankings.

### 5. Collaboration & Submissions (`/api/classrooms`, `/api/assignments`)
- `POST /classrooms`: Create a room (Teacher only).
- `POST /classrooms/join`: Join via 6-digit code.
- `GET /assignments/my`: Student view of assigned tasks.
- `POST /assignments/{id}/submit`: Student submission (Text or Code).
- `POST /assignments/submissions/{id}/grade`: Teacher grading and feedback.
- `POST /assignments/attachments`: Multi-part form upload for assignment files.

### 6. Analytics & Administrative (`/api/admin`, `/api/analytics`)
- `GET /admin/dashboard`: Global stats on user growth, course popularity, and system health.
- `GET /analytics/dashboard`: Detailed metrics snapshot for platform admins.
- `GET /analytics/teaching`: Aggregated student performance data for teachers (pass rates, average quiz scores).
- `POST /admin/courses`: CRUD operations for curriculum management.

---

## 📈 Platform Metrics (KPI Tracking)
The API tracks and serves the following key metrics through the `/analytics` endpoints:
- **Engagement**: Daily active users (DAU), average lesson completion time.
- **Academic**: Quiz failure rates (to identify difficult content), average assignment grades.
- **Growth**: Monthly enrollment trends per course.
- **Social**: Leaderboard movement and badge issuance frequency.

---

## 🔒 Security
- **JWT Auth**: Header `Authorization: Bearer <token>` required for all `/api/admin`, `/api/teacher`, and most `/api/users` routes.
- **Role Guards**: Endpoints like `POST /admin/courses` will return `403 Forbidden` if the user is not an Admin.
- **Swagger Documentation**: Full OpenAPI 3.0 specs are available at `/swagger/index.html` when running in development mode, containing all parameter descriptions and response DTO schemas.
