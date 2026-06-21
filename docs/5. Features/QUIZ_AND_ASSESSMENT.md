# Quiz and Assessment Engine

Assessments are split between static (hand-crafted) and Dynamic (AI-generated) content.

---

## 🧠 Question Types
- **Multiple Choice**: Standard single-correct option.
- **True/False**: Quick concept verification.
- **Fill in the Blank**: Text-based matching, excellent for keyword memorization.
- **Output Prediction**: The student is shown a block of C# code and must choose the correct console output.

---

## 🪄 AI Generation Architecture

The `AiQuizGenerationService` performs the following:
1. **Context Harvesting**: Pulls the text content of the current lesson.
2. **Prompter**: Instructs Gemini to "Act as a certification examiner" and generate a specific number of questions in a structured JSON schema.
3. **Validation**: The backend verifies the JSON integrity before saving the questions to the database.
4. **Fallback**: If the AI fails, a high-quality fallback quiz based on general C# knowledge is served.

---

## 🎖️ Rewards
- **XP Gain**: Traditionally 20-50 XP per passed quiz.
- **Streaks**: Passing a quiz counts as "Daily Activity" for maintaining a streak.
- **Badge**: The "Quiz Master" badge is awarded after the first 3 passed assessments.
