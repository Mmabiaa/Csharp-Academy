export default function Home() {
  return (
    <div className="max-w-7xl mx-auto px-4 py-12">
      <h1 className="text-4xl font-bold text-gray-900 mb-4">Welcome to C# Academy!</h1>
      <p className="text-lg text-gray-600 mb-8">Your journey to mastering C# starts here.</p>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-2">Learn C#</h2>
          <p className="text-gray-600">Structured courses from beginner to advanced.</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-2">Practice</h2>
          <p className="text-gray-600">Interactive coding playgrounds and quizzes.</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-2">AI Assistant</h2>
          <p className="text-gray-600">Get help from our AI tutor anytime.</p>
        </div>
      </div>
    </div>
  );
}
