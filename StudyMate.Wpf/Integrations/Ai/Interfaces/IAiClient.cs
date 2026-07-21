using StudyMate.Wpf.Models.Ai;

namespace StudyMate.Wpf.Integrations.Ai.Interfaces
{
    public interface IAiClient
    {
        Task<AiStudyMaterialResult> GenerateStudyMaterialAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
