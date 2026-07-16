using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace StudyMate.Wpf.ViewModels;

public class QuizQuestionViewModel : ViewModelBase
{
    private string _questionText = string.Empty;
    private string _explanation = string.Empty;
    private QuizOptionViewModel? _selectedOption;
    private bool _isSubmitted;

    public QuizQuestionViewModel()
    {
        SelectOptionCommand = new RelayCommand<QuizOptionViewModel>(SelectOption);
    }

    public string QuestionText
    {
        get => _questionText;
        set
        {
            _questionText = value;
            OnPropertyChanged();
        }
    }

    public string Explanation
    {
        get => _explanation;
        set
        {
            _explanation = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<QuizOptionViewModel> Options { get; set; }
        = new();

    public QuizOptionViewModel? SelectedOption
    {
        get => _selectedOption;
        set
        {
            if (_selectedOption == value)
            {
                return;
            }

            _selectedOption = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasSelectedOption));
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public bool IsSubmitted
    {
        get => _isSubmitted;
        set
        {
            if (_isSubmitted == value)
            {
                return;
            }

            _isSubmitted = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsCorrect));
            OnPropertyChanged(nameof(ResultText));
            OnPropertyChanged(nameof(CanSelectOption));
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public bool HasSelectedOption => SelectedOption is not null;

    public bool IsCorrect => IsSubmitted && SelectedOption?.IsCorrect == true;

    public string ResultText =>
        !IsSubmitted
            ? string.Empty
            : IsCorrect
                ? "Correct!"
                : "Incorrect";

    public bool CanSelectOption => !IsSubmitted;

    public ICommand SelectOptionCommand { get; }

    private void SelectOption(QuizOptionViewModel? option)
    {
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", 
            $"[{System.DateTime.Now:HH:mm:ss}] SelectOption called: {option?.Text?.Substring(0, Math.Min(20, option.Text.Length))}\n");
        
        if (option is null || IsSubmitted)
        {
            return;
        }

        foreach (var item in Options)
        {
            item.IsSelected = item == option;
        }

        SelectedOption = option;
    }
}
