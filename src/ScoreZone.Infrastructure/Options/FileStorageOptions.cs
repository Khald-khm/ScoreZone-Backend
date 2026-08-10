using System.ComponentModel.DataAnnotations;

namespace ScoreZone.Infrastructure.Options
{
    public class FileStorageOptions
    {
        [Required]
        public string StoragePath { get; set; } = string.Empty;
        [Required]
        public long MaxFileSize { get; set; } = 5242880; // 5 mega
        [Required]
        public string[] AllowedExtensions { get; set; } = {".jpg", ".jpeg", ".png", ".pdf", ".docx", ".doc"};
    }
}