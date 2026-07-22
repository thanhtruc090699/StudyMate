using System.Windows;
using System.Windows.Controls;
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
            // Load event handler
        }
    }
}
