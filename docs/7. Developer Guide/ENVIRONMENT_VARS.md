# Environment Variables Guide

C# Academy uses context-specific configuration files to keep secrets and environment-dependent settings separate from the code.

---

## 🏗️ Backend Variables (`src/backend/.env`)

These are loaded at startup via the `DotNetEnv` library.

| Variable | Description | Requirement |
|---|---|---|
| `CONNECTION_STRING` | MySQL connection string. | **Required** |
| `GEMINI_API_KEY` | Google AI Studio key for AI Tutor & Quizzes. | *Recommended* |
| `GEMINI_MODEL` | The specific model ID (e.g., `gemini-1.5-flash`). | Optional |
| `ASPNETCORE_ENVIRONMENT` | `Development` or `Production`. | Optional |

---

## 🎨 Frontend Variables (`src/frontend/.env`)

These are embedded into the build via Vite's `import.meta.env`.

| Variable | Description | Note |
|---|---|---|
| `VITE_API_URL` | The URL of your running backend. | **Required** |
| `VITE_GOOGLE_CLIENT_ID` | OAuth Client ID for Google Login. | *Optional* |

---

## 🛡️ Security Best Practices

1. **Never Commit `.env`**: Both folders contain `.gitignore` rules to exclude `.env` files. 
2. **Use `.env.example`**: We provide example files with dummy values to show new contributors exactly what variables they need to configure manually.
3. **JWT Keys**: Note that the JWT signing key is currently stored in `appsettings.json` for development simplicity, but should be moved to an environment variable in a production deployment.
