using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class StudyFileRepository : IStudyFileRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public StudyFileRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        public async Task<StudyFile?> GetStudyFileAsync(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.StudyFiles.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<List<StudyFile>> GetByFolderIdAsync(int folderId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.StudyFiles
                .Where(f => f.FolderId == folderId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<StudyFile> AddAsync(StudyFile studyFile)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.StudyFiles.Add(studyFile);
            await context.SaveChangesAsync();
            return studyFile;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var file = await context.StudyFiles.FindAsync(id);
            if (file != null)
            {
                context.StudyFiles.Remove(file);
                await context.SaveChangesAsync();
            }
        }
    }
}
