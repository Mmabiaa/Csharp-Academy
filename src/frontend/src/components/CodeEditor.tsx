interface CodeEditorProps {
  value: string;
  onChange: (value: string) => void;
  rows?: number;
  readOnly?: boolean;
}

export default function CodeEditor({ value, onChange, rows = 10, readOnly = false }: CodeEditorProps) {
  return (
    <textarea
      value={value}
      onChange={(e) => onChange(e.target.value)}
      readOnly={readOnly}
      rows={rows}
      className="w-full font-mono text-sm bg-gray-900 text-green-400 p-4 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
      spellCheck={false}
    />
  );
}
