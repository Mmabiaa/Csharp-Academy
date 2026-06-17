using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class ClassroomRepository : IClassroomRepository
{
    private readonly ApplicationDbContext _context;

    public ClassroomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Classroom?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Classrooms
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .Include(c => c.Members)
                .ThenInclude(m => m.User)
            .Include(c => c.Attachments)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Classroom?> GetByJoinCodeAsync(string joinCode, CancellationToken cancellationToken = default)
    {
        return await _context.Classrooms
            .FirstOrDefaultAsync(c => c.JoinCode == joinCode.ToUpperInvariant(), cancellationToken);
    }

    public async Task<List<Classroom>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
    {
        return await _context.Classrooms
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .Include(c => c.Members)
                .ThenInclude(m => m.User)
            .Include(c => c.Attachments)
            .Where(c => c.TeacherId == teacherId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Classroom>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Classrooms
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .Include(c => c.Attachments)
            .Where(c => c.Members.Any(m => m.UserId == studentId))
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Classroom> CreateAsync(Classroom classroom, CancellationToken cancellationToken = default)
    {
        _context.Classrooms.Add(classroom);
        await _context.SaveChangesAsync(cancellationToken);
        return classroom;
    }

    public async Task<ClassroomMember?> GetMemberAsync(int classroomId, int userId, CancellationToken cancellationToken = default)
    {
        return await _context.ClassroomMembers
            .FirstOrDefaultAsync(m => m.ClassroomId == classroomId && m.UserId == userId, cancellationToken);
    }

    public async Task<ClassroomMember> AddMemberAsync(ClassroomMember member, CancellationToken cancellationToken = default)
    {
        _context.ClassroomMembers.Add(member);
        await _context.SaveChangesAsync(cancellationToken);
        return member;
    }
}
