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

    public async Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<Quiz> CreateQuizWithQuestionsAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(q => q.LessonId == quiz.LessonId, cancellationToken);

        if (existing is not null)
        {
            _context.QuestionOptions.RemoveRange(existing.Questions.SelectMany(q => q.Options));
            _context.Questions.RemoveRange(existing.Questions);
            existing.Title = quiz.Title;
            existing.Questions = quiz.Questions;
            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync(cancellationToken);
        return quiz;
    }

    public async Task<QuizAttempt> SaveAttemptAsync(QuizAttempt attempt, CancellationToken cancellationToken = default)
    {
        _context.QuizAttempts.Add(attempt);
        await _context.SaveChangesAsync(cancellationToken);
        return attempt;
    }
}
