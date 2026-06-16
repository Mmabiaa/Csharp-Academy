function App() {
  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 py-6">
          <h1 className="text-3xl font-bold text-gray-900">C# Academy</h1>
          <p className="text-gray-600 mt-2">Intelligent C# Learning Environment</p>
        </div>
      </header>
      <main className="max-w-7xl mx-auto px-4 py-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <div className="bg-white p-6 rounded-lg shadow">
            <h2 className="text-xl font-semibold mb-4">Welcome!</h2>
            <p className="text-gray-600">Your journey to mastering C# starts here.</p>
          </div>
        </div>
      </main>
    </div>
  )
}

export default App
