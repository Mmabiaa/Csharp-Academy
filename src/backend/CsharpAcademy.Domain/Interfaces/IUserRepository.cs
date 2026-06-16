using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task SaveAsync(User user, CancellationToken cancellationToken = default);
    Task<List<Badge>> GetUserBadgesAsync(int userId, CancellationToken cancellationToken = default);
    Task AwardBadgeAsync(int userId, int badgeId, CancellationToken cancellationToken = default);
    Task<bool> HasBadgeAsync(int userId, int badgeId, CancellationToken cancellationToken = default);
}
