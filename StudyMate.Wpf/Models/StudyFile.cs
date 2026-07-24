using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudyMate.Wpf.Models
{
    public class StudyFile : INotifyPropertyChanged
    {
        private long _fileSizeBytes;

        public int Id { get; set; }

        public int FolderId { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileExtension { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

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

        public DateTime UploadedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public StudyFolder? Folder { get; set; }

        public ICollection<AiAnalysis> AiAnalyses { get; set; } = new List<AiAnalysis>();

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
