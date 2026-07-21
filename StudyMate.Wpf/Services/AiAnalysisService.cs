using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    public class AiAnalysisService : IAiAnalysisService
    {
        private readonly IAiAnalysisRepository _aiAnalysisRepository;

        public AiAnalysisService(IAiAnalysisRepository aiAnalysisRepository)
        {
            _aiAnalysisRepository = aiAnalysisRepository;
        }

        public async Task<AiAnalysis> GenerateAnalysisAsync(int studyFileId)
        {
            
            var analysis = new AiAnalysis
            {
                StudyFileId = studyFileId,
                AnalysisResult = "Generated analysis result",
                CreatedAt = DateTime.UtcNow
            };
            return await _aiAnalysisRepository.AddAsync(analysis);
        }

    }
}
