using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lesson?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Lessons
            .Include(l => l.CourseModule)
                .ThenInclude(m => m.Course)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<int> CountByCourseIdAsync(int courseId, CancellationToken cancellationToken = default)
    {
        return await _context.Lessons
            .Where(l => l.CourseModule.CourseId == courseId)
            .CountAsync(cancellationToken);
    }

    public async Task<Lesson> CreateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return lesson;
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        var existingLesson = await _context.Lessons
            .Include(l => l.Videos)
            .FirstOrDefaultAsync(l => l.Id == lesson.Id, cancellationToken);

        if (existingLesson == null) return;

        // Update basic properties
        _context.Entry(existingLesson).CurrentValues.SetValues(lesson);

        // Synchronize Videos
        // Remove videos not in the new list
        foreach (var existingVideo in existingLesson.Videos.ToList())
        {
            if (!lesson.Videos.Any(v => v.Id == existingVideo.Id))
                _context.LessonVideos.Remove(existingVideo);
        }

        // Add or update videos
        foreach (var video in lesson.Videos)
        {
            var existingVideo = existingLesson.Videos.FirstOrDefault(v => v.Id == video.Id);
            if (existingVideo != null)
            {
                _context.Entry(existingVideo).CurrentValues.SetValues(video);
            }
            else
            {
                video.LessonId = lesson.Id;
                existingLesson.Videos.Add(video);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var lesson = await _context.Lessons.FindAsync(new object[] { id }, cancellationToken);
        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

public class ModuleRepository : IModuleRepository
{
    private readonly ApplicationDbContext _context;
    public ModuleRepository(ApplicationDbContext context) => _context = context;

    public async Task<CourseModule?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.CourseModules.FindAsync(new object[] { id }, cancellationToken);

    public async Task<CourseModule> CreateAsync(CourseModule module, CancellationToken cancellationToken = default)
    {
        _context.CourseModules.Add(module);
        await _context.SaveChangesAsync(cancellationToken);
        return module;
    }

    public async Task UpdateAsync(CourseModule module, CancellationToken cancellationToken = default)
    {
        _context.Entry(module).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var module = await _context.CourseModules.FindAsync(new object[] { id }, cancellationToken);
        if (module != null)
        {
            _context.CourseModules.Remove(module);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _context.CourseModules.CountAsync(cancellationToken);
}
