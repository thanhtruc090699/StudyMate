namespace StudyMate.Wpf.Models.Ai
{
    public class StructuredContent
    {
        public List<ContentSection> Sections { get; set; } = new List<ContentSection>();
    }

    public class ContentSection
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
