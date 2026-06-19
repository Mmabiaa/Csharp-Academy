import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/shared/Home";
import Courses from "./pages/student/Courses";
import CourseDetail from "./pages/student/CourseDetail";
import LessonPage from "./pages/student/LessonPage";
import QuizPage from "./pages/student/QuizPage";
import Profile from "./pages/student/Profile";
import Playground from "./pages/student/Playground";
import Assistant from "./pages/student/Assistant";
import CertificateVerify from "./pages/shared/CertificateVerify";
import Leaderboard from "./pages/student/Leaderboard";
import Classrooms from "./pages/student/Classrooms";
import Analytics from "./pages/teachers/Analytics";
import Practices from "./pages/student/Practices";
import AdminDashboard from "./pages/admin/AdminDashboard";
import AdminCourses from "./pages/admin/AdminCourses";
import AdminChallenges from "./pages/admin/AdminChallenges";
import AdminPractices from "./pages/admin/AdminPractices";
import TeacherPortal from "./pages/teachers/TeacherPortal";
import Assignments from "./pages/teachers/Assignments";
import Challenges from "./pages/student/Challenges";
import ProgressDashboard from "./pages/student/ProgressDashboard";
import Login from "./pages/shared/Login";
import Register from "./pages/shared/Register";

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/courses" element={<Courses />} />
          <Route path="/courses/:id" element={<CourseDetail />} />
          <Route path="/practices" element={<Practices />} />
          <Route path="/challenges" element={<Challenges />} />
          <Route path="/lessons/:id" element={<LessonPage />} />
          <Route path="/lessons/:id/quiz" element={<QuizPage />} />
          <Route path="/playground" element={<Playground />} />
          <Route path="/assistant" element={<Assistant />} />
          <Route path="/leaderboard" element={<Leaderboard />} />
          <Route path="/classrooms" element={<Classrooms />} />
          <Route path="/analytics" element={<Analytics />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/progress" element={<ProgressDashboard />} />
          <Route path="/assignments" element={<Assignments />} />
          <Route path="/teacher" element={<TeacherPortal />} />
          <Route path="/admin" element={<AdminDashboard />} />
          <Route path="/admin/courses" element={<AdminCourses />} />
          <Route path="/admin/challenges" element={<AdminChallenges />} />
          <Route path="/admin/practices" element={<AdminPractices />} />
          <Route path="/certificates/:code" element={<CertificateVerify />} />
          <Route path="/certificates" element={<CertificateVerify />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
