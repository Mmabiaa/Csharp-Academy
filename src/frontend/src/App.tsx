import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Home from "./pages/Home";
import Courses from "./pages/Courses";
import CourseDetail from "./pages/CourseDetail";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { useAuth } from "./context/AuthContext";

function App() {
  const { user, isAuthenticated, logout } = useAuth();

  return (
    <Router>
      <div className="min-h-screen bg-gray-50">
        <header className="bg-white shadow-sm">
          <div className="max-w-7xl mx-auto px-4 py-4 flex items-center justify-between">
            <Link to="/" className="text-2xl font-bold text-gray-900">C# Academy</Link>
            <nav className="flex items-center gap-6">
              <Link to="/" className="text-gray-600 hover:text-gray-900 font-medium">Home</Link>
              <Link to="/courses" className="text-gray-600 hover:text-gray-900 font-medium">Courses</Link>
              {isAuthenticated ? (
                <>
                  <span className="text-gray-600 text-sm">
                    Hi, {user?.firstName}
                  </span>
                  <button
                    onClick={logout}
                    className="text-gray-600 hover:text-gray-900 font-medium"
                  >
                    Logout
                  </button>
                </>
              ) : (
                <>
                  <Link to="/login" className="text-gray-600 hover:text-gray-900 font-medium">Login</Link>
                  <Link to="/register" className="bg-blue-600 text-white px-4 py-1.5 rounded-md hover:bg-blue-700 font-medium">
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
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
