using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// WPF implementation of file dialog and message box services.
    /// </summary>
    public class FileDialogService : IFileDialogService
    {
        public string? ShowOpenPdfFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = ".pdf",
                Title = "Select a PDF file to upload"
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public bool ShowConfirmation(string message, string title = "Confirm")
        {
            var result = MessageBox.Show(
                message,
                title,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            return result == MessageBoxResult.Yes;
        }

        public void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string message, string title = "Information")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    /// <summary>
    /// Extension methods for file operations that work with the dialog service.
    /// </summary>
    public static class FileServiceExtensions
    {
        public static bool FileExists(this IFileDialogService _, string path)
        {
            return File.Exists(path);
        }

        public static Stream OpenReadFile(this IFileDialogService _, string path)
        {
            return File.OpenRead(path);
        }
    }
}
