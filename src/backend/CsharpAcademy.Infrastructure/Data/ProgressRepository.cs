using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class ProgressRepository : IProgressRepository
{
    private readonly ApplicationDbContext _context;

    public ProgressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Progress?> GetByUserAndLessonAsync(int userId, int lessonId, CancellationToken cancellationToken = default)
    {
        return await _context.ProgressRecords
            .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lessonId, cancellationToken);
    }

    public async Task<List<Progress>> GetCompletedByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.ProgressRecords
            .Include(p => p.Lesson)
                .ThenInclude(l => l.CourseModule)
            .Where(p => p.UserId == userId
                && p.IsCompleted
                && p.Lesson.CourseModule.CourseId == courseId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Progress> MarkCompleteAsync(int userId, int lessonId, CancellationToken cancellationToken = default)
    {
        var existing = await GetByUserAndLessonAsync(userId, lessonId, cancellationToken);
        if (existing is not null)
        {
            if (!existing.IsCompleted)
            {
                existing.IsCompleted = true;
                existing.CompletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return existing;
        }

        var progress = new Progress
        {
            UserId = userId,
            LessonId = lessonId,
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _context.ProgressRecords.Add(progress);
        await _context.SaveChangesAsync(cancellationToken);
        return progress;
    }

    public async Task<List<int>> GetCompletedLessonIdsAsync(int userId, CancellationToken cancellationToken = default) =>
        await _context.ProgressRecords
            .Where(p => p.UserId == userId && p.IsCompleted)
            .Select(p => p.LessonId)
            .ToListAsync(cancellationToken);

    public async Task<List<int>> GetCompletedPracticeIdsAsync(int userId, CancellationToken cancellationToken = default) =>
        await _context.PracticeCompletions
            .Where(p => p.UserId == userId)
            .Select(p => p.ExerciseId)
            .ToListAsync(cancellationToken);
}
