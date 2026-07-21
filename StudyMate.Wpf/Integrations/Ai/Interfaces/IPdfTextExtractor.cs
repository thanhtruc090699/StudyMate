namespace StudyMate.Wpf.Integrations.Ai.Interfaces
{
    public interface IPdfTextExtractor
    {
        Task<string> ExtractTextAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
