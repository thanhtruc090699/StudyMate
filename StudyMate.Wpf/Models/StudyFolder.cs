using System.ComponentModel.DataAnnotations.Schema;

namespace StudyMate.Wpf.Models
{
    public class StudyFolder
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public int FileCount { get; set; }
    }
}
