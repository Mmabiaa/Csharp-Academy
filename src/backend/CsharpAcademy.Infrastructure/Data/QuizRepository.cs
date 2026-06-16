using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationDbContext _context;

    public QuizRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Quiz?> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(q => q.LessonId == lessonId, cancellationToken);
    }

    public async Task<QuizAttempt> SaveAttemptAsync(QuizAttempt attempt, CancellationToken cancellationToken = default)
    {
        _context.QuizAttempts.Add(attempt);
        await _context.SaveChangesAsync(cancellationToken);
        return attempt;
    }
}
