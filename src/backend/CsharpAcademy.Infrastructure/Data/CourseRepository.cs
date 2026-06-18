using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
            .ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdWithModulesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .Include(c => c.Modules.OrderBy(m => m.Order))
                .ThenInclude(m => m.Lessons.OrderBy(l => l.Order))
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Course> CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);
        return course;
    }

    public async Task UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.Courses.CountAsync(cancellationToken);
}
