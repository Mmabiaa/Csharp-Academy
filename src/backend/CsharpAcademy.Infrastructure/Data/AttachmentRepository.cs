using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using CsharpAcademy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly ApplicationDbContext _context;

    public AttachmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Attachment> AddAsync(Attachment attachment, CancellationToken ct = default)
    {
        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync(ct);
        return attachment;
    }

    public async Task<List<Attachment>> GetByClassroomIdAsync(int classroomId, CancellationToken ct = default)
    {
        return await _context.Attachments
            .Where(a => a.ClassroomId == classroomId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<List<Attachment>> GetByAssignmentIdAsync(int assignmentId, CancellationToken ct = default)
    {
        return await _context.Attachments
            .Where(a => a.AssignmentId == assignmentId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<List<Attachment>> GetBySubmissionIdAsync(int submissionId, CancellationToken ct = default)
    {
        return await _context.Attachments
            .Where(a => a.SubmissionId == submissionId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task DeleteAsync(int attachmentId, CancellationToken ct = default)
    {
        var attachment = await _context.Attachments.FindAsync(new object[] { attachmentId }, ct);
        if (attachment != null)
        {
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<Attachment?> GetByIdAsync(int attachmentId, CancellationToken ct = default)
    {
        return await _context.Attachments.FindAsync(new object[] { attachmentId }, ct);
    }
}
