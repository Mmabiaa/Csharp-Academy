using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId, cancellationToken);
    }

    public async Task<Enrollment> CreateAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync(cancellationToken);
        return enrollment;
    }

    public async Task<List<Enrollment>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .Where(e => e.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
    {
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.Enrollments.CountAsync(cancellationToken);
}
