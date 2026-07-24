using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for study file records.
    /// </summary>
    public interface IStudyFileRepository
    {
        /// <summary>
        /// Retrieves a study file by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the study file.
        /// </param>
        /// <returns>
        /// The study file if found; otherwise, <see langword="null"/>.
        /// </returns>
        Task<StudyFile?> GetStudyFileAsync(int id);

        /// <summary>
        /// Retrieves all study files associated with a specific folder.
        /// </summary>
        /// <param name="folderId">
        /// The identifier of the folder.
        /// </param>
        /// <returns>
        /// A list of study files belonging to the specified folder.
        /// </returns>
        Task<List<StudyFile>> GetByFolderIdAsync(int folderId);

        /// <summary>
        /// Adds a new study file record to the data store.
        /// </summary>
        /// <param name="studyFile">
        /// The study file to add.
        /// </param>
        /// <returns>
        /// The persisted study file record with generated properties populated.
        /// </returns>
        Task<StudyFile> AddAsync(StudyFile studyFile);

        /// <summary>
        /// Deletes a study file record by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the study file to delete.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous delete operation.
        /// </returns>
        Task DeleteAsync(int id);
    }
}
