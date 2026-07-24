# Codeumfang-Analyse (cloc)

**Datum der Analyse:** 24. Juli 2026  
**Werkzeug:** cloc v2.10 (https://github.com/AlDanial/cloc)  
**Projekt:** StudyMate  

---

## Zusammenfassung

| Kategorie | Anzahl Zeilen |
|-----------|---------------|
| **Gesamt (C# + XAML)** | **3.454** |
| Davon C#-Code | 2.207 |
| Davon XAML-Code | 1.247 |
| Kommentarzeilen | 70 |
| Leerzeilen | 975 |

**Anzahl Dateien:** 49 (44 C#, 5 XAML)  
**Analysezeit:** 1,23 Sekunden  

---

## Detailergebnis nach Dateien (sortiert nach Code-Zeilen)

### Top 20 Dateien nach Code-Umfang

| Rang | Datei | Kommentare | Leerzeilen | Code-Zeilen |
|------|-------|------------|------------|-------------|
| 1 | `Views/FileDetailView.xaml` | 17 | 210 | 483 |
| 2 | `Views/FileListView.xaml` | 12 | 180 | 396 |
| 3 | `ViewModels/FileDetailViewModel.cs` | 2 | 72 | 337 |
| 4 | `Views/FolderListView.xaml` | 15 | 117 | 325 |
| 5 | `ViewModels/FileListViewModel.cs` | 1 | 26 | 197 |
| 6 | `Integrations/Ai/AipromptBuilder.cs` | 0 | 42 | 160 |
| 7 | `Migrations/AppDbContextModelSnapshot.cs` | 1 | 41 | 124 |
| 8 | `Integrations/Ai/AiClient.cs` | 2 | 30 | 123 |
| 9 | `ViewModels/FolderListViewModel.cs` | 0 | 19 | 102 |
| 10 | `Migrations/20260713142448_InitialCreate.cs` | 3 | 9 | 97 |
| 11 | `ViewModels/QuizQuestionViewModel.cs` | 0 | 18 | 88 |
| 12 | `App.xaml.cs` | 8 | 23 | 85 |
| 13 | `Converters/Converters.cs` | 0 | 11 | 73 |
| 14 | `Services/FileStorageService.cs` | 2 | 13 | 66 |
| 15 | `Services/StudyFileService.cs` | 1 | 11 | 66 |
| 16 | `Services/AiAnalysisService.cs` | 0 | 14 | 62 |
| 17 | `Data/AppDbContext.cs` | 0 | 11 | 56 |
| 18 | `ViewModels/QuizOptionViewModel.cs` | 1 | 12 | 52 |
| 19 | `Views/FileListView.xaml.cs` | 0 | 10 | 49 |
| 20 | `Models/StudyFile.cs` | 0 | 15 | 48 |

### Vollständige Liste aller Dateien

| Datei | Leerzeilen | Kommentare | Code |
|-------|------------|------------|------|
| `StudyMate.Wpf/Views/FileDetailView.xaml` | 210 | 17 | 483 |
| `StudyMate.Wpf/Views/FileListView.xaml` | 180 | 12 | 396 |
| `StudyMate.Wpf/ViewModels/FileDetailViewModel.cs` | 72 | 2 | 337 |
| `StudyMate.Wpf/Views/FolderListView.xaml` | 117 | 15 | 325 |
| `StudyMate.Wpf/ViewModels/FileListViewModel.cs` | 26 | 1 | 197 |
| `StudyMate.Wpf/Integrations/Ai/AipromptBuilder.cs` | 42 | 0 | 160 |
| `StudyMate.Wpf/Migrations/AppDbContextModelSnapshot.cs` | 41 | 1 | 124 |
| `StudyMate.Wpf/Integrations/Ai/AiClient.cs` | 30 | 2 | 123 |
| `StudyMate.Wpf/ViewModels/FolderListViewModel.cs` | 19 | 0 | 102 |
| `StudyMate.Wpf/Migrations/20260713142448_InitialCreate.cs` | 9 | 3 | 97 |
| `StudyMate.Wpf/ViewModels/QuizQuestionViewModel.cs` | 18 | 0 | 88 |
| `StudyMate.Wpf/App.xaml.cs` | 23 | 8 | 85 |
| `StudyMate.Wpf/Converters/Converters.cs` | 11 | 0 | 73 |
| `StudyMate.Wpf/Services/FileStorageService.cs` | 13 | 2 | 66 |
| `StudyMate.Wpf/Services/StudyFileService.cs` | 11 | 1 | 66 |
| `StudyMate.Wpf/Services/AiAnalysisService.cs` | 14 | 0 | 62 |
| `StudyMate.Wpf/Data/AppDbContext.cs` | 11 | 0 | 56 |
| `StudyMate.Wpf/ViewModels/QuizOptionViewModel.cs` | 12 | 1 | 52 |
| `StudyMate.Wpf/Views/FileListView.xaml.cs` | 10 | 0 | 49 |
| `StudyMate.Wpf/Models/StudyFile.cs` | 15 | 0 | 48 |
| `StudyMate.Wpf/Repositories/StudyFileRepository.cs` | 1 | 0 | 47 |
| `StudyMate.Wpf/Repositories/AiAnalysisRepository.cs` | 6 | 0 | 34 |
| `StudyMate.Wpf/MainWindow.xaml` | 14 | 0 | 31 |
| `StudyMate.Wpf/Services/StudyFolderService.cs` | 8 | 0 | 30 |
| `StudyMate.Wpf/Repositories/StudyFolderRepository.cs` | 4 | 0 | 27 |
| `StudyMate.Wpf/Integrations/Ai/PdfTextExtractor.cs` | 3 | 0 | 24 |
| `StudyMate.Wpf/MainWindow.xaml.cs` | 4 | 0 | 23 |
| `StudyMate.Wpf/ViewModels/MainViewModel.cs` | 3 | 0 | 20 |
| `StudyMate.Wpf/Models/AiAnalysis.cs` | 9 | 0 | 18 |
| `StudyMate.Wpf/Views/FileDetailView.xaml.cs` | 2 | 1 | 17 |
| `StudyMate.Wpf/Data/AppDbContextFactory.cs` | 4 | 0 | 15 |
| `StudyMate.Wpf/Models/StudyFolder.cs` | 2 | 0 | 13 |
| `StudyMate.Wpf/App.xaml` | 0 | 0 | 12 |
| `StudyMate.Wpf/Models/Ai/StructuredContent.cs` | 1 | 0 | 12 |
| `StudyMate.Wpf/Services/Interfaces/IStudyFileService.cs` | 2 | 0 | 12 |
| `StudyMate.Wpf/Models/Ai/AiStudyMaterialResult.cs` | 5 | 0 | 11 |
| `StudyMate.Wpf/Repositories/Interfaces/IStudyFileRepository.cs` | 2 | 0 | 11 |
| `StudyMate.Wpf/ViewModels/ViewModelBase.cs` | 3 | 0 | 11 |
| `StudyMate.Wpf/Views/FolderListView.xaml.cs` | 1 | 0 | 11 |
| `StudyMate.Wpf/Models/Ai/AiSettings.cs` | 3 | 0 | 10 |
| `StudyMate.Wpf/Models/Ai/QuizQuestion.cs` | 1 | 0 | 10 |
| `StudyMate.Wpf/Repositories/Interfaces/IAiAnalysisRepository.cs` | 2 | 0 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IAiAnalysisService.cs` | 3 | 0 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IFileStorageService.cs` | 2 | 0 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IStudyFolderService.cs` | 1 | 0 | 9 |
| `StudyMate.Wpf/Integrations/Ai/Interfaces/IAiClient.cs` | 1 | 0 | 8 |
| `StudyMate.Wpf/Integrations/Ai/Interfaces/IPdfTextExtractor.cs` | 0 | 0 | 7 |
| `StudyMate.Wpf/Repositories/Interfaces/IStudyFolderRepository.cs` | 3 | 0 | 7 |
| `StudyMate.Wpf/AssemblyInfo.cs` | 1 | 4 | 5 |

---

## Aufschlüsselung nach Sprachkategorien

| Sprache | Dateien | Leerzeilen | Kommentare | Code |
|---------|---------|------------|------------|------|
| **C#** | 44 | 454 | 26 | 2.207 |
| **XAML** | 5 | 521 | 44 | 1.247 |
| **SUMME** | **49** | **975** | **70** | **3.454** |

---

## Verteilung nach Kategorien

### ViewModels (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| FileDetailViewModel.cs | 337 |
| FileListViewModel.cs | 197 |
| FolderListViewModel.cs | 102 |
| QuizQuestionViewModel.cs | 88 |
| QuizOptionViewModel.cs | 52 |
| MainViewModel.cs | 20 |
| ViewModelBase.cs | 11 |
| **SUMME** | **807** |

### Views (XAML + Code-Behind)

| Datei | Typ | Code-Zeilen |
|-------|-----|-------------|
| FileDetailView.xaml | XAML | 483 |
| FileListView.xaml | XAML | 396 |
| FolderListView.xaml | XAML | 325 |
| MainWindow.xaml | XAML | 31 |
| FileListView.xaml.cs | C# | 49 |
| FileDetailView.xaml.cs | C# | 17 |
| FolderListView.xaml.cs | C# | 11 |
| MainWindow.xaml.cs | C# | 23 |
| **SUMME** | | **1.335** |

### Services (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| FileStorageService.cs | 66 |
| StudyFileService.cs | 66 |
| AiAnalysisService.cs | 62 |
| StudyFolderService.cs | 30 |
| **SUMME** | **224** |

### Repositories (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| StudyFileRepository.cs | 47 |
| AiAnalysisRepository.cs | 34 |
| StudyFolderRepository.cs | 27 |
| **SUMME** | **108** |

### Models (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| StudyFile.cs | 48 |
| AiAnalysis.cs | 18 |
| StudyFolder.cs | 13 |
| Ai/AiStudyMaterialResult.cs | 11 |
| Ai/StructuredContent.cs | 12 |
| Ai/AiSettings.cs | 10 |
| Ai/QuizQuestion.cs | 10 |
| **SUMME** | **122** |

### Integrations / AI (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| AipromptBuilder.cs | 160 |
| AiClient.cs | 123 |
| PdfTextExtractor.cs | 24 |
| Interfaces/IAiClient.cs | 8 |
| Interfaces/IPdfTextExtractor.cs | 7 |
| **SUMME** | **322** |

### Datenzugriff (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| AppDbContext.cs | 56 |
| AppDbContextFactory.cs | 15 |
| **SUMME** | **71** |

### Converters (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| Converters.cs | 73 |
| **SUMME** | **73** |

### Migrations (automatisch generiert)

| Datei | Code-Zeilen | Hinweis |
|-------|-------------|---------|
| AppDbContextModelSnapshot.cs | 124 | Automatisch generiert |
| 20260713142448_InitialCreate.cs | 97 | Automatisch generiert |
| **SUMME** | **221** | **Nicht manuell geschrieben** |

### Interfaces (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| Repositories/Interfaces/IStudyFileRepository.cs | 11 |
| Repositories/Interfaces/IAiAnalysisRepository.cs | 10 |
| Repositories/Interfaces/IStudyFolderRepository.cs | 7 |
| Services/Interfaces/IStudyFileService.cs | 12 |
| Services/Interfaces/IAiAnalysisService.cs | 10 |
| Services/Interfaces/IFileStorageService.cs | 10 |
| Services/Interfaces/IStudyFolderService.cs | 9 |
| Integrations/Ai/Interfaces/IAiClient.cs | 8 |
| Integrations/Ai/Interfaces/IPdfTextExtractor.cs | 7 |
| **SUMME** | **84** |

---

## cloc-Kommandozeile

Die Analyse wurde mit folgendem Befehl durchgeführt:

```bash
cloc.exe . --include-lang="C#,XAML" --exclude-dir=bin,obj --by-file-by-lang
```

**Parameter:**
- `--include-lang="C#,XAML"`: Nur C# und XAML-Dateien analysieren
- `--exclude-dir=bin,obj`: Build-Output-Verzeichnisse ausschließen
- `--by-file-by-lang`: Detaillierte Ausgabe pro Datei

---

## Hinweise zur Interpretation

### Automatisch generierte Dateien

Folgende Dateien wurden automatisch von Entity Framework Core Tools generiert und sollten bei der Betrachtung des manuell geschriebenen Codes separat betrachtet werden:

- `Migrations/AppDbContextModelSnapshot.cs` (124 Code-Zeilen)
- `Migrations/20260713142448_InitialCreate.cs` (97 Code-Zeilen)

**Gesamtzeilen automatisch generiert:** 221 Code-Zeilen

### Bereinigter Codeumfang (ohne automatisch generierten Code)

| Kategorie | bereinigte Zeilen |
|-----------|-------------------|
| Gesamt (C# + XAML) | 3.233 |
| Davon C#-Code | 1.986 |
| Davon XAML-Code | 1.247 |

**Berechnung:** 3.454 − 221 = 3.233 Code-Zeilen (manuell erstellt oder KI-gestützt)

### Kommentare im Code

Der Anteil der Kommentarzeilen am Gesamtcode beträgt:

- **Absolut:** 70 Kommentarzeilen
- **Relativ:** 2,0 % der Gesamtzeilen (70 / 3.454)
- **Im Verhältnis zum Code:** 2,1 % (70 / 3.384 Code + Kommentare)

Dies ist ein typischer Wert für moderne C#/WPF-Projekte, wo Code weitgehend selbsterklärend durch aussagekräftige Namen strukturiert ist.

---

## Vergleich mit anderen Projekten

Die folgenden Werte können als Referenz für ähnliche WPF-Projekte dienen:

| Metrik | StudyMate | Typisches WPF-Projekt |
|--------|-----------|----------------------|
| Gesamtzeilen (C# + XAML) | 3.454 | 2.000 – 10.000 |
| XAML-Anteil | 36,1 % | 30–40 % |
| Kommentaranteil | 2,0 % | 5–15 % |
| Dateien gesamt | 49 | 20–100 |

**Hinweis:** Diese Vergleichswerte sind Schätzwerte basierend auf typischen WPF-Anwendungen mit MVVM-Architektur.

---

*Erstellt mit cloc v2.10 am 24. Juli 2026.*
