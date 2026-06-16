using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class CodingExerciseRepository : ICodingExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public CodingExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CodingExercise?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CodingExercises
            .Include(e => e.Lesson)
                .ThenInclude(l => l.CourseModule)
                    .ThenInclude(m => m.Course)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<CodingExercise>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        return await _context.CodingExercises
            .Where(e => e.LessonId == lessonId)
            .OrderBy(e => e.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<CodingExercise>> GetAllWithLessonAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CodingExercises
            .Include(e => e.Lesson)
                .ThenInclude(l => l.CourseModule)
                    .ThenInclude(m => m.Course)
            .OrderBy(e => e.LessonId)
            .ThenBy(e => e.Order)
            .ToListAsync(cancellationToken);
    }
}
