using CsharpAcademy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private static readonly DateTime SeedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Progress> ProgressRecords { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<UserBadge> UserBadges { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomMember> ClassroomMembers { get; set; }
    public DbSet<CodingExercise> CodingExercises { get; set; }
    public DbSet<TutorialStep> TutorialSteps { get; set; }
    public DbSet<PracticeCompletion> PracticeCompletions { get; set; }
    public DbSet<LessonVideo> LessonVideos { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
    public DbSet<CodingChallenge> CodingChallenges { get; set; }
    public DbSet<ChallengeCompletion> ChallengeCompletions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.UserId, e.CourseId })
            .IsUnique();

        modelBuilder.Entity<Progress>()
            .HasIndex(p => new { p.UserId, p.LessonId })
            .IsUnique();

        modelBuilder.Entity<CourseModule>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.CourseModule)
            .WithMany(m => m.Lessons)
            .HasForeignKey(l => l.CourseModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.User)
            .WithMany(u => u.ProgressRecords)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.Lesson)
            .WithMany()
            .HasForeignKey(p => p.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.Quiz)
            .WithMany()
            .HasForeignKey(a => a.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => c.CertificateCode)
            .IsUnique();

        modelBuilder.Entity<Certificate>()
            .HasIndex(c => new { c.UserId, c.CourseId })
            .IsUnique();

        modelBuilder.Entity<Certificate>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Certificate>()
            .HasOne(c => c.Course)
            .WithMany()
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Classroom>()
            .HasIndex(c => c.JoinCode)
            .IsUnique();

        modelBuilder.Entity<ClassroomMember>()
            .HasIndex(m => new { m.ClassroomId, m.UserId })
            .IsUnique();

        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Teacher)
            .WithMany()
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Classroom>()
            .HasOne(c => c.Course)
            .WithMany()
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ClassroomMember>()
            .HasOne(m => m.Classroom)
            .WithMany(c => c.Members)
            .HasForeignKey(m => m.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClassroomMember>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CodingExercise>()
            .HasOne(e => e.Lesson)
            .WithMany(l => l.CodingExercises)
            .HasForeignKey(e => e.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TutorialStep>()
            .HasOne(s => s.Lesson)
            .WithMany(l => l.TutorialSteps)
            .HasForeignKey(s => s.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PracticeCompletion>()
            .HasIndex(p => new { p.UserId, p.ExerciseId })
            .IsUnique();

        modelBuilder.Entity<PracticeCompletion>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PracticeCompletion>()
            .HasOne(p => p.Exercise)
            .WithMany()
            .HasForeignKey(p => p.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LessonVideo>()
            .HasOne(v => v.Lesson)
            .WithMany(l => l.Videos)
            .HasForeignKey(v => v.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.CreatedBy)
            .WithMany()
            .HasForeignKey(a => a.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssignmentSubmission>()
            .HasIndex(s => new { s.AssignmentId, s.UserId })
            .IsUnique();

        modelBuilder.Entity<AssignmentSubmission>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Submissions)
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssignmentSubmission>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChallengeCompletion>()
            .HasIndex(c => new { c.UserId, c.ChallengeId })
            .IsUnique();

        modelBuilder.Entity<ChallengeCompletion>()
            .HasOne(c => c.Challenge)
            .WithMany()
            .HasForeignKey(c => c.ChallengeId)
            .OnDelete(DeleteBehavior.Cascade);

        PlatformSeedData.Apply(modelBuilder, SeedDate);
    }
}
