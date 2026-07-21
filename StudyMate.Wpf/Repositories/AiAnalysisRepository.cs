using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    public class AiAnalysisRepository : IAiAnalysisRepository
    {
        private readonly AppDbContext _dbContext;

        public AiAnalysisRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AiAnalysis> GetByIdAsync(int id)
        {
            return await _dbContext.AiAnalyses.FindAsync(id);
        }

        public async Task<AiAnalysis> GetLatestByStudyFileIdAsync(int studyFileId)
        {
            return await _dbContext.AiAnalyses.Where(x=> x.StudyFileId == studyFileId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<AiAnalysis>> GetByStudyFileIdAsync(int studyFileId)
        {
            return await _dbContext.AiAnalyses.Where(x => x.StudyFileId == studyFileId)
                .OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<AiAnalysis> AddAsync(AiAnalysis analysis)
        {
            _dbContext.AiAnalyses.Add(analysis);
            await _dbContext.SaveChangesAsync();
            return analysis;
        }

        public async Task UpdateAsync(AiAnalysis analysis)
        {
            _dbContext.Update(analysis);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AiAnalysis analysis)
        {
            _dbContext.AiAnalyses.Remove(analysis);
            await _dbContext.SaveChangesAsync();
        }

    }
}
