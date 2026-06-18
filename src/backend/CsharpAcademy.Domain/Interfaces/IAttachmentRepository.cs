using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IAttachmentRepository
{
    Task<Attachment> AddAsync(Attachment attachment, CancellationToken ct = default);
    Task<List<Attachment>> GetByClassroomIdAsync(int classroomId, CancellationToken ct = default);
    Task<List<Attachment>> GetByAssignmentIdAsync(int assignmentId, CancellationToken ct = default);
    Task<List<Attachment>> GetBySubmissionIdAsync(int submissionId, CancellationToken ct = default);
    Task DeleteAsync(int attachmentId, CancellationToken ct = default);
    Task<Attachment?> GetByIdAsync(int attachmentId, CancellationToken ct = default);
}
