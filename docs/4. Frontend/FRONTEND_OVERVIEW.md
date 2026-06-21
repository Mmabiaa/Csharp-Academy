# Frontend Overview

The C# Academy frontend is a High-Performance Single Page Application (SPA) built with **React 18** and **Vite**.

---

## 🛠️ Technology Stack

- **Core**: React 18 (Functional Components, Hooks)
- **Language**: TypeScript (Strict typing for all API DTOs)
- **Styling**: Tailwind CSS + Custom 3D design system
- **Routing**: React Router 6.24
- **State Management**: React Context + TanStack Query
- **Markdown**: `react-markdown` with `remark-gfm`
- **Icons**: `lucide-react`

---

## 🏗️ Project Structure (`src/frontend/src`)

```
├── components/   # Reusable UI elements
├── context/      # Global state (Auth, Sound, Notifications)
├── hooks/        # Custom hooks (Voice, Auth etc.)
├── lib/          # api.ts (Centralized networking)
├── pages/        # Route-level components
│   ├── admin/    # Platform oversight
│   ├── shared/   # Landing, Auth
│   ├── student/  # Learning path, Playground, Assistant
│   └── teachers/ # Portal, Assignments
├── App.tsx       # Main router and layout wrap
├── main.tsx      # Entry point with Context Providers
└── index.css     # Design tokens and global 3D styles
```

---

## 🚦 Navigation & Guarding

Routes are wrapped in a generic `Layout` component that handles the Sidebar and Topbar. Navigation is role-aware:
- **Anonymous**: Can see Home, Login, Register, and verify certificates.
- **Student**: Can access Courses, Playground, Dashboard, and Assistant.
- **Admin/Teacher**: Can see their respective management portals.

The UI manually hides navigation links based on the `roles` array in the `AuthContext` user object.
