namespace StudyMate.Wpf.Services.Interfaces
{
    /// <summary>
    /// Abstraction for file dialog operations to enable unit testing of ViewModels.
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Shows an open file dialog for PDF files.
        /// </summary>
        /// <returns>The selected file path, or null if cancelled.</returns>
        string? ShowOpenPdfFileDialog();

        /// <summary>
        /// Shows a confirmation dialog.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The dialog title.</param>
        /// <returns>True if user confirms, false otherwise.</returns>
        bool ShowConfirmation(string message, string title = "Confirm");

        /// <summary>
        /// Shows an error message dialog.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="title">The dialog title.</param>
        void ShowError(string message, string title = "Error");

        /// <summary>
        /// Shows an information message dialog.
        /// </summary>
        /// <param name="message">The information message.</param>
        /// <param name="title">The dialog title.</param>
        void ShowInfo(string message, string title = "Information");
    }
}
