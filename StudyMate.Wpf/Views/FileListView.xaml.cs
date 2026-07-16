using System.Windows;
using System.Windows.Controls;
using StudyMate.Wpf.Helpers;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf.Views
{
    public partial class FileListView : UserControl
    {
        public FileListView()
        {
            InitializeComponent();
        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is not FileListViewModel viewModel)
            {
                DebugLogger.Log("FileListView: DataContext is not FileListViewModel");
                return;
            }

            if (viewModel.SelectedFile is not StudyFile file)
            {
                DebugLogger.Log("FileListView: SelectedFile is null");
                return;
            }

            DebugLogger.Log($"FileListView: Opening file '{file.OriginalFileName}'");
            DebugLogger.Log($"FileListView: ViewModel instance hash = {viewModel.GetHashCode()}");

            if (viewModel.OpenFileCommand.CanExecute(file))
            {
                DebugLogger.Log("FileListView: OpenFileCommand.CanExecute = true");
                viewModel.OpenFileCommand.Execute(file);
            }
            else
            {
                DebugLogger.Log("FileListView: OpenFileCommand.CanExecute = false");
            }
        }
    }
}
