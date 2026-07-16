using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Helpers;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.ViewModels;

public class FileDetailViewModel : ViewModelBase
{
    private StudyFile? _selectedFile;
    private string _summary = string.Empty;
    private int _selectedTabIndex;
    private bool _isLoading;
    private int _currentQuestionIndex;

    public FileDetailViewModel()
    {
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
            
            DebugLogger.Log($"SelectedTabIndex changed to {value}");
            
            // Re-evaluate commands when switching tabs
            CommandManager.InvalidateRequerySuggested();
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

            CommandManager.InvalidateRequerySuggested();
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

    public ICommand PreviousQuestionCommand { get; }

    public ICommand NextQuestionCommand { get; }

    public ICommand CheckAnswerCommand { get; }

    public void LoadMockData(StudyFile file)
    {
        DebugLogger.Log($"FileDetailViewModel.LoadMockData called: {file.OriginalFileName}");
        DebugLogger.Log($"FileDetailViewModel instance hash = {GetHashCode()}");

        SelectedFile = file;

        Summary =
            "Machine Learning is a subset of artificial intelligence " +
            "that enables systems to learn from data and improve with " +
            "experience without being explicitly programmed.";

        KeyPoints.Clear();

        KeyPoints.Add(
            "Machine Learning improves performance through experience.");

        KeyPoints.Add(
            "There are two main types: Supervised Learning and Unsupervised Learning.");

        KeyPoints.Add(
            "Common algorithms include Linear Regression, Decision Trees, and Neural Networks.");

        KeyPoints.Add(
            "Machine Learning is used in image recognition, recommendation systems, and natural language processing.");

        Questions.Clear();

        var question1 = new QuizQuestionViewModel
        {
            QuestionText = "Which of the following is a type of machine learning?",
            Explanation = "Supervised learning is one of the main machine learning approaches.",
        };
        
        question1.Options.Add(new QuizOptionViewModel(question1) { Text = "A. Structured Programming" });
        question1.Options.Add(new QuizOptionViewModel(question1) { Text = "B. Supervised Learning", IsCorrect = true });
        question1.Options.Add(new QuizOptionViewModel(question1) { Text = "C. Web Development" });
        question1.Options.Add(new QuizOptionViewModel(question1) { Text = "D. Database Management" });
        
        Questions.Add(question1);

        var question2 = new QuizQuestionViewModel
        {
            QuestionText = "What is the primary goal of unsupervised learning?",
            Explanation = "Unsupervised learning aims to discover hidden patterns in unlabeled data.",
        };
        
        question2.Options.Add(new QuizOptionViewModel(question2) { Text = "A. To classify labeled data" });
        question2.Options.Add(new QuizOptionViewModel(question2) { Text = "B. To discover patterns in unlabeled data", IsCorrect = true });
        question2.Options.Add(new QuizOptionViewModel(question2) { Text = "C. To predict numerical values" });
        question2.Options.Add(new QuizOptionViewModel(question2) { Text = "D. To optimize database queries" });
        
        Questions.Add(question2);

        CurrentQuestionIndex = 0;
        IsAnswerSubmitted = false;

        OnPropertyChanged(nameof(CurrentQuestion));
        OnPropertyChanged(nameof(QuizProgressText));
        CommandManager.InvalidateRequerySuggested();

        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] LoadMockData: Questions={Questions.Count}, CurrentQuestion={CurrentQuestion != null}\n");
    }

    private void CloseFile()
    {
        SelectedFile = null;
        Summary = string.Empty;

        KeyPoints.Clear();
        Questions.Clear();

        CurrentQuestionIndex = 0;
        IsAnswerSubmitted = false;
    }

    private bool CanCheckAnswer()
    {
        var canExecute = CurrentQuestion?.SelectedOption is not null
               && CurrentQuestion.IsSubmitted == false;
        
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", 
            $"[{System.DateTime.Now:HH:mm:ss}] CanCheckAnswer: {canExecute}, HasSelectedOption={CurrentQuestion?.SelectedOption != null}, IsSubmitted={CurrentQuestion?.IsSubmitted}\n");
        return canExecute;
    }

    private void CheckAnswer()
    {
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] CheckAnswer called\n");
        
        if (CurrentQuestion?.SelectedOption is null)
        {
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] SelectedOption is null\n");
            return;
        }

        CurrentQuestion.IsSubmitted = true;
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] IsSubmitted=true, Correct={CurrentQuestion.SelectedOption.IsCorrect}\n");

        foreach (var option in CurrentQuestion.Options)
        {
            option.Refresh();
        }

        OnPropertyChanged(nameof(CurrentQuestion));
        CommandManager.InvalidateRequerySuggested();
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] CheckAnswer completed\n");
    }

    private bool CanGoPrevious()
    {
        var canExecute = CurrentQuestionIndex > 0;
        DebugLogger.Log($"CanGoPrevious: {canExecute}, Index={CurrentQuestionIndex}");
        return canExecute;
    }

    private void PreviousQuestion()
    {
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] PreviousQuestion called, Index={CurrentQuestionIndex}\n");
        
        if (!CanGoPrevious())
        {
            return;
        }

        CurrentQuestionIndex--;
        IsAnswerSubmitted = false;
        OnPropertyChanged(nameof(IsAnswerSubmitted));
        OnPropertyChanged(nameof(CurrentQuestion));
        CommandManager.InvalidateRequerySuggested();
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] PreviousQuestion completed, NewIndex={CurrentQuestionIndex}\n");
    }

    private bool CanGoNext()
    {
        var canExecute = CurrentQuestion?.IsSubmitted == true
               && CurrentQuestionIndex < Questions.Count - 1;
        DebugLogger.Log($"CanGoNext: {canExecute}, IsSubmitted={CurrentQuestion?.IsSubmitted}, Index={CurrentQuestionIndex}, Count={Questions.Count}");
        return canExecute;
    }

    private void NextQuestion()
    {
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] NextQuestion called, Index={CurrentQuestionIndex}\n");
        
        if (!CanGoNext())
        {
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] NextQuestion cannot execute\n");
            return;
        }

        CurrentQuestionIndex++;
        IsAnswerSubmitted = false;
        OnPropertyChanged(nameof(IsAnswerSubmitted));
        OnPropertyChanged(nameof(CurrentQuestion));
        CommandManager.InvalidateRequerySuggested();
        System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\quiz.log", $"[{DateTime.Now:HH:mm:ss}] NextQuestion completed, NewIndex={CurrentQuestionIndex}\n");
    }
}
