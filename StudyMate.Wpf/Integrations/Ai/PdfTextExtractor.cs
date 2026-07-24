using System.Text;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using UglyToad.PdfPig;

namespace StudyMate.Wpf.Integrations.Ai
{
    /// <summary>
    /// Extracts text content from PDF files for AI analysis.
    /// Uses the PdfPig library (UglyToad.PdfPig) to open PDF documents
    /// and extract text from each page sequentially.
    /// 
    /// PDF Text Extraction Process:
    /// 1. Opens the PDF file at the specified path using PdfDocument.Open()
    /// 2. Iterates through all pages in the document
    /// 3. Extracts raw text content from each page using page.Text property
    /// 4. Appends each page's text with line breaks to preserve structure
    /// 5. Returns the complete extracted text as a single string
    /// 
    /// Third-party library: PdfPig by UglyToad
    /// NuGet package: PdfPig - A fast, maintained PDF text extraction library
    /// </summary>
    public class PdfTextExtractor : IPdfTextExtractor
    {
        /// <summary>
        /// Extracts all text content from a PDF file asynchronously.
        /// Reads the PDF page by page and concatenates the text with line breaks.
        /// </summary>
        /// <param name="filePath">The full path to the PDF file to extract text from.</param>
        /// <param name="cancellationToken">Token to cancel the extraction operation.</param>
        /// <returns>The complete text content extracted from all pages of the PDF.</returns>
        /// <exception cref="System.IO.IOException">Thrown when the file cannot be accessed.</exception>
        /// <exception cref="UglyToad.PdfPig.Exceptions.PdfReadException">Thrown when the file is not a valid PDF.</exception>
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
