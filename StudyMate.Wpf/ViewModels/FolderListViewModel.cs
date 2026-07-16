using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudyMate.Wpf.ViewModels
{
    public partial class FolderListViewModel : ObservableObject
    {
        private readonly IStudyFolderService _folderService;
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
            FileListViewModel fileListViewModel)
        {
            _folderService = folderService;
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

        public async Task LoadAsync()
        {
            var result = await _folderService.GetAllFoldersAsync();
            Folders = new ObservableCollection<StudyFolder>(result);
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
