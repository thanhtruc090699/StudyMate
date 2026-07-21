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
            System.Diagnostics.Debug.WriteLine($"[AI Service] Starting analysis for file ID: {studyFileId}");
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] GenerateAnalysisAsync START for file ID={studyFileId}\n");
            
            var studyFile = await _studyFileService.GetFileByIdAsync(studyFileId);

            if (studyFile == null)
            {
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] File not found: {studyFileId}\n");
                throw new InvalidOperationException("study file not found");
            }
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] File path: {studyFile.FilePath}\n");

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
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] [AI SERVICE] Calling AI Client for StudyFileId={studyFileId}...\n");
                var aiResult = await _aiClient.GenerateStudyMaterialAsync(studyFile.FilePath, cancellationToken);
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] [AI SERVICE] AI result received for StudyFileId={studyFileId}\n");
                
                analysis.Summary = aiResult.Summary;
                analysis.StructuredContentJson = System.Text.Json.JsonSerializer.Serialize(aiResult.StructuredContent);
                analysis.QuizJson = System.Text.Json.JsonSerializer.Serialize(aiResult.QuizQuestions);
                analysis.ModelName = aiResult.ModelName;
                analysis.Status = "Completed";
                analysis.ErrorMessage = null;
                analysis.CreatedAt = DateTime.UtcNow;

                await _aiAnalysisRepository.UpdateAsync(analysis);

                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] [AI SERVICE] Analysis saved successfully. AnalysisId={analysis.Id}, StudyFileId={analysis.StudyFileId}, Status={analysis.Status}\n");

                return analysis;



            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] [AI SERVICE] Analysis failed. StudyFileId={studyFileId}, Error={e}\n");
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
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] [SERVICE] GetLatestAnalysisByStudyFileIdAsync({studyFileId}): {(analysis == null ? "NULL" : $"Found Id={analysis.Id}, Status={analysis.Status}")}\n");
            return analysis;
        }

    }
}
