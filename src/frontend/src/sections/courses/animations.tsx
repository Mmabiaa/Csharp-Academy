export default function CoursesAnimations() {
  return (
    <style>{`
        @keyframes duo-icon-bounce {
          0%, 100% { transform: translateY(0) scale(1); }
          40% { transform: translateY(-5px) scale(1.08); }
          70% { transform: translateY(-2px) scale(1.04); }
        }
        @keyframes duo-icon-spin {
          0% { transform: rotate(0deg) scale(1); }
          50% { transform: rotate(12deg) scale(1.12); }
          100% { transform: rotate(0deg) scale(1); }
        }
        .duo-course-icon { animation: duo-icon-bounce 2.6s ease-in-out infinite; }
        .duo-course-icon-zap { animation: duo-icon-spin 2.2s ease-in-out infinite; }
        .duo-course-card:nth-child(2n) .duo-course-icon,
        .duo-course-card:nth-child(2n) .duo-course-icon-zap { animation-delay: 0.3s; }
        .duo-course-card:nth-child(3n) .duo-course-icon,
        .duo-course-card:nth-child(3n) .duo-course-icon-zap { animation-delay: 0.6s; }
        .duo-course-card:nth-child(4n) .duo-course-icon,
        .duo-course-card:nth-child(4n) .duo-course-icon-zap { animation-delay: 0.9s; }
        @media (prefers-reduced-motion: reduce) {
          .duo-course-icon, .duo-course-icon-zap { animation: none; }
        }
      `}</style>
  );
}
