using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class TutorialStepRepository : ITutorialStepRepository
{
    private readonly ApplicationDbContext _context;

    public TutorialStepRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TutorialStep>> GetByLessonIdAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        return await _context.TutorialSteps
            .Where(s => s.LessonId == lessonId)
            .OrderBy(s => s.Order)
            .ToListAsync(cancellationToken);
    }
}
