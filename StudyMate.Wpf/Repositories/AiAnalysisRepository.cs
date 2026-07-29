using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class AiAnalysisRepository : IAiAnalysisRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public AiAnalysisRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        public async Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var result = await context.AiAnalyses
                .Where(x => x.StudyFileId == studyFileId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
            
            return result;
        }

        /// <inheritdoc />
        public async Task<AiAnalysis> AddAsync(AiAnalysis analysis)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.AiAnalyses.Add(analysis);
            await context.SaveChangesAsync();
            return analysis;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(AiAnalysis analysis)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Update(analysis);
            await context.SaveChangesAsync();
        }
    }
}
