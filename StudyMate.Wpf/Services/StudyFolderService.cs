using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <summary>
    /// Service for managing study folder operations.
    /// Provides methods for retrieving and creating study folders.
    /// </summary>
    public class StudyFolderService : IStudyFolderService
    {
        private readonly IStudyFolderRepository _folderRepository;

        /// <summary>
        /// Initializes a new instance of the StudyFolderService class.
        /// </summary>
        /// <param name="folderRepository">The repository for folder data access.</param>
        public StudyFolderService(IStudyFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        /// <summary>
        /// Retrieves all study folders from the repository.
        /// </summary>
        /// <returns>A list of all study folders.</returns>
        public async Task<List<StudyFolder>> GetAllFoldersAsync()
        {
            return await _folderRepository.GetAllAsync();
        }

        /// <summary>
        /// Creates a new study folder with the specified name.
        /// Validates that the folder name is not null or whitespace and trims it before saving.
        /// Sets CreatedAt and UpdatedAt timestamps to the current UTC time.
        /// </summary>
        /// <param name="folderName">The name of the folder to create.</param>
        /// <returns>The created study folder with assigned ID and timestamps.</returns>
        /// <exception cref="InvalidOperationException">Thrown when folder name is null, empty, or whitespace.</exception>
        public async Task<StudyFolder> CreateFolderAsync(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                throw new InvalidOperationException("Folder name is required");

            var folder = new StudyFolder
            {
                Name = folderName.Trim(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _folderRepository.AddAsync(folder);

        }

    }
}
