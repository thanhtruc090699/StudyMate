using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Service for physical file storage operations in AppData upload directory.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves a file with unique GUID-prefixed filename. Sanitizes filename and resets stream position.
        /// </summary>
        Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath);

        /// <summary>
        /// Deletes a file by stored filename. Silently succeeds if file doesn't exist.
        /// </summary>
        Task DeleteFileAsync(string storedFileName);
    }
}
