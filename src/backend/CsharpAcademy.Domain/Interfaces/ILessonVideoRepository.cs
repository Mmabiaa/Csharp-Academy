using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ILessonVideoRepository
{
    Task<List<LessonVideo>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<LessonVideo> CreateAsync(LessonVideo video, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
