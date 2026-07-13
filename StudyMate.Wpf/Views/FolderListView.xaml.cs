using System.Windows.Controls;
using StudyMate.Wpf.ViewModels;

namespace StudyMate.Wpf.Views
{
    public partial class FolderListView : UserControl
    {
        public FolderListView(FolderListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += async (_, _) => await viewModel.LoadAsync();

        }
    }
}
