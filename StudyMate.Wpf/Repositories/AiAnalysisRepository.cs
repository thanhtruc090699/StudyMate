using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class AiAnalysisRepository : IAiAnalysisRepository
    {
        private readonly AppDbContext _dbContext;

        public AiAnalysisRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId)
        {
            var result = await _dbContext.AiAnalyses.Where(x=> x.StudyFileId == studyFileId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
            
            return result;
        }

        /// <inheritdoc />
        public async Task<AiAnalysis> AddAsync(AiAnalysis analysis)
        {
            _dbContext.AiAnalyses.Add(analysis);
            await _dbContext.SaveChangesAsync();
            return analysis;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(AiAnalysis analysis)
        {
            _dbContext.Update(analysis);
            await _dbContext.SaveChangesAsync();
        }
    }
}
