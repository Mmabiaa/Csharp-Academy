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

    public async Task<CodingExercise> CreateAsync(CodingExercise exercise, CancellationToken cancellationToken = default)
    {
        _context.CodingExercises.Add(exercise);
        await _context.SaveChangesAsync(cancellationToken);
        return exercise;
    }

    public async Task UpdateAsync(CodingExercise exercise, CancellationToken cancellationToken = default)
    {
        _context.CodingExercises.Update(exercise);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var e = await _context.CodingExercises.FindAsync(new object[] { id }, cancellationToken);
        if (e != null)
        {
            _context.CodingExercises.Remove(e);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
