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
| 16.07.2026 | Truc Trinh | Lisa Pro | UI Redesign - Modern Purple Theme & Two-Panel Layout | **AI Guidance:**<br>- Redesigned FolderListView and FileListView XAML with modern purple theme (#6C4CF1)<br>- Refactored MainViewModel to coordinate between FolderListViewModel and FileListViewModel<br>- Implemented folder selection communication via constructor injection<br>- Fixed ObjectDisposedException by changing DbContext lifetime from Singleton to Scoped<br>- Fixed UI state management: prevented duplicate empty states on initial load<br>- Fixed binding error: added Mode=OneWay to readonly Files.Count property<br>- Removed Mode=OneTime from dynamic bindings to enable real-time UI updates<br>- Translated all Vietnamese comments to English<br><br>**My proactive work:**<br>- Designed modern card-based UI for files with PDF icons and action menus<br>- Implemented three-state UI logic (no folder / empty folder / has files)<br>- Added visibility triggers for Upload button, headers, and file lists<br>- Integrated file dialogs for upload/replace operations<br>- Tested all UI states and transitions end-to-end<br>- Verified dependency injection scope management |
|  |  |  |  |  |

## Optional Supplementary Notes

**Handling incorrect AI responses:**
- The AI initially suggested `Mode=OneTime` for Files.Count binding, which prevented UI updates. I identified this issue during testing and changed it to `Mode=OneWay` to maintain reactivity while respecting the readonly property.

**Deliberately rejected suggestions:**
- Rejected using code-behind or converters for complex visibility logic. Instead, insisted on pure XAML MultiDataTriggers for better maintainability and separation of concerns.

**AI as sparring partner:**
- Used AI primarily for architecture review (Repository vs Service pattern)
- Validated dependency injection lifetime choices (Scoped vs Singleton)
- Code review and bug identification in existing implementations

## Independence and Responsibility

We confirm that the use of AI in this work has been fully and accurately documented to the best of our knowledge.
We take responsibility for the technical accuracy, the selection of adopted content, and the entire submitted work.

- Date: 16.07.2026
- Group name: StudyMate