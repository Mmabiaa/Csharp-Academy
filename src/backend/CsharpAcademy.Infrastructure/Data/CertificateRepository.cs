using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class CertificateRepository : ICertificateRepository
{
    private readonly ApplicationDbContext _context;

    public CertificateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.Certificates
            .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId, cancellationToken);
    }

    public async Task<Certificate?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Certificates
            .Include(c => c.User)
            .Include(c => c.Course)
            .FirstOrDefaultAsync(c => c.CertificateCode == code, cancellationToken);
    }

    public async Task<List<Certificate>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Certificates
            .Include(c => c.Course)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.IssuedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Certificate> CreateAsync(Certificate certificate, CancellationToken cancellationToken = default)
    {
        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync(cancellationToken);
        return certificate;
    }
}
