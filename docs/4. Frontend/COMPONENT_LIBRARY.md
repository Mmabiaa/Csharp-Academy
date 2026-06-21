# Component Library

The platform uses a modular component architecture, ensuring consistency and reconfigurability across different portals.

---

## 🛠️ Specialized Components

### `Assistant`
- **Context**: Used in `Assistant.tsx`.
- **Primary Logic**: Manages message sequences, auto-scrolling, and markdown rendering. Uses the `playSound` context for feedback.

### `CodeEditor` & `ConsolePanel`
- **Context**: Used in **Playground** and **Lesson Tasks**.
- **Role**: Provides a syntax-highlighted (or monochromatic monospace) area for code input and a real-time output viewer for backend execution results.

### `NotificationOverlay`
- **Context**: Global (located in `Layout.tsx`).
- **Role**: Renders the queue of notifications from the `NotificationContext`. Includes success animations and XP gain indicators.

### `VideoPlayer`
- **Context**: Lesson pages.
- **Role**: Intelligently handles YouTube embeds with provider-specific parameters (e.g., `autoplay`, `rel=0`).

---

## 🎨 Global UI Tokens (CSS Classes)

Instead of a React component library (like MUI), we use "UI Utility Classes" in Tailwind for maximum performance and design flexibility:

- `.duo-btn3d`: Default 3D button effect.
- `.duo-card-hover`: Hover scaling and shadow-darkening for course cards.
- `.duo-input`: Standardized 3D input field style.
- `.duo-badge`: Small pill-style labels for difficulty levels.

---

## 🏗️ Hierarchy Strategy

We divide components into two categories:
1. **Atoms/Molecules**: Small, reusable items like buttons, labels, and icons.
2. **Organisms**: Complex sections like the **Navigation Sidebar** or the **Lesson Task Runner** which orchestrates multiple child components and contexts.
