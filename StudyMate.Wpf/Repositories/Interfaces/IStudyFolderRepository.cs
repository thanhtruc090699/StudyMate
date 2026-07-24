using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces;

/// <summary>
/// Defines methods for accessing and manipulating study folder data.
/// </summary>
public interface IStudyFolderRepository
{
    /// <summary>
    /// Retrieves all study folders asynchronously.
    /// </summary>
    /// <returns>A task containing a list of all study folders, ordered by creation date.</returns>
    Task<List<StudyFolder>> GetAllAsync();

    /// <summary>
    /// Adds a new study folder asynchronously.
    /// </summary>
    /// <param name="folder">The study folder to add.</param>
    /// <returns>A task containing the added study folder with generated properties populated.</returns>
    Task<StudyFolder> AddAsync(StudyFolder folder);
}
