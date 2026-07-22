using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    internal class FileStorageService : IFileStorageService
    {
        private readonly string _uploadPath;

        public FileStorageService()
        {
            // Use absolute path in AppData folder
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StudyMate",
                "uploads"
            );
            Directory.CreateDirectory(appDataPath);
            _uploadPath = appDataPath;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath)
        {
            if(fileStream == null || fileStream.Length == 0)
            {
                throw new ArgumentException("File stream cannot be null or empty.", nameof(fileStream));
            }

            if(string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentException("Original file name cannot be null or whitespace.", nameof(originalFileName));
            }
            if(string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("Folder path cannot be null or whitespace.", nameof(folderPath));
            }

            var sanitizedFileName = Path.GetInvalidFileNameChars().Aggregate(originalFileName,
                (current, c) => current.Replace(c.ToString(), string.Empty));

            var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}";

            // Use the configured upload path
            var fullPath = Path.Combine(_uploadPath, uniqueFileName);
            
            try
            {
                if(fileStream.CanSeek)
                    fileStream.Position = 0;

                using (var fileStreamToSave = new FileStream(fullPath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileStreamToSave);
                }

                return uniqueFileName;

            }
            catch (Exception e)
            {
                throw new IOException($"Failed to save file '{originalFileName}': {e.Message}", e);
            }
        }

        public async Task DeleteFileAsync(string storedFileName)
        {
            if (string.IsNullOrWhiteSpace(storedFileName))
            {
                throw new ArgumentException("Stored file name cannot be null or whitespace.", nameof(storedFileName));
            }
            
            var filePath = Path.Combine(_uploadPath, storedFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
