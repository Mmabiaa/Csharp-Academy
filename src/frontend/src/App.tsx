import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
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
import Login from "./pages/Login";
import Register from "./pages/Register";
import { useAuth } from "./context/AuthContext";

function App() {
  const { user, isAuthenticated, isTeacher, logout } = useAuth();

  return (
    <Router>
      <div className="min-h-screen bg-gray-50">
        <header className="bg-white shadow-sm">
          <div className="max-w-7xl mx-auto px-4 py-4 flex items-center justify-between">
            <Link to="/" className="text-2xl font-bold text-gray-900">C# Academy</Link>
            <nav className="flex items-center gap-5 text-sm">
              <Link to="/courses" className="text-gray-600 hover:text-gray-900 font-medium">Courses</Link>
              <Link to="/practices" className="text-gray-600 hover:text-gray-900 font-medium">Practices</Link>
              <Link to="/playground" className="text-gray-600 hover:text-gray-900 font-medium">Playground</Link>
              <Link to="/assistant" className="text-gray-600 hover:text-gray-900 font-medium">AI Tutor</Link>
              <Link to="/leaderboard" className="text-gray-600 hover:text-gray-900 font-medium">Leaderboard</Link>
              {isAuthenticated && (
                <Link to="/classrooms" className="text-gray-600 hover:text-gray-900 font-medium">Classrooms</Link>
              )}
              {isTeacher && (
                <Link to="/analytics" className="text-gray-600 hover:text-gray-900 font-medium">Analytics</Link>
              )}
              {isAuthenticated && (
                <Link to="/profile" className="text-gray-600 hover:text-gray-900 font-medium">Profile</Link>
              )}
              {isAuthenticated ? (
                <>
                  <span className="text-gray-500">Hi, {user?.firstName}</span>
                  <button onClick={logout} className="text-gray-600 hover:text-gray-900 font-medium">
                    Logout
                  </button>
                </>
              ) : (
                <>
                  <Link to="/login" className="text-gray-600 hover:text-gray-900 font-medium">Login</Link>
                  <Link to="/register" className="bg-blue-600 text-white px-3 py-1.5 rounded-md hover:bg-blue-700 font-medium">
                    Register
                  </Link>
                </>
              )}
            </nav>
          </div>
        </header>
        <main>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/courses" element={<Courses />} />
            <Route path="/courses/:id" element={<CourseDetail />} />
            <Route path="/practices" element={<Practices />} />
            <Route path="/lessons/:id" element={<LessonPage />} />
            <Route path="/lessons/:id/quiz" element={<QuizPage />} />
            <Route path="/playground" element={<Playground />} />
            <Route path="/assistant" element={<Assistant />} />
            <Route path="/leaderboard" element={<Leaderboard />} />
            <Route path="/classrooms" element={<Classrooms />} />
            <Route path="/analytics" element={<Analytics />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/certificates/:code" element={<CertificateVerify />} />
            <Route path="/certificates" element={<CertificateVerify />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
