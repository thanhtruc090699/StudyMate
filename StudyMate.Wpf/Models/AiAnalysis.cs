namespace StudyMate.Wpf.Models
{
    public class AiAnalysis
    {
        public int Id { get; set; }

        public int StudyFileId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string StructuredContentJson { get; set; } = string.Empty;

        public string QuizJson { get; set; } = string.Empty;

        public string Status { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ModelName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public StudyFile? StudyFile { get; set; }
    }
}
