# AI Usage Protocol for Research Project

## Project Information

- Group name: StudyMate
- Title of research project: StudyMate - AI-powered Study Assistant
- AI used: yes
- AI tools used: LLM Chat Assistant

If no AI was used, stating "no" is sufficient here. In this case, the following sections do not need to be filled out.

## Brief Explanation

This protocol is maintained as a Markdown file in the group's Git repository.
Essential AI usages are documented briefly and promptly here.
Traceability across versions is provided by the Git history of this file.

AI tools were used as support for this research project.
The essential usages are documented below.
All adopted content has been reviewed for technical accuracy, adapted as needed, and responsibly integrated into the work.

## Overview of AI Usage

Enter the essential usages here.
If similar usages are closely related, you can summarize them.
Maintain the protocol as promptly as possible so that the Git history makes the development traceable. Use AI to record your prompt history according to this template.  

| Date | User | Tool | Usage Description | Implementation & Adaptation |
| --- | --- | --- | --- | --- |
| 14.07.2026 | Truc Trinh | Lisa Pro | Backend File Service Implementation | **AI Guidance:**<br>- Explained Repository vs Service layer architecture<br>- Explained FileStorageService vs StudyFileService separation of concerns<br>- Identified bug in UpdateFileAsync: wrong delete logic<br>- Identified bug in FileStorageService: invalid path with @-character<br>- Reviewed and validated all code implementations<br><br>**My proactive work:**<br>- Created documentation folder<br>- Implemented all repositories and services<br>- Fixed all bugs identified by AI<br>- Reviewed and tested all code before commit |
| 14.07.2026 | Truc Trinh | Lisa Pro | UI Implementation for File Management | **AI Guidance:**<br>- Created FileListViewModel with MVVM pattern<br>- Implemented FileListView XAML with modern UI design<br>- Added converters (NullToVisibility, CountToVisibility, BoolToVisibility)<br>- Configured DI container with new services and views<br>- Fixed XAML binding errors (DisplayMemberPath vs ItemTemplate)<br><br>**My proactive work:**<br>- Designed two-panel layout (Folders + Files)<br>- Implemented upload/delete/update file features<br>- Added file dialog integration<br>- Tested complete CRUD operations for files<br>- Verified UI responsiveness and error handling |
| 16.07.2026 | Truc Trinh | Lisa Pro | Modern UI Redesign (Purple Theme) | **AI Guidance:**<br>- Redesigned FolderListView and FileListView XAML with modern purple theme (#6C4CF1)<br>- Refactored MainViewModel to coordinate between FolderListViewModel and FileListViewModel<br>- Implemented folder selection communication via constructor injection<br>- Fixed ObjectDisposedException by changing DbContext lifetime from Singleton to Scoped<br>- Fixed UI state management: prevented duplicate empty states on initial load<br>- Fixed binding error: added Mode=OneWay to readonly Files.Count property<br>- Translated all Vietnamese comments to English<br><br>**My proactive work:**<br>- Designed modern card-based UI for files with PDF icons and action menus<br>- Implemented three-state UI logic (no folder / empty folder / has files)<br>- Added visibility triggers for Upload button, headers, and file lists<br>- Integrated file dialogs for upload/replace operations<br>- Tested all UI states and transitions end-to-end |
| 16.07.2026 | Truc Trinh | Lisa Pro | Quiz Feature Implementation | **AI Guidance:**<br>- Created FileDetailViewModel with Summary and Quiz tabs<br>- Implemented QuizQuestionViewModel and QuizOptionViewModel classes<br>- Added CheckAnswerCommand with validation logic<br>- Implemented Previous/Next navigation with state persistence<br>- Created converters (ResultTextConverter, InvertedBoolConverter)<br>- Configured RadioButton Command binding in ItemsControl<br>- Fixed dual-window startup bug (removed StartupUri from App.xaml)<br><br>**My proactive work:**<br>- Designed three-column layout (360px Folders \| 440px Files \| * Details)<br>- Implemented TabControl with Summary and Quiz tabs<br>- Created mock quiz data (2 ML questions with explanations)<br>- Added color-coded feedback (green Correct/red Incorrect borders)<br>- Implemented explanation display after answer submission<br>- Tested complete quiz interaction flow |
| 21.07.2026 | Truc Trinh | Lisa Pro | Complete AI Integration - PDF Analysis with Lisa Chat API | **AI Guidance:**<br>- Identified HTTP 405 error root cause: API endpoint only accepts JSON chat completion format, not multipart/form-data<br>- Recommended IPdfTextExtractor pattern with PdfPig library for PDF text extraction before AI analysis<br>- Refactored AiClient to send JSON payload matching OpenAI chat completion schema<br>- Implemented ParseChatResponse() to extract AI responses and parse results<br>- Fixed DI configuration: Changed ViewModels from AddScoped to AddSingleton for shared instances<br>- Implemented OnSelectedFileChanged partial method to auto-pass selected file to FileDetailViewModel<br>- Refactored GenerateAnalysisAsync to LoadAnalysisAsync with smart caching: check database first<br>- Fixed file storage path mismatch: Database stores filename only, AI needs full physical path<br>- Fixed .env configuration: BaseUrl without /api suffix, endpoint as api/chat/completions<br>- Added comprehensive logging throughout AI flow (HTTP status, response length, parse results)<br>- Implemented ErrorMessage property in FileDetailViewModel for user-facing error feedback<br><br>**My proactive work:**<br>- Installed PdfPig NuGet package and implemented PdfTextExtractor<br>- Integrated IPdfTextExtractor into AiClient via dependency injection<br>- Debugged multiple HTTP 405 errors by testing API with PowerShell Invoke-RestMethod<br>- Verified file upload flow and tested PDF text extraction manually<br>- Resolved database vs filesystem path inconsistency<br>- Removed LoadMockData calls to prevent overriding AI results with mock data<br>- Tested end-to-end flow: Upload PDF → Extract text → Call Lisa API → Save to database → Display in UI |
| 29.07.2026 | Truc Trinh | Lisa Pro | MVVM Architecture Improvements - Testability & Clean Code | **My Initiative:**<br>- Self-reviewed code and noticed MessageBox.Show() directly in ViewModel violates separation of concerns<br>- Decided to create IFileDialogService abstraction for better testability<br>- Identified async void event handler in FolderListViewModel as potential memory leak<br>- Requested AI to review my implementation and suggest improvements<br><br>**AI Guidance:**<br>- Confirmed my IFileDialogService design is correct pattern<br>- Suggested using CommunityToolkit's partial OnSelectedFolderChanged() instead of manual event subscription<br>- Recommended CancellationToken parameter for LoadAnalysisAsync to prevent race conditions<br><br>**My Implementation:**<br>- Created IFileDialogService interface with 4 methods (ShowOpenPdfFileDialog, ShowConfirmation, FileExists, OpenReadFile)<br>- Implemented FileDialogService wrapping OpenFileDialog and MessageBox<br>- Refactored FileListViewModel and FolderListViewModel to use service injection<br>- Removed async void handler, implemented partial method pattern<br>- Added CancellationToken support and tested rapid file-switching scenarios<br>- Registered services in App.xaml.cs DI container |
| 29.07.2026 | Truc Trinh | Lisa Pro | EF Core Production Setup - Migrations & Configuration Validation | **My Initiative:**<br>- Read Microsoft EF Core documentation and realized EnsureCreated() is not suitable for production<br>- Decided to implement proper migration workflow for database schema management<br>- Noticed ObjectDisposedException when sharing DbContext across ViewModels - investigated root cause myself<br>- Designed configuration validation logic to fail fast at startup if .env misconfigured<br><br>**AI Guidance:**<br>- Explained IDbContextFactory pattern for short-lived contexts<br>- Confirmed my migration approach is correct for SQLite<br>- Suggested validating .env before creating MainWindow to show errors early<br>- Clarified Scoped vs Singleton lifetime for DbContext<br><br>**My Implementation:**<br>- Created migration `UpdateForEFCore10` with complete schema for all entities<br>- Implemented validation in App.xaml.cs: checks ApiKey, AiBaseUrl, AiEndpoint, AiModel<br>- Refactored repositories to use `_dbContextFactory.CreateDbContextAsync()`<br>- Changed ViewModel registration from Scoped to Singleton (confirmed no DbContext dependency)<br>- Updated README with setup instructions and migration commands<br>- Tested migration rollback and re-application |
| 29.07.2026 | Truc Trinh | Lisa Pro | Error Handling & UX Polish | **My Initiative:**<br>- Discovered bug during testing: user can upload multiple files while AI is analyzing, causing conflicts<br>- Noticed JSON parsing exceptions show cryptic error messages to users<br>- Decided to add IsAnalyzing state tracking and user-friendly error handling<br><br>**AI Guidance:**<br>- Reviewed my error handling approach<br>- Suggested wrapping JsonSerializer.Deserialize in try-catch blocks<br>- Recommended showing ErrorMessage property in UI with visual feedback<br><br>**My Implementation:**<br>- Added IsAnalyzing computed property in FileListViewModel checking FileDetailViewModel.IsLoading<br>- Implemented validation in UploadFileCommand with dialog warning<br>- Added try-catch around all JSON parsing with specific error messages<br>- Tested error scenarios: invalid JSON, network timeout, API errors<br>- Removed unused using statements (code cleanup) |

## Optional Supplementary Notes

**Handling incorrect AI responses:**
- The AI initially suggested `Mode=OneTime` for Files.Count binding, which prevented UI updates. I identified this issue during testing and changed it to `Mode=OneWay` to maintain reactivity while respecting the readonly property.
- AI initially generated multipart/form-data request for file upload, but Lisa API only supports JSON chat completion format. Had to manually test with PowerShell to confirm API capabilities, then refactored to text extraction + JSON payload approach.
- First implementation attempted to send file path directly to AI API, resulting in HTTP 405 Method Not Allowed. Solution: Extract PDF text client-side using PdfPig, then send text content in message body.

**Deliberately rejected suggestions:**
- Rejected using code-behind or converters for complex visibility logic. Instead, insisted on pure XAML MultiDataTriggers for better maintainability and separation of concerns.
- Rejected keeping mock data auto-trigger when selecting files. Removed LoadMockData call from FileListViewModel.OpenFile to ensure only real AI analysis is displayed.
- Rejected storing full file paths in database. Instead, build full path dynamically in AiClient using Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).

**AI as sparring partner:**
- Used AI primarily for architecture review (Repository vs Service pattern)
- Validated dependency injection lifetime choices (Scoped vs Singleton for ViewModels)
- Code review and bug identification in existing implementations
- Debugging HTTP 405 errors by analyzing API endpoint structure and expected payload format
- Architecture design: Separated IPdfTextExtractor interface for testability and future extensibility (could add OCR support later)

**Critical bugs found through AI collaboration:**
1. **File path mismatch**: FileStorageService saves to `AppData/Local/StudyMate/uploads/` but AiClient was looking in `AppData/Local/Temp/uploads/` - Fixed by building full path dynamically
2. **Database cache issue**: SelectedFile setter called GenerateAnalysisAsync every time, creating duplicate API calls - Refactored to check database first with LoadAnalysisAsync
3. **ViewModel instance mismatch**: Using AddScoped caused different instances in different views - Changed to AddSingleton for shared state
4. **API endpoint format**: .env had BaseUrl ending with /api AND endpoint starting with / causing malformed URLs - Standardized to BaseUrl without suffix, endpoint with full path

**Critical issues I discovered independently:**
1. **Upload conflict bug**: While testing on 29.07, I noticed users could upload multiple PDFs rapidly while AI was still analyzing. This caused file handle conflicts. My solution: Added IsAnalyzing state check before allowing upload.

2. **async void memory leak**: While studying MVVM best practices, I identified that subscribing to PropertyChanged with async void handler in FolderListViewModel could cause memory leaks. I proactively refactored to use CommunityToolkit's partial method pattern.

3. **EnsureCreated limitation**: After reading EF Core documentation about production deployments, I realized EnsureCreated() doesn't track schema changes. I initiated the migration to MigrateAsync pattern for proper version control.

4. **ObjectDisposedException debugging**: When encountering this error, I traced through the code myself and discovered Singleton ViewModels were holding onto disposed Scoped DbContext. I proposed the fix by changing ViewModel lifetime to Singleton (since they don't directly hold DbContext anymore).

## Independence and Responsibility

We confirm that the use of AI in this work has been fully and accurately documented to the best of our knowledge.
We take responsibility for the technical accuracy, the selection of adopted content, and the entire submitted work.

- Date: 21.07.2026 (last updated)
- Group name: StudyMate