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
}
