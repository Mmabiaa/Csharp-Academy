using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ILessonRepository
{
    Task<Lesson?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CountByCourseIdAsync(int courseId, CancellationToken cancellationToken = default);
}
