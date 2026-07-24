using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Defines methods for business logic operations related to study files.
    /// </summary>
    public interface IStudyFileService
    {
        /// <summary>
        /// Uploads a file to a study folder asynchronously.
        /// </summary>
        /// <param name="folderId">The folder identifier where the file will be stored.</param>
        /// <param name="fileStream">The file content stream.</param>
        /// <param name="fileName">The original file name.</param>
        /// <param name="contentType">The MIME type of the file.</param>
        /// <returns>A task containing the created study file entity.</returns>
        Task<StudyFile> UploadFileAsync(int folderId, Stream fileStream, string fileName, string contentType);

        /// <summary>
        /// Retrieves a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file.</param>
        /// <returns>A task containing the study file if found; otherwise, null.</returns>
        Task<StudyFile?> GetFileByIdAsync(int id);

        /// <summary>
        /// Retrieves all study files for a specific folder.
        /// </summary>
        /// <param name="folderId">The folder identifier to filter by.</param>
        /// <returns>A task containing a list of study files in the specified folder.</returns>
        Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId);

        /// <summary>
        /// Deletes a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteFileAsync(int id);
    }
}
