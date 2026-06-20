import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
    fetchCourses,
    fetchCourseById,
    updateCourse,
    deleteCourse,
    createModule,
    updateModule,
    deleteModule,
    createLesson,
    updateLesson,
    deleteLesson,
    fetchLesson,
    createLessonVideo,
    updateLessonVideo,
    deleteLessonVideo,
    LessonVideo
} from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
    BookOpen,
    Plus,
    Trash2,
    Edit3,
    ChevronRight,
    Layers,
    FileText,
    Save,
    X,
    ArrowLeft,
    Settings,
    Eye,
    Video
} from "lucide-react";


type ViewState = {
    type: "courses" | "modules" | "lessons";
    id: number | null;
    parentId: number | null;
    name: string;
};

export default function AdminCourses() {
    const { token, isAdmin } = useAuth();
    const queryClient = useQueryClient();
    const [view, setView] = useState<ViewState>({ type: "courses", id: null, parentId: null, name: "Courses" });
    const [editingId, setEditingId] = useState<number | null>(null);
    const [editForm, setEditForm] = useState<any>({});

    const { data: courses, isLoading: coursesLoading } = useQuery({
        queryKey: ["admin-courses"],
        queryFn: () => fetchCourses(),
        enabled: view.type === "courses",
    });

    const { data: courseDetail, isLoading: courseLoading } = useQuery({
        queryKey: ["admin-course", view.id],
        queryFn: () => fetchCourseById(view.id!),
        enabled: view.type === "modules" && view.id !== null,
    });

    const updateCourseMutation = useMutation({
        mutationFn: (data: any) => updateCourse(token!, editingId!, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-courses"] });
            setEditingId(null);
        }
    });

    const updateModuleMutation = useMutation({
        mutationFn: (data: any) => updateModule(token!, editingId!, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-course", view.id] });
            setEditingId(null);
        }
    });

    const updateLessonMutation = useMutation({
        mutationFn: (data: any) => updateLesson(token!, editingId!, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-course", view.parentId] });
            setEditingId(null);
        }
    });

    const createCourseMutation = useMutation({
        mutationFn: (data: any) => updateCourse(token!, 0, data), // Backend usually handles 0 as new
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-courses"] });
            setEditingId(null);
        }
    });

    const createModuleMutation = useMutation({
        mutationFn: (data: any) => createModule(token!, { ...data, courseId: view.id! }),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-course", view.id] });
            setEditingId(null);
        }
    });

    const createLessonMutation = useMutation({
        mutationFn: (data: any) => createLesson(token!, { ...data, moduleId: view.id! }),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-course", view.parentId] });
            setEditingId(null);
        }
    });

    const handleAddVideo = () => {
        const currentVideos = editForm.videos || [];
        setEditForm({
            ...editForm,
            videos: [...currentVideos, { title: "", videoUrl: "", provider: "YouTube", durationMinutes: 10 }]
        });
    };

    const handleRemoveVideo = (index: number) => {
        const currentVideos = [...editForm.videos];
        currentVideos.splice(index, 1);
        setEditForm({ ...editForm, videos: currentVideos });
    };

    const handleVideoChange = (index: number, field: string, value: any) => {
        const currentVideos = [...editForm.videos];
        currentVideos[index] = { ...currentVideos[index], [field]: value };
        setEditForm({ ...editForm, videos: currentVideos });
    };

    const deleteItemMutation = useMutation({
        mutationFn: async ({ type, id }: { type: string, id: number }) => {
            if (type === "courses") await deleteCourse(token!, id);
            if (type === "modules") await deleteModule(token!, id);
            if (type === "lessons") await deleteLesson(token!, id);
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-courses"] });
            queryClient.invalidateQueries({ queryKey: ["admin-course"] });
        }
    });

    if (!isAdmin) return <div className="p-8 text-center font-black text-red-500">Access Denied</div>;

    const handleEdit = async (item: any) => {
        setEditingId(item.id);
        if (view.type === "lessons") {
            // Fetch full lesson details for list of videos, best practices etc
            try {
                const fullLesson = await fetchLesson(item.id, token!);
                setEditForm(fullLesson);
            } catch (error) {
                console.error("Failed to fetch lesson details", error);
                setEditForm(item); // Fallback to basic data
            }
        } else {
            setEditForm(item);
        }
    };

    const handleBack = () => {
        if (view.type === "lessons") {
            setView({ type: "modules", id: view.parentId, parentId: null, name: "Modules" });
        } else if (view.type === "modules") {
            setView({ type: "courses", id: null, parentId: null, name: "Courses" });
        }
    };

    const startCreate = () => {
        setEditingId(-1);
        if (view.type === "courses") setEditForm({ title: "", description: "", level: "Beginner", estimatedHours: 10 });
        if (view.type === "modules") setEditForm({ title: "", description: "", learningObjectives: "", order: 1 });
        if (view.type === "lessons") setEditForm({ title: "", content: "", order: 1, type: "Reading" });
    };

    return (
        <div className="space-y-6 pb-24 md:pb-0">
            <div className="flex items-center justify-between gap-4">
                <div className="flex items-center gap-4">
                    {view.type !== "courses" && (
                        <button onClick={handleBack} className="p-2 hover:bg-neutral-100 rounded-full transition-colors">
                            <ArrowLeft className="w-6 h-6 text-neutral-600" />
                        </button>
                    )}
                    <div>
                        <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">
                            Curriculum Management
                        </p>
                        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">{view.name}</h1>
                    </div>
                </div>
                <button onClick={startCreate} className="duo-btn3d duo-btn3d-green">
                    <Plus className="w-4 h-4" />
                    Add {view.type === "courses" ? "Course" : view.type === "modules" ? "Module" : "Lesson"}
                </button>
            </div>

            {view.type === "courses" && (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    {courses?.map(course => (
                        <div key={course.id} className="duo-card duo-card-hover group">
                            <div className="flex justify-between items-start mb-4">
                                <div className="w-12 h-12 rounded-2xl bg-[#DDF4FF] flex items-center justify-center shadow-[0_3px_0_#1CB0F6]">
                                    <BookOpen className="w-6 h-6 text-[#1CB0F6]" />
                                </div>
                                <div className="flex gap-2">
                                    <button onClick={() => handleEdit(course)} className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors">
                                        <Edit3 className="w-5 h-5" />
                                    </button>
                                    <button
                                        onClick={() => { if (confirm("Delete course?")) deleteItemMutation.mutate({ type: "courses", id: course.id }) }}
                                        className="p-2 text-neutral-400 hover:text-[#FF4B4B] transition-colors"
                                    >
                                        <Trash2 className="w-5 h-5" />
                                    </button>
                                </div>
                            </div>
                            <h3 className="text-lg font-black text-neutral-900 mb-2">{course.title}</h3>
                            <p className="text-sm font-bold text-neutral-500 mb-6 line-clamp-2">{course.description}</p>
                            <button
                                onClick={() => setView({ type: "modules", id: course.id, parentId: null, name: course.title })}
                                className="w-full flex items-center justify-center gap-2 py-3 rounded-2xl bg-[#f7f7f7] border-2 border-transparent hover:border-[#58CC02] hover:bg-white font-black text-neutral-600 hover:text-[#58CC02] transition-all"
                            >
                                Structure
                                <ChevronRight className="w-4 h-4" />
                            </button>
                        </div>
                    ))}
                </div>
            )}

            {view.type === "modules" && courseDetail && (
                <div className="space-y-4">
                    {courseDetail.modules.map(module => (
                        <div key={module.id} className="duo-card group flex items-center justify-between gap-4">
                            <div className="flex items-center gap-4">
                                <div className="w-10 h-10 rounded-xl bg-[#D7FFB8] flex items-center justify-center text-[#46A302]">
                                    <Layers className="w-5 h-5" />
                                </div>
                                <div>
                                    <h3 className="font-black text-neutral-900">{module.title}</h3>
                                    <p className="text-xs font-bold text-neutral-500 uppercase tracking-wide">
                                        {module.lessons?.length || 0} Lessons
                                    </p>
                                </div>
                            </div>
                            <div className="flex items-center gap-2">
                                <button
                                    onClick={() => setView({ type: "lessons", id: module.id, parentId: courseDetail.id, name: module.title })}
                                    className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors"
                                >
                                    <Eye className="w-5 h-5" />
                                </button>
                                <button onClick={() => handleEdit(module)} className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors">
                                    <Edit3 className="w-5 h-5" />
                                </button>
                                <button
                                    onClick={() => { if (confirm("Delete module?")) deleteItemMutation.mutate({ type: "modules", id: module.id }) }}
                                    className="p-2 text-neutral-400 hover:text-[#FF4B4B] transition-colors"
                                >
                                    <Trash2 className="w-5 h-5" />
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            )}

            {view.type === "lessons" && courseDetail && (
                <div className="space-y-4">
                    {courseDetail.modules.find(m => m.id === view.id)?.lessons?.map(lesson => (
                        <div key={lesson.id} className="duo-card group flex items-center justify-between gap-4">
                            <div className="flex items-center gap-4">
                                <div className="w-10 h-10 rounded-xl bg-[#EEEDFE] flex items-center justify-center text-[#534AB7]">
                                    <FileText className="w-5 h-5" />
                                </div>
                                <div>
                                    <h3 className="font-black text-neutral-900">{lesson.title}</h3>
                                    <p className="text-xs font-bold text-neutral-500 uppercase tracking-wide">
                                        Order: {lesson.order}
                                    </p>
                                </div>
                            </div>
                            <div className="flex items-center gap-2">
                                <button onClick={() => handleEdit(lesson)} className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors">
                                    <Edit3 className="w-5 h-5" />
                                </button>
                                <button
                                    onClick={() => { if (confirm("Delete lesson?")) deleteItemMutation.mutate({ type: "lessons", id: lesson.id }) }}
                                    className="p-2 text-neutral-400 hover:text-[#FF4B4B] transition-colors"
                                >
                                    <Trash2 className="w-5 h-5" />
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            )}

            {editingId && (
                <div className="fixed inset-0 z-[100] flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
                    <div className="bg-white w-full max-w-2xl rounded-[32px] overflow-hidden shadow-2xl animate-pop">
                        <div className="px-8 py-6 border-b-2 border-neutral-100 flex items-center justify-between">
                            <h2 className="text-xl font-black text-neutral-900">
                                {editingId === -1 ? "Create" : "Edit"} {view.type.slice(0, -1)}
                            </h2>
                            <button onClick={() => setEditingId(null)} className="p-2 hover:bg-neutral-100 rounded-full transition-colors">
                                <X className="w-6 h-6 text-neutral-400" />
                            </button>
                        </div>
                        <div className="p-8 space-y-6 max-h-[70vh] overflow-y-auto">
                            {view.type === "courses" && (
                                <>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Title</label>
                                        <input
                                            value={editForm.title}
                                            onChange={e => setEditForm({ ...editForm, title: e.target.value })}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Description</label>
                                        <textarea
                                            value={editForm.description}
                                            onChange={e => setEditForm({ ...editForm, description: e.target.value })}
                                            rows={4}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                </>
                            )}
                            {view.type === "modules" && (
                                <>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Module Title</label>
                                        <input
                                            value={editForm.title}
                                            onChange={e => setEditForm({ ...editForm, title: e.target.value })}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Learning Objectives</label>
                                        <textarea
                                            value={editForm.learningObjectives}
                                            onChange={e => setEditForm({ ...editForm, learningObjectives: e.target.value })}
                                            rows={4}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Order</label>
                                        <input
                                            type="number"
                                            value={editForm.order}
                                            onChange={e => setEditForm({ ...editForm, order: parseInt(e.target.value) })}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                </>
                            )}
                            {view.type === "lessons" && (
                                <>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Lesson Title</label>
                                        <input
                                            value={editForm.title}
                                            onChange={e => setEditForm({ ...editForm, title: e.target.value })}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Content (Markdown)</label>
                                        <textarea
                                            value={editForm.content}
                                            onChange={e => setEditForm({ ...editForm, content: e.target.value })}
                                            rows={10}
                                            className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-mono text-sm leading-relaxed"
                                        />
                                    </div>
                                    <div className="space-y-4">
                                        <div className="space-y-2">
                                            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Order</label>
                                            <input
                                                type="number"
                                                value={editForm.order}
                                                onChange={e => setEditForm({ ...editForm, order: parseInt(e.target.value) })}
                                                className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                            />
                                        </div>

                                        <div className="space-y-4">
                                            <div className="flex items-center justify-between">
                                                <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Lesson Videos ({editForm.videos?.length || 0})</label>
                                                <button
                                                    onClick={handleAddVideo}
                                                    className="flex items-center gap-1 text-[10px] font-black text-[#58CC02] uppercase tracking-wider hover:bg-[#D7FFB8] px-2 py-1 rounded-lg transition-colors border-2 border-[#58CC02]"
                                                >
                                                    <Plus className="w-3 h-3" />
                                                    Add Video URL
                                                </button>
                                            </div>

                                            <div className="space-y-3">
                                                {editForm.videos?.map((v: any, index: number) => (
                                                    <div key={index} className="p-4 rounded-2xl bg-neutral-50 border-2 border-neutral-100 space-y-3">
                                                        <div className="flex items-center justify-between">
                                                            <div className="flex items-center gap-2">
                                                                <Video className="w-5 h-5 text-red-500" />
                                                                <span className="text-[10px] font-black text-neutral-400 uppercase tracking-widest">Video URL</span>
                                                            </div>
                                                            <button
                                                                onClick={() => handleRemoveVideo(index)}
                                                                className="p-1 px-2 hover:bg-red-50 rounded-lg text-red-500 font-bold text-[10px] uppercase transition-all"
                                                            >
                                                                Delete
                                                            </button>
                                                        </div>
                                                        <div className="space-y-2">
                                                            <input
                                                                value={v.title}
                                                                onChange={e => handleVideoChange(index, "title", e.target.value)}
                                                                className="w-full px-4 py-2 rounded-xl bg-white border-2 border-transparent focus:border-[#58CC02] outline-none text-xs font-bold"
                                                                placeholder="Video Name (e.g. Introduction to C#)"
                                                            />
                                                            <input
                                                                value={v.videoUrl}
                                                                onChange={e => handleVideoChange(index, "videoUrl", e.target.value)}
                                                                className="w-full px-4 py-2 rounded-xl bg-white border-2 border-transparent focus:border-[#58CC02] outline-none text-[11px] font-mono"
                                                                placeholder="YouTube/Vimeo URL"
                                                            />
                                                        </div>
                                                    </div>
                                                ))}
                                                {(!editForm.videos || editForm.videos.length === 0) && (
                                                    <p className="text-xs font-bold text-neutral-400 italic p-6 text-center border-2 border-dashed border-neutral-200 rounded-3xl">
                                                        No videos. Click "Add Video URL" to include lesson videos.
                                                    </p>
                                                )}
                                            </div>
                                        </div>

                                        <div className="space-y-2">
                                            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Best Practices</label>
                                            <textarea
                                                value={editForm.bestPractices || ""}
                                                onChange={e => setEditForm({ ...editForm, bestPractices: e.target.value })}
                                                rows={3}
                                                className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-sm"
                                                placeholder="What tips should students follow?"
                                            />
                                        </div>
                                    </div>
                                </>
                            )}
                        </div>
                        <div className="px-8 py-6 bg-neutral-50 flex gap-4">
                            <button
                                onClick={() => {
                                    if (editingId === -1) {
                                        if (view.type === "courses") createCourseMutation.mutate(editForm);
                                        if (view.type === "modules") createModuleMutation.mutate(editForm);
                                        if (view.type === "lessons") createLessonMutation.mutate(editForm);
                                    } else {
                                        if (view.type === "courses") updateCourseMutation.mutate(editForm);
                                        if (view.type === "modules") updateModuleMutation.mutate(editForm);
                                        if (view.type === "lessons") updateLessonMutation.mutate(editForm);
                                    }
                                }}
                                className="flex-1 duo-btn3d duo-btn3d-green"
                            >
                                <Save className="w-4 h-4" />
                                Save Changes
                            </button>
                            <button onClick={() => setEditingId(null)} className="flex-1 duo-btn3d duo-btn3d-white">
                                Cancel
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
