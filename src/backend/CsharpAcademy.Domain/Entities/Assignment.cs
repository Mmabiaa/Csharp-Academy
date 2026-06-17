namespace CsharpAcademy.Domain.Entities;

public class Assignment : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int? CourseId { get; set; }
    public Course? Course { get; set; }
    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public int? ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public int MaxPoints { get; set; } = 100;
    public bool RequiresCode { get; set; }
    public ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

public class AssignmentSubmission : Entity
{
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public SubmissionStatus Status { get; set; } = SubmissionStatus.Submitted;
    public int? Grade { get; set; }
    public string? Feedback { get; set; }
    public int? GradedById { get; set; }
    public DateTime? GradedAt { get; set; }
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

public enum SubmissionStatus
{
    Submitted = 0,
    Graded = 1,
    Returned = 2
}
