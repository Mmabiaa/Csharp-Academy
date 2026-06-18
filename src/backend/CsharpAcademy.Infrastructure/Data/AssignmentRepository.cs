using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public AssignmentRepository(ApplicationDbContext context) => _context = context;

    public async Task<Assignment?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.Assignments
            .Include(a => a.Course)
            .Include(a => a.Lesson)
            .Include(a => a.Classroom)
            .Include(a => a.Attachments)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<List<Assignment>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default) =>
        await _context.Assignments
            .Include(a => a.Submissions)
            .Include(a => a.Attachments)
            .Where(a => a.CreatedById == teacherId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<List<Assignment>> GetForStudentAsync(int userId, CancellationToken cancellationToken = default)
    {
        var classroomIds = await _context.ClassroomMembers
            .Where(m => m.UserId == userId)
            .Select(m => m.ClassroomId)
            .ToListAsync(cancellationToken);

        return await _context.Assignments
            .Include(a => a.Submissions.Where(s => s.UserId == userId))
            .Include(a => a.Attachments)
            .Where(a => a.ClassroomId == null || classroomIds.Contains(a.ClassroomId.Value))
            .OrderByDescending(a => a.DueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AssignmentSubmission>> GetSubmissionsAsync(int assignmentId, CancellationToken cancellationToken = default) =>
        await _context.AssignmentSubmissions
            .Include(s => s.User)
            .Where(s => s.AssignmentId == assignmentId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync(cancellationToken);

    public async Task<AssignmentSubmission?> GetSubmissionAsync(int submissionId, CancellationToken cancellationToken = default) =>
        await _context.AssignmentSubmissions
            .Include(s => s.User)
            .Include(s => s.Assignment)
            .Include(s => s.Attachments)
            .FirstOrDefaultAsync(s => s.Id == submissionId, cancellationToken);

    public async Task<AssignmentSubmission?> GetUserSubmissionAsync(int assignmentId, int userId, CancellationToken cancellationToken = default) =>
        await _context.AssignmentSubmissions
            .Include(s => s.Attachments)
            .FirstOrDefaultAsync(s => s.AssignmentId == assignmentId && s.UserId == userId, cancellationToken);

    public async Task<Assignment> CreateAsync(Assignment assignment, CancellationToken cancellationToken = default)
    {
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);
        return assignment;
    }

    public async Task<AssignmentSubmission> SubmitAsync(AssignmentSubmission submission, CancellationToken cancellationToken = default)
    {
        _context.AssignmentSubmissions.Add(submission);
        await _context.SaveChangesAsync(cancellationToken);
        return submission;
    }

    public async Task GradeSubmissionAsync(AssignmentSubmission submission, CancellationToken cancellationToken = default)
    {
        _context.AssignmentSubmissions.Update(submission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.Assignments.CountAsync(cancellationToken);
}
