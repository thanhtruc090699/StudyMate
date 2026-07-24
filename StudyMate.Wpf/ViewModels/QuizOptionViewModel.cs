namespace StudyMate.Wpf.ViewModels;

public class QuizOptionViewModel : ViewModelBase
{
    private string _text = string.Empty;
    private bool _isSelected;
    private QuizQuestionViewModel? _parentQuestion;

    public QuizOptionViewModel()
    {
    }

    public QuizOptionViewModel(QuizQuestionViewModel parent)
    {
        _parentQuestion = parent;
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }

    public bool IsCorrect { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            
            _isSelected = value;
            OnPropertyChanged();
            
            if (value && _parentQuestion != null)
            {
                _parentQuestion.SelectedOption = this;
            }
        }
    }

    public void SetSelectedWithoutTrigger(bool isSelected)
    {
        if (_isSelected == isSelected) return;
        
        _isSelected = isSelected;
        OnPropertyChanged(nameof(IsSelected));
    }

    public void Refresh()
    {
        OnPropertyChanged(nameof(IsSelected));
    }
    
    public void SetParent(QuizQuestionViewModel parent)
    {
        _parentQuestion = parent;
    }
}
