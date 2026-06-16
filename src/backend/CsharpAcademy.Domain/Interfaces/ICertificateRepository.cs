using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ICertificateRepository
{
    Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default);
    Task<Certificate?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Certificate>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<Certificate> CreateAsync(Certificate certificate, CancellationToken cancellationToken = default);
}
