using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyMate.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string applicationTitle = "StudyMate";

        public FolderListViewModel FolderListViewModel { get; }
        public FileListViewModel FileListViewModel { get; }

        public MainViewModel(
            FolderListViewModel folderListViewModel,
            FileListViewModel fileListViewModel)
        {
            FolderListViewModel = folderListViewModel;
            FileListViewModel = fileListViewModel;
        }
    }
}
