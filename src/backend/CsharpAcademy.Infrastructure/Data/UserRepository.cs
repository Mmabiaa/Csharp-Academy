using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FindAsync([id], cancellationToken);
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Badge>> GetUserBadgesAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserBadges
            .Where(ub => ub.UserId == userId)
            .Select(ub => ub.Badge)
            .ToListAsync(cancellationToken);
    }

    public async Task AwardBadgeAsync(int userId, int badgeId, CancellationToken cancellationToken = default)
    {
        if (await HasBadgeAsync(userId, badgeId, cancellationToken))
        {
            return;
        }

        _context.UserBadges.Add(new UserBadge
        {
            UserId = userId,
            BadgeId = badgeId,
            EarnedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> HasBadgeAsync(int userId, int badgeId, CancellationToken cancellationToken = default)
    {
        return await _context.UserBadges
            .AnyAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId, cancellationToken);
    }

    public async Task<List<User>> GetTopByXpAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .OrderByDescending(u => u.Xp)
            .ThenBy(u => u.FirstName)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.OrderBy(u => u.Email).ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.Users.CountAsync(cancellationToken);

    public async Task<bool> HasCompletedPracticeAsync(int userId, int exerciseId, CancellationToken cancellationToken = default)
    {
        return await _context.PracticeCompletions
            .AnyAsync(p => p.UserId == userId && p.ExerciseId == exerciseId, cancellationToken);
    }

    public async Task RecordPracticeCompletionAsync(int userId, int exerciseId, CancellationToken cancellationToken = default)
    {
        _context.PracticeCompletions.Add(new PracticeCompletion
        {
            UserId = userId,
            ExerciseId = exerciseId,
            CompletedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SavePasswordResetTokenAsync(PasswordResetToken token, CancellationToken cancellationToken = default)
    {
        _context.PasswordResetTokens.Add(token);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PasswordResetToken?> GetValidPasswordResetTokenAsync(int userId, string otp, CancellationToken cancellationToken = default)
    {
        return await _context.PasswordResetTokens
            .Where(t => t.UserId == userId && t.Token == otp && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdatePasswordResetTokenAsync(PasswordResetToken token, CancellationToken cancellationToken = default)
    {
        _context.PasswordResetTokens.Update(token);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
