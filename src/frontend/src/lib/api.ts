const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api";

export interface Course {
  id: number;
  title: string;
  description: string;
  level: string;
  estimatedHours: number;
  lessonCount: number;
}

export interface Lesson {
  id: number;
  title: string;
  content: string;
  order: number;
  type?: string;
  durationMinutes?: number;
}

export interface LessonVideo {
  id: number;
  title: string;
  videoUrl: string;
  provider: string;
  embedUrl: string;
  durationMinutes: number;
}

export interface LessonDetail extends Lesson {
  moduleId: number;
  moduleTitle: string;
  courseId: number;
  courseTitle: string;
  hasQuiz: boolean;
  isCompleted: boolean;
  bestPractices: string;
  voiceSummary: string;
  videos: LessonVideo[];
  hasTutorial: boolean;
  hasPractice: boolean;
  hasVideos: boolean;
}

export interface Module {
  id: number;
  title: string;
  description: string;
  learningObjectives?: string;
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
  error?: string | null;
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
  attachments: Attachment[];
}

export interface Attachment {
  id: number;
  fileName: string;
  fileUrl: string;
  fileType: string;
  fileSize: number;
  uploadedById: number;
  createdAt: string;
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

export async function fetchTeachingAnalytics(token: string): Promise<AnalyticsDashboard> {
  const response = await fetch(`${API_BASE_URL}/analytics/teaching`, {
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

export interface CodingExercise {
  id: number;
  lessonId: number;
  lessonTitle?: string;
  courseTitle?: string;
  title: string;
  instructions: string;
  starterCode: string;
  hint: string;
  difficulty: number;
}

export interface TutorialStep {
  id: number;
  title: string;
  content: string;
  codeSample: string | null;
  order: number;
}

export interface PracticeResult {
  passed: boolean;
  output: string;
  message: string;
  xpEarned: number;
}

export async function fetchAllPractices(): Promise<CodingExercise[]> {
  const response = await fetch(`${API_BASE_URL}/practices`);
  return handleResponse<CodingExercise[]>(response);
}

export async function fetchLessonExercises(lessonId: number): Promise<CodingExercise[]> {
  const response = await fetch(`${API_BASE_URL}/practices/lessons/${lessonId}`);
  return handleResponse<CodingExercise[]>(response);
}

export async function fetchTutorialSteps(lessonId: number): Promise<TutorialStep[]> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/tutorial`);
  return handleResponse<TutorialStep[]>(response);
}

export async function submitPractice(exerciseId: number, code: string, token: string): Promise<PracticeResult> {
  const response = await fetch(`${API_BASE_URL}/practices/${exerciseId}/submit`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ code }),
  });
  return handleResponse<PracticeResult>(response);
}

// --- Platform: Admin, Teacher, Assignments, Challenges, Progress ---

export interface AdminDashboard {
  totalUsers: number;
  totalCourses: number;
  totalEnrollments: number;
  totalAssignments: number;
  totalChallenges: number;
  recentUsers: { id: number; email: string; name: string; xp: number }[];
}

export interface TeacherDashboard {
  classroomCount: number;
  assignmentCount: number;
  pendingGrading: number;
  recentAssignments: {
    id: number;
    title: string;
    submissionCount: number;
    ungradedCount: number;
    dueDate: string | null;
  }[];
}

export interface Assignment {
  id: number;
  title: string;
  description: string;
  instructions: string;
  courseId: number | null;
  courseTitle: string | null;
  lessonId: number | null;
  classroomId: number | null;
  dueDate: string | null;
  maxPoints: number;
  requiresCode: boolean;
  submissionCount: number;
  mySubmission: Submission | null;
  attachments: Attachment[];
}

export interface Submission {
  id: number;
  assignmentId: number;
  userId: number;
  studentName: string;
  content: string;
  submittedAt: string;
  status: string;
  grade: number | null;
  feedback: string | null;
  attachments: Attachment[];
}

export interface Challenge {
  id: number;
  title: string;
  description: string;
  difficulty: string;
  starterCode: string;
  hint: string;
  tags: string;
  xpReward: number;
}

export interface ChallengeResult {
  passed: boolean;
  output: string;
  message: string;
  xpEarned: number;
}

export interface LessonVideo {
  id: number;
  title: string;
  videoUrl: string;
  provider: string;
  embedUrl: string;
  durationMinutes: number;
}

export interface UserProgressSummary {
  totalXp: number;
  coursesEnrolled: number;
  lessonsCompleted: number;
  practicesCompleted: number;
  challengesCompleted: number;
  courses: {
    courseId: number;
    courseTitle: string;
    completionPercentage: number;
    totalLessons: number;
    completedLessons: number;
  }[];
}

export async function fetchAdminDashboard(token: string): Promise<AdminDashboard> {
  const response = await fetch(`${API_BASE_URL}/admin/dashboard`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function createCourse(
  token: string,
  data: { title: string; description: string; level?: string; estimatedHours: number }
): Promise<Course> {
  const response = await fetch(`${API_BASE_URL}/admin/courses`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function fetchTeacherDashboard(token: string): Promise<TeacherDashboard> {
  const response = await fetch(`${API_BASE_URL}/teacher/dashboard`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function fetchMyAssignments(token: string): Promise<Assignment[]> {
  const response = await fetch(`${API_BASE_URL}/assignments/my`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function fetchTeachingAssignments(token: string): Promise<Assignment[]> {
  const response = await fetch(`${API_BASE_URL}/assignments/teaching`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function fetchClassroomAssignments(token: string): Promise<Assignment[]> {
  const response = await fetch(`${API_BASE_URL}/assignments/classroom`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function createAssignment(
  token: string,
  data: {
    title: string;
    description: string;
    instructions: string;
    courseId?: number;
    lessonId?: number;
    dueDate?: string;
    maxPoints: number;
    requiresCode: boolean;
  }
): Promise<Assignment> {
  const response = await fetch(`${API_BASE_URL}/assignments`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function fetchAssignmentSubmissions(assignmentId: number, token: string): Promise<Submission[]> {
  const response = await fetch(`${API_BASE_URL}/assignments/${assignmentId}/submissions`, {
    headers: authHeaders(token),
  });
  return handleResponse(response);
}

export async function submitAssignment(assignmentId: number, content: string, token: string): Promise<Submission> {
  const response = await fetch(`${API_BASE_URL}/assignments/${assignmentId}/submit`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ content }),
  });
  return handleResponse(response);
}

export async function gradeSubmission(
  submissionId: number,
  grade: number,
  feedback: string,
  token: string
): Promise<Submission> {
  const response = await fetch(`${API_BASE_URL}/assignments/submissions/${submissionId}/grade`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ grade, feedback }),
  });
  return handleResponse(response);
}

export async function fetchChallenges(): Promise<Challenge[]> {
  const response = await fetch(`${API_BASE_URL}/challenges`);
  return handleResponse(response);
}

export async function submitChallenge(challengeId: number, code: string, token: string): Promise<ChallengeResult> {
  const response = await fetch(`${API_BASE_URL}/challenges/${challengeId}/submit`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify({ code }),
  });
  return handleResponse(response);
}

export async function fetchLessonVideos(lessonId: number): Promise<LessonVideo[]> {
  const response = await fetch(`${API_BASE_URL}/lessons/${lessonId}/videos`);
  return handleResponse(response);
}

export async function fetchUserProgressSummary(token: string): Promise<UserProgressSummary> {
  const response = await fetch(`${API_BASE_URL}/users/me/progress`, { headers: authHeaders(token) });
  return handleResponse(response);
}

// --- Admin Management Functions ---

export async function updateCourse(token: string, id: number, data: Partial<Course>): Promise<Course> {
  const response = await fetch(`${API_BASE_URL}/admin/courses/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function deleteCourse(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/courses/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete course");
}

export async function createModule(token: string, data: Partial<Module> & { courseId: number }): Promise<Module> {
  const response = await fetch(`${API_BASE_URL}/admin/modules`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function updateModule(token: string, id: number, data: Partial<Module>): Promise<Module> {
  const response = await fetch(`${API_BASE_URL}/admin/modules/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function deleteModule(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/modules/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete module");
}

export async function createLesson(token: string, data: Partial<LessonDetail> & { moduleId: number }): Promise<LessonDetail> {
  const response = await fetch(`${API_BASE_URL}/admin/lessons`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function updateLesson(token: string, id: number, data: Partial<LessonDetail>): Promise<LessonDetail> {
  const response = await fetch(`${API_BASE_URL}/admin/lessons/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function deleteLesson(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/lessons/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete lesson");
}

export async function createChallenge(token: string, data: Partial<Challenge>): Promise<Challenge> {
  const response = await fetch(`${API_BASE_URL}/admin/challenges`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function updateChallenge(token: string, id: number, data: Partial<Challenge>): Promise<Challenge> {
  const response = await fetch(`${API_BASE_URL}/admin/challenges/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function deleteChallenge(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/challenges/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete challenge");
}

export async function createPractice(token: string, data: Partial<CodingExercise>): Promise<CodingExercise> {
  const response = await fetch(`${API_BASE_URL}/admin/practices`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function updatePractice(token: string, id: number, data: Partial<CodingExercise>): Promise<CodingExercise> {
  const response = await fetch(`${API_BASE_URL}/admin/practices/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return handleResponse(response);
}

export async function deletePractice(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/practices/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete practice");
}

export async function uploadAttachment(
  token: string,
  file: File,
  params: { classroomId?: number; assignmentId?: number; submissionId?: number }
): Promise<Attachment> {
  const formData = new FormData();
  formData.append("file", file);
  if (params.classroomId) formData.append("classroomId", params.classroomId.toString());
  if (params.assignmentId) formData.append("assignmentId", params.assignmentId.toString());
  if (params.submissionId) formData.append("submissionId", params.submissionId.toString());

  const response = await fetch(`${API_BASE_URL}/assignments/attachments`, {
    method: "POST",
    headers: authHeaders(token),
    body: formData,
  });
  return handleResponse(response);
}

export async function deleteAttachment(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/assignments/attachments/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete attachment");
}

export function getFileUrl(path: string): string {
  const base = API_BASE_URL.replace("/api", "");
  return `${base}${path}`;
}

export async function createLessonVideo(token: string, data: Partial<LessonVideo>): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/videos`, {
    method: "POST",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!response.ok) throw new Error("Failed to create video");
}

export async function fetchAllLessonVideos(token: string): Promise<LessonVideo[]> {
  const response = await fetch(`${API_BASE_URL}/admin/videos`, { headers: authHeaders(token) });
  return handleResponse(response);
}

export async function updateLessonVideo(token: string, id: number, data: Partial<LessonVideo>): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/videos/${id}`, {
    method: "PUT",
    headers: { ...authHeaders(token), "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!response.ok) throw new Error("Failed to update video");
}

export async function deleteLessonVideo(token: string, id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/admin/videos/${id}`, {
    method: "DELETE",
    headers: authHeaders(token),
  });
  if (!response.ok) throw new Error("Failed to delete video");
}
