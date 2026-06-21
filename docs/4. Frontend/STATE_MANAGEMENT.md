# Frontend State Management

C# Academy manages state using a tiered approach, choosing the simplest tool for the specific requirement. This ensures performance and a clear data flow.

---

## 1. Global State (React Context)

For cross-cutting data that needs to be accessed by many distant components, we use the native **React Context API**.

### `AuthContext`
- **Data**: Current user (User object), JWT Token, Roles.
- **Persistence**: Token and User partial are stored in `localStorage`.
- **Key Methods**: `login`, `logout`, `refreshUser`, `updateUserSettings`.
- **Computed**: `isAuthenticated`, `isAdmin`, `isTeacher`.

### `SoundContext`
- **Data**: `isSoundEnabled` (boolean).
- **Methods**: `playSound(type)`.
- **Assets**: Manages 7 core MP3 files (click, success, error, notification, complete, enrolled, quiz).
- **Behavior**: Provides a hook `useSound()` used by buttons and achievement watchers.

### `NotificationContext`
- **Data**: A queue of active `GamificationNotification` objects.
- **Methods**: `showNotification()`, `hideNotification()`.
- **Behavior**: Auto-triggers the `notification` sound and manages the visual lifecycle of gamification toasts.

---

## 2. Server State (TanStack Query)

For all data fetched from the API, we use **TanStack React Query v5**. It handles the "heavy lifting" of the networking layer:
- **Caching**: Data stays in memory unless invalidated.
- **Revalidation**: Automatically refetches data when the window is refocused.
- **Loading/Error States**: Provides clean `isLoading` and `error` flags, reducing boilerplate in UI components.
- **Optimistic Updates**: (Used in v1.1.0) XP updates reflected immediately while the API handles the request in the background.

---

## 3. Local State (React Hooks)

For UI-only states that don't need to persist outside a single page:
- **`useState`**: Used for form inputs (Login, Register), sidebar toggle, and active tab selections.
- **`useRef`**: Used for scroll-to-bottom logic in the AI Assistant chat and managing audio references.
- **`useSearchParams`**: Used specifically in the AI Assistant to pull the current `lesson` ID from the URL for contextual help.

---

## 4. URL State (React Router)

The URL is the "Source of Truth" for content navigation:
- `/courses/:id`
- `/lessons/:id`
- `/challenges`
- `/admin`

This allows for easy deep-linking and a predictable browser history (Back button behavior).
