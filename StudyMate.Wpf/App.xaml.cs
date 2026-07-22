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
using System.Windows;
using System.IO;
using StudyMate.Wpf.Models.Ai;
using StudyMate.Wpf.Integrations.Ai;
using StudyMate.Wpf.Integrations.Ai.Interfaces;

namespace StudyMate.Wpf;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null;
    public static IServiceScope Scope { get; private set; } = null;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        ConfigureServices(services);

        Services = services.BuildServiceProvider();
        
        // Create scope for the entire application
        Scope = Services.CreateScope();

        // Ensure database is created
        var dbContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();

        var mainWindow = new MainWindow
        {
            DataContext = Scope.ServiceProvider.GetRequiredService<MainViewModel>()
        };
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        // Dispose scope when app exits
        if (Scope != null)
        {
            await Task.Delay(100); // Wait for final operations
            Scope.Dispose();
        }

        base.OnExit(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StudyMate"
        );

        Directory.CreateDirectory(appDataPath);

        var dbPath = Path.Combine(appDataPath, "studymate.db");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Repositories
        services.AddScoped<IStudyFolderRepository, StudyFolderRepository>();
        services.AddScoped<IStudyFileRepository, StudyFileRepository>();
        services.AddScoped<IAiAnalysisRepository, AiAnalysisRepository>();

        // Services
        services.AddScoped<IStudyFolderService, StudyFolderService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IStudyFileService, StudyFileService>();

        // ViewModels - Singleton để chia sẻ instance xuyên suốt app
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

        DotNetEnv.Env.Load();

        var AiSettings = new AiSettings()
        {
            BaseUrl = Environment.GetEnvironmentVariable("AiBaseUrl"),
            AnalysisEndpoint = Environment.GetEnvironmentVariable("AiEndpoint"),
            ApiKey = Environment.GetEnvironmentVariable("ApiKey"),
            ModelName = Environment.GetEnvironmentVariable("AiModel")
        };
        services.AddSingleton<AiSettings>(AiSettings);
        services.AddSingleton<IPdfTextExtractor, PdfTextExtractor>();
        services.AddHttpClient<IAiClient, AiClient>();
        services.AddScoped<IAiAnalysisService, AiAnalysisService>();
    }

}
