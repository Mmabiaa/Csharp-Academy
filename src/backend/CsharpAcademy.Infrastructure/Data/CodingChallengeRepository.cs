using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class CodingChallengeRepository : ICodingChallengeRepository
{
    private readonly ApplicationDbContext _context;

    public CodingChallengeRepository(ApplicationDbContext context) => _context = context;

    public async Task<CodingChallenge?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.CodingChallenges.FirstOrDefaultAsync(c => c.Id == id && c.IsPublished, cancellationToken);

    public async Task<List<CodingChallenge>> GetAllPublishedAsync(CancellationToken cancellationToken = default) =>
        await _context.CodingChallenges
            .Where(c => c.IsPublished)
            .OrderBy(c => c.Order)
            .ToListAsync(cancellationToken);

    public async Task<bool> HasUserCompletedAsync(int userId, int challengeId, CancellationToken cancellationToken = default) =>
        await _context.ChallengeCompletions
            .AnyAsync(c => c.UserId == userId && c.ChallengeId == challengeId, cancellationToken);

    public async Task RecordCompletionAsync(int userId, int challengeId, CancellationToken cancellationToken = default)
    {
        _context.ChallengeCompletions.Add(new ChallengeCompletion
        {
            UserId = userId,
            ChallengeId = challengeId,
            CompletedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> GetCompletedIdsByUserAsync(int userId, CancellationToken cancellationToken = default) =>
        await _context.ChallengeCompletions
            .Where(c => c.UserId == userId)
            .Select(c => c.ChallengeId)
            .ToListAsync(cancellationToken);
}
