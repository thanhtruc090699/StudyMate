using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;

namespace StudyMate.Wpf.Repositories
{
    public class StudyFileRepository : IStudyFileRepository
    {
        private readonly AppDbContext _dbContext;
        public StudyFileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StudyFile> GetStudyFileAsync(int id)
        {
            return await _dbContext.StudyFiles.FindAsync(id);
        }
        public async Task<List<StudyFile>> GetByFolderIdAsync(int folderId)
        {
            return await _dbContext.StudyFiles
                .Where(f => f.FolderId == folderId)
                .ToListAsync();
        }
        public async Task<StudyFile> AddAsync(StudyFile studyFile)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== Adding file to DB ===");
                System.Diagnostics.Debug.WriteLine($"FolderId={studyFile.FolderId}, OriginalFileName={studyFile.OriginalFileName}");
                System.Diagnostics.Debug.WriteLine($"StoredFileName={studyFile.StoredFileName}, FilePath={studyFile.FilePath}");
                System.Diagnostics.Debug.WriteLine($"FileExtension={studyFile.FileExtension}, ContentType={studyFile.ContentType}");
                System.Diagnostics.Debug.WriteLine($"FileSizeBytes={studyFile.FileSizeBytes}");
                
                _dbContext.StudyFiles.Add(studyFile);
                System.Diagnostics.Debug.WriteLine($"Calling SaveChangesAsync...");
                
                var result = await _dbContext.SaveChangesAsync();
                
                System.Diagnostics.Debug.WriteLine($"Saved {result} entities. File ID: {studyFile.Id}");
                return studyFile;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {dbEx.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {dbEx.StackTrace}");
                if (dbEx.InnerException?.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner inner exception: {dbEx.InnerException.InnerException.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding file to DB: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
        public async Task UpdateAsync(StudyFile file)
        {
            _dbContext.StudyFiles.Update(file);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var file = await _dbContext.StudyFiles.FindAsync(id);
            if (file != null)
            {
                _dbContext.StudyFiles.Remove(file);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.StudyFiles.AnyAsync(f => f.Id == id);
        }
    }
}
