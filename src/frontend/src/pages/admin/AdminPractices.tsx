import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchAllPractices, updatePractice, createPractice, deletePractice, fetchCourses } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
    Code2,
    Plus,
    Trash2,
    Edit3,
    Save,
    X,
    Zap,
    Search,
    BookOpen,
    Info,
    CheckCircle2,
    List
} from "lucide-react";

export default function AdminPractices() {
    const { token, isAdmin } = useAuth();
    const queryClient = useQueryClient();
    const [editingId, setEditingId] = useState<number | null>(null);
    const [editForm, setEditForm] = useState<any>({});
    const [searchTerm, setSearchTerm] = useState("");

    const { data: practices, isLoading } = useQuery({
        queryKey: ["admin-practices"],
        queryFn: () => fetchAllPractices(),
    });

    const { data: courses } = useQuery({
        queryKey: ["admin-courses-minimal"],
        queryFn: () => fetchCourses(),
    });

    const mutation = useMutation({
        mutationFn: (data: any) => editingId === -1 ? createPractice(token!, data) : updatePractice(token!, editingId!, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-practices"] });
            setEditingId(null);
        }
    });

    if (!isAdmin) return <div className="p-8 text-center font-black text-red-500">Access Denied</div>;

    const filteredPractices = practices?.filter(p =>
        p.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
        p.instructions.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const startEdit = (practice: any) => {
        setEditingId(practice.id);
        setEditForm(practice);
    };

    const startCreate = () => {
        setEditingId(-1);
        setEditForm({
            title: "",
            instructions: "",
            starterCode: "using System;\n\npublic class Program\n{\n    public static void Main()\n    {\n        // Write your solution here\n    }\n}",
            hint: "",
            difficulty: 1,
            lessonId: null
        });
    };

    return (
        <div className="space-y-6 pb-24 md:pb-0">
            <div className="flex flex-wrap items-center justify-between gap-4">
                <div>
                    <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">
                        Skill Reinforcement
                    </p>
                    <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Practice Exercises</h1>
                </div>
                <button onClick={startCreate} className="duo-btn3d duo-btn3d-green">
                    <Plus className="w-4 h-4" />
                    New Practice
                </button>
            </div>

            <div className="relative">
                <Search className="absolute left-4 top-1/2 -translate-y-1/2 w-5 h-5 text-neutral-400" />
                <input
                    placeholder="Search practices..."
                    value={searchTerm}
                    onChange={e => setSearchTerm(e.target.value)}
                    className="w-full pl-12 pr-4 py-4 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
                />
            </div>

            <div className="duo-card !p-0 overflow-hidden">
                <table className="w-full">
                    <thead className="bg-[#fcfcfc] border-b-2 border-neutral-100">
                        <tr>
                            <th className="text-left px-6 py-4 text-xs font-black text-neutral-400 uppercase tracking-wider">Exercise</th>
                            <th className="text-left px-6 py-4 text-xs font-black text-neutral-400 uppercase tracking-wider">Course/Lesson</th>
                            <th className="text-center px-6 py-4 text-xs font-black text-neutral-400 uppercase tracking-wider">Difficulty</th>
                            <th className="text-right px-6 py-4 text-xs font-black text-neutral-400 uppercase tracking-wider">Actions</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y border-neutral-100">
                        {filteredPractices?.map(practice => (
                            <tr key={practice.id} className="hover:bg-neutral-50/50 transition-colors">
                                <td className="px-6 py-4">
                                    <div className="flex items-center gap-3">
                                        <div className="w-8 h-8 rounded-lg bg-[#D7FFB8] flex items-center justify-center text-[#46A302]">
                                            <Code2 className="w-4 h-4" />
                                        </div>
                                        <div>
                                            <p className="font-black text-neutral-900">{practice.title}</p>
                                            <p className="text-xs font-bold text-neutral-500 line-clamp-1 truncate max-w-[300px]">
                                                {practice.instructions}
                                            </p>
                                        </div>
                                    </div>
                                </td>
                                <td className="px-6 py-4">
                                    <div className="flex flex-col">
                                        <span className="text-xs font-black text-[#1CB0F6] uppercase tracking-wide">{practice.courseTitle || "Global"}</span>
                                        <span className="text-xs font-bold text-neutral-400">{practice.lessonTitle || "Practice Pool"}</span>
                                    </div>
                                </td>
                                <td className="px-6 py-4 text-center">
                                    <div className="flex justify-center gap-0.5">
                                        {[1, 2, 3, 4, 5].map(star => (
                                            <Zap
                                                key={star}
                                                className={`w-3 h-3 ${star <= practice.difficulty ? "text-[#FFC800]" : "text-neutral-200"}`}
                                                fill={star <= practice.difficulty ? "currentColor" : "none"}
                                            />
                                        ))}
                                    </div>
                                </td>
                                <td className="px-6 py-4 text-right">
                                    <div className="flex justify-end gap-2">
                                        <button onClick={() => startEdit(practice)} className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors rounded-xl hover:bg-[#DDF4FF]">
                                            <Edit3 className="w-4 h-4" />
                                        </button>
                                        <button className="p-2 text-neutral-400 hover:text-[#FF4B4B] transition-colors rounded-xl hover:bg-[#FFDFE0]">
                                            <Trash2 className="w-4 h-4" />
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {editingId !== null && (
                <div className="fixed inset-0 z-[100] flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
                    <div className="bg-white w-full max-w-4xl rounded-[32px] overflow-hidden shadow-2xl animate-pop">
                        <div className="px-8 py-6 border-b-2 border-neutral-100 flex items-center justify-between">
                            <h2 className="text-xl font-black text-neutral-900">{editingId === -1 ? "Create Practice" : "Edit Practice"}</h2>
                            <button onClick={() => setEditingId(null)} className="p-2 hover:bg-neutral-100 rounded-full transition-colors">
                                <X className="w-6 h-6 text-neutral-400" />
                            </button>
                        </div>
                        <div className="p-8 space-y-6 max-h-[75vh] overflow-y-auto">
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                                <div className="space-y-5">
                                    <div className="space-y-2">
                                        <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1">Title</label>
                                        <input
                                            value={editForm.title}
                                            onChange={e => setEditForm({ ...editForm, title: e.target.value })}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-neutral-800"
                                        />
                                    </div>

                                    <div className="space-y-2">
                                        <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1">Instructions</label>
                                        <textarea
                                            value={editForm.instructions}
                                            onChange={e => setEditForm({ ...editForm, instructions: e.target.value })}
                                            rows={4}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-neutral-800 text-sm"
                                            placeholder="What should the student do?"
                                        />
                                    </div>

                                    <div className="grid grid-cols-2 gap-4">
                                        <div className="space-y-2">
                                            <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1">Difficulty (1-5)</label>
                                            <input
                                                type="number"
                                                min="1"
                                                max="5"
                                                value={editForm.difficulty}
                                                onChange={e => setEditForm({ ...editForm, difficulty: parseInt(e.target.value) })}
                                                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-neutral-800"
                                            />
                                        </div>
                                        <div className="space-y-2">
                                            <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1">Lesson ID (optional)</label>
                                            <input
                                                type="number"
                                                value={editForm.lessonId || ""}
                                                onChange={e => setEditForm({ ...editForm, lessonId: e.target.value ? parseInt(e.target.value) : null })}
                                                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-neutral-800"
                                            />
                                        </div>
                                    </div>

                                    <div className="space-y-2">
                                        <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1">Hint</label>
                                        <input
                                            value={editForm.hint}
                                            onChange={e => setEditForm({ ...editForm, hint: e.target.value })}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-neutral-800 text-sm"
                                        />
                                    </div>
                                </div>

                                <div className="space-y-5">
                                    <div className="space-y-2">
                                        <label className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.15em] pl-1 flex items-center gap-2">
                                            <Code2 className="w-3 h-3" /> Starter Code
                                        </label>
                                        <textarea
                                            value={editForm.starterCode}
                                            onChange={e => setEditForm({ ...editForm, starterCode: e.target.value })}
                                            rows={14}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-900 text-[#7FE787] border-2 border-transparent focus:border-[#58CC02] transition-all font-mono text-[13px] leading-relaxed"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="px-8 py-6 bg-neutral-50 flex gap-4">
                            <button
                                onClick={() => mutation.mutate(editForm)}
                                className="flex-1 duo-btn3d duo-btn3d-green"
                            >
                                <Save className="w-4 h-4" />
                                {editingId === -1 ? "Create Practice" : "Save Changes"}
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
