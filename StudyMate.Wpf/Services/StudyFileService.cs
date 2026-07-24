using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <inheritdoc />
    public class StudyFileService : IStudyFileService
    {
        private readonly IStudyFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;

        public StudyFileService(IStudyFileRepository repository, IFileStorageService fileStorageService)
        {
            _fileRepository = repository;
            _fileStorageService = fileStorageService;
        }

        /// <inheritdoc />
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
                contentType = "application/octet-stream";
            }

            var fileSize = fileStream.Length;
            
            var storedFileName = await _fileStorageService.SaveFileAsync(fileStream, fileName, "./uploads");

            var studyFile = new StudyFile
            {
                FolderId = folderId,
                OriginalFileName = fileName,
                StoredFileName = storedFileName,
                FilePath = storedFileName,
                FileExtension = Path.GetExtension(fileName),
                ContentType = contentType,
                FileSizeBytes = fileSize,
                UploadedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _fileRepository.AddAsync(studyFile);
        }

        /// <inheritdoc />
        public async Task<StudyFile?> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetStudyFileAsync(id);
        }

        /// <inheritdoc />
        public async Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId)
        {
            return await _fileRepository.GetByFolderIdAsync(folderId);
        }

        /// <inheritdoc />
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
    }
}
