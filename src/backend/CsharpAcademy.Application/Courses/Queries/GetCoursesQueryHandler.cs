using CsharpAcademy.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Application.Courses.Queries;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetCoursesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            })
            .ToListAsync(cancellationToken);
    }
}
