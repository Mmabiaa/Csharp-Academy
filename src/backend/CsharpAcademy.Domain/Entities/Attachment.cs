namespace CsharpAcademy.Domain.Entities;

public class Attachment : Entity
{
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int? ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
    public int? AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }
    public int? SubmissionId { get; set; }
    public AssignmentSubmission? Submission { get; set; }
    public int UploadedById { get; set; }
    public User UploadedBy { get; set; } = null!;
}
