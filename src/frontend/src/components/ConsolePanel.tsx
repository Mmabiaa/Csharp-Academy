import { useState, useEffect, useRef, useCallback } from "react";
import { runCode } from "../lib/api";
import { Play, AlertTriangle, Terminal, ChevronRight } from "lucide-react";

// ─── helpers ────────────────────────────────────────────────────────────────

/**
 * Count how many Console.ReadLine() / Console.Read() calls are present
 * so we know how many input prompts to collect.
 */
function countReadLineCalls(code: string): number {
    const matches = code.match(/Console\.(ReadLine|ReadKey|Read)\s*\(/gi);
    return matches ? matches.length : 0;
}

/**
 * Extract the text of each Console.Write / Console.WriteLine that
 * immediately precedes a ReadLine call — used as human-readable prompts.
 * Falls back to "Input N:" if no prompt string is found.
 */
function extractPrompts(code: string): string[] {
    const prompts: string[] = [];
    // Match Console.Write("...") or Console.WriteLine("...") before a ReadLine
    const promptPattern =
        /Console\.Write(?:Line)?\s*\(\s*(?:\$?"([^"\\]*(?:\\.[^"\\]*)*)"|'([^']*)')\s*\)\s*;?\s*(?:\/\/[^\n]*)?\n[\s\S]*?Console\.(ReadLine|ReadKey|Read)\s*\(/gi;

    let match: RegExpExecArray | null;
    while ((match = promptPattern.exec(code)) !== null) {
        prompts.push((match[1] || match[2] || "").trim());
    }
    return prompts;
}

// ─── types ──────────────────────────────────────────────────────────────────

type ConsoleLine =
    | { kind: "output"; text: string }
    | { kind: "prompt"; text: string; inputIndex: number }
    | { kind: "userInput"; text: string }
    | { kind: "error"; text: string }
    | { kind: "info"; text: string };

interface ConsolePanelProps {
    code: string;
    /** Optional external trigger — increment to fire a run */
    runTrigger?: number;
    /** Called when execution completes */
    onResult?: (output: string, error: string | null) => void;
}

// ─── component ──────────────────────────────────────────────────────────────

export default function ConsolePanel({ code, runTrigger, onResult }: ConsolePanelProps) {
    const [lines, setLines] = useState<ConsoleLine[]>([]);
    const [phase, setPhase] = useState<"idle" | "collecting" | "running" | "done">("idle");
    const [inputValues, setInputValues] = useState<string[]>([]);
    const [currentInput, setCurrentInput] = useState("");
    const [currentPromptIdx, setCurrentPromptIdx] = useState(0);
    const [prompts, setPrompts] = useState<string[]>([]);
    const [totalReads, setTotalReads] = useState(0);
    const [loading, setLoading] = useState(false);

    const inputRef = useRef<HTMLInputElement>(null);
    const bottomRef = useRef<HTMLDivElement>(null);

    // Auto-scroll to bottom
    useEffect(() => {
        bottomRef.current?.scrollIntoView({ behavior: "smooth" });
    }, [lines, phase]);

    // Focus input when collecting
    useEffect(() => {
        if (phase === "collecting") {
            setTimeout(() => inputRef.current?.focus(), 50);
        }
    }, [phase, currentPromptIdx]);

    const appendLine = useCallback((line: ConsoleLine) => {
        setLines((prev) => [...prev, line]);
    }, []);

    const startRun = useCallback(async () => {
        const reads = countReadLineCalls(code);
        setTotalReads(reads);

        if (reads === 0) {
            // No input needed — run directly
            setPhase("running");
            setLoading(true);
            setLines([{ kind: "info", text: "Running…" }]);
            try {
                const result = await runCode(code);
                setLines([]);
                if (result.success) {
                    const outputLines = result.output.split("\n");
                    outputLines.forEach((l) => appendLine({ kind: "output", text: l }));
                    onResult?.(result.output, null);
                } else {
                    appendLine({ kind: "error", text: result.error ?? "Execution failed" });
                    onResult?.("", result.error ?? "Execution failed");
                }
            } catch (err) {
                const msg = err instanceof Error ? err.message : "Failed to run code";
                setLines([{ kind: "error", text: msg }]);
                onResult?.("", msg);
            } finally {
                setLoading(false);
                setPhase("done");
            }
            return;
        }

        // Need inputs — enter collecting phase
        const extractedPrompts = extractPrompts(code);
        const generatedPrompts: string[] = [];
        for (let i = 0; i < reads; i++) {
            generatedPrompts.push(extractedPrompts[i] || `Input ${i + 1}:`);
        }

        setLines([]);
        setPrompts(generatedPrompts);
        setInputValues([]);
        setCurrentPromptIdx(0);
        setCurrentInput("");
        setPhase("collecting");

        // Show the first prompt
        appendLine({ kind: "prompt", text: generatedPrompts[0], inputIndex: 0 });
    }, [code, appendLine, onResult]);

    // External trigger
    useEffect(() => {
        if (runTrigger !== undefined && runTrigger > 0) {
            startRun();
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [runTrigger]);

    const submitInput = useCallback(async () => {
        const value = currentInput;
        const newInputValues = [...inputValues, value];

        // Replace the pending prompt line with one that has the user's answer shown inline
        setLines((prev) => {
            const updated = [...prev];
            // Find last prompt line index manually (ES2020 compat)
            let idx = -1;
            for (let i = updated.length - 1; i >= 0; i--) {
                if (updated[i].kind === "prompt") { idx = i; break; }
            }
            if (idx !== -1) {
                const existing = updated[idx] as { kind: "prompt"; text: string; inputIndex: number };
                updated[idx] = { kind: "prompt", text: existing.text, inputIndex: currentPromptIdx };
            }
            updated.push({ kind: "userInput", text: value });
            return updated;
        });

        setCurrentInput("");

        if (newInputValues.length < totalReads) {
            // More inputs needed
            setInputValues(newInputValues);
            const nextIdx = currentPromptIdx + 1;
            setCurrentPromptIdx(nextIdx);
            appendLine({ kind: "prompt", text: prompts[nextIdx] || `Input ${nextIdx + 1}:`, inputIndex: nextIdx });
        } else {
            // All inputs collected — execute
            setInputValues(newInputValues);
            setPhase("running");
            setLoading(true);
            appendLine({ kind: "info", text: "Running…" });

            try {
                const result = await runCode(code, newInputValues);
                // Remove the "Running…" info line
                setLines((prev) => {
                    const cleaned = prev.filter((l) => l.kind !== "info");
                    const outputLines = result.success
                        ? result.output.split("\n").map((t): ConsoleLine => ({ kind: "output", text: t }))
                        : [{ kind: "error" as const, text: result.error ?? "Execution failed" }];
                    return [...cleaned, ...outputLines];
                });
                onResult?.(result.success ? result.output : "", result.success ? null : result.error ?? "Error");
            } catch (err) {
                const msg = err instanceof Error ? err.message : "Failed to run code";
                setLines((prev) => [...prev.filter((l) => l.kind !== "info"), { kind: "error", text: msg }]);
                onResult?.("", msg);
            } finally {
                setLoading(false);
                setPhase("done");
            }
        }
    }, [currentInput, inputValues, totalReads, currentPromptIdx, prompts, code, appendLine, onResult]);

    const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
            e.preventDefault();
            submitInput();
        }
    };

    const reset = () => {
        setLines([]);
        setPhase("idle");
        setInputValues([]);
        setCurrentInput("");
        setCurrentPromptIdx(0);
        setLoading(false);
    };

    const hasReadLine = countReadLineCalls(code) > 0;

    return (
        <div className="rounded-2xl overflow-hidden border-2 border-neutral-800 shadow-[0_4px_0_#00000020]">
            {/* Terminal chrome bar */}
            <div className="bg-neutral-800 px-4 py-2.5 flex items-center justify-between">
                <div className="flex items-center gap-2">
                    <span className="w-3 h-3 rounded-full bg-[#FF4B4B]" />
                    <span className="w-3 h-3 rounded-full bg-[#FFC800]" />
                    <span className="w-3 h-3 rounded-full bg-[#58CC02]" />
                </div>
                <div className="flex items-center gap-2">
                    {hasReadLine && (
                        <span className="text-[10px] font-black uppercase tracking-widest text-[#58CC02] bg-[#58CC02]/10 px-2 py-0.5 rounded-lg border border-[#58CC02]/30">
                            Interactive
                        </span>
                    )}
                    <div className="flex items-center gap-1.5 text-neutral-400 text-xs font-black uppercase tracking-wide">
                        <Terminal className="w-3.5 h-3.5" />
                        Console
                    </div>
                </div>
            </div>

            {/* Terminal body */}
            <div className="bg-[#0D1117] min-h-[120px] max-h-[320px] overflow-y-auto p-4 font-mono text-sm">
                {lines.length === 0 && phase === "idle" && (
                    <p className="text-neutral-600 text-xs select-none">
                        {hasReadLine
                            ? "▶  Click \"Run code\" — the console will ask for your inputs interactively."
                            : "▶  Click \"Run code\" to see output here."}
                    </p>
                )}

                {lines.map((line, i) => {
                    if (line.kind === "output") {
                        return (
                            <div key={i} className="text-[#7FE787] leading-relaxed whitespace-pre-wrap">
                                {line.text}
                            </div>
                        );
                    }
                    if (line.kind === "error") {
                        return (
                            <div key={i} className="flex items-start gap-2 text-[#FF6B6B] leading-relaxed">
                                <AlertTriangle className="w-3.5 h-3.5 mt-0.5 shrink-0" />
                                <span className="whitespace-pre-wrap">{line.text}</span>
                            </div>
                        );
                    }
                    if (line.kind === "info") {
                        return (
                            <div key={i} className="text-neutral-500 italic text-xs">
                                {line.text}
                            </div>
                        );
                    }
                    if (line.kind === "prompt") {
                        const isActive =
                            phase === "collecting" && (line as any).inputIndex === currentPromptIdx;
                        return (
                            <div key={i} className="flex items-center gap-1 text-[#FFC800] leading-relaxed">
                                <ChevronRight className="w-3.5 h-3.5 shrink-0" />
                                <span>{(line as any).text}</span>
                                {isActive && <span className="ml-1 animate-pulse text-neutral-400">_</span>}
                            </div>
                        );
                    }
                    if (line.kind === "userInput") {
                        return (
                            <div key={i} className="text-[#CE82FF] leading-relaxed pl-5">
                                {line.text}
                            </div>
                        );
                    }
                    return null;
                })}

                {/* Active input row */}
                {phase === "collecting" && (
                    <div className="flex items-center gap-1 mt-0.5">
                        <ChevronRight className="w-3.5 h-3.5 text-[#FFC800] shrink-0" />
                        <span className="text-[#FFC800]">{prompts[currentPromptIdx]}</span>
                        <input
                            ref={inputRef}
                            type="text"
                            value={currentInput}
                            onChange={(e) => setCurrentInput(e.target.value)}
                            onKeyDown={handleKeyDown}
                            className="flex-1 bg-transparent text-[#CE82FF] caret-[#CE82FF] outline-none ml-2 min-w-0"
                            autoComplete="off"
                            spellCheck={false}
                            placeholder=""
                            aria-label={`Console input ${currentPromptIdx + 1}`}
                        />
                    </div>
                )}

                <div ref={bottomRef} />
            </div>

            {/* Action bar */}
            <div className="bg-neutral-900 border-t border-neutral-800 px-4 py-2.5 flex items-center justify-between gap-2">
                <div className="text-xs text-neutral-600 font-bold">
                    {phase === "collecting" && (
                        <span className="text-[#FFC800]">
                            Input {currentPromptIdx + 1} of {totalReads} — press <kbd className="bg-neutral-800 px-1 rounded text-neutral-400">Enter</kbd> to confirm
                        </span>
                    )}
                    {phase === "running" && <span className="text-neutral-500">Executing…</span>}
                    {phase === "done" && <span className="text-[#58CC02]">Done</span>}
                </div>
                <div className="flex gap-2">
                    {phase !== "idle" && (
                        <button
                            onClick={reset}
                            className="text-xs font-black text-neutral-500 hover:text-neutral-300 transition-colors px-2 py-1"
                        >
                            Clear
                        </button>
                    )}
                    <button
                        onClick={startRun}
                        disabled={loading || phase === "collecting"}
                        className="duo-btn3d duo-btn3d-green !px-4 !py-2 !text-xs disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        <Play className="w-3.5 h-3.5" />
                        {loading ? "Running…" : "Run code"}
                    </button>
                </div>
            </div>
        </div>
    );
}
