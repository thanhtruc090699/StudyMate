namespace StudyMate.Wpf.Models.Ai
{
    /// <summary>
    /// Represents structured educational content organized into sections.
    /// Used to present study material in an organized, learner-friendly format.
    /// </summary>
    public class StructuredContent
    {
        /// <summary>
        /// Gets or sets the list of content sections that make up the structured material.
        /// Each section covers a distinct topic or chapter from the source document.
        /// </summary>
        public List<ContentSection> Sections { get; set; } = new List<ContentSection>();
    }

    /// <summary>
    /// Represents a single section within structured content.
    /// Contains a title and detailed explanation of a specific topic.
    /// </summary>
    public class ContentSection
    {
        /// <summary>
        /// Gets or sets the title of the section.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed content/explanation for this section.
        /// May contain multiple paragraphs separated by double newlines.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
