namespace StudyMate.Wpf.Models
{
    public class StudyFile
    {
        public int Id { get; set; }

        public int FolderId { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileExtension { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSizeBytes { get; set; }

        public DateTime UploadedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public StudyFolder? Folder { get; set; }

        public ICollection<AiAnalysis> AiAnalyses { get; set; } = new List<AiAnalysis>();

    }
}