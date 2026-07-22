using System.Windows;
using System.Windows.Controls;
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
                return;
            }

            if (viewModel.SelectedFile is not StudyFile file)
            {
                return;
            }

            if (viewModel.OpenFileCommand.CanExecute(file))
            {
                viewModel.OpenFileCommand.Execute(file);
            }
        }
    }
}
