# Admin Panel

The Admin Panel is the "Command Center" for the platform, accessible only to users with the `Admin` role.

---

## 🛠️ Management Capabilities

### 1. User Oversight
Admins can view all registered users, their total XP, and account creation dates. They have the power to elevate a Student to the Teacher role.

### 2. Content Authoring
A full CRUD (Create, Read, Update, Delete) interface for:
- **Courses**: Title, Description, Difficulty, and Estimated Hours.
- **Modules & Lessons**: Hierarchical management of curriculum content.
- **Challenges**: Independent puzzles with XP rewards.
- **Practices**: Lesson-specific coding exercises.

### 3. Video Management
A centralized dashboard to link YouTube videos to specific lessons across the platform.

---

## 📊 Platform Analytics

Admins see a birds-eye view of the entire system:
- **Total Registered Users**.
- **Active Enrollments**.
- **Successful Quiz Completions**.
- **Certificates Issued**.
- **Content Engagement**: Which courses are most popular and where students typically drop off.

---

## 🔒 Security
The Admin Panel interface and all underlying API routes (`/api/admin/*`) are protected by rigid Role-Based Access Control (RBAC). A student attempting to navigate to `/admin` will be automatically redirected to the home page by the `AdminDashboard.tsx` guard logic.
