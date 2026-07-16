using System.Windows;
using System.Windows.Controls;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf.Views
{
    public partial class FileListView : UserControl
    {
        private readonly FileListViewModel _viewModel;

        public FileListView(FileListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        public async void InitializeAsync()
        {
            await _viewModel.LoadAsync();
        }

        private void FileMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is StudyFile file)
            {
                _viewModel.SelectedFile = file;
            }
        }
    }
}
