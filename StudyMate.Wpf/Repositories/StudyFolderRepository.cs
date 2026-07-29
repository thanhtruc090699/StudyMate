using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class StudyFolderRepository : IStudyFolderRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public StudyFolderRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        public async Task<List<StudyFolder>> GetAllAsync()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.StudyFolders
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<StudyFolder> AddAsync(StudyFolder folder)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.StudyFolders.Add(folder);
            await context.SaveChangesAsync();
            return folder;
        }
    }
}
