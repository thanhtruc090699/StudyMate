using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Defines methods for business logic operations related to study folders.
    /// </summary>
    public interface IStudyFolderService
    {
        /// <summary>
        /// Retrieves all study folders asynchronously.
        /// </summary>
        /// <returns>A task containing a list of all study folders.</returns>
        Task<List<StudyFolder>> GetAllFoldersAsync();

        /// <summary>
        /// Creates a new study folder with the specified name.
        /// </summary>
        /// <param name="folderName">The name for the new study folder.</param>
        /// <returns>A task containing the created study folder.</returns>
        Task<StudyFolder> CreateFolderAsync(string folderName);
    }
}
