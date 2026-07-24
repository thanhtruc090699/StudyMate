using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Defines methods for business logic operations related to AI-powered content analysis.
    /// </summary>
    public interface IAiAnalysisService
    {
        /// <summary>
        /// Generates AI-powered analysis for a study file.
        /// </summary>
        /// <param name="studyFileId">The unique identifier of the study file to analyze.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task containing the generated AI analysis.</returns>
        Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the most recent AI analysis for a specific study file.
        /// </summary>
        /// <param name="studyFileId">The unique identifier of the study file.</param>
        /// <returns>A task containing the latest AI analysis if found.</returns>
        Task<AiAnalysis> GetLatestAnalysisByStudyFileIdAsync(int studyFileId);
    }
}
