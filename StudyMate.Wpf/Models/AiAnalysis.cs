namespace StudyMate.Wpf.Models
{
    /// <summary>
    /// Represents an AI analysis result generated from a study file.
    /// Contains structured learning material, summaries, and quiz questions extracted by the AI service.
    /// </summary>
    public class AiAnalysis
    {
        /// <summary>
        /// Gets or sets the unique identifier for the AI analysis.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated study file.
        /// </summary>
        public int StudyFileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the analysis, typically derived from the document title.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the AI-generated summary of the study file content.
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the JSON-serialized structured content containing organized sections
        /// with titles and detailed explanations.
        /// </summary>
        public string StructuredContentJson { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the JSON-serialized quiz questions generated from the study content.
        /// </summary>
        public string QuizJson { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the analysis (e.g., "Pending", "Completed", "Failed").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the error message if the analysis failed.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the AI model used to generate this analysis.
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the analysis was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the analysis was last updated. Can be null.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the associated study file navigation property.
        /// </summary>
        public StudyFile? StudyFile { get; set; }
    }
}
