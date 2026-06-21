# Frontend Design System

C# Academy features a clean, high-contrast UI inspired by premium educational platforms like **Duolingo**. The design focuses on gamification, accessibility, and playful interaction.

---

## 🎨 Color Palette

We use a fixed token system defined in the core `index.css`.

| Token | Hex | Usage |
|---|---|---|
| **Duo Green** | `#58CC02` | Progress, Success, Primary Buttons |
| **Duo Blue** | `#1CB0F6` | Info, Navigation, Links |
| **Duo Yellow** | `#FFC800` | XP, Streaks, Warnings |
| **Duo Red** | `#FF4B4B` | Errors, Health (Hearts) |
| **Duo Gray** | `#E5E5E5` | Borders, Disabled states |
| **Duo Dark** | `#3C3C3C` | Primary Typography |

---

## 🔘 The "3D" Design Language

Almost all interactive elements (Cards, Buttons, Inputs) follow a flat-3D aesthetic:
- **Shadows**: We use a `box-shadow` of 2px, 4px, or 6px matching the border color to create a "tactile" feel.
- **Active State**: On click (active), elements transform by `translateY(2px)` and the shadow is reduced or removed, simulating a physical button press.

```css
/* Example 3D Button Style */
.duo-btn-primary {
  background: #1CB0F6;
  border: 2px solid #1899D6;
  box-shadow: 0 4px 0 #1899D6;
  transition: all 0.1s;
}
.duo-btn-primary:active {
  transform: translateY(2px);
  box-shadow: none;
}
```

---

## 🔡 Typography

- **Primary Font**: `DIN Round Pro` (if available) or `Nunito`.
- **Secondary Font**: `Trebuchet MS` for fallback and UI labels.
- **Code Font**: `Fira Code` or `Consolas` for the Playground and in-lesson snippets.

---

## ✨ Animations

We avoid heavy transitions in favor of "snappy" micro-interactions:
- **Pop-in**: Lessons and cards use a light scale-up animation (`popIn`) when appearing.
- **XP Shake**: XP toasts use a subtle bounce to catch the eye.
- **Bouncy Loader**: The AI typing indicator uses a 3-dot bounce animation with a `0.15s` delay between dots.

---

## 📱 Responsiveness

The system uses **Tailwind CSS** for layout:
- **Desktop**: Sidebar navigation on the left.
- **Mobile**: Bottom navigation bar for easy thumb access.
- **Layout Max Width**: Content is capped at `1200px` for course views and `780px` for the AI Assistant to ensure optimal readability.
