using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IProgressRepository
{
    Task<Progress?> GetByUserAndLessonAsync(int userId, int lessonId, CancellationToken cancellationToken = default);
    Task<List<Progress>> GetCompletedByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default);
    Task<Progress> MarkCompleteAsync(int userId, int lessonId, CancellationToken cancellationToken = default);
}
