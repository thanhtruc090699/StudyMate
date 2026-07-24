using System.IO;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <summary>
    /// Service for physical file storage operations.
    /// Manages saving and deleting files in the application's upload directory (AppData).
    /// Generates unique filenames by prefixing with GUID to prevent collisions.
    /// </summary>
    internal class FileStorageService : IFileStorageService
    {
        private readonly string _uploadPath;

        /// <summary>
        /// Initializes a new instance of the FileStorageService class.
        /// Creates the upload directory in %LOCALAPPDATA%\StudyMate\uploads if it doesn't exist.
        /// </summary>
        public FileStorageService()
        {
            // Use absolute path in AppData folder
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StudyMate",
                "uploads"
            );
            Directory.CreateDirectory(appDataPath);
            _uploadPath = appDataPath;
        }

        /// <summary>
        /// Saves a file stream to disk with a unique filename.
        /// Sanitizes the original filename by removing invalid path characters and prefixes with a GUID.
        /// Resets stream position to 0 if seekable before copying. Always saves to the configured upload path.
        /// </summary>
        /// <param name="fileStream">The file content stream. Must not be null or empty.</param>
        /// <param name="originalFileName">The original filename used for sanitization. Must not be null or whitespace.</param>
        /// <param name="folderPath">Ignored parameter (legacy). Files are always saved to the configured upload path.</param>
        /// <returns>The unique filename (not full path) assigned to the saved file.</returns>
        /// <exception cref="ArgumentException">Thrown when fileStream is null/empty, originalFileName or folderPath is null/whitespace.</exception>
        /// <exception cref="IOException">Thrown when file save operation fails.</exception>
        public async Task<string> SaveFileAsync(Stream fileStream, string originalFileName, string folderPath)
        {
            if(fileStream == null || fileStream.Length == 0)
            {
                throw new ArgumentException("File stream cannot be null or empty.", nameof(fileStream));
            }

            if(string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentException("Original file name cannot be null or whitespace.", nameof(originalFileName));
            }
            if(string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("Folder path cannot be null or whitespace.", nameof(folderPath));
            }

            var sanitizedFileName = Path.GetInvalidFileNameChars().Aggregate(originalFileName,
                (current, c) => current.Replace(c.ToString(), string.Empty));

            var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}";

            // Use the configured upload path
            var fullPath = Path.Combine(_uploadPath, uniqueFileName);
            
            try
            {
                if(fileStream.CanSeek)
                    fileStream.Position = 0;

                using (var fileStreamToSave = new FileStream(fullPath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileStreamToSave);
                }

                return uniqueFileName;

            }
            catch (Exception e)
            {
                throw new IOException($"Failed to save file '{originalFileName}': {e.Message}", e);
            }
        }

        /// <summary>
        /// Deletes a file from disk by its stored filename.
        /// Silently succeeds if the file does not exist.
        /// </summary>
        /// <param name="storedFileName">The unique filename (as returned by SaveFileAsync) to delete. Must not be null or whitespace.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when storedFileName is null or whitespace.</exception>
        public async Task DeleteFileAsync(string storedFileName)
        {
            if (string.IsNullOrWhiteSpace(storedFileName))
            {
                throw new ArgumentException("Stored file name cannot be null or whitespace.", nameof(storedFileName));
            }
            
            var filePath = Path.Combine(_uploadPath, storedFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
