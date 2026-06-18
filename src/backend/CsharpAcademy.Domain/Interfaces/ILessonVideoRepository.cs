using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ILessonVideoRepository
{
    Task<List<LessonVideo>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default);
}
