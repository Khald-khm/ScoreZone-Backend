using Microsoft.AspNetCore.Http;

namespace ScoreZone.Application.Shared.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileUrl);
    }
}