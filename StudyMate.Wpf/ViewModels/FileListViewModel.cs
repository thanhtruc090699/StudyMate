using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace StudyMate.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing files within a selected folder.
    /// Handles file upload, deletion, and selection. Coordinates with FileDetailViewModel
    /// to display analysis results when a file is selected.
    /// </summary>
    public partial class FileListViewModel : ObservableObject
    {
        private readonly IStudyFileService _fileService;
        private readonly IStudyFolderService _folderService;
        private readonly FileDetailViewModel _fileDetailViewModel;

        /// <summary>
        /// Gets or sets the collection of files in the currently selected folder.
        /// </summary>
        [ObservableProperty] private ObservableCollection<StudyFile> files = new();
        
        /// <summary>
        /// Gets or sets the collection of all available folders for selection.
        /// </summary>
        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();
        
        /// <summary>
        /// Gets or sets the currently selected folder containing the displayed files.
        /// </summary>
        [ObservableProperty] private StudyFolder? selectedFolder;
        
        /// <summary>
        /// Gets or sets the currently selected file. Setting this property triggers
        /// the FileDetailViewModel to load analysis content for the file.
        /// </summary>
        [ObservableProperty] private StudyFile? selectedFile;
        
        /// <summary>
        /// Gets or sets the error message to display when an operation fails.
        /// </summary>
        [ObservableProperty] private string? errorMessage;

        /// <summary>
        /// Handles changes to SelectedFile by propagating the selection to FileDetailViewModel.
        /// </summary>
        /// <param name="value">The newly selected file.</param>
        partial void OnSelectedFileChanged(StudyFile? value)
        {
            if (value != null)
            {
                _fileDetailViewModel.SelectedFile = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the success message to display after a successful operation.
        /// </summary>
        [ObservableProperty] private string? successMessage;
        
        /// <summary>
        /// Gets or sets a value indicating whether an async operation is in progress.
        /// </summary>
        [ObservableProperty] private bool isLoading;

        /// <summary>
        /// Initializes a new instance of the FileListViewModel class.
        /// </summary>
        /// <param name="fileService">Service for file operations.</param>
        /// <param name="folderService">Service for folder retrieval.</param>
        /// <param name="fileDetailViewModel">ViewModel to update when a file is selected.</param>
        public FileListViewModel(
            IStudyFileService fileService,
            IStudyFolderService folderService,
            FileDetailViewModel fileDetailViewModel)
        {
            _fileService = fileService;
            _folderService = folderService;
            _fileDetailViewModel = fileDetailViewModel;
        }

        /// <summary>
        /// Loads all folders for the folder selector dropdown.
        /// Sets IsLoading during the operation and captures errors to ErrorMessage.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadAsync()
        {
            try
            {
                IsLoading = true;
                var result = await _folderService.GetAllFoldersAsync();
                Folders = new ObservableCollection<StudyFolder>(result);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading folders: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Loads files for the specified folder and clears any previous file list.
        /// Resets error and success messages, and updates IsLoading state.
        /// </summary>
        /// <param name="folder">The folder to load files from. If null, the method returns immediately.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadFolderAsync(StudyFolder folder)
        {
            if (folder == null)
            {
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = null;
                SuccessMessage = null;

                SelectedFolder = folder;

                var result = await _fileService.GetFilesByFolderIdAsync(folder.Id);
                
                Files.Clear();
                foreach (var file in result)
                {
                    Files.Add(file);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading files: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Opens a file dialog to select a PDF file and uploads it to the currently selected folder.
        /// Validates that a folder is selected before showing the dialog. The file must exist on disk.
        /// On success, inserts the uploaded file at the top of the list and displays a success message.
        /// Captures any exceptions as error messages. Only PDF files are accepted by the dialog filter.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand]
        private async Task UploadFileAsync()
        {
            if (SelectedFolder == null)
            {
                ErrorMessage = "Please select a folder first";
                return;
            }

            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = ".pdf",
                Title = "Select a PDF file to upload"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    IsLoading = true;
                    ErrorMessage = null;
                    SuccessMessage = null;

                    var filePath = openFileDialog.FileName;
                    var fileName = Path.GetFileName(filePath);
                    
                    if (!File.Exists(filePath))
                    {
                        ErrorMessage = $"File not found: {filePath}";
                        return;
                    }

                    using var fileStream = File.OpenRead(filePath);
                    var contentType = GetContentType(fileName);
                    
                    var uploadedFile = await _fileService.UploadFileAsync(
                        SelectedFolder.Id,
                        fileStream,
                        fileName,
                        contentType
                    );

                    Files.Insert(0, uploadedFile);
                    SuccessMessage = $"File '{fileName}' uploaded successfully!";
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error uploading file: {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        /// <summary>
        /// Deletes the specified file after confirming with the user via a message box.
        /// Removes the file from the collection and clears the selection if the deleted file was selected.
        /// Updates IsLoading during the operation and captures any exceptions as error messages.
        /// </summary>
        /// <param name="file">The file to delete. If null, displays an error message and returns.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand]
        private async Task DeleteFileAsync(StudyFile? file)
        {
            if (file == null)
            {
                ErrorMessage = "Please select a file to delete";
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{file.OriginalFileName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    IsLoading = true;
                    ErrorMessage = null;
                    SuccessMessage = null;

                    await _fileService.DeleteFileAsync(file.Id);
                    Files.Remove(file);
                    if (SelectedFile?.Id == file.Id)
                    {
                        selectedFile = null;
                    }

                    SuccessMessage = "File deleted successfully!";
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error deleting file: {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        /// <summary>
        /// Sets the specified file as the selected file, which triggers loading its analysis in FileDetailViewModel.
        /// </summary>
        /// <param name="file">The file to open. If null, the method returns immediately.</param>
        [RelayCommand]
        private void OpenFile(StudyFile? file)
        {
            if (file is null)
            {
                return;
            }

            SelectedFile = file;
        }

        /// <summary>
        /// Determines the MIME content type based on file extension.
        /// Defaults to "application/octet-stream" for unknown extensions.
        /// </summary>
        /// <param name="fileName">The name of the file to determine content type for.</param>
        /// <returns>The MIME type string corresponding to the file extension.</returns>
        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".doc" => "application/msword",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }
    }
}
