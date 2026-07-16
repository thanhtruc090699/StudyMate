using System.Windows;
using System.Windows.Controls;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            InitializeAsync();
        }

        private async void InitializeAsync()
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
