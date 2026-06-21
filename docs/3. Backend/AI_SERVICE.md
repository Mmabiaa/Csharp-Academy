# AI Assistant Service

The **AI Tutor** is one of the flagship features of C# Academy. It provides personalized, real-time guidance to students using the Google Gemini model.

---

## 🤖 Core Implementation

The system uses the `AiAssistantService.cs` located in the Infrastructure layer. It leverages the **Google Gemini Pro** model (defaulting to `gemini-1-5-flash`) via the `GeminiClient` helper.

### 🧩 Key Components
1. **AiAssistantService**: Orchestrates the communication, handling context preparation and fallback logic.
2. **GeminiClient**: A lightweight static helper that manages HTTP communication with the Google Generative AI API.
3. **OfflineTutor**: A rule-based fallback system that provides static but high-quality explanations for core C# concepts when the API is unavailable.

---

## 🧠 Contextual Awareness

Unlike a generic chatbot, the AI Tutor is aware of the student's current learning state:
- **Lesson Context**: If a student opens the chat from a specific lesson (e.g., "Loops"), the frontend sends a `lessonContext` parameter.
- **Prompt Engineering**: The backend injects a system prompt:
  > *"You are a friendly C# tutor for C# Academy. Explanations should be simple, mention best practices, and use emoji where appropriate. If a student asks unrelated questions, politely guide them back to C#."*

---

## 🛡️ Graceful Degradation (Offline Mode)

We prioritize availability. If any of the following occur:
- `GEMINI_API_KEY` is missing in `.env`.
- API Rate limit reached (429).
- Network failure.

The service catches the `GeminiException` and triggers the **Offline Fallback**. It scans the user's message for keywords (e.g., "class", "loop", "variable") and returns a high-quality pre-written explanation. The frontend reflects this by showing a "Degraded" or "Offline" status dot.

---

## ⚙️ Technical Specs

- **Provider**: Google Gemini
- **Model**: `gemini-1.5-flash` or `gemini-2.0-flash` (configurable in `.env`)
- **Max Tokens**: 1800 (controlled via `maxOutputTokens`)
- **Safety Settings**: Configured to BLOCK_NONE for educational purposes, allowing the AI to discuss potentially sensitive coding concepts (like hacking-prevention) if needed.
- **Cost Efficiency**: No history is stored on the server side; each request is stateless to minimize token usage and latency.

---

## 🧪 Quiz Generation
The `AiQuizGenerationService` uses the same infrastructure to dynamically generate quiz questions based on lesson content, ensuring that no two students get the exact same assessment.
