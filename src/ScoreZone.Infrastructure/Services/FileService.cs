using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Domain.Shared.Exceptions;
using ScoreZone.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace ScoreZone.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _storagePath;
        private readonly long _maxFileSize;
        private readonly string[] _allowedExtensions;

        public FileService(IOptions<FileStorageOptions> options)
        {
            _storagePath = options.Value.StoragePath;
            _maxFileSize = options.Value.MaxFileSize;
            _allowedExtensions = options.Value.AllowedExtensions;

            if(!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }
        
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if( file == null || file.Length == 0)
                throw new AppException(404, "No File Found To Upload.");
            
            if( file.Length > _maxFileSize)
                throw new DomainException(400, $"File Exceded Maximum Size Of {_maxFileSize / 1024}m.");
            

            var extension = Path.GetExtension(file.FileName).ToLower();

            if(!_allowedExtensions.Contains(extension))
                throw new  DomainException(400, $"File Extension {extension} is Not Allowed.");
            
            var safeName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_storagePath, safeName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return filePath;
            
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            if(string.IsNullOrWhiteSpace(fileUrl))
                throw new DomainException(400, "File Name is Required.");
            
            if(!File.Exists(fileUrl))
                throw new AppException(404, "File Not Found.");
            
            File.Delete(fileUrl);

            return Task.CompletedTask;
            
        }
    }
}