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
                errorMessage = null;
                isLoading = true;

                var createdFolder = await _folderService.CreateFolderAsync(newFolderName);

                Folders.Insert(0, createdFolder);

                NewFolderName = string.Empty;



            }
            catch (Exception ex) {

                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
