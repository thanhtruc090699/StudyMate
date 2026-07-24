using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;
using StudyMate.Wpf.Integrations.Ai.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <summary>
    /// Service for managing AI analysis operations on study files.
    /// Generates summaries, structured content, and quiz questions via the AI client.
    /// Tracks analysis status (Pending, Completed, Failed) with error messages.
    /// </summary>
    public class AiAnalysisService : IAiAnalysisService
    {
        private readonly IAiAnalysisRepository _aiAnalysisRepository;
        private readonly IStudyFileService _studyFileService;
        private readonly IAiClient _aiClient;

        /// <summary>
        /// Initializes a new instance of the AiAnalysisService class.
        /// </summary>
        /// <param name="aiAnalysisRepository">The repository for AI analysis data access.</param>
        /// <param name="aiClient">The AI client for generating study material analysis.</param>
        /// <param name="studyFileService">Service for retrieving study file information.</param>
        public AiAnalysisService(IAiAnalysisRepository aiAnalysisRepository, IAiClient aiClient, IStudyFileService studyFileService)
        {
            _aiAnalysisRepository = aiAnalysisRepository;
            _aiClient = aiClient;
            _studyFileService = studyFileService;
        }

        /// <summary>
        /// Generates or regenerates AI analysis for a study file.
        /// Creates an initial analysis record with "Pending" status, calls the AI client, and updates with results.
        /// On failure, marks the analysis as "Failed" and stores the error message before re-throwing.
        /// </summary>
        /// <param name="studyFileId">The ID of the study file to analyze.</param>
        /// <param name="cancellationToken">Optional cancellation token to abort the operation.</param>
        /// <returns>The completed or failed AiAnalysis record with summary, structured content, and quiz data.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the study file is not found.</exception>
        public async Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId, CancellationToken cancellationToken = default)
        {
            var studyFile = await _studyFileService.GetFileByIdAsync(studyFileId);

            if (studyFile == null)
            {
                throw new InvalidOperationException("study file not found");
            }

            var analysis = new AiAnalysis
            {
                StudyFileId = studyFileId,
                Name = $"Analysis for {studyFile.OriginalFileName}",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _aiAnalysisRepository.AddAsync(analysis);

            try
            {
                var aiResult = await _aiClient.GenerateStudyMaterialAsync(studyFile.FilePath, cancellationToken);
                
                analysis.Summary = aiResult.Summary;
                analysis.StructuredContentJson = System.Text.Json.JsonSerializer.Serialize(aiResult.StructuredContent);
                analysis.QuizJson = System.Text.Json.JsonSerializer.Serialize(aiResult.QuizQuestions);
                analysis.ModelName = aiResult.ModelName;
                analysis.Status = "Completed";
                analysis.ErrorMessage = null;
                analysis.CreatedAt = DateTime.UtcNow;

                await _aiAnalysisRepository.UpdateAsync(analysis);

                return analysis;
            }
            catch (Exception e)
            {
                analysis.Status = "Failed";
                analysis.ErrorMessage = e.Message;
                analysis.UpdatedAt = DateTime.UtcNow;

                await _aiAnalysisRepository.UpdateAsync(analysis);

                throw;
            }
        }

        /// <summary>
        /// Retrieves the most recent AI analysis for a specific study file.
        /// </summary>
        /// <param name="studyFileId">The ID of the study file.</param>
        /// <returns>The latest AiAnalysis record if found; otherwise, null.</returns>
        public async Task<AiAnalysis?> GetLatestAnalysisByStudyFileIdAsync(int studyFileId)
        {
            var analysis = await _aiAnalysisRepository.GetLatestByStudyFileIdAsync(studyFileId);
            return analysis;
        }

    }
}
