using CommunityToolkit.Mvvm.ComponentModel;

namespace StudyMate.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string applicationTitle = "StudyMate";

    }
}
