import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchChallenges, updateChallenge, createChallenge, deleteChallenge } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
    Trophy,
    Plus,
    Trash2,
    Edit3,
    Save,
    X,
    Sparkles,
    Search,
    Code2,
    Lightbulb,
    Tags
} from "lucide-react";

export default function AdminChallenges() {
    const { token, isAdmin } = useAuth();
    const queryClient = useQueryClient();
    const [editingId, setEditingId] = useState<number | null>(null);
    const [editForm, setEditForm] = useState<any>({});
    const [searchTerm, setSearchTerm] = useState("");

    const { data: challenges, isLoading } = useQuery({
        queryKey: ["admin-challenges"],
        queryFn: () => fetchChallenges(),
    });

    const mutation = useMutation({
        mutationFn: (data: any) => editingId === -1 ? createChallenge(token!, data) : updateChallenge(token!, editingId!, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["admin-challenges"] });
            setEditingId(null);
        }
    });

    if (!isAdmin) return <div className="p-8 text-center font-black text-red-500">Access Denied</div>;

    const filteredChallenges = challenges?.filter(c =>
        c.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
        c.tags.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const startEdit = (challenge: any) => {
        setEditingId(challenge.id);
        setEditForm(challenge);
    };

    const startCreate = () => {
        setEditingId(-1);
        setEditForm({
            title: "",
            description: "",
            difficulty: "Medium",
            starterCode: "using System;\n\npublic class Program\n{\n    public static void Main()\n    {\n        // Your code here\n    }\n}",
            hint: "",
            tags: "",
            xpReward: 50
        });
    };

    return (
        <div className="space-y-6 pb-24 md:pb-0">
            <div className="flex flex-wrap items-center justify-between gap-4">
                <div>
                    <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">
                        Challenge Repository
                    </p>
                    <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Challenges</h1>
                </div>
                <button onClick={startCreate} className="duo-btn3d duo-btn3d-green">
                    <Plus className="w-4 h-4" />
                    Create Challenge
                </button>
            </div>

            <div className="relative">
                <Search className="absolute left-4 top-1/2 -translate-y-1/2 w-5 h-5 text-neutral-400" />
                <input
                    placeholder="Search challenges or tags..."
                    value={searchTerm}
                    onChange={e => setSearchTerm(e.target.value)}
                    className="w-full pl-12 pr-4 py-4 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
                />
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {filteredChallenges?.map(challenge => (
                    <div key={challenge.id} className="duo-card duo-card-hover">
                        <div className="flex justify-between items-start mb-4">
                            <div className="w-12 h-12 rounded-2xl bg-[#EEEDFE] flex items-center justify-center shadow-[0_3px_0_#534AB7]">
                                <Trophy className="w-6 h-6 text-[#534AB7]" />
                            </div>
                            <div className="flex gap-1">
                                <button onClick={() => startEdit(challenge)} className="p-2 text-neutral-400 hover:text-[#1CB0F6] transition-colors">
                                    <Edit3 className="w-4 h-4" />
                                </button>
                                <button className="p-2 text-neutral-400 hover:text-[#FF4B4B] transition-colors">
                                    <Trash2 className="w-4 h-4" />
                                </button>
                            </div>
                        </div>
                        <h3 className="text-lg font-black text-neutral-900 mb-2">{challenge.title}</h3>
                        <p className="text-sm font-bold text-neutral-500 mb-4 line-clamp-2">{challenge.description}</p>
                        <div className="flex flex-wrap gap-2">
                            <span className={`px-3 py-1 rounded-full text-[10px] font-black uppercase tracking-wider ${challenge.difficulty === "Easy" ? "bg-[#D7FFB8] text-[#46A302]" :
                                    challenge.difficulty === "Medium" ? "bg-[#FFF8E1] text-[#946800]" :
                                        "bg-[#FFDFE0] text-[#CC3A3A]"
                                }`}>
                                {challenge.difficulty}
                            </span>
                            <span className="px-3 py-1 bg-neutral-100 text-neutral-500 rounded-full text-[10px] font-black uppercase tracking-wider">
                                {challenge.xpReward} XP
                            </span>
                        </div>
                    </div>
                ))}
            </div>

            {editingId !== null && (
                <div className="fixed inset-0 z-[100] flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
                    <div className="bg-white w-full max-w-4xl rounded-[32px] overflow-hidden shadow-2xl animate-pop">
                        <div className="px-8 py-6 border-b-2 border-neutral-100 flex items-center justify-between">
                            <h2 className="text-xl font-black text-neutral-900">{editingId === -1 ? "Create Challenge" : "Edit Challenge"}</h2>
                            <button onClick={() => setEditingId(null)} className="p-2 hover:bg-neutral-100 rounded-full transition-colors">
                                <X className="w-6 h-6 text-neutral-400" />
                            </button>
                        </div>
                        <div className="p-8 space-y-6 max-h-[75vh] overflow-y-auto">
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                <div className="space-y-4">
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Title</label>
                                        <input
                                            value={editForm.title}
                                            onChange={e => setEditForm({ ...editForm, title: e.target.value })}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Description</label>
                                        <textarea
                                            value={editForm.description}
                                            onChange={e => setEditForm({ ...editForm, description: e.target.value })}
                                            rows={3}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                        />
                                    </div>
                                    <div className="grid grid-cols-2 gap-4">
                                        <div className="space-y-2">
                                            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Difficulty</label>
                                            <select
                                                value={editForm.difficulty}
                                                onChange={e => setEditForm({ ...editForm, difficulty: e.target.value })}
                                                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                            >
                                                <option>Easy</option>
                                                <option>Medium</option>
                                                <option>Hard</option>
                                            </select>
                                        </div>
                                        <div className="space-y-2">
                                            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">XP Reward</label>
                                            <input
                                                type="number"
                                                value={editForm.xpReward}
                                                onChange={e => setEditForm({ ...editForm, xpReward: parseInt(e.target.value) })}
                                                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                            />
                                        </div>
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Tags (Comma separated)</label>
                                        <div className="relative">
                                            <Tags className="absolute left-4 top-1/2 -translate-y-1/2 w-4 h-4 text-neutral-400" />
                                            <input
                                                value={editForm.tags}
                                                onChange={e => setEditForm({ ...editForm, tags: e.target.value })}
                                                className="w-full pl-12 pr-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="space-y-4">
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1 flex items-center gap-2">
                                            <Code2 className="w-3 h-3" /> Starter Code
                                        </label>
                                        <textarea
                                            value={editForm.starterCode}
                                            onChange={e => setEditForm({ ...editForm, starterCode: e.target.value })}
                                            rows={8}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-900 text-[#7FE787] border-2 border-transparent focus:border-[#58CC02] transition-all font-mono text-[13px] leading-relaxed"
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1 flex items-center gap-2">
                                            <Lightbulb className="w-3 h-3" /> Hint
                                        </label>
                                        <textarea
                                            value={editForm.hint}
                                            onChange={e => setEditForm({ ...editForm, hint: e.target.value })}
                                            rows={3}
                                            className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
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
                                {editingId === -1 ? "Create Challenge" : "Save Changes"}
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
