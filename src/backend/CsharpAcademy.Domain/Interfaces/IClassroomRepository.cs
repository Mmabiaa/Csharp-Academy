using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Domain.Interfaces;

public interface IClassroomRepository
{
    Task<Classroom?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Classroom?> GetByJoinCodeAsync(string joinCode, CancellationToken cancellationToken = default);
    Task<List<Classroom>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default);
    Task<List<Classroom>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);
    Task<Classroom> CreateAsync(Classroom classroom, CancellationToken cancellationToken = default);
    Task<ClassroomMember?> GetMemberAsync(int classroomId, int userId, CancellationToken cancellationToken = default);
    Task<ClassroomMember> AddMemberAsync(ClassroomMember member, CancellationToken cancellationToken = default);
}
