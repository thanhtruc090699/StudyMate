using System.Text;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using UglyToad.PdfPig;

namespace StudyMate.Wpf.Integrations.Ai
{
    public class PdfTextExtractor : IPdfTextExtractor
    {
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
