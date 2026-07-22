using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Repositories.Interfaces
{
    public interface IAiAnalysisRepository
    {
        Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId);
        Task<AiAnalysis> AddAsync(AiAnalysis analysis);
        Task UpdateAsync(AiAnalysis analysis);
    }
}
