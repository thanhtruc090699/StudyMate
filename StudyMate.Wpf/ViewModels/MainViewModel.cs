using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyMate.Wpf.ViewModels
{
    /// <summary>
    /// Main application ViewModel that coordinates the primary view models for the StudyMate application.
    /// Acts as a container for FolderListViewModel, FileListViewModel, and FileDetailViewModel.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string applicationTitle = "StudyMate";

        /// <summary>
        /// Gets the ViewModel responsible for managing study folders list and creation.
        /// </summary>
        public FolderListViewModel FolderListViewModel { get; }

        /// <summary>
        /// Gets the ViewModel responsible for displaying and managing files within a selected folder.
        /// </summary>
        public FileListViewModel FileListViewModel { get; }

        /// <summary>
        /// Gets the ViewModel responsible for displaying detailed file analysis, summaries, and quiz content.
        /// </summary>
        public FileDetailViewModel FileDetailViewModel { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class with the specified child ViewModels.
        /// </summary>
        /// <param name="folderListViewModel">The FolderListViewModel instance for folder management.</param>
        /// <param name="fileListViewModel">The FileListViewModel instance for file management.</param>
        /// <param name="fileDetailViewModel">The FileDetailViewModel instance for file detail display.</param>
        public MainViewModel(
            FolderListViewModel folderListViewModel,
            FileListViewModel fileListViewModel,
            FileDetailViewModel fileDetailViewModel)
        {
            FolderListViewModel = folderListViewModel;
            FileListViewModel = fileListViewModel;
            FileDetailViewModel = fileDetailViewModel;
        }
    }
}
