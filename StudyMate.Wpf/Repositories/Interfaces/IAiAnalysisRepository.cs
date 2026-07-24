using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for accessing and manipulating AI analysis data.
    /// </summary>
    public interface IAiAnalysisRepository
    {
        /// <summary>
        /// Retrieves the most recent AI analysis for a specific study file.
        /// </summary>
        /// <param name="studyFileId">The unique identifier of the study file.</param>
        /// <returns>A task containing the latest AI analysis if found; otherwise, null.</returns>
        Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId);

        /// <summary>
        /// Adds a new AI analysis asynchronously.
        /// </summary>
        /// <param name="analysis">The AI analysis to add.</param>
        /// <returns>A task containing the added AI analysis with generated properties populated.</returns>
        Task<AiAnalysis> AddAsync(AiAnalysis analysis);

        /// <summary>
        /// Updates an existing AI analysis asynchronously.
        /// </summary>
        /// <param name="analysis">The AI analysis with updated values.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateAsync(AiAnalysis analysis);
    }
}
