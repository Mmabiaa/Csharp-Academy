import { Code2 } from "lucide-react";

interface CodeEditorProps {
  value: string;
  onChange: (value: string) => void;
  rows?: number;
  readOnly?: boolean;
}

export default function CodeEditor({
  value,
  onChange,
  rows = 10,
  readOnly = false,
}: CodeEditorProps) {
  return (
    <div className="rounded-2xl overflow-hidden border-2 border-neutral-800 shadow-[0_4px_0_#00000020]">
      {/* Editor chrome bar */}
      <div className="bg-neutral-800 px-4 py-2.5 flex items-center justify-between">
        <div className="flex items-center gap-2">
          <span className="w-3 h-3 rounded-full bg-[#FF4B4B]" />
          <span className="w-3 h-3 rounded-full bg-[#FFC800]" />
          <span className="w-3 h-3 rounded-full bg-[#58CC02]" />
        </div>
        <div className="flex items-center gap-1.5 text-neutral-400 text-xs font-black uppercase tracking-wide">
          <Code2 className="w-3.5 h-3.5" />
          C#
        </div>
      </div>
      <textarea
        value={value}
        onChange={(e) => onChange(e.target.value)}
        readOnly={readOnly}
        rows={rows}
        className="w-full font-mono text-sm bg-neutral-900 text-[#7FE787] p-4 focus:outline-none resize-none block"
        spellCheck={false}
      />
    </div>
  );
}