using System.IO;
using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IStudyFileService
    {
        Task<StudyFile> UploadFileAsync(int folderId, Stream fileStream, string fileName, string contentType);
        Task<StudyFile> GetFilesByIdAsync(int id);
        Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId);
        Task DeleteFileAsync(int id);
        Task UpdateFileAsync(int id, Stream newFileStream, string newFileName, string contentType);
    }
}
