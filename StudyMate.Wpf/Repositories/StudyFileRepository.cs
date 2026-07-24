using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <inheritdoc />
    public class StudyFileRepository : IStudyFileRepository
    {
        private readonly AppDbContext _dbContext;

        public StudyFileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<StudyFile?> GetStudyFileAsync(int id)
        {
            return await _dbContext.StudyFiles.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<List<StudyFile>> GetByFolderIdAsync(int folderId)
        {
            return await _dbContext.StudyFiles
                .Where(f => f.FolderId == folderId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<StudyFile> AddAsync(StudyFile studyFile)
        {
            try
            {
                _dbContext.StudyFiles.Add(studyFile);
                await _dbContext.SaveChangesAsync();
                return studyFile;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var file = await _dbContext.StudyFiles.FindAsync(id);
            if (file != null)
            {
                _dbContext.StudyFiles.Remove(file);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
