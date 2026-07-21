using System.IO;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IAiAnalysisService
    {
        Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId, CancellationToken cancellationToken = default);

        Task<AiAnalysis> GetLatestAnalysisByStudyFileIdAsync(int studyFileId);

    }
}
