import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import Courses from "./pages/Courses";
import CourseDetail from "./pages/CourseDetail";
import LessonPage from "./pages/LessonPage";
import QuizPage from "./pages/QuizPage";
import Profile from "./pages/Profile";
import Playground from "./pages/Playground";
import Assistant from "./pages/Assistant";
import CertificateVerify from "./pages/CertificateVerify";
import Leaderboard from "./pages/Leaderboard";
import Classrooms from "./pages/Classrooms";
import Analytics from "./pages/Analytics";
import Practices from "./pages/Practices";
import AdminDashboard from "./pages/AdminDashboard";
import TeacherPortal from "./pages/TeacherPortal";
import Assignments from "./pages/Assignments";
import Challenges from "./pages/Challenges";
import ProgressDashboard from "./pages/ProgressDashboard";
import Login from "./pages/Login";
import Register from "./pages/Register";

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
