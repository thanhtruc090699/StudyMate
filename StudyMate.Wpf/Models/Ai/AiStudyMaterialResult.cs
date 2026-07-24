
namespace StudyMate.Wpf.Models.Ai
{
    /// <summary>
    /// Represents the complete AI-generated study material result returned from the AI service.
    /// Contains a name, summary, structured content sections, and quiz questions.
    /// </summary>
    public class AiStudyMaterialResult
    {
        /// <summary>
        /// Gets or sets the name/title of the study material.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the comprehensive summary of the entire study document.
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the structured content divided into topical sections.
        /// </summary>
        public StructuredContent StructuredContent { get; set; } = new StructuredContent();

        /// <summary>
        /// Gets or sets the list of multiple-choice quiz questions generated from the study material.
        /// </summary>
        public List<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        /// <summary>
        /// Gets or sets the name of the AI model that generated this result.
        /// </summary>
        public string ModelName { get; set; } = string.Empty;
    }
}
