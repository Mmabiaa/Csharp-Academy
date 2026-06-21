using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ICodingExerciseRepository
{
    Task<CodingExercise?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<CodingExercise>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<List<CodingExercise>> GetAllWithLessonAsync(CancellationToken cancellationToken = default);
    Task<CodingExercise> CreateAsync(CodingExercise exercise, CancellationToken cancellationToken = default);
    Task UpdateAsync(CodingExercise exercise, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
