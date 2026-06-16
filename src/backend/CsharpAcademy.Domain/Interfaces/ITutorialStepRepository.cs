using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ITutorialStepRepository
{
    Task<List<TutorialStep>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default);
}
