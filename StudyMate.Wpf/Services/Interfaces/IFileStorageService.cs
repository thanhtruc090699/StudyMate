using System.IO;
using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath);
        Task DeleteFileAsync(string storedFileName);
    }
}
