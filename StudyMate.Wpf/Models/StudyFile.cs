using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudyMate.Wpf.Models
{
    /// <summary>
    /// Represents a study file uploaded by the user, containing metadata about the file
    /// and its relationship to folders and AI analyses. Implements property change notification
    /// for WPF data binding.
    /// </summary>
    public class StudyFile : INotifyPropertyChanged
    {
        private long _fileSizeBytes;

        /// <summary>
        /// Gets or sets the unique identifier for the study file.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the parent folder containing this file.
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// Gets or sets the original filename as provided by the user during upload.
        /// </summary>
        public string OriginalFileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stored filename (typically a GUID or hashed name) used in the file system.
        /// </summary>
        public string StoredFileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the relative path where the file is stored.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file extension (e.g., ".pdf", ".docx").
        /// </summary>
        public string FileExtension { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MIME content type of the file (e.g., "application/pdf").
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the size of the file in bytes.
        /// </summary>
        public long FileSizeBytes
        {
            get => _fileSizeBytes;
            set
            {
                if (_fileSizeBytes == value) return;
                _fileSizeBytes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FileSizeDisplay));
            }
        }

        /// <summary>
        /// Gets or sets the date and time when the file was uploaded.
        /// </summary>
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the file record was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the file record was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the parent folder navigation property.
        /// </summary>
        public StudyFolder? Folder { get; set; }

        /// <summary>
        /// Gets or sets the collection of AI analyses associated with this file.
        /// </summary>
        public ICollection<AiAnalysis> AiAnalyses { get; set; } = new List<AiAnalysis>();

        /// <summary>
        /// Gets a human-readable representation of the file size, formatted as KB or MB.
        /// </summary>
        public string FileSizeDisplay
        {
            get
            {
                if (FileSizeBytes >= 1024 * 1024)
                {
                    return $"{FileSizeBytes / 1024d / 1024d:F1} MB";
                }

                return $"{FileSizeBytes / 1024d:F1} KB";
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Defaults to the caller member name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
