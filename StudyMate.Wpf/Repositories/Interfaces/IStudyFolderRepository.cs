using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces;

/// <summary>
/// Defines data access operations for study folder records.
/// </summary>
public interface IStudyFolderRepository
{
    /// <summary>
    /// Retrieves all study folders from the data store.
    /// </summary>
    /// <returns>
    /// A list of all study folders, typically ordered by creation date.
    /// </returns>
    Task<List<StudyFolder>> GetAllAsync();

    /// <summary>
    /// Adds a new study folder record to the data store.
    /// </summary>
    /// <param name="folder">
    /// The study folder to add.
    /// </param>
    /// <returns>
    /// The persisted study folder record with generated properties populated.
    /// </returns>
    Task<StudyFolder> AddAsync(StudyFolder folder);
}
