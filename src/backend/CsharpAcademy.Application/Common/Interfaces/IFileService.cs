using Microsoft.AspNetCore.Http;

namespace CsharpAcademy.Application.Common.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string subFolder);
    void DeleteFile(string fileUrl);
}
