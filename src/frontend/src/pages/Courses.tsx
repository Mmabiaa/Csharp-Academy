export default function Courses() {
  const courses = [
    { id: 1, title: "C# Fundamentals for Beginners", description: "Learn the basics of C# programming language, including variables, loops, and functions." },
    { id: 2, title: "Object-Oriented Programming with C#", description: "Master OOP concepts like classes, inheritance, and polymorphism in C#." },
  ];

  return (
    <div className="max-w-7xl mx-auto px-4 py-12">
      <h1 className="text-4xl font-bold text-gray-900 mb-8">Courses</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {courses.map((course) => (
          <div key={course.id} className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
            <h2 className="text-2xl font-semibold mb-2 text-gray-900">{course.title}</h2>
            <p className="text-gray-600">{course.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
