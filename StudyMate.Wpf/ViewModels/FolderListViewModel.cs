using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;

namespace StudyMate.Wpf.ViewModels
{
    public partial class FolderListViewModel : ObservableObject
    {
        private readonly IStudyFolderService _folderService;

        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();

        [ObservableProperty] private string newFolderName = string.Empty;

        [ObservableProperty] private string? errorMessage;

        [ObservableProperty] private bool isLoading;

        public FolderListViewModel(IStudyFolderService folderService)
        {
            _folderService = folderService;

        }

        public async Task LoadAsync()
        {
            var result = await _folderService.GetAllFoldersAsync();
            Folders = new ObservableCollection<StudyFolder>(result);
        }

        [RelayCommand]
        private async Task CreateFolderAsync()
        {
            try
            {
                ErrorMessage = null;
                IsLoading = true;

                var createdFolder = await _folderService.CreateFolderAsync(NewFolderName);

                Folders.Insert(0, createdFolder);

                NewFolderName = string.Empty;
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
