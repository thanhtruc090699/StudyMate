using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using StudyMate.Wpf.ViewModels;
using StudyMate.Wpf.Views;
using StudyMate.Wpf.Services;

namespace StudyMate.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FolderListView _folderListView;
        private readonly FileListView _fileListView;

        public MainWindow()
        {
            InitializeComponent();
            
            // Load views from DI container
            _folderListView = App.Services.GetRequiredService<FolderListView>();
            _fileListView = App.Services.GetRequiredService<FileListView>();

            FolderListControl.Content = _folderListView;
            FileListControl.Content = _fileListView;

            // Initialize asynchronously
            InitializeViewsAsync();
        }

        private async void InitializeViewsAsync()
        {
            await Task.Delay(100); // Small delay to ensure UI is ready
            
            if (_folderListView.DataContext is FolderListViewModel folderViewModel)
            {
                await folderViewModel.LoadAsync();
            }

            _fileListView.InitializeAsync();
        }
    }
}