using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ILessonRepository
{
    Task<Lesson?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Lesson> CreateAsync(Lesson lesson, CancellationToken cancellationToken = default);
    Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CountByCourseIdAsync(int courseId, CancellationToken cancellationToken = default);
}

public interface IModuleRepository 
{
    Task<CourseModule?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CourseModule> CreateAsync(CourseModule module, CancellationToken cancellationToken = default);
    Task UpdateAsync(CourseModule module, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
