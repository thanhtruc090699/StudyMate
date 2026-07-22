using StudyMate.Wpf.Models;
using StudyMate.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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

        private void MoreButton_PreviewMouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (sender is not Button button)
            {
                return;
            }

            if (button.ContextMenu is null)
            {
                return;
            }

            button.ContextMenu.DataContext = DataContext;

            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = PlacementMode.Bottom;

            button.ContextMenu.IsOpen = true;

            e.Handled = true;
        }
    }
}
