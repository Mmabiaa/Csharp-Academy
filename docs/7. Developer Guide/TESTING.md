# Testing Strategy

C# Academy adopts a pragmatic testing philosophy, focusing on the most critical business and security paths.

---

## 🧪 Testing Layers

### 1. Backend: MediatR Request Testing (Unit/Integration)
Since our logic resides in **Handlers**, we test them in isolation. We mock the `DbContext` or use the **EF Core In-Memory Provider** to verify that:
- `RegisterCommandHandler` correctly hashes passwords.
- `AwardXpHandler` handles streak math correctly.
- `RoslynExecutionService` successfully blocks forbidden namespaces.

### 2. Frontend: Component Validation
Key UI components like the **Code Editor** and **Quiz Runner** are manually verified for:
- Correct state handling of user inputs.
- Accurate calculation of total XP gain on completion.
- Responsive behavior across mobile and tablet breakpoints.

---

## 🛠️ How to run tests (Planned)

In a future CI/CD pipeline, the following commands would be used:

### Backend (xUnit)
```bash
dotnet test src/backend/CsharpAcademy.Tests
```

### Frontend (Vitest)
```bash
npm test --prefix src/frontend
```

---

## 🔍 Quality Assurance Checklist

- [x] **Auth Guarding**: Verify non-logged-in users cannot access lessons.
- [x] **SQL Injection**: Validate that all queries use EF Core parameterized methods (Automatic Schutz).
- [x] **XSS Prevention**: React's default auto-escaping and the use of Markdown sanitization prevent cross-site scripting in AI chat.
- [x] **Mobile UX**: Sidebar collapses into a bottom navigation bar on screens < 768px.
