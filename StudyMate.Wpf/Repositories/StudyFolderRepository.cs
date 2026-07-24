using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class StudyFolderRepository : IStudyFolderRepository
    {
        private readonly AppDbContext _dbContext;

        public StudyFolderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<List<StudyFolder>> GetAllAsync()
        {
            return await _dbContext.StudyFolders
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<StudyFolder> AddAsync(StudyFolder folder)
        {
            _dbContext.StudyFolders.Add(folder);
            await _dbContext.SaveChangesAsync();
            return folder;
        }
    }
}
