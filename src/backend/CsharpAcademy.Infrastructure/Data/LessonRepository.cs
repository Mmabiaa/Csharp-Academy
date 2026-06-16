using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lesson?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Lessons
            .Include(l => l.CourseModule)
                .ThenInclude(m => m.Course)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<int> CountByCourseIdAsync(int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.Lessons
            .Where(l => l.CourseModule.CourseId == courseId)
            .CountAsync(cancellationToken);
    }
}
