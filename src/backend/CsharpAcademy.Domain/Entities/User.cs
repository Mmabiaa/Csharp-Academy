using Microsoft.AspNetCore.Identity;

namespace CsharpAcademy.Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Xp { get; set; } = 0;
    public int CurrentStreak { get; set; } = 0;
    public int MaxStreak { get; set; } = 0;
    public string? ProfileImageUrl { get; set; }
    public bool VoiceRecognitionEnabled { get; set; } = false;
    public bool VoiceFeedbackEnabled { get; set; } = true;
    public DateTime? LastActiveDate { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Progress> ProgressRecords { get; set; } = new List<Progress>();
}
