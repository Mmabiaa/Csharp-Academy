using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user, IList<string> roles);
}
