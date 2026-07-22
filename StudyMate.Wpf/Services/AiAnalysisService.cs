using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;
using StudyMate.Wpf.Integrations.Ai.Interfaces;

namespace StudyMate.Wpf.Services
{
    public class AiAnalysisService : IAiAnalysisService
    {
        private readonly IAiAnalysisRepository _aiAnalysisRepository;
        private readonly IStudyFileService _studyFileService;
        private readonly IAiClient _aiClient;

        public AiAnalysisService(IAiAnalysisRepository aiAnalysisRepository, IAiClient aiClient, IStudyFileService studyFileService)
        {
            _aiAnalysisRepository = aiAnalysisRepository;
            _aiClient = aiClient;
            _studyFileService = studyFileService;
        }

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

        public async Task<AiAnalysis?> GetLatestAnalysisByStudyFileIdAsync(int studyFileId)
        {
            var analysis = await _aiAnalysisRepository.GetLatestByStudyFileIdAsync(studyFileId);
            return analysis;
        }

    }
}
