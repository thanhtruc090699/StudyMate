using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <summary>
    /// Repository for managing AI analysis data operations.
    /// </summary>
    public class AiAnalysisRepository : IAiAnalysisRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AiAnalysisRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for data access.</param>
        public AiAnalysisRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves the most recent AI analysis for a specific study file.
        /// </summary>
        /// <param name="studyFileId">The study file identifier to search by.</param>
        /// <returns>The latest AI analysis if found; otherwise, null.</returns>
        public async Task<AiAnalysis?> GetLatestByStudyFileIdAsync(int studyFileId)
        {
            var result = await _dbContext.AiAnalyses.Where(x=> x.StudyFileId == studyFileId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
            
            return result;
        }

        /// <summary>
        /// Adds a new AI analysis to the database.
        /// </summary>
        /// <param name="analysis">The AI analysis to add.</param>
        /// <returns>The added AI analysis with generated properties populated.</returns>
        public async Task<AiAnalysis> AddAsync(AiAnalysis analysis)
        {
            _dbContext.AiAnalyses.Add(analysis);
            await _dbContext.SaveChangesAsync();
            return analysis;
        }

        /// <summary>
        /// Updates an existing AI analysis in the database.
        /// </summary>
        /// <param name="analysis">The AI analysis with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(AiAnalysis analysis)
        {
            _dbContext.Update(analysis);
            await _dbContext.SaveChangesAsync();
        }
    }
}
