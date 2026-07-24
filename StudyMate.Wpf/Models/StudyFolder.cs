using System.ComponentModel.DataAnnotations.Schema;

namespace StudyMate.Wpf.Models
{
    /// <summary>
    /// Represents a study folder that organizes study files within the application.
    /// </summary>
    public class StudyFolder
    {
        /// <summary>
        /// Gets or sets the unique identifier for the study folder.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the study folder.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the folder was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the folder was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the count of files contained in this folder.
        /// This property is not mapped to the database and is populated at query time.
        /// </summary>
        [NotMapped]
        public int FileCount { get; set; }
    }
}
