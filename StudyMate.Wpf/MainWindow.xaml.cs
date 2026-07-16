using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using StudyMate.Wpf.ViewModels;
using StudyMate.Wpf.Views;

namespace StudyMate.Wpf
{
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

            // Add views to grid
            MainGrid.Children.Add(_folderListView);
            Grid.SetColumn(_folderListView, 0);

            var fileBorder = new Border
            {
                Child = _fileListView,
                BorderBrush = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#E5E7EB"),
                BorderThickness = new Thickness(1, 0, 0, 0)
            };
            MainGrid.Children.Add(fileBorder);
            Grid.SetColumn(fileBorder, 1);

            // Initialize asynchronously
            InitializeViewsAsync();
        }

        private async void InitializeViewsAsync()
        {
            await Task.Delay(100);
            
            if (DataContext is MainViewModel mainViewModel)
            {
                await mainViewModel.FolderListViewModel.LoadAsync();
                await mainViewModel.FileListViewModel.LoadAsync();
            }
        }
    }
}
