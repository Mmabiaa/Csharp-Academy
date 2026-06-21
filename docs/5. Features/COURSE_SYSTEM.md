# Course and Content System

C# Academy organizes learning content into a strict, manageable hierarchy.

## 🌳 Structure
- **Course**: The curriculum theme (e.g., "C# Fundamentals", "Design Patterns").
- **Module**: Thematical grouping within a course (e.g., "Basics", "OOPS").
- **Lesson**: The core learning unit containing:
  - **Narrative Content** (Markdown)
  - **Video Tutorials**
  - **Best Practices**
  - **Voice Summaries**
- **Exercise/Quiz**: Verification steps attached to each lesson.

## 📈 Completion Logic
Student progress is captured in the `Progress` table. A lesson is considered "Completed" once:
1. The student clicks "Complete Lesson" after reading.
2. The student passes the associated Quiz (if present).
3. The student successfully runs and passes the associated Coding Practice (if present).

---

# Quiz and Assessment Engine

Assessments are split between static (hand-crafted) and Dynamic (AI-generated) content.

## 🧠 Question Types
- **Multiple Choice**: Single correct option.
- **True/False**: Boolean assessment.
- **Fill in the Blank**: Text-based exact matching for syntax.
- **Output Prediction**: The student is shown code and must predict what `Console.WriteLine` will print.

## 🪄 AI Generation
Using the `AiQuizGenerationService`, teachers can click "Generate with AI" from the Admin Panel. The backend sends the lesson content to **Gemini**, which returns a JSON-formatted quiz designed specifically for that lesson's topic.

---

# Classroom Management

Teachers can manage their student cohorts through **Classrooms**.

- **Creation**: Teachers generate a room with a descriptive name and an optional linked course.
- **Join Code**: Every classroom has a unique code (e.g., `XJ39K2`).
- **Assignments**: Teachers create tasks with due dates and custom instructions.
- **Grading**: Teachers view student submissions, providing a numeric score and text-based feedback.
- **Analytics**: Teachers can see which students are falling behind or excelling via the **Teacher Analytics** dashboard.
