using StudyMate.Wpf.Models;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.Services
{
    /// <inheritdoc />
    public class StudyFolderService : IStudyFolderService
    {
        private readonly IStudyFolderRepository _folderRepository;

        public StudyFolderService(IStudyFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        /// <inheritdoc />
        public async Task<List<StudyFolder>> GetAllFoldersAsync()
        {
            return await _folderRepository.GetAllAsync();
        }

        /// <inheritdoc />
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
