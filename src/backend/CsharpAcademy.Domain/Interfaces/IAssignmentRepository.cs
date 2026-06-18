using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IAssignmentRepository
{
    Task<Assignment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Assignment>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default);
    Task<List<Assignment>> GetForStudentAsync(int userId, CancellationToken cancellationToken = default);
    Task<List<AssignmentSubmission>> GetSubmissionsAsync(int assignmentId, CancellationToken cancellationToken = default);
    Task<AssignmentSubmission?> GetSubmissionAsync(int submissionId, CancellationToken cancellationToken = default);
    Task<AssignmentSubmission?> GetUserSubmissionAsync(int assignmentId, int userId, CancellationToken cancellationToken = default);
    Task<Assignment> CreateAsync(Assignment assignment, CancellationToken cancellationToken = default);
    Task<AssignmentSubmission> SubmitAsync(AssignmentSubmission submission, CancellationToken cancellationToken = default);
    Task GradeSubmissionAsync(AssignmentSubmission submission, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
