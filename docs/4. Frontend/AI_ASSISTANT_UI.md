# AI Assistant UI

The **AI Tutor** interface (`Assistant.tsx`) is a dedicated chat space designed specifically for the unique needs of a programming student.

---

## 🛠️ Chat Architecture

- **Contextual Querying**: If accessed from a lesson page, the URL contains `?lesson=TopicName`. The UI automatically creates a context banner showing "Currently studying: TopicName".
- **Message List**: A classic chat log using the `duo-pop-in` animation for each message.
- **Auto-scroll**: Uses a `useRef` and `scrollIntoView` in a `useEffect` hook to keep the latest response visible.

---

## 📝 Markdown Rendering

Responses from the AI frequently contain C# code snippets. To handle this, we use:
- **`react-markdown`**: Converts AI text into standard HTML.
- **`remark-gfm`**: Enables GitHub Flavored Markdown (tables, task lists).
- **CSS Styling**: The `.duo-md` class in `index.css` styles code blocks with a dark, syntax-heavy aesthetic (`background: #1E2030`) and rounded corners, matching the **Playground**'s appearance.

---

## ✨ Micro-Interactions

- **Typing Indicator**: A bouncy 3-dot animation (`duo-typing`) suggests the AI is thinking, providing natural pacing.
- **Suggested Questions**: A grid of common C# questions appears when the chat is empty, helping students get started.
- **Status Dot**: A small live indicator shows "AI Online", "Degraded" (Offline Mode), or "Error".

---

## 🗣️ Voice Narration (Optional)

The UI includes a toggle for **Voice Summary**. When enabled, the `useVoiceNarration` hook reads the AI's explanation using the browser's speech synthesis engine, which is especially helpful for auditory learners or those with visual impairments.

---

## 📱 Adaptive Layout

The chat wrap (`duo-ai-wrap`) is capped at `780px` centered width. This ensures that long lines of code don't stretch too far across the screen, preventing horizontal scanning fatigue.
