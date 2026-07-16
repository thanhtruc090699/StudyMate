using System.Windows;
using System.Windows.Controls;
using StudyMate.Wpf.Helpers;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf.Views
{
    public partial class FileDetailView : UserControl
    {
        public FileDetailView()
        {
            InitializeComponent();
            Loaded += FileDetailView_Loaded;
        }

        private void FileDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is FileDetailViewModel vm)
            {
                DebugLogger.Log($"FileDetailView: DataContext loaded, instance hash = {vm.GetHashCode()}");
                DebugLogger.Log($"FileDetailView: SelectedFile = {vm.SelectedFile?.OriginalFileName ?? "null"}");
            }
            else
            {
                DebugLogger.Log($"FileDetailView: DataContext is NOT FileDetailViewModel, it is: {DataContext?.GetType().Name ?? "null"}");
            }
        }
    }
}
