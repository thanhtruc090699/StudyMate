using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace StudyMate.Wpf.ViewModels;

/// <summary>
/// ViewModel representing a single quiz question with multiple choice options.
/// Manages question text, explanation, option selection, and submission state.
/// Coordinates with QuizOptionViewModel instances to track user answers.
/// </summary>
public class QuizQuestionViewModel : ViewModelBase
{
    private string _questionText = string.Empty;
    private string _explanation = string.Empty;
    private QuizOptionViewModel? _selectedOption;
    private bool _isSubmitted;

    /// <summary>
    /// Initializes a new instance of the QuizQuestionViewModel class.
    /// </summary>
    public QuizQuestionViewModel()
    {
        SelectOptionCommand = new RelayCommand<QuizOptionViewModel>(SelectOption);
    }

    /// <summary>
    /// Gets or sets the question text displayed to the user.
    /// </summary>
    public string QuestionText
    {
        get => _questionText;
        set
        {
            _questionText = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the explanation shown after the user submits an answer.
    /// </summary>
    public string Explanation
    {
        get => _explanation;
        set
        {
            _explanation = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the collection of answer options for this question.
    /// </summary>
    public ObservableCollection<QuizOptionViewModel> Options { get; set; }
        = new();

    /// <summary>
    /// Gets or sets the currently selected option. Updates HasSelectedOption when changed.
    /// </summary>
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
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user has submitted an answer for this question.
    /// When set, updates IsCorrect, ResultText, and CanSelectOption properties.
    /// </summary>
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
        }
    }

    /// <summary>
    /// Gets a value indicating whether an option has been selected.
    /// </summary>
    public bool HasSelectedOption => SelectedOption is not null;

    /// <summary>
    /// Gets a value indicating whether the submitted answer is correct.
    /// Returns true only if submitted and the selected option is marked as correct.
    /// </summary>
    public bool IsCorrect => IsSubmitted && SelectedOption?.IsCorrect == true;

    /// <summary>
    /// Gets the result text to display after submission ("Correct!" or "Incorrect").
    /// Returns empty string if not yet submitted.
    /// </summary>
    public string ResultText =>
        !IsSubmitted
            ? string.Empty
            : IsCorrect
                ? "Correct!"
                : "Incorrect";

    /// <summary>
    /// Gets a value indicating whether the user can still select an option.
    /// Selection is disabled after submission.
    /// </summary>
    public bool CanSelectOption => !IsSubmitted;

    /// <summary>
    /// Gets the command to select an option. Bound from the view.
    /// </summary>
    public ICommand SelectOptionCommand { get; }

    /// <summary>
    /// Selects the specified option by marking it as selected and deselecting all others.
    /// Does nothing if the option is null or if the question has already been submitted.
    /// </summary>
    /// <param name="option">The option to select.</param>
    private void SelectOption(QuizOptionViewModel? option)
    {
        if (option is null || IsSubmitted)
        {
            return;
        }

        foreach (var item in Options)
        {
            item.SetSelectedWithoutTrigger(item == option);
        }

        SelectedOption = option;
    }
}
