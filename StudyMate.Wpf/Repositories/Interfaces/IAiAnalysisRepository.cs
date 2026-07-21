using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Repositories.Interfaces
{
    public interface IAiAnalysisRepository
    {
        Task<AiAnalysis> GetByIdAsync(int id);
        Task<AiAnalysis> GetLatestByStudyFileIdAsync(int studyFileId);

        Task<List<AiAnalysis>> GetByStudyFileIdAsync(int studyFileId);

        Task<AiAnalysis> AddAsync(AiAnalysis analysis);

        Task UpdateAsync(AiAnalysis analysis);
        Task DeleteAsync(AiAnalysis analysis);
    }
}
