# Classroom Management

Teachers use Classrooms to organize students and formalize the learning process.

---

## 🏛️ Architecture

- **Classrooms**: Owned by a Teacher. Members join via a unique `JoinCode`.
- **Enrollments**: Members are automatically enrolled in the course linked to the classroom.
- **Assignments**: Specific tasks (Reading, Quizzes, or Custom Projects) assigned to the classroom.

---

## 👨‍🏫 Teacher Workflows

### 1. Room Creation
Teachers can create multiple rooms (e.g., "Monday Group", "Evening Cohort") and link them to the appropriate curriculum level.

### 2. Assignment Oversight
Teachers see a real-time list of:
- Total students.
- Average completion percentage for the course.
- Number of ungraded submissions.

### 3. Grading Platform
A specialized side-by-side view allowing teachers to:
- Read student-submitted code or text.
- Execute student code in the playground for verification.
- Provide a numeric score and textual feedback.
- Award custom XP bonuses for exceptional work.

---

## 📊 Analytics
The **Teaching Dashboard** provides charts on user engagement, quiz pass rates, and common points of failure, allowing teachers to identify which topics need more classroom review.
