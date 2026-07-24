using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    /// <summary>
    /// Repository for managing study folder data operations.
    /// </summary>
    public class StudyFolderRepository : IStudyFolderRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudyFolderRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for data access.</param>
        public StudyFolderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all study folders from the database, ordered by creation date (newest first).
        /// </summary>
        /// <returns>A list of all study folders.</returns>
        public async Task<List<StudyFolder>> GetAllAsync()
        {
            return await _dbContext.StudyFolders
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new study folder to the database.
        /// </summary>
        /// <param name="folder">The study folder to add.</param>
        /// <returns>The added study folder with generated properties populated.</returns>
        public async Task<StudyFolder> AddAsync(StudyFolder folder)
        {
            _dbContext.StudyFolders.Add(folder);
            await _dbContext.SaveChangesAsync();
            return folder;
        }
    }
}
