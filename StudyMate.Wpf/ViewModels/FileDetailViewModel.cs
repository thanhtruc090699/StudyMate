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

public class FileDetailViewModel : ViewModelBase
{

    private readonly IAiAnalysisService _aiAnalysisService;
    private StudyFile? _selectedFile;
    private string _summary = string.Empty;
    private int _selectedTabIndex;
    private bool _isLoading;
    private int _currentQuestionIndex;
    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

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

    public ObservableCollection<string> KeyPoints { get; }

    public ObservableCollection<QuizQuestionViewModel> Questions { get; }

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

    public QuizQuestionViewModel? CurrentQuestion =>
        Questions.Count == 0
            ? null
            : Questions[CurrentQuestionIndex];

    public string QuizProgressText =>
        Questions.Count == 0
            ? "No quiz available"
            : $"Question {CurrentQuestionIndex + 1} of {Questions.Count}";

    public bool IsAnswerSubmitted { get; set; }

    public ICommand CloseCommand { get; }

    public IRelayCommand PreviousQuestionCommand { get; }

    public IRelayCommand NextQuestionCommand { get; }

    public IRelayCommand CheckAnswerCommand { get; }

    private void CloseFile()
    {
        SelectedFile = null;
        Summary = string.Empty;

        KeyPoints.Clear();
        Questions.Clear();

        CurrentQuestionIndex = 0;
        IsAnswerSubmitted = false;
    }

    private void RefreshQuizCommands()
    {
        CheckAnswerCommand.NotifyCanExecuteChanged();
        PreviousQuestionCommand.NotifyCanExecuteChanged();
        NextQuestionCommand.NotifyCanExecuteChanged();
    }

    private bool CanCheckAnswer()
    {
        return SelectedTabIndex == 1 
               && CurrentQuestion?.SelectedOption is not null
               && CurrentQuestion.IsSubmitted == false;
    }

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

    private bool CanGoPrevious()
    {
        return SelectedTabIndex == 1 && CurrentQuestionIndex > 0;
    }

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

    private bool CanGoNext()
    {
        return SelectedTabIndex == 1 
               && CurrentQuestion?.IsSubmitted == true
               && CurrentQuestionIndex < Questions.Count - 1;
    }

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

    private void LoadStructuredContent(string? structuredContentJson)
    {
        KeyPoints.Clear();

        if (string.IsNullOrWhiteSpace(structuredContentJson))
        {
            return;
        }

        try
        {
            var structuredContent = JsonSerializer.Deserialize<StructuredContent>(structuredContentJson, AiResponseParser.JsonOptions);

            if (structuredContent?.Sections != null)
            {
                foreach (var section in structuredContent.Sections)
                {
                    if (!string.IsNullOrWhiteSpace(section.Title))
                    {
                        KeyPoints.Add(section.Title);
                    }
                }
            }
        }
        catch (Exception)
        {
            // Ignore parsing errors
        }
    }

    private void LoadQuiz(string? quizJson)
    {
        Questions.Clear();

        if (string.IsNullOrWhiteSpace(quizJson))
        {
            return;
        }

        try
        {
            var quizQuestions = JsonSerializer.Deserialize<List<QuizQuestion>>(quizJson, AiResponseParser.JsonOptions);
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
