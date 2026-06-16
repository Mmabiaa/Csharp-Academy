using CsharpAcademy.Application.Classrooms.Commands;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using static CsharpAcademy.Application.Classrooms.Commands.CreateClassroomCommandHandler;

namespace CsharpAcademy.Application.Classrooms.Queries;

public record GetMyClassroomsQuery(int UserId, bool IsTeacher) : IRequest<List<ClassroomDto>>;

public record GetClassroomByIdQuery(int ClassroomId) : IRequest<ClassroomDto?>;

public class GetMyClassroomsQueryHandler : IRequestHandler<GetMyClassroomsQuery, List<ClassroomDto>>
{
    private readonly IClassroomRepository _classroomRepository;

    public GetMyClassroomsQueryHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<List<ClassroomDto>> Handle(GetMyClassroomsQuery request, CancellationToken cancellationToken)
    {
        var list = request.IsTeacher
            ? await _classroomRepository.GetByTeacherIdAsync(request.UserId, cancellationToken)
            : await _classroomRepository.GetByStudentIdAsync(request.UserId, cancellationToken);

        return list.Select(c =>
        {
            c.Teacher ??= new Domain.Entities.User();
            c.Members ??= new List<Domain.Entities.ClassroomMember>();
            return MapToDto(c);
        }).ToList();
    }
}

public class GetClassroomByIdQueryHandler : IRequestHandler<GetClassroomByIdQuery, ClassroomDto?>
{
    private readonly IClassroomRepository _classroomRepository;

    public GetClassroomByIdQueryHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<ClassroomDto?> Handle(GetClassroomByIdQuery request, CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetByIdAsync(request.ClassroomId, cancellationToken);
        return classroom is null ? null : MapToDto(classroom);
    }
}
