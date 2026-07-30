using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using StudyMate.Wpf.Data;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.ViewModels;
using StudyMate.Wpf.Repositories;
using StudyMate.Wpf.Repositories.Interfaces;
using StudyMate.Wpf.Services;
using StudyMate.Wpf.Services.Interfaces;
using StudyMate.Wpf.Views;
using StudyMate.Wpf.Converters;
using System.IO;
using StudyMate.Wpf.Models.Ai;
using StudyMate.Wpf.Integrations.Ai;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using System.Windows;

namespace StudyMate.Wpf;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }
    public static IServiceScope? Scope { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);

        Services = services.BuildServiceProvider();
        Scope = Services.CreateScope();

        // Validate configuration before proceeding
        if (!ValidateConfiguration(out string errorMessage))
        {
            ShowConfigurationError(errorMessage);
            Shutdown();
            return;
        }

        // Apply pending migrations
        try
        {
            var dbContextFactory = Scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            ShowConfigurationError($"Database migration failed: {ex.Message}");
            Shutdown();
            return;
        }

        var mainWindow = new MainWindow
        {
            DataContext = Scope.ServiceProvider.GetRequiredService<MainViewModel>()
        };
        
        mainWindow.Show();

        // Initialize ViewModels after window is shown
        _ = SafeInitializeAsync(mainWindow);
    }

    private async Task SafeInitializeAsync(MainWindow window)
    {
        try
        {
            if (window.DataContext is MainViewModel mainViewModel)
            {
                await mainViewModel.InitializeAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Failed to initialize application: {ex.Message}\n\nPlease check your configuration and try again.",
                "Initialization Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (Scope != null)
        {
            await Task.Delay(100);
            Scope.Dispose();
        }

        base.OnExit(e);
    }

    private bool ValidateConfiguration(out string errorMessage)
    {
        errorMessage = string.Empty;

        // Try multiple locations for .env file
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var possiblePaths = new[]
        {
            Path.Combine(baseDirectory, ".env"),
            Path.Combine(baseDirectory, "..", "..", "..", ".env"),
            Path.Combine(baseDirectory, "..", "..", "StudyMate.Wpf", ".env")
        };

        string? envPath = null;
        foreach (var path in possiblePaths)
        {
            var fullPath = Path.GetFullPath(path);
            if (File.Exists(fullPath))
            {
                envPath = fullPath;
                break;
            }
        }

        if (envPath == null)
        {
            errorMessage = $".env file not found.\n\nPlease copy .env.example to .env and configure your API credentials.\n\nSearch location: {baseDirectory}";
            return false;
        }

        DotNetEnv.Env.Load(envPath);

        var apiKey = Environment.GetEnvironmentVariable("ApiKey");
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            errorMessage = "ApiKey is missing in .env file.\n\nPlease check your .env configuration.";
            return false;
        }

        var baseUrl = Environment.GetEnvironmentVariable("AiBaseUrl");
        if (string.IsNullOrWhiteSpace(baseUrl) || !Uri.TryCreate(baseUrl, UriKind.Absolute, out _))
        {
            errorMessage = "AiBaseUrl is missing or invalid in .env file.";
            return false;
        }

        var endpoint = Environment.GetEnvironmentVariable("AiEndpoint");
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            errorMessage = "AiEndpoint is missing in .env file.";
            return false;
        }

        var model = Environment.GetEnvironmentVariable("AiModel");
        if (string.IsNullOrWhiteSpace(model))
        {
            errorMessage = "AiModel is missing in .env file.";
            return false;
        }

        return true;
    }

    private void ShowConfigurationError(string message)
    {
        MessageBox.Show(
            message,
            "Configuration Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error
        );
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StudyMate"
        );

        Directory.CreateDirectory(appDataPath);

        var dbPath = Path.Combine(appDataPath, "studymate.db");

        // Use DbContextFactory for short-lived contexts
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Repositories
        services.AddScoped<IStudyFolderRepository, StudyFolderRepository>();
        services.AddScoped<IStudyFileRepository, StudyFileRepository>();
        services.AddScoped<IAiAnalysisRepository, AiAnalysisRepository>();

        // Services
        services.AddScoped<IStudyFolderService, StudyFolderService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IStudyFileService, StudyFileService>();
        services.AddScoped<IFileDialogService, FileDialogService>();

        // ViewModels - Singleton for shared instances across app
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<FileListViewModel>();
        services.AddSingleton<FileDetailViewModel>();
        services.AddSingleton<FolderListViewModel>();

        // Views
        services.AddTransient<FolderListView>();
        services.AddTransient<FileListView>();
        services.AddTransient<FileDetailView>();

        // Converters
        services.AddSingleton<NullToVisibilityConverter>();
        services.AddSingleton<CountToVisibilityConverter>();
        services.AddSingleton<BoolToVisibilityConverter>();

        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.Combine(baseDir, ".env");
        if (File.Exists(envPath))
        {
            DotNetEnv.Env.Load(envPath);
        }
        else
        {
            var projectPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            var projectEnvPath = Path.Combine(projectPath, ".env");
            if (File.Exists(projectEnvPath))
            {
                DotNetEnv.Env.Load(projectEnvPath);
            }
        }

#pragma warning disable CS8601
        var AiSettings = new AiSettings()
        {
            BaseUrl = Environment.GetEnvironmentVariable("AiBaseUrl") ?? string.Empty,
            AnalysisEndpoint = Environment.GetEnvironmentVariable("AiEndpoint") ?? string.Empty,
            ApiKey = Environment.GetEnvironmentVariable("ApiKey") ?? string.Empty,
            ModelName = Environment.GetEnvironmentVariable("AiModel") ?? string.Empty
        };
#pragma warning restore CS8601
        services.AddSingleton<AiSettings>(AiSettings);
        services.AddSingleton<IPdfTextExtractor, PdfTextExtractor>();
        services.AddHttpClient<IAiClient, AiClient>();
        services.AddScoped<IAiAnalysisService, AiAnalysisService>();
    }
}
