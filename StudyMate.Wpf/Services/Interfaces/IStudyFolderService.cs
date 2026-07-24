using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Service for study folder business operations.
    /// </summary>
    public interface IStudyFolderService
    {
        /// <summary>
        /// Retrieves all study folders.
        /// </summary>
        Task<List<StudyFolder>> GetAllFoldersAsync();

        /// <summary>
        /// Creates a new folder with validation (name required, trimmed).
        /// Sets CreatedAt and UpdatedAt timestamps.
        /// </summary>
        Task<StudyFolder> CreateFolderAsync(string folderName);
    }
}
