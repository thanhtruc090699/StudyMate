using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Service for study file business operations including upload, retrieval, and deletion.
    /// </summary>
    public interface IStudyFileService
    {
        /// <summary>
        /// Uploads a file to a folder. Validates stream and filename. Saves physical file and creates database record.
        /// </summary>
        Task<StudyFile> UploadFileAsync(int folderId, Stream fileStream, string fileName, string contentType);

        /// <summary>
        /// Retrieves a study file by ID.
        /// </summary>
        Task<StudyFile?> GetFileByIdAsync(int id);

        /// <summary>
        /// Retrieves all files in a folder.
        /// </summary>
        Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId);

        /// <summary>
        /// Deletes a file (database record and physical file). Throws if not found.
        /// </summary>
        Task DeleteFileAsync(int id);
    }
}
