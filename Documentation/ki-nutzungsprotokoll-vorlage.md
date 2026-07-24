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
| 16.07.2026 | Truc Trinh | Lisa Pro | Modern UI Redesign (Purple Theme) | **What AI did:** Applied purple theme, fixed database connection, corrected UI bindings<br>**What we did:** Designed card-based file cards, built three-state display logic, tested UI transitions |
| 16.07.2026 | Truc Trinh | Lisa Pro | Quiz Feature | **What AI did:** Built quiz ViewModel classes, implemented answer checking<br>**What we did:** Created three-panel layout, designed quiz UI with color feedback, wrote ML test questions |
| 21.07.2026 | Truc Trinh | Lisa Pro | PDF Analysis with AI | **What AI did:** Fixed API errors (HTTP 405), implemented PDF text extraction, set up JSON calls<br>**What we did:** Installed PDF reader library, connected to Lisa API, debugged responses, tested full flow |
|  |  |  |  |  |

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

## Independence and Responsibility

We confirm that the use of AI in this work has been fully and accurately documented to the best of our knowledge.
We take responsibility for the technical accuracy, the selection of adopted content, and the entire submitted work.

- Date: 21.07.2026 (last updated)
- Group name: StudyMate