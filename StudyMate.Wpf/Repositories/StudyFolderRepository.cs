using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    public class StudyFolderRepository : IStudyFolderRepository
    {
        private readonly AppDbContext _dbContext;

        public StudyFolderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StudyFolder>> GetAllAsync()
        {
            return await _dbContext.StudyFolders
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<StudyFolder> AddAsync(StudyFolder folder)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Adding folder to DB: Name={folder.Name}");
                _dbContext.StudyFolders.Add(folder);
                var result = await _dbContext.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"Saved {result} entities. Folder ID: {folder.Id}");
                return folder;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding folder to DB: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}
