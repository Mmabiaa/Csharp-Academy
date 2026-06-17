interface VideoPlayerProps {
  embedUrl: string;
  title: string;
}

export default function VideoPlayer({ embedUrl, title }: VideoPlayerProps) {
  return (
    <div className="relative w-full rounded-xl overflow-hidden bg-black aspect-video shadow-lg ring-1 ring-slate-700">
      <iframe
        src={embedUrl}
        title={title}
        className="absolute inset-0 w-full h-full"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
        allowFullScreen
      />
    </div>
  );
}
