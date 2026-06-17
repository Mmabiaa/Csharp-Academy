using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface ICodingChallengeRepository
{
    Task<CodingChallenge?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<CodingChallenge>> GetAllPublishedAsync(CancellationToken cancellationToken = default);
    Task<bool> HasUserCompletedAsync(int userId, int challengeId, CancellationToken cancellationToken = default);
    Task RecordCompletionAsync(int userId, int challengeId, CancellationToken cancellationToken = default);
}
