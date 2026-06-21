# EF Core and Migrations

The platform employs **Entity Framework Core (EF Core)** as the Object-Relational Mapper (ORM), using the Code-First approach.

---

## 🛠️ Configuration

- **Provider**: `Pomelo.EntityFrameworkCore.MySql`
- **DbContext**: `ApplicationDbContext` located in `CsharpAcademy.Infrastructure/Data`.
- **Naming**: Automatic mapping of PascalCase entities to standard database tables.

---

## 🚀 Migrations Strategy

We use migrations to evolve the database schema without losing data.
- **Current Snapshot**: v1.1.0 covers all gamification and classroom tables.
- **Applying Migrations**:
  ```bash
  dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
  ```

---

## 🧬 Seeding Data (`PlatformSeedData.cs`)

To ensure a seamless start for new developers or examiners, the platform includes a robust seed engine:
- **Default Accounts**: Admin, Teacher, and Student demo accounts are created on first run.
- **Initial Curriculum**: 5+ courses are pre-loaded with modules, lessons, and quizzes.
- **Sandbox Challenges**: 8 coding challenges are seeded.

---

# PDF Generation (QuestPDF)

When a student completes a course, the system generates an official A4 Certificate.

- **Library**: `QuestPDF` (Fluent, layout-based engine).
- **Service**: `CertificatePdfService.cs`.
- **Trigger**: Sparked when `LessonCompleteHandler` detects 100% course completion.
- **Security**: Each certificate contains a unique **Verification Code** (`CertificateCode`) that can be validated at `/certificates/verify/{code}` in the public frontend without logging in.

---

# Environment Variables

The platform uses separate `.env` files for backend and frontend.

### Backend `.env`
| Key | Example | Purpose |
|---|---|---|
| `CONNECTION_STRING` | `Server=localhost;...` | MySQL location |
| `GEMINI_API_KEY` | `AQ.Ab8RN...` | AI Engine key |
| `ASPNETCORE_ENVIRONMENT` | `Development` | App behavior toggle |

### Frontend `.env`
| Key | Example | Purpose |
|---|---|---|
| `VITE_API_URL` | `http://localhost:5000/api` | API Location |
| `VITE_GOOGLE_CLIENT_ID` | `...apps.googleusercontent.com` | Google OAuth |
