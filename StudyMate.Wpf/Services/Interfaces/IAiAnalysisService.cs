using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IAiAnalysisService
    {
        Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId);

        Task<AiAnalysis> GetAnalysisByIdAsync(int analysisId);
        Task<AiAnalysis> GetLatestAnalysisByStudyFileIdAsync(int studyFileId);

        Task<List<AiAnalysis>> GetAnalysesHistoryByStudyFileIdAsync(int studyFileId);

        Task<AiAnalysis> UpdateAnalysisAsync(int studyFileId);
        Task DeleteAnalysisAsync(int analysisId);

    }
}
