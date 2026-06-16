using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IQuizRepository
{
    Task<Quiz?> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<QuizAttempt> SaveAttemptAsync(QuizAttempt attempt, CancellationToken cancellationToken = default);
}
