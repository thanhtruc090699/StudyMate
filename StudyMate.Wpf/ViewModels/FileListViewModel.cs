using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyMate.Wpf.Models;
using StudyMate.Wpf.Services.Interfaces;
using System.Collections.ObjectModel;
using System.IO;

namespace StudyMate.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel for managing files within a selected folder - upload, delete, and selection.
    /// Coordinates with FileDetailViewModel to display analysis when a file is selected.
    /// </summary>
    public partial class FileListViewModel : ObservableObject
    {
        private readonly IStudyFileService _fileService;
        private readonly IStudyFolderService _folderService;
        private readonly FileDetailViewModel _fileDetailViewModel;
        private readonly IFileDialogService _dialogService;

        [ObservableProperty] private ObservableCollection<StudyFile> files = new();
        
        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();
        
        [ObservableProperty] private StudyFolder? selectedFolder;
        
        [ObservableProperty] private StudyFile? selectedFile;
        
        [ObservableProperty] private string? errorMessage;

        partial void OnSelectedFileChanged(StudyFile? value)
        {
            if (value != null)
            {
                _fileDetailViewModel.SelectedFile = value;
            }
        }
        
        [ObservableProperty] private string? successMessage;
        
        [ObservableProperty] private bool isLoading;

        public FileListViewModel(
            IStudyFileService fileService,
            IStudyFolderService folderService,
            FileDetailViewModel fileDetailViewModel,
            IFileDialogService dialogService)
        {
            _fileService = fileService;
            _folderService = folderService;
            _fileDetailViewModel = fileDetailViewModel;
            _dialogService = dialogService;
        }

        /// <summary>
        /// Loads all folders for the folder selector dropdown.
        /// </summary>
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
        /// Loads files for the specified folder.
        /// </summary>
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
        /// Opens a file dialog to upload a PDF file to the selected folder.
        /// Validates folder selection and file existence before uploading.
        /// </summary>
        [RelayCommand]
        private async Task UploadFileAsync()
        {
            if (SelectedFolder == null)
            {
                ErrorMessage = "Please select a folder first";
                return;
            }

            var filePath = _dialogService.ShowOpenPdfFileDialog();
            if (filePath == null)
            {
                return; // User cancelled
            }

            try
            {
                IsLoading = true;
                ErrorMessage = null;
                SuccessMessage = null;

                var fileName = Path.GetFileName(filePath);
                
                if (!_dialogService.FileExists(filePath))
                {
                    ErrorMessage = $"File not found: {filePath}";
                    return;
                }

                using var fileStream = _dialogService.OpenReadFile(filePath);
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

        /// <summary>
        /// Deletes a file after user confirmation. Removes from collection and clears selection if needed.
        /// </summary>
        [RelayCommand]
        private async Task DeleteFileAsync(StudyFile? file)
        {
            if (file == null)
            {
                ErrorMessage = "Please select a file to delete";
                return;
            }

            var confirmed = _dialogService.ShowConfirmation(
                $"Are you sure you want to delete '{file.OriginalFileName}'?",
                "Confirm Delete"
            );

            if (!confirmed)
            {
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = null;
                SuccessMessage = null;

                await _fileService.DeleteFileAsync(file.Id);
                Files.Remove(file);
                if (SelectedFile?.Id == file.Id)
                {
                    SelectedFile = null;
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

        [RelayCommand]
        private void OpenFile(StudyFile? file)
        {
            if (file is null)
            {
                return;
            }

            SelectedFile = file;
        }

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
