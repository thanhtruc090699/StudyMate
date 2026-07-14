using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    public class StudyFileService : IStudyFileService
    {
        private readonly IStudyFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;

        public StudyFileService(IStudyFileRepository repository, IFileStorageService fileStorageService)
        {
            _fileRepository = repository;
            _fileStorageService = fileStorageService;
        }

        public async Task<StudyFile> UploadFileAsync(int folderId, Stream fileStream, string fileName, string contentType)
        {
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new ArgumentException("File stream cannot be null or empty.", nameof(fileStream));
            }

            if(string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name cannot be null or whitespace.", nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "application/octet-stream"; // Default content type
            }

            var storedFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            var filePath = await _fileStorageService.SaveFileAsync(fileStream, fileName, "./uploads");

            var studyFile = new StudyFile
            {
                FolderId = folderId,
                OriginalFileName = fileName,
                StoredFileName = storedFileName,
                FilePath = filePath,
                FileExtension = Path.GetExtension(fileName),
                ContentType = contentType,
                FileSizeBytes = fileStream.Length,
                UploadedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };



            return await _fileRepository.AddAsync(studyFile);
        }

        public async Task<StudyFile> GetFilesByIdAsync(int id)
        {
            return await _fileRepository.GetStudyFileAsync(id);
        }

        public async Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId)
        {
            return await _fileRepository.GetByFolderIdAsync(folderId);
        }
        public async Task DeleteFileAsync(int id)
        {
            var file = await _fileRepository.GetStudyFileAsync(id);
            if (file == null)
            {
                throw new InvalidOperationException($"File with ID {id} does not exist.");
            }
            await _fileStorageService.DeleteFileAsync(file.StoredFileName);
            await _fileRepository.DeleteAsync(id);
        }
        public async Task UpdateFileAsync(int id, Stream newFileStream, string newFileName, string contentType)
        {
            if(newFileStream == null || newFileStream.Length == 0)
            {
                throw new ArgumentException("File stream cannot be null or empty.", nameof(newFileStream));
            }
            if(string.IsNullOrWhiteSpace(newFileName))
            {
                throw new ArgumentException("File name cannot be null or whitespace.", nameof(newFileName));
            }

            if(string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "application/octet-stream"; // Default content type
            }

            var existingFile = await _fileRepository.GetStudyFileAsync(id);
            if (existingFile == null)
            {
                throw new InvalidOperationException($"File with ID {id} does not exist.");
            }

            await _fileStorageService.DeleteFileAsync(existingFile.StoredFileName);

            var storedFileName = Guid.NewGuid().ToString() + Path.GetExtension(newFileName);
            var filePath = await _fileStorageService.SaveFileAsync(newFileStream, newFileName, "./uploads");
            existingFile.OriginalFileName = newFileName;
            existingFile.StoredFileName = storedFileName;
            existingFile.FilePath = filePath;
            existingFile.FileExtension = Path.GetExtension(newFileName);
            existingFile.FileSizeBytes = newFileStream.Length;
            existingFile.ContentType = contentType;
            existingFile.UpdatedAt = DateTime.UtcNow;
            await _fileRepository.UpdateAsync(existingFile);
        }

    }
}
