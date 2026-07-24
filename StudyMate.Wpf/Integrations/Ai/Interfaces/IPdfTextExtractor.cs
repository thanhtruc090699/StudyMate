namespace StudyMate.Wpf.Integrations.Ai.Interfaces
{
    /// <summary>
    /// Defines methods for extracting text content from PDF files.
    /// </summary>
    public interface IPdfTextExtractor
    {
        /// <summary>
        /// Extracts text content from a PDF file asynchronously.
        /// </summary>
        /// <param name="filePath">The path to the PDF file.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task containing the extracted text content.</returns>
        Task<string> ExtractTextAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
