using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyMate.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string applicationTitle = "StudyMate";

        public FolderListViewModel FolderListViewModel { get; }

        public FileListViewModel FileListViewModel { get; }

        public FileDetailViewModel FileDetailViewModel { get; }

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
