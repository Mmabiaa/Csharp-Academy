# Contributing to C# Academy

We welcome contributions! Whether you're fixing a bug, adding a new course, or improving the UI, here is the protocol.

---

## 🛠️ Code Style Guide

### Backend (C#)
- Follow **standard C# naming conventions** (PascalCase for classes/methods, camelCase for local variables).
- Use **File-scoped namespaces** to reduce indentation.
- Handlers should be **stateless** and focus on one specific use case.
- Every new feature should include a **FluentValidation** validator.

### Frontend (React/TS)
- Components should be **functional** using Hooks.
- Use **TypeScript** for all new files; avoid the `any` type.
- Styles should use **Tailwind utility classes**. Avoid custom CSS unless adding to the core `index.css` design tokens.
- Component names must be **PascalCase** (e.g., `CourseCard.tsx`).

---

## 🌿 Branching Strategy

- **main**: Stable production code. No direct commits allowed.
- **develop**: Integration branch for upcoming releases.
- **feature/XXX**: For new features.
- **fix/XXX**: For bug fixes.

---

## 📝 Pull Request Process

1. Fork the repo and create your branch from `develop`.
2. Ensure your code passes all linting and build checks.
3. Submit a PR with a clear description of:
    - What problem does this solve?
    - How was it tested?
    - Any new environment variables needed?
4. Wait for a maintainer to review and merge.

---

## 🎓 Adding New Courses

Content is currently managed via the `PlatformSeedData.cs` and the Admin Panel. To add a new official course:
1. Define the Course, Modules, and Lessons in the `SeedData` file.
2. Include at least one **Quiz** or **Coding Exercise** per module.
3. Ensure the `Order` property is set correctly for sequential learning.
