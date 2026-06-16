const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api";

export interface Course {
  id: number;
  title: string;
  description: string;
}

export interface Lesson {
  id: number;
  title: string;
  content: string;
  order: number;
}

export interface LessonDetail extends Lesson {
  moduleId: number;
  moduleTitle: string;
  courseId: number;
  courseTitle: string;
  hasQuiz: boolean;
  isCompleted: boolean;
}

export interface Module {
  id: number;
  title: string;
  description: string;
  order: number;
  lessons: Lesson[];
}

export interface CourseDetail extends Course {
  modules: Module[];
}

export interface CourseProgress {
  courseId: number;
  completionPercentage: number;
  completedLessonIds: number[];
  isEnrolled: boolean;
}

export interface AuthResponse {
  token: string;
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}

export interface Enrollment {
  id: number;
  courseId: number;
  courseTitle: string;
  enrolledAt: string;
  completionPercentage: number;
}

export interface LessonCompleteResult {
  lessonId: number;
  courseCompletionPercentage: number;
  xpEarned: number;
  totalXp: number;
  currentStreak: number;
  newBadges: string[];
  courseCompleted: boolean;
  certificateCode: string | null;
}

export interface Quiz {
  id: number;
  title: string;
  lessonId: number;
  questions: QuizQuestion[];
}

export interface QuizQuestion {
  id: number;
  text: string;
  type: string;
  options: QuizOption[];
}

export interface QuizOption {
  id: number;
  text: string;
}

export interface QuizResult {
  score: number;
  totalQuestions: number;
  passed: boolean;
  xpEarned: number;
  totalXp: number;
  newBadges: string[];
  questionResults: { questionId: number; isCorrect: boolean; correctOptionId: number }[];
}

export interface UserProfile {
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  xp: number;
  currentStreak: number;
  maxStreak: number;
  badges: { id: number; name: string; description: string }[];
  enrollments: { courseId: number; courseTitle: string; completionPercentage: number }[];
}

function authHeaders(token: string) {
  return { Authorization: `Bearer ${token}` };
}

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const error = await response.json().catch(() => ({ message: "Request failed" }));
    throw new Error(error.message || "Request failed");
  }
  return response.json();
}

export async function fetchCourses(): Promise<Course[]> {
  const response = await fetch(`${API_BASE_URL}/courses`);
  return handleResponse<Course[]>(response);
}

export async function fetchCourseById(id: number): Promise<CourseDetail> {
  const response = await fetch(`${API_BASE_URL}/courses/${id}`);
  return handleResponse<CourseDetail>(response);
}

export async function fetchCourseProgress(courseId: number, token: string): Promise<CourseProgress> {
  const response = await fetch(`${API_BASE_URL}/courses/${courseId}/progress`, {
    headers: authHeaders(token),
  });
  return handleResponse<CourseProgress>(response);
}

export async function fetchLesson(id: number, token?: string): Promise<LessonDetail> {
  const response = await fetch(`${API_BASE_URL}/lessons/${id}`, {
    headers: token ? authHeaders(token) : {},
  });
  return handleResponse<LessonDetail>(response);
}

export async function completeLesson(lessonId: number, token: string): Promise<LessonCompleteResult> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/complete`, {
    method: "POST",
    headers: authHeaders(token),
  });
  return handleResponse<LessonCompleteResult>(response);
}

export async function fetchQuiz(lessonId: number): Promise<Quiz> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/quiz`);
  return handleResponse<Quiz>(response);
}

export async function submitQuiz(
  lessonId: number,
  optionAnswers: Record<number, number>,
  textAnswers: Record<number, string>,
  token: string
): Promise<QuizResult> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/quiz/submit`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ optionAnswers, textAnswers }),
  });
  return handleResponse<QuizResult>(response);
}

export async function fetchUserProfile(token: string): Promise<UserProfile> {
  const response = await fetch(`${API_BASE_URL}/users/me`, {
    headers: authHeaders(token),
  });
  return handleResponse<UserProfile>(response);
}

export async function register(
  email: string,
  password: string,
  firstName: string,
  lastName: string,
  role?: string
): Promise<AuthResponse> {
  const response = await fetch(`${API_BASE_URL}/auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password, firstName, lastName, role: role ?? "Student" }),
  });
  return handleResponse<AuthResponse>(response);
}

export async function login(email: string, password: string): Promise<AuthResponse> {
  const response = await fetch(`${API_BASE_URL}/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });
  return handleResponse<AuthResponse>(response);
}

export async function enrollInCourse(courseId: number, token: string): Promise<Enrollment> {
  const response = await fetch(`${API_BASE_URL}/courses/${courseId}/enroll`, {
    method: "POST",
    headers: authHeaders(token),
  });
  return handleResponse<Enrollment>(response);
}

export interface CodeExecutionResult {
  success: boolean;
  output: string;
  error: string | null;
}

export interface AiAssistantResponse {
  reply: string;
  usedAiProvider: boolean;
}

export interface Certificate {
  id: number;
  courseId: number;
  courseTitle: string;
  certificateCode: string;
  issuedAt: string;
}

export interface CertificateVerification {
  certificateCode: string;
  studentName: string;
  courseTitle: string;
  issuedAt: string;
}

export interface LeaderboardEntry {
  rank: number;
  userId: number;
  displayName: string;
  xp: number;
  currentStreak: number;
}

export async function runCode(code: string): Promise<CodeExecutionResult> {
  const response = await fetch(`${API_BASE_URL}/playground/run`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ code }),
  });
  return handleResponse<CodeExecutionResult>(response);
}

export async function askAssistant(message: string, lessonContext?: string): Promise<AiAssistantResponse> {
  const response = await fetch(`${API_BASE_URL}/assistant/chat`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ message, lessonContext }),
  });
  return handleResponse<AiAssistantResponse>(response);
}

export async function fetchCertificates(token: string): Promise<Certificate[]> {
  const response = await fetch(`${API_BASE_URL}/certificates`, {
    headers: authHeaders(token),
  });
  return handleResponse<Certificate[]>(response);
}

export async function verifyCertificate(code: string): Promise<CertificateVerification> {
  const response = await fetch(`${API_BASE_URL}/certificates/verify/${code}`);
  return handleResponse<CertificateVerification>(response);
}

export async function fetchLeaderboard(top = 10): Promise<LeaderboardEntry[]> {
  const response = await fetch(`${API_BASE_URL}/leaderboard?top=${top}`);
  return handleResponse<LeaderboardEntry[]>(response);
}

export interface Classroom {
  id: number;
  name: string;
  description: string;
  joinCode: string;
  courseId: number | null;
  courseTitle: string | null;
  teacherName: string;
  memberCount: number;
  members: { userId: number; name: string; email: string; joinedAt: string }[];
}

export interface AnalyticsDashboard {
  totalUsers: number;
  totalEnrollments: number;
  totalQuizAttempts: number;
  averageCourseCompletion: number;
  quizPassRate: number;
  totalClassrooms: number;
  certificatesIssued: number;
  courseStats: { courseId: number; courseTitle: string; enrollmentCount: number; averageCompletion: number }[];
}

export async function fetchClassrooms(token: string): Promise<Classroom[]> {
  const response = await fetch(`${API_BASE_URL}/classrooms`, {
    headers: authHeaders(token),
  });
  return handleResponse<Classroom[]>(response);
}

export async function createClassroom(
  token: string,
  data: { name: string; description: string; courseId: number | null }
): Promise<Classroom> {
  const response = await fetch(`${API_BASE_URL}/classrooms`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse<Classroom>(response);
}

export async function joinClassroom(token: string, joinCode: string): Promise<Classroom> {
  const response = await fetch(`${API_BASE_URL}/classrooms/join`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ joinCode }),
  });
  return handleResponse<Classroom>(response);
}

export async function fetchAnalyticsDashboard(token: string): Promise<AnalyticsDashboard> {
  const response = await fetch(`${API_BASE_URL}/analytics/dashboard`, {
    headers: authHeaders(token),
  });
  return handleResponse<AnalyticsDashboard>(response);
}

export async function generateQuiz(lessonId: number, token: string, count = 5): Promise<{ quizId: number; questionCount: number; usedAi: boolean }> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/quiz/generate?count=${count}`, {
    method: "POST",
    headers: authHeaders(token),
  });
  return handleResponse(response);
}

export function getCertificatePdfUrl(code: string): string {
  return `${API_BASE_URL}/certificates/${code}/pdf`;
}
