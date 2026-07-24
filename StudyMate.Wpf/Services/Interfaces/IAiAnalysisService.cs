using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Service for AI analysis operations - generation and retrieval of study material analysis.
    /// </summary>
    public interface IAiAnalysisService
    {
        /// <summary>
        /// Generates AI analysis for a study file. Creates pending record, calls AI client, updates with results.
        /// On failure, marks as "Failed" with error message before re-throwing.
        /// </summary>
        Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the most recent analysis for a study file.
        /// </summary>
        /// <returns>
        /// The latest AI analysis if found; otherwise, <see langword="null"/>.
        /// </returns>
        Task<AiAnalysis?> GetLatestAnalysisByStudyFileIdAsync(int studyFileId);
    }
}
