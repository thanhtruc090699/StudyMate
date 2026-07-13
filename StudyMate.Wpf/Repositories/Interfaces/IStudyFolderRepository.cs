using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Repositories.Interfaces;

public interface IStudyFolderRepository
{
    Task<List<StudyFolder>> GetAllAsync();

    Task<StudyFolder> AddAsync(StudyFolder folder);
}