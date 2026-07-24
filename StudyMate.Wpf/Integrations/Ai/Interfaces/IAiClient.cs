using StudyMate.Wpf.Models.Ai;

namespace StudyMate.Wpf.Integrations.Ai.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with external AI services to generate study materials.
    /// </summary>
    public interface IAiClient
    {
        /// <summary>
        /// Generates structured study materials from a file using AI.
        /// </summary>
        /// <param name="filePath">The path to the file to analyze.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task containing the AI-generated study material results.</returns>
        Task<AiStudyMaterialResult> GenerateStudyMaterialAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
