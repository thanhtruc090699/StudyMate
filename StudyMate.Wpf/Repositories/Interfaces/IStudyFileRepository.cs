using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for accessing and manipulating study file data.
    /// </summary>
    public interface IStudyFileRepository
    {
        /// <summary>
        /// Retrieves a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file.</param>
        /// <returns>A task containing the study file if found; otherwise, null.</returns>
        Task<StudyFile?> GetStudyFileAsync(int id);

        /// <summary>
        /// Retrieves all study files for a specific folder.
        /// </summary>
        /// <param name="folderId">The folder identifier to filter by.</param>
        /// <returns>A task containing a list of study files in the specified folder.</returns>
        Task<List<StudyFile>> GetByFolderIdAsync(int folderId);

        /// <summary>
        /// Adds a new study file asynchronously.
        /// </summary>
        /// <param name="studyFile">The study file to add.</param>
        /// <returns>A task containing the added study file with generated properties populated.</returns>
        Task<StudyFile> AddAsync(StudyFile studyFile);

        /// <summary>
        /// Deletes a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteAsync(int id);
    }
}
