using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class LessonVideoRepository : ILessonVideoRepository
{
    private readonly ApplicationDbContext _context;

    public LessonVideoRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<LessonVideo>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default) =>
        await _context.LessonVideos
            .Where(v => v.LessonId == lessonId)
            .OrderBy(v => v.Order)
            .ToListAsync(cancellationToken);

    public async Task<LessonVideo> CreateAsync(LessonVideo video, CancellationToken cancellationToken = default)
    {
        _context.LessonVideos.Add(video);
        await _context.SaveChangesAsync(cancellationToken);
        return video;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var v = await _context.LessonVideos.FindAsync(new object[] { id }, cancellationToken);
        if (v != null)
        {
            _context.LessonVideos.Remove(v);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
