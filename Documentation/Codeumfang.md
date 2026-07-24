# Codeumfang-Analyse (cloc)

**Datum der Analyse:** 24. Juli 2026  
**Werkzeug:** cloc v2.10 (https://github.com/AlDanial/cloc)  
**Projekt:** StudyMate 

---

## Zusammenfassung

| Kategorie | Anzahl Zeilen |
|-----------|---------------|
| **Gesamt (C# + XAML)** | **3.652** |
| Davon C#-Code | 2.223 |
| Davon XAML-Code | 1.429 |
| Kommentarzeilen | 513 |
| Leerzeilen | 864 |

**Anzahl Dateien:** 52 (44 C#, 8 XAML)  
**Analysezeit:** 0,27 Sekunden

---

## Detailergebnis nach Dateien (sortiert nach Code-Zeilen)

### Top 20 Dateien nach Code-Umfang

| Rang | Datei | Kommentare | Leerzeilen | Code-Zeilen |
|------|-------|------------|------------|-------------|
| 1 | `Resources/Styles.xaml` | 37 | 23 | 443 |
| 2 | `Views/FileDetailView.xaml` | 17 | 126 | 345 |
| 3 | `ViewModels/FileDetailViewModel.cs` | 9 | 72 | 337 |
| 4 | `Views/FileListView.xaml` | 12 | 112 | 304 |
| 5 | `Views/FolderListView.xaml` | 11 | 71 | 227 |
| 6 | `ViewModels/FileListViewModel.cs` | 17 | 32 | 197 |
| 7 | `Integrations/Ai/AipromptBuilder.cs` | 28 | 42 | 160 |
| 8 | `Migrations/AppDbContextModelSnapshot.cs` | 1 | 41 | 124 |
| 9 | `Integrations/Ai/AiClient.cs` | 36 | 30 | 123 |
| 10 | `ViewModels/FolderListViewModel.cs` | 10 | 24 | 102 |
| 11 | `Migrations/20260713142448_InitialCreate.cs` | 3 | 9 | 97 |
| 12 | `ViewModels/QuizQuestionViewModel.cs` | 0 | 18 | 88 |
| 13 | `App.xaml.cs` | 8 | 23 | 85 |
| 14 | `Converters/Converters.cs` | 93 | 11 | 73 |
| 15 | `Data/AppDbContext.cs` | 10 | 29 | 73 |
| 16 | `Services/FileStorageService.cs` | 3 | 13 | 66 |
| 17 | `Services/StudyFileService.cs` | 5 | 12 | 66 |
| 18 | `Services/AiAnalysisService.cs` | 3 | 14 | 62 |
| 19 | `Resources/Colors.xaml` | 21 | 8 | 56 |
| 20 | `ViewModels/QuizOptionViewModel.cs` | 0 | 12 | 52 |

### Vollständige Liste aller Dateien

| Datei | Leerzeilen | Kommentare | Code |
|-------|------------|------------|------|
| `StudyMate.Wpf/Resources/Styles.xaml` | 23 | 37 | 443 |
| `StudyMate.Wpf/Views/FileDetailView.xaml` | 126 | 17 | 345 |
| `StudyMate.Wpf/ViewModels/FileDetailViewModel.cs` | 72 | 9 | 337 |
| `StudyMate.Wpf/Views/FileListView.xaml` | 112 | 12 | 304 |
| `StudyMate.Wpf/Views/FolderListView.xaml` | 71 | 11 | 227 |
| `StudyMate.Wpf/ViewModels/FileListViewModel.cs` | 32 | 17 | 197 |
| `StudyMate.Wpf/Integrations/Ai/AipromptBuilder.cs` | 42 | 28 | 160 |
| `StudyMate.Wpf/Migrations/AppDbContextModelSnapshot.cs` | 41 | 1 | 124 |
| `StudyMate.Wpf/Integrations/Ai/AiClient.cs` | 30 | 36 | 123 |
| `StudyMate.Wpf/ViewModels/FolderListViewModel.cs` | 24 | 10 | 102 |
| `StudyMate.Wpf/Migrations/20260713142448_InitialCreate.cs` | 9 | 3 | 97 |
| `StudyMate.Wpf/ViewModels/QuizQuestionViewModel.cs` | 18 | 0 | 88 |
| `StudyMate.Wpf/App.xaml.cs` | 23 | 8 | 85 |
| `StudyMate.Wpf/Converters/Converters.cs` | 11 | 93 | 73 |
| `StudyMate.Wpf/Data/AppDbContext.cs` | 29 | 10 | 73 |
| `StudyMate.Wpf/Services/FileStorageService.cs` | 13 | 3 | 66 |
| `StudyMate.Wpf/Services/StudyFileService.cs` | 12 | 5 | 66 |
| `StudyMate.Wpf/Services/AiAnalysisService.cs` | 14 | 3 | 62 |
| `StudyMate.Wpf/Resources/Colors.xaml` | 8 | 21 | 56 |
| `StudyMate.Wpf/ViewModels/QuizOptionViewModel.cs` | 12 | 0 | 52 |
| `StudyMate.Wpf/Views/FileListView.xaml.cs` | 10 | 0 | 49 |
| `StudyMate.Wpf/Models/StudyFile.cs` | 18 | 0 | 48 |
| `StudyMate.Wpf/Repositories/StudyFileRepository.cs` | 6 | 5 | 47 |
| `StudyMate.Wpf/Repositories/AiAnalysisRepository.cs` | 6 | 4 | 33 |
| `StudyMate.Wpf/MainWindow.xaml` | 14 | 0 | 31 |
| `StudyMate.Wpf/Services/StudyFolderService.cs` | 8 | 3 | 30 |
| `StudyMate.Wpf/Repositories/StudyFolderRepository.cs` | 4 | 3 | 27 |
| `StudyMate.Wpf/Integrations/Ai/PdfTextExtractor.cs` | 3 | 4 | 24 |
| `StudyMate.Wpf/MainWindow.xaml.cs` | 4 | 0 | 23 |
| `StudyMate.Wpf/ViewModels/MainViewModel.cs` | 5 | 0 | 20 |
| `StudyMate.Wpf/Models/AiAnalysis.cs` | 11 | 0 | 18 |
| `StudyMate.Wpf/Views/FileDetailView.xaml.cs` | 2 | 1 | 17 |
| `StudyMate.Wpf/App.xaml` | 0 | 0 | 15 |
| `StudyMate.Wpf/Data/AppDbContextFactory.cs` | 4 | 12 | 15 |
| `StudyMate.Wpf/Models/StudyFolder.cs` | 5 | 0 | 13 |
| `StudyMate.Wpf/Models/Ai/StructuredContent.cs` | 2 | 0 | 12 |
| `StudyMate.Wpf/Services/Interfaces/IStudyFileService.cs` | 4 | 15 | 12 |
| `StudyMate.Wpf/Models/Ai/AiStudyMaterialResult.cs` | 5 | 0 | 11 |
| `StudyMate.Wpf/Repositories/Interfaces/IStudyFileRepository.cs` | 4 | 39 | 11 |
| `StudyMate.Wpf/ViewModels/ViewModelBase.cs` | 3 | 0 | 11 |
| `StudyMate.Wpf/Views/FolderListView.xaml.cs` | 1 | 0 | 11 |
| `StudyMate.Wpf/Models/Ai/AiSettings.cs` | 3 | 0 | 10 |
| `StudyMate.Wpf/Models/Ai/QuizQuestion.cs` | 3 | 0 | 10 |
| `StudyMate.Wpf/Repositories/Interfaces/IAiAnalysisRepository.cs` | 3 | 32 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IAiAnalysisService.cs` | 2 | 10 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IFileStorageService.cs` | 2 | 9 | 10 |
| `StudyMate.Wpf/Services/Interfaces/IStudyFolderService.cs` | 2 | 10 | 9 |
| `StudyMate.Wpf/Integrations/Ai/Interfaces/IAiClient.cs` | 1 | 9 | 8 |
| `StudyMate.Wpf/Resources/Converters.xaml` | 3 | 2 | 8 |
| `StudyMate.Wpf/Integrations/Ai/Interfaces/IPdfTextExtractor.cs` | 0 | 9 | 7 |
| `StudyMate.Wpf/Repositories/Interfaces/IStudyFolderRepository.cs` | 3 | 18 | 7 |
| `StudyMate.Wpf/AssemblyInfo.cs` | 1 | 4 | 5 |

---

## Aufschlüsselung nach Sprachkategorien

| Sprache | Dateien | Leerzeilen | Kommentare | Code |
|---------|---------|------------|------------|------|
| **C#** | 44 | 507 | 413 | 2.223 |
| **XAML** | 8 | 357 | 100 | 1.429 |
| **SUMME** | **52** | **864** | **513** | **3.652** |

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
| Resources/Styles.xaml | XAML | 443 |
| FileDetailView.xaml | XAML | 345 |
| FileListView.xaml | XAML | 304 |
| FolderListView.xaml | XAML | 227 |
| Resources/Colors.xaml | XAML | 56 |
| MainWindow.xaml | XAML | 31 |
| Resources/Converters.xaml | XAML | 8 |
| App.xaml | XAML | 15 |
| FileListView.xaml.cs | C# | 49 |
| FileDetailView.xaml.cs | C# | 17 |
| FolderListView.xaml.cs | C# | 11 |
| MainWindow.xaml.cs | C# | 23 |
| **SUMME** | | **1.524** |

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
| AiAnalysisRepository.cs | 33 |
| StudyFolderRepository.cs | 27 |
| **SUMME** | **107** |

### Models (C#)

| Datei | Code-Zeilen |
|-------|-------------|
| StudyFile.cs | 48 |
| AiAnalysis.cs | 18 |
| StudyFolder.cs | 13 |
| Ai/StructuredContent.cs | 12 |
| Ai/AiStudyMaterialResult.cs | 11 |
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
| Gesamt (C# + XAML) | 3.431 |
| Davon C#-Code | 2.002 |
| Davon XAML-Code | 1.429 |

**Berechnung:** 3.652 − 221 = 3.431 Code-Zeilen (manuell erstellt oder KI-gestützt)

### Kommentare im Code

Der Anteil der Kommentarzeilen am Gesamtcode beträgt:

- **Absolut:** 513 Kommentarzeilen
- **Relativ:** 14,0 % der Gesamtzeilen (513 / 3.652)
- **Im Verhältnis zum Code:** 18,8 % (513 / 2.727 Code + Kommentare)

Der höhere Kommentaranteil resultiert aus der XML-Dokumentation für wichtige Interfaces, Services und ViewModel-Methoden gemäß dem dokumentierten XML-Dokumentationsrichtlinien.

---

## Vergleich mit anderen Projekten

Die folgenden Werte können als Referenz für ähnliche WPF-Projekte dienen:

| Metrik | StudyMate | Typisches WPF-Projekt |
|--------|-----------|----------------------|
| Gesamtzeilen (C# + XAML) | 3.652 | 2.000 – 10.000 |
| XAML-Anteil | 39,1 % | 30–40 % |
| Kommentaranteil | 14,0 % | 5–15 % |
| Dateien gesamt | 52 | 20–100 |

**Hinweis:** Diese Vergleichswerte sind Schätzwerte basierend auf typischen WPF-Anwendungen mit MVVM-Architektur.

---

*Erstellt mit cloc v2.10 am 24. Juli 2026. Letzte Aktualisierung: 3.652 Code-Zeilen in 52 Dateien.*
