using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudyMate.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing the collection of study folders.
    /// Handles folder creation, loading, and selection. When a folder is selected,
    /// it triggers the FileListViewModel to load the corresponding files.
    /// </summary>
    public partial class FolderListViewModel : ObservableObject
    {
        private readonly IStudyFolderService _folderService;
        private readonly IStudyFileService _fileService;
        private readonly FileListViewModel _fileListViewModel;

        /// <summary>
        /// Gets or sets the collection of study folders displayed in the UI.
        /// </summary>
        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();
        
        /// <summary>
        /// Gets or sets the name input for creating a new folder.
        /// </summary>
        [ObservableProperty] private string newFolderName = string.Empty;
        
        /// <summary>
        /// Gets or sets the error message to display when an operation fails.
        /// </summary>
        [ObservableProperty] private string? errorMessage;
        
        /// <summary>
        /// Gets or sets a value indicating whether an async operation is in progress.
        /// </summary>
        [ObservableProperty] private bool isLoading;
        
        /// <summary>
        /// Gets or sets a value indicating whether the create folder form is visible.
        /// </summary>
        [ObservableProperty] private bool isCreateFolderVisible;

        /// <summary>
        /// Gets or sets the currently selected folder. Changing this property triggers loading files for that folder.
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanOpenFolder))]
        private StudyFolder? selectedFolder;

        /// <summary>
        /// Gets a value indicating whether a folder is selected and can be opened.
        /// </summary>
        public bool CanOpenFolder => SelectedFolder != null;

        /// <summary>
        /// Gets the command to show the create folder form.
        /// </summary>
        public ICommand ShowCreateFolderCommand { get; }
        
        /// <summary>
        /// Gets the command to cancel folder creation and hide the form.
        /// </summary>
        public ICommand CancelCreateFolderCommand { get; }

        /// <summary>
        /// Initializes a new instance of the FolderListViewModel class.
        /// </summary>
        /// <param name="folderService">Service for folder operations.</param>
        /// <param name="fileService">Service for retrieving file counts per folder.</param>
        /// <param name="fileListViewModel">ViewModel to update when folder selection changes.</param>
        public FolderListViewModel(
            IStudyFolderService folderService,
            IStudyFileService fileService,
            FileListViewModel fileListViewModel)
        {
            _folderService = folderService;
            _fileService = fileService;
            _fileListViewModel = fileListViewModel;

            ShowCreateFolderCommand = new RelayCommand(ShowCreateFolderForm);
            CancelCreateFolderCommand = new RelayCommand(CancelCreateFolder);

            PropertyChanged += FolderListViewModel_PropertyChanged;
        }

        /// <summary>
        /// Handles property change events. When SelectedFolder changes, loads the files for that folder.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Property change event arguments.</param>
        private async void FolderListViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedFolder) && SelectedFolder != null)
            {
                await _fileListViewModel.LoadFolderAsync(SelectedFolder);
            }
        }

        /// <summary>
        /// Loads all folders from the service and populates their file counts.
        /// Sets IsLoading during the operation and ensures it is reset on completion or failure.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadAsync()
        {
            try
            {
                IsLoading = true;
                var result = await _folderService.GetAllFoldersAsync();
                
                foreach (var folder in result)
                {
                    var files = await _fileService.GetFilesByFolderIdAsync(folder.Id);
                    folder.FileCount = files.Count;
                }
                
                Folders = new ObservableCollection<StudyFolder>(result);
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Shows the create folder form by resetting error state and setting visibility flag.
        /// </summary>
        private void ShowCreateFolderForm()
        {
            ErrorMessage = string.Empty;
            NewFolderName = string.Empty;
            IsCreateFolderVisible = true;
        }

        /// <summary>
        /// Cancels folder creation by clearing the input and hiding the form.
        /// </summary>
        private void CancelCreateFolder()
        {
            NewFolderName = string.Empty;
            ErrorMessage = string.Empty;
            IsCreateFolderVisible = false;
        }

        /// <summary>
        /// Creates a new folder with the specified name and inserts it at the top of the list.
        /// Validates that the folder name is not empty. Trims whitespace before saving.
        /// Updates IsLoading during the operation and captures any exceptions as error messages.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand]
        private async Task CreateFolderAsync()
        {
            try
            {
                ErrorMessage = null;
                IsLoading = true;

                if (string.IsNullOrWhiteSpace(NewFolderName))
                {
                    ErrorMessage = "Please enter a folder name.";
                    return;
                }

                var createdFolder = await _folderService.CreateFolderAsync(NewFolderName.Trim());
                createdFolder.FileCount = 0;

                Folders.Insert(0, createdFolder);

                NewFolderName = string.Empty;
                IsCreateFolderVisible = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
