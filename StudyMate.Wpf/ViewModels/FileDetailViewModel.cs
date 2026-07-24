using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Integrations.Ai;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Models.Ai;
using StudyMate.Wpf.Services;
using StudyMate.Wpf.Services.Interfaces;

namespace StudyMate.Wpf.ViewModels;

/// <summary>
/// ViewModel responsible for displaying detailed analysis of a selected study file.
/// Manages summary display, key points extracted from AI analysis, and interactive quiz functionality.
/// Automatically generates or retrieves AI analysis when a file is selected.
/// </summary>
public class FileDetailViewModel : ViewModelBase
{

    private readonly IAiAnalysisService _aiAnalysisService;
    private StudyFile? _selectedFile;
    private string _summary = string.Empty;
    private int _selectedTabIndex;
    private bool _isLoading;
    private int _currentQuestionIndex;
    private string? _errorMessage;

    /// <summary>
    /// Gets or sets the error message to display when analysis generation fails.
    /// </summary>
    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Initializes a new instance of the FileDetailViewModel class.
    /// </summary>
    /// <param name="aiAnalysisService">Service for retrieving and generating AI analysis.</param>
    public FileDetailViewModel(IAiAnalysisService aiAnalysisService)
    {
        _aiAnalysisService = aiAnalysisService;

        KeyPoints = new ObservableCollection<string>();
        Questions = new ObservableCollection<QuizQuestionViewModel>();

        CloseCommand = new RelayCommand(CloseFile);
        PreviousQuestionCommand = new RelayCommand(
            PreviousQuestion,
            CanGoPrevious);

        NextQuestionCommand = new RelayCommand(
            NextQuestion,
            CanGoNext);

        CheckAnswerCommand = new RelayCommand(
            CheckAnswer,
            CanCheckAnswer);
    }

    /// <summary>
    /// Gets or sets the currently selected study file. Setting this property triggers
    /// asynchronous loading of AI analysis for the file. Resets the selected tab to 0.
    /// </summary>
    public StudyFile? SelectedFile
    {
        get => _selectedFile;
        set
        {
            if (_selectedFile == value)
            {
                return;
            }

            _selectedFile = value;
            OnPropertyChanged();

            if (_selectedFile != null)
            {
                _ = LoadAnalysisAsync(_selectedFile);
            }

            SelectedTabIndex = 0;
        }
    }

    /// <summary>
    /// Gets or sets the AI-generated summary of the selected file.
    /// </summary>
    public string Summary
    {
        get => _summary;
        set
        {
            if (_summary == value)
            {
                return;
            }

            _summary = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the collection of key points extracted from the AI analysis structured content.
    /// Each item represents a section title with optional content.
    /// </summary>
    public ObservableCollection<string> KeyPoints { get; }

    /// <summary>
    /// Gets the collection of quiz questions generated from the AI analysis.
    /// </summary>
    public ObservableCollection<QuizQuestionViewModel> Questions { get; }

    /// <summary>
    /// Gets or sets the index of the currently selected tab (0 = Summary/Key Points, 1 = Quiz).
    /// Changing the tab refreshes quiz command states.
    /// </summary>
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            if (_selectedTabIndex == value)
            {
                return;
            }

            _selectedTabIndex = value;
            OnPropertyChanged();
            
            RefreshQuizCommands();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether analysis is being loaded.
    /// </summary>
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading == value)
            {
                return;
            }

            _isLoading = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the index of the currently displayed quiz question.
    /// Updates CurrentQuestion and QuizProgressText properties when changed.
    /// </summary>
    public int CurrentQuestionIndex
    {
        get => _currentQuestionIndex;
        set
        {
            if (_currentQuestionIndex == value)
            {
                return;
            }

            _currentQuestionIndex = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(QuizProgressText));

            RefreshQuizCommands();
        }
    }

    /// <summary>
    /// Gets the currently displayed quiz question based on CurrentQuestionIndex.
    /// Returns null if there are no questions.
    /// </summary>
    public QuizQuestionViewModel? CurrentQuestion =>
        Questions.Count == 0
            ? null
            : Questions[CurrentQuestionIndex];

    /// <summary>
    /// Gets a text description of the current quiz progress (e.g., "Question 2 of 5").
    /// Returns "No quiz available" if there are no questions.
    /// </summary>
    public string QuizProgressText =>
        Questions.Count == 0
            ? "No quiz available"
            : $"Question {CurrentQuestionIndex + 1} of {Questions.Count}";

    /// <summary>
    /// Gets or sets a value indicating whether the user has submitted an answer for the current question.
    /// Used to control navigation between questions.
    /// </summary>
    public bool IsAnswerSubmitted { get; set; }

    /// <summary>
    /// Gets the command to close the current file and clear all displayed content.
    /// </summary>
    public ICommand CloseCommand { get; }

    /// <summary>
    /// Gets the command to navigate to the previous quiz question.
    /// Can execute only when on the Quiz tab and not at the first question.
    /// </summary>
    public IRelayCommand PreviousQuestionCommand { get; }

    /// <summary>
    /// Gets the command to navigate to the next quiz question.
    /// Can execute only when the current question is answered and not at the last question.
    /// </summary>
    public IRelayCommand NextQuestionCommand { get; }

    /// <summary>
    /// Gets the command to check the selected answer for the current question.
    /// Can execute only when on the Quiz tab, an option is selected, and the answer hasn't been submitted yet.
    /// </summary>
    public IRelayCommand CheckAnswerCommand { get; }

    /// <summary>
    /// Closes the current file by clearing all displayed content including summary, key points, and questions.
    /// Resets question index and answer submission state.
    /// </summary>
    private void CloseFile()
    {
        SelectedFile = null;
        Summary = string.Empty;

        KeyPoints.Clear();
        Questions.Clear();

        CurrentQuestionIndex = 0;
        IsAnswerSubmitted = false;
    }

    /// <summary>
    /// Notifies all quiz-related commands to re-evaluate their CanExecute conditions.
    /// Called when navigation or submission state changes.
    /// </summary>
    private void RefreshQuizCommands()
    {
        CheckAnswerCommand.NotifyCanExecuteChanged();
        PreviousQuestionCommand.NotifyCanExecuteChanged();
        NextQuestionCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Determines whether the check answer command can execute.
    /// Requires being on the Quiz tab, having selected an option, and not having submitted yet.
    /// </summary>
    /// <returns>True if the command can execute; otherwise, false.</returns>
    private bool CanCheckAnswer()
    {
        return SelectedTabIndex == 1 
               && CurrentQuestion?.SelectedOption is not null
               && CurrentQuestion.IsSubmitted == false;
    }

    /// <summary>
    /// Submits the selected answer for the current question and updates option display states.
    /// Marks the question as submitted and refreshes command states.
    /// </summary>
    private void CheckAnswer()
    {
        if (CurrentQuestion?.SelectedOption is null)
        {
            return;
        }

        CurrentQuestion.IsSubmitted = true;

        foreach (var option in CurrentQuestion.Options)
        {
            option.Refresh();
        }

        OnPropertyChanged(nameof(CurrentQuestion));
        RefreshQuizCommands();
    }

    /// <summary>
    /// Determines whether navigation to the previous question is allowed.
    /// Requires being on the Quiz tab and not being at the first question.
    /// </summary>
    /// <returns>True if navigation to the previous question is allowed; otherwise, false.</returns>
    private bool CanGoPrevious()
    {
        return SelectedTabIndex == 1 && CurrentQuestionIndex > 0;
    }

    /// <summary>
    /// Navigates to the previous question and resets its submission state.
    /// Clears any previously selected options on the target question.
    /// </summary>
    private void PreviousQuestion()
    {
        if (!CanGoPrevious())
        {
            return;
        }

        CurrentQuestionIndex--;
        
        var previousQuestion = CurrentQuestion;
        if (previousQuestion != null)
        {
            previousQuestion.IsSubmitted = false;
            foreach (var option in previousQuestion.Options)
            {
                option.SetSelectedWithoutTrigger(false);
            }
            previousQuestion.SelectedOption = null;
        }
        
        IsAnswerSubmitted = false;
        OnPropertyChanged(nameof(IsAnswerSubmitted));
        OnPropertyChanged(nameof(CurrentQuestion));
        RefreshQuizCommands();
    }

    /// <summary>
    /// Determines whether navigation to the next question is allowed.
    /// Requires being on the Quiz tab, having answered the current question, and not being at the last question.
    /// </summary>
    /// <returns>True if navigation to the next question is allowed; otherwise, false.</returns>
    private bool CanGoNext()
    {
        return SelectedTabIndex == 1 
               && CurrentQuestion?.IsSubmitted == true
               && CurrentQuestionIndex < Questions.Count - 1;
    }

    /// <summary>
    /// Navigates to the next question and resets its submission state.
    /// Clears any previously selected options on the target question.
    /// </summary>
    private void NextQuestion()
    {
        if (!CanGoNext())
        {
            return;
        }

        CurrentQuestionIndex++;
        
        var newQuestion = CurrentQuestion;
        if (newQuestion != null)
        {
            newQuestion.IsSubmitted = false;
            foreach (var option in newQuestion.Options)
            {
                option.SetSelectedWithoutTrigger(false);
            }
            newQuestion.SelectedOption = null;
        }
        
        IsAnswerSubmitted = false;
        OnPropertyChanged(nameof(IsAnswerSubmitted));
        OnPropertyChanged(nameof(CurrentQuestion));
        RefreshQuizCommands();
    }

    /// <summary>
    /// Loads or generates AI analysis for the specified study file.
    /// Retrieves existing analysis if available and completed; otherwise triggers generation.
    /// Updates IsLoading during the operation and captures exceptions to ErrorMessage.
    /// </summary>
    /// <param name="file">The study file to analyze.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task LoadAnalysisAsync(StudyFile file)
    {
        if (file == null) return;

        IsLoading = true;
        OnPropertyChanged(nameof(IsLoading));

        try
        {
            var analysis = await _aiAnalysisService.GetLatestAnalysisByStudyFileIdAsync(file.Id);
            
            if (analysis == null)
            {
                analysis = await _aiAnalysisService.GenerateAnalysisAsync(file.Id);
            }
            else if (analysis.Status != "Completed")
            {
                analysis = await _aiAnalysisService.GenerateAnalysisAsync(file.Id);
            }

            ApplyAnalysis(analysis);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Unable to generate study material: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    /// <summary>
    /// Applies the AI analysis results to the ViewModel properties.
    /// Populates Summary, KeyPoints, and Questions collections from the analysis data.
    /// Resets quiz state to the first question.
    /// </summary>
    /// <param name="analysis">The AI analysis result to apply.</param>
    private void ApplyAnalysis(AiAnalysis analysis)
    {
        Summary = analysis.Summary;

        LoadStructuredContent(analysis.StructuredContentJson);
        LoadQuiz(analysis.QuizJson);

        CurrentQuestionIndex = 0;
        IsAnswerSubmitted = false;

        OnPropertyChanged(nameof(CurrentQuestion));
        OnPropertyChanged(nameof(QuizProgressText));
        RefreshQuizCommands();
    }

    /// <summary>
    /// Parses the structured content JSON and populates the KeyPoints collection.
    /// Each section's title and content (if present) are combined into a single key point.
    /// Silently ignores parsing errors.
    /// </summary>
    /// <param name="structuredContentJson">JSON string containing structured content sections.</param>
    private void LoadStructuredContent(string? structuredContentJson)
    {
        KeyPoints.Clear();

        if (string.IsNullOrWhiteSpace(structuredContentJson))
        {
            return;
        }

        try
        {
            var structuredContent = JsonSerializer.Deserialize<StructuredContent>(structuredContentJson, AiClient.JsonOptions);

            if (structuredContent?.Sections != null)
            {
                foreach (var section in structuredContent.Sections)
                {
                    if (!string.IsNullOrWhiteSpace(section.Title))
                    {
                        var keyPoint = string.IsNullOrWhiteSpace(section.Content)
                            ? section.Title
                            : $"{section.Title}\n{section.Content}";
                        
                        KeyPoints.Add(keyPoint);
                    }
                }
            }
        }
        catch (Exception)
        {
            // Ignore parsing errors
        }
    }

    /// <summary>
    /// Parses the quiz JSON and populates the Questions collection with QuizQuestionViewModel instances.
    /// Subscribes to property changes on each question to refresh command states when needed.
    /// Silently ignores parsing errors.
    /// </summary>
    /// <param name="quizJson">JSON string containing quiz questions.</param>
    private void LoadQuiz(string? quizJson)
    {
        Questions.Clear();

        if (string.IsNullOrWhiteSpace(quizJson))
        {
            return;
        }

        try
        {
            var quizQuestions = JsonSerializer.Deserialize<List<QuizQuestion>>(quizJson, AiClient.JsonOptions);
            if (quizQuestions != null)
            {
                foreach (var question in quizQuestions)
                {
                    var questionVm = new QuizQuestionViewModel()
                    {
                        QuestionText = question.Question,
                        Explanation = question.Explanation
                    };

                    questionVm.PropertyChanged += (_, e) =>
                    {
                        if (e.PropertyName == nameof(QuizQuestionViewModel.SelectedOption) ||
                            e.PropertyName == nameof(QuizQuestionViewModel.IsSubmitted))
                        {
                            RefreshQuizCommands();
                        }
                    };
                    
                    int index = 0;
                    foreach (var option in question.Options)
                    {
                        questionVm.Options.Add(new QuizOptionViewModel(questionVm)
                        {
                            Text = option,
                            IsCorrect = (index == question.CorrectOptionIndex)
                        });
                        index++;
                    }

                    Questions.Add(questionVm);
                }
            }
        }
        catch (Exception)
        {
            // Ignore parsing errors
        }
    }
}
