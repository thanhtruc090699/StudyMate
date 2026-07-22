using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Repositories.Interfaces
{
    public interface IStudyFileRepository
    {
        Task<StudyFile?> GetStudyFileAsync(int id);
        Task<List<StudyFile>> GetByFolderIdAsync(int FolderId);
        Task<StudyFile> AddAsync(StudyFile studyFile);
        Task DeleteAsync(int id);
    }
}
