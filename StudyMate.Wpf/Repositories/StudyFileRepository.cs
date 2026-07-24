using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <summary>
    /// Repository for managing study file data operations.
    /// </summary>
    public class StudyFileRepository : IStudyFileRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudyFileRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for data access.</param>
        public StudyFileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file.</param>
        /// <returns>The study file if found; otherwise, null.</returns>
        public async Task<StudyFile?> GetStudyFileAsync(int id)
        {
            return await _dbContext.StudyFiles.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all study files associated with a specific folder.
        /// </summary>
        /// <param name="folderId">The folder identifier to filter by.</param>
        /// <returns>A list of study files belonging to the specified folder.</returns>
        public async Task<List<StudyFile>> GetByFolderIdAsync(int folderId)
        {
            return await _dbContext.StudyFiles
                .Where(f => f.FolderId == folderId)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new study file to the database.
        /// </summary>
        /// <param name="studyFile">The study file to add.</param>
        /// <returns>The added study file with generated properties populated.</returns>
        /// <exception cref="Exception">Thrown when database operation fails.</exception>
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

        /// <summary>
        /// Deletes a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the study file to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
