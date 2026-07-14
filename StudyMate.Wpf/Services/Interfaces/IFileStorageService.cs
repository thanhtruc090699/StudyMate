using System.IO;
using StudyMate.Wpf.Models;


namespace StudyMate.Wpf.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath);
        Task<byte[]> GetFileAsync(string storedFileName);
        Task DeleteFileAsync(string storedFileName);
        Task<bool> FileExistsAsync(string storedFileName);

    }
}
