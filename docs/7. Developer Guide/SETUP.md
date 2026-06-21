# Developer Setup Guide

Follow this guide to get **C# Academy** running on your local machine for development.

---

## 📋 Prerequisites

Before starting, ensure you have the following installed:
- **.NET 9 or 10 SDK** (Core framework)
- **Node.js (v18+)** and **npm** (Frontend engine)
- **MySQL Server (v8.0+)** (Database)
- **Git** (Source control)
- **IDE**: Visual Studio 2022, JetBrains Rider, or VS Code.

---

## 🏗️ Step 1: Clone the Repository

```bash
git clone https://github.com/Mmabiaa/Csharp-Academy.git
cd Csharp-Academy
```

---

## ⚙️ Step 2: Backend Configuration

1. Navigate to the backend directory:
   ```bash
   cd src/backend
   ```
2. Create your environment file:
   - Copy `.env.example` to `.env`.
   - Update **`CONNECTION_STRING`** with your MySQL credentials.
   - Update **`GEMINI_API_KEY`** with your key from [Google AI Studio](https://aistudio.google.com/).
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Apply Database Migrations:
   ```bash
   dotnet ef database update --project CsharpAcademy.Infrastructure --startup-project CsharpAcademy.Api
   ```

---

## 🎨 Step 3: Frontend Configuration

1. Navigate to the frontend directory:
   ```bash
   cd ../frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Create your environment file:
   - Create `.env` (refer to `.env.example`).
   - Ensure `VITE_API_BASE_URL` points to your running backend (usually `http://localhost:5000/api/v1`).

---

## 🚀 Step 4: Running the Platform

For the best development experience, run both projects simultaneously:

### Launch Backend
```bash
# In src/backend
dotnet run --project CsharpAcademy.Api
```

### Launch Frontend
```bash
# In src/frontend
npm run dev
```

---

## 🔑 Access Credentials

The platform is pre-seeded with these demo accounts:

| Role | Email | Password |
|---|---|---|
| **Admin** | `admin@academy.com` | `Admin123!` |
| **Teacher** | `teacher@academy.com` | `Teacher123!` |
| **Student** | `student@academy.com` | `Student123!` |

---

## ❌ Common Issues & Fixes

### 1. MySQL Connection Refused
- **Cause**: MySQL service is not running or credentials in `.env` are wrong.
- **Fix**: Check `services.msc` and verify your connection string.

### 2. Gemini AI Returns 403/401
- **Cause**: Invalid API Key or Quota exceeded.
- **Fix**: The app will fallback to the "Offline Tutor" automatically, but check your API Key in `.env`.

### 3. Roslyn Code Execution Fails
- **Cause**: Missing assembly references or security block.
- **Fix**: Check `RoslynCodeExecutionService.cs` to ensure the required namespaces aren't blacklisted for your test case.
