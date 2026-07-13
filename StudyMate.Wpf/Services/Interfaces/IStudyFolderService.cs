using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IStudyFolderService
    {
        Task<List<StudyFolder>> GetAllFoldersAsync();
        Task<StudyFolder> CreateFolderAsync(string folderName);
    }
}
