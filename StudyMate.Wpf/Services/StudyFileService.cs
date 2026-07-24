using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <summary>
    /// Service for managing study file operations including upload, retrieval, and deletion.
    /// Coordinates file storage and repository operations to persist file metadata and content.
    /// </summary>
    public class StudyFileService : IStudyFileService
    {
        private readonly IStudyFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;

        /// <summary>
        /// Initializes a new instance of the StudyFileService class.
        /// </summary>
        /// <param name="repository">The repository for file data access.</param>
        /// <param name="fileStorageService">Service for physical file storage operations.</param>
        public StudyFileService(IStudyFileRepository repository, IFileStorageService fileStorageService)
        {
            _fileRepository = repository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Uploads a file to the specified folder and persists its metadata.
        /// Saves the physical file via FileStorageService and creates a database record.
        /// Uses default content type "application/octet-stream" if none is provided.
        /// </summary>
        /// <param name="folderId">The ID of the folder to upload the file to.</param>
        /// <param name="fileStream">The file content stream. Must not be null or empty.</param>
        /// <param name="fileName">The original file name. Must not be null or whitespace.</param>
        /// <param name="contentType">The MIME content type. Defaults to application/octet-stream if empty.</param>
        /// <returns>The created StudyFile record with metadata including stored file path and size.</returns>
        /// <exception cref="ArgumentException">Thrown when fileStream is null/empty or fileName is null/whitespace.</exception>
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

            var fileSize = fileStream.Length;
            
            // Save file first
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

        /// <summary>
        /// Retrieves a study file by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the file.</param>
        /// <returns>The study file if found; otherwise, null.</returns>
        public async Task<StudyFile?> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetStudyFileAsync(id);
        }

        /// <summary>
        /// Retrieves all study files belonging to the specified folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder.</param>
        /// <returns>A list of all study files in the folder.</returns>
        public async Task<List<StudyFile>> GetFilesByFolderIdAsync(int folderId)
        {
            return await _fileRepository.GetByFolderIdAsync(folderId);
        }

        /// <summary>
        /// Deletes a study file by its ID, removing both the database record and the physical file.
        /// Throws an exception if the file does not exist.
        /// </summary>
        /// <param name="id">The ID of the file to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the file with the specified ID does not exist.</exception>
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
