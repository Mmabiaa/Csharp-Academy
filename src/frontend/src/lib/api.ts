const API_BASE_URL = import.meta.env.VITE_API_URL || "https://localhost:5001/api";

export interface Course {
  id: number;
  title: string;
  description: string;
}

export async function fetchCourses(): Promise<Course[]> {
  const response = await fetch(`${API_BASE_URL}/courses`);
  if (!response.ok) {
    throw new Error("Failed to fetch courses");
  }
  return response.json();
}
