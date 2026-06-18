namespace CsharpAcademy.Domain.Entities;

public class Classroom : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string JoinCode { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public User Teacher { get; set; } = null!;
    public int? CourseId { get; set; }
    public Course? Course { get; set; }
    public ICollection<ClassroomMember> Members { get; set; } = new List<ClassroomMember>();
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

public class ClassroomMember : Entity
{
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
