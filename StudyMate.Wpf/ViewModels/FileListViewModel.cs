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
    public partial class FileListViewModel : ObservableObject
    {
        private readonly IStudyFileService _fileService;
        private readonly IStudyFolderService _folderService;

        [ObservableProperty] private ObservableCollection<StudyFile> files = new();
        [ObservableProperty] private ObservableCollection<StudyFolder> folders = new();
        [ObservableProperty] private StudyFolder? selectedFolder;
        [ObservableProperty] private StudyFile? selectedFile;
        [ObservableProperty] private string? errorMessage;
        [ObservableProperty] private string? successMessage;
        [ObservableProperty] private bool isLoading;

        public FileListViewModel(
            IStudyFileService fileService,
            IStudyFolderService folderService)
        {
            _fileService = fileService;
            _folderService = folderService;
        }

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

        [RelayCommand]
        private async Task LoadFilesAsync()
        {
            if (SelectedFolder == null)
            {
                ErrorMessage = "Please select a folder first";
                return;
            }

            await LoadFolderAsync(SelectedFolder);
        }

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
                Filter = "All files (*.*)|*.*|PDF files (*.pdf)|*.pdf|Word documents (*.docx)|*.docx|Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select a file to upload"
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
                    
                    // Debug: Check if file exists and get size
                    if (!File.Exists(filePath))
                    {
                        ErrorMessage = $"File not found: {filePath}";
                        return;
                    }

                    using var fileStream = File.OpenRead(filePath);
                    var contentType = GetContentType(fileName);

                    System.Diagnostics.Debug.WriteLine($"Uploading file: {fileName}, Size: {fileStream.Length} bytes, ContentType: {contentType}");

                    System.Diagnostics.Debug.WriteLine($"Calling UploadFileAsync... FolderId={SelectedFolder.Id}, FileName={fileName}");
                    
                    var uploadedFile = await _fileService.UploadFileAsync(
                        SelectedFolder.Id,
                        fileStream,
                        fileName,
                        contentType
                    );

                    System.Diagnostics.Debug.WriteLine($"UploadFileAsync returned. File ID: {uploadedFile.Id}");

                    Files.Insert(0, uploadedFile);
                    SuccessMessage = $"File '{fileName}' uploaded successfully!";
                    
                    System.Diagnostics.Debug.WriteLine($"Successfully inserted into UI");
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error uploading file: {ex.Message}";
                    System.Diagnostics.Debug.WriteLine($"Upload error: {ex.ToString()}");
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.ToString() ?? "None"}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        [RelayCommand]
        private async Task DeleteFileAsync()
        {
            if (SelectedFile == null)
            {
                ErrorMessage = "Please select a file to delete";
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{SelectedFile.OriginalFileName}'?",
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

                    await _fileService.DeleteFileAsync(SelectedFile.Id);
                    Files.Remove(SelectedFile);
                    SelectedFile = null;

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

        [RelayCommand]
        private void OpenFile(StudyFile? file)
        {
            if (file != null)
            {
                try
                {
                    if (File.Exists(file.FilePath))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = file.FilePath,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        ErrorMessage = $"File not found: {file.FilePath}";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error opening file: {ex.Message}";
                }
            }
        }

        [RelayCommand]
        private void Back()
        {
            SelectedFolder = null;
            Files.Clear();
        }

        [RelayCommand]
        private async Task UpdateFileAsync()
        {
            if (SelectedFile == null)
            {
                ErrorMessage = "Please select a file to update";
                return;
            }

            var openFileDialog = new OpenFileDialog
            {
                Filter = "All files (*.*)|*.*|PDF files (*.pdf)|*.pdf|Word documents (*.docx)|*.docx|Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select a new file to replace current file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    IsLoading = true;
                    ErrorMessage = null;
                    SuccessMessage = null;

                    using var fileStream = File.OpenRead(openFileDialog.FileName);
                    var fileName = Path.GetFileName(openFileDialog.FileName);
                    var contentType = GetContentType(fileName);

                    await _fileService.UpdateFileAsync(
                        SelectedFile.Id,
                        fileStream,
                        fileName,
                        contentType
                    );

                    // Update the file in the list
                    var updatedFile = await _fileService.GetFileByIdAsync(SelectedFile.Id);
                    var index = Files.IndexOf(SelectedFile);
                    Files[index] = updatedFile;

                    SuccessMessage = $"File '{fileName}' updated successfully!";
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error updating file: {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                }
            }
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
