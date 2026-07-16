using System.Windows.Controls;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf.Views
{
    public partial class FolderListView : UserControl
    {
        private readonly FolderListViewModel _viewModel;

        public FolderListView(FolderListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            Loaded += async (_, _) => await viewModel.LoadAsync();
        }
    }
}
