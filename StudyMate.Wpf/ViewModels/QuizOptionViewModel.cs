namespace StudyMate.Wpf.ViewModels;

/// <summary>
/// ViewModel representing a single option in a multiple-choice quiz question.
/// Tracks selection state and correctness, and notifies the parent question when selected.
/// </summary>
public class QuizOptionViewModel : ViewModelBase
{
    private string _text = string.Empty;
    private bool _isSelected;
    private QuizQuestionViewModel? _parentQuestion;

    /// <summary>
    /// Initializes a new instance of the QuizOptionViewModel class without a parent reference.
    /// Use SetParent to associate with a parent question if needed.
    /// </summary>
    public QuizOptionViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the QuizOptionViewModel class with a parent question reference.
    /// </summary>
    /// <param name="parent">The parent QuizQuestionViewModel that owns this option.</param>
    public QuizOptionViewModel(QuizQuestionViewModel parent)
    {
        _parentQuestion = parent;
    }

    /// <summary>
    /// Gets or sets the text content of this option displayed to the user.
    /// </summary>
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option is the correct answer.
    /// </summary>
    public bool IsCorrect { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this option is currently selected by the user.
    /// When set to true, notifies the parent question to update its SelectedOption property.
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            
            _isSelected = value;
            OnPropertyChanged();
            
            // Notify parent question when selected
            if (value && _parentQuestion != null)
            {
                _parentQuestion.SelectedOption = this;
            }
        }
    }

    /// <summary>
    /// Sets the selected state without triggering the parent question notification.
    /// Used for bulk selection updates during navigation or reset operations.
    /// </summary>
    /// <param name="isSelected">True to mark as selected; false otherwise.</param>
    public void SetSelectedWithoutTrigger(bool isSelected)
    {
        if (_isSelected == isSelected) return;
        
        _isSelected = isSelected;
        OnPropertyChanged(nameof(IsSelected));
    }

    /// <summary>
    /// Raises PropertyChanged for IsSelected to refresh UI state.
    /// Called after answer submission to update visual feedback (colors, icons).
    /// </summary>
    public void Refresh()
    {
        OnPropertyChanged(nameof(IsSelected));
    }
    
    /// <summary>
    /// Sets the parent question reference for this option.
    /// Use this method when the option was created without a parent constructor parameter.
    /// </summary>
    /// <param name="parent">The parent QuizQuestionViewModel.</param>
    public void SetParent(QuizQuestionViewModel parent)
    {
        _parentQuestion = parent;
    }
}
