import { Link } from "react-router-dom";

export default function Home() {
  return (
    <div className="max-w-7xl mx-auto px-4 py-12">
      <h1 className="text-4xl font-bold text-gray-900 mb-4">Welcome to C# Academy!</h1>
      <p className="text-lg text-gray-600 mb-8">Your journey to mastering C# starts here.</p>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <Link to="/courses" className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
          <h2 className="text-xl font-semibold mb-2">📚 Structured Courses</h2>
          <p className="text-gray-600">Step-by-step lessons from beginner to advanced with progress tracking.</p>
        </Link>
        <Link to="/practices" className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
          <h2 className="text-xl font-semibold mb-2">💻 Coding Practices</h2>
          <p className="text-gray-600">Hands-on exercises with instant feedback and XP rewards.</p>
        </Link>
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-2">🎧 Voice Lessons</h2>
          <p className="text-gray-600">Listen to lesson summaries with built-in voice narration on every lesson.</p>
        </div>
        <Link to="/playground" className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
          <h2 className="text-xl font-semibold mb-2">⚡ Playground</h2>
          <p className="text-gray-600">Run C# code snippets instantly in the browser.</p>
        </Link>
        <Link to="/assistant" className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
          <h2 className="text-xl font-semibold mb-2">🤖 AI Tutor</h2>
          <p className="text-gray-600">Get help from our AI assistant anytime you're stuck.</p>
        </Link>
        <Link to="/leaderboard" className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
          <h2 className="text-xl font-semibold mb-2">🏆 Leaderboard</h2>
          <p className="text-gray-600">Compete with other learners and climb the XP rankings.</p>
        </Link>
      </div>
    </div>
  );
}
