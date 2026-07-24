using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for AI analysis records.
    /// </summary>
    public interface IAiAnalysisRepository
    {
        /// <summary>
        /// Retrieves the most recently created AI analysis associated with
        /// the specified study file.
        /// </summary>
        /// <param name="studyFileId">
        /// The identifier of the study file.
        /// </param>
        /// <returns>
        /// The most recent AI analysis, or <see langword="null"/> when no
        /// analysis exists for the study file.
        /// </returns>
        Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId);

        /// <summary>
        /// Adds a new AI analysis record to the data store.
        /// </summary>
        /// <param name="analysis">
        /// The AI analysis to add.
        /// </param>
        /// <returns>
        /// The persisted AI analysis record.
        /// </returns>
        Task<AiAnalysis> AddAsync(AiAnalysis analysis);

        /// <summary>
        /// Persists changes made to an existing AI analysis record.
        /// </summary>
        /// <param name="analysis">
        /// The AI analysis containing the updated values.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous update operation.
        /// </returns>
        Task UpdateAsync(AiAnalysis analysis);
    }
}
