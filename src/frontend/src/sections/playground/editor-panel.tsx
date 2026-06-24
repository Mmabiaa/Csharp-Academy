import { Play } from "lucide-react";
import CodeEditor from "../../components/CodeEditor";
import ConsolePanel from "../../components/ConsolePanel";

interface EditorPanelProps {
  code: string;
  runTrigger: number;
  onCodeChange: (code: string) => void;
  onRun: () => void;
}

export default function EditorPanel({ code, runTrigger, onCodeChange, onRun }: EditorPanelProps) {
  return (
    <div className="duo-card space-y-4">
      <CodeEditor value={code} onChange={onCodeChange} rows={16} />
      <button onClick={onRun} className="duo-btn3d duo-btn3d-green" id="playground-run-btn">
        <Play className="w-4 h-4" />
        Run code
      </button>
      <ConsolePanel code={code} runTrigger={runTrigger} />
    </div>
  );
}
