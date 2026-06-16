using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default);
    Task<Enrollment> CreateAsync(Enrollment enrollment, CancellationToken cancellationToken = default);
    Task<List<Enrollment>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken = default);
}
