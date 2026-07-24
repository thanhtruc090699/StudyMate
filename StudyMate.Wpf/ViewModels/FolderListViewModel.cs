using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudyMate.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel for managing study folders - creation, listing, and selection.
    /// Triggers file loading when a folder is selected.
    /// </summary>
    public partial class FolderListViewModel : ObservableObject
    {
        private readonly IStudyFolderService _folderService;
        private readonly IStudyFileService _fileService;
        private readonly FileListViewModel _fileListViewModel;

        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();
        
        [ObservableProperty] private string newFolderName = string.Empty;
        
        [ObservableProperty] private string? errorMessage;
        
        [ObservableProperty] private bool isLoading;
        
        [ObservableProperty] private bool isCreateFolderVisible;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanOpenFolder))]
        private StudyFolder? selectedFolder;

        public bool CanOpenFolder => SelectedFolder != null;

        public ICommand ShowCreateFolderCommand { get; }
        
        public ICommand CancelCreateFolderCommand { get; }

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

        private async void FolderListViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedFolder) && SelectedFolder != null)
            {
                await _fileListViewModel.LoadFolderAsync(SelectedFolder);
            }
        }

        /// <summary>
        /// Loads all folders with their file counts.
        /// </summary>
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

        private void ShowCreateFolderForm()
        {
            ErrorMessage = string.Empty;
            NewFolderName = string.Empty;
            IsCreateFolderVisible = true;
        }

        private void CancelCreateFolder()
        {
            NewFolderName = string.Empty;
            ErrorMessage = string.Empty;
            IsCreateFolderVisible = false;
        }

        /// <summary>
        /// Creates a new folder with validation. Inserts at top of list on success.
        /// </summary>
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
