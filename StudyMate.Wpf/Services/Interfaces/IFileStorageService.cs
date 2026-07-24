using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Defines methods for physical file storage operations.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves a file stream to persistent storage.
        /// </summary>
        /// <param name="fileStream">The file content stream to save.</param>
        /// <param name="originalFileName">The original name of the uploaded file.</param>
        /// <param name="folderPath">The target folder path for storage.</param>
        /// <returns>A task containing the stored file name.</returns>
        Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath);

        /// <summary>
        /// Deletes a file from persistent storage.
        /// </summary>
        /// <param name="storedFileName">The name of the stored file to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteFileAsync(string storedFileName);
    }
}
