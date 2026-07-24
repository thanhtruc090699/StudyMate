using System.Text;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using UglyToad.PdfPig;

namespace StudyMate.Wpf.Integrations.Ai
{
    /// <summary>
    /// Extracts text from PDF documents using the third-party PdfPig library.
    /// </summary>
    public class PdfTextExtractor : IPdfTextExtractor
    {
        /// <inheritdoc />
        public Task<string> ExtractTextAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                var textBuilder = new StringBuilder();
                
                using (var document = PdfDocument.Open(filePath))
                {
                    foreach (var page in document.GetPages())
                    {
                        textBuilder.AppendLine(page.Text);
                    }
                }
                
                return textBuilder.ToString();
            }, cancellationToken);
        }
    }
}
