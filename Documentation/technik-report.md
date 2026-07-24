# Technik-Report: StudyMate

**Projekt:** StudyMate – AI-powered Study Assistant  
**Datum:** 24. Juli 2026  
**Repository:** https://github.com/NET-2026/thi-thanh-truc-trinh.git  

---

## 1. Technischer Überblick

| Bereich | Verwendete Technologie |
|---------|------------------------|
| Programmiersprache | C# |
| Benutzeroberfläche | WPF und XAML |
| Framework | .NET 10 |
| Architektur | MVVM, Service Layer, Repository Pattern |
| Datenbank | SQLite mit Entity Framework Core |
| Externer Dienst | Lisa Chat API |
| PDF-Verarbeitung | PdfPig |

**Quellen:** `StudyMate.Wpf.csproj`, `README.md`

---

## 2. Projektstruktur

| Ordner / Bereich | Aufgabe |
|------------------|---------|
| Models | Datenmodelle der Anwendung (StudyFolder, StudyFile, AiAnalysis) |
| Views | Benutzeroberflächen in XAML (FolderListView, FileListView, FileDetailView) |
| ViewModels | Zustände, Commands und Präsentationslogik (MVVM-Pattern) |
| Services | Geschäftslogik und Verarbeitung (FileStorageService, AiAnalysisService) |
| Repositories | Datenbankzugriff mit Entity Framework Core |
| Data | DbContext und Datenbankkonfiguration |
| Integrations/Ai | PDF-Textextraktion und Kommunikation mit Lisa API |
| Migrations | Automatisch erzeugte Datenbankmigrationen durch EF Core Tools |
| Converters | Umwandlung von Werten für XAML-Bindings (z.B. Null zu Visibility) |

**Gesamtumfang:** 49 Dateien (44 C#, 5 XAML), insgesamt 3.454 Code-Zeilen  
**Quelle:** `Documentation/Codeumfang.md`

---

## 3. Verarbeitung mit Lisa API

| Schritt | Beschreibung |
|---------|--------------|
| 1. PDF-Upload | Der Benutzer lädt eine PDF-Datei über die Benutzeroberfläche hoch. |
| 2. Textextraktion | Die Bibliothek PdfPig extrahiert den Text aus der PDF-Datei. |
| 3. API-Anfrage | Der extrahierte Text wird an die Lisa Chat API gesendet. |
| 4. Analyse | Lisa erstellt eine Zusammenfassung, strukturierte Lerninhalte und Quizfragen. |
| 5. Speicherung | Die Ergebnisse werden in der SQLite-Datenbank gespeichert. |
| 6. Anzeige | Zusammenfassung und Quiz werden in der WPF-Oberfläche angezeigt. |

**Konfiguration:** Die API-Einstellungen (BaseUrl, Endpoint, ApiKey, Model) werden über eine `.env`-Datei verwaltet.  
**Quelle:** `.env.example`, `Integrations/Ai/`

---

## 4. Datenbankstruktur

Die Anwendung verwendet eine lokale SQLite-Datenbank mit Entity Framework Core.

### Tabelle: StudyFolder

| Feld | Bedeutung |
|------|-----------|
| Id | Eindeutige Identifikation des Ordners |
| Name | Name des Lernordners |
| CreatedAt | Zeitpunkt der Erstellung |
| UpdatedAt | Zeitpunkt der letzten Änderung |

### Tabelle: StudyFile

| Feld | Bedeutung |
|------|-----------|
| Id | Eindeutige Identifikation der Datei |
| FolderId | Verknüpfung mit dem zugehörigen Lernordner |
| OriginalFileName | Ursprünglicher Name der PDF-Datei |
| StoredFileName | Eindeutiger interner Dateiname |
| FilePath | Speicherinformation der Datei |
| FileSizeBytes | Größe der Datei |
| UploadedAt | Zeitpunkt des Uploads |

### Tabelle: AiAnalysis

| Feld | Bedeutung |
|------|-----------|
| Id | Eindeutige Identifikation der Analyse |
| StudyFileId | Verknüpfung mit der analysierten PDF-Datei |
| Name | Titel des analysierten Lernmaterials |
| Summary | Von Lisa erzeugte Zusammenfassung |
| StructuredContentJson | Strukturierte Lerninhalte im JSON-Format |
| QuizJson | Quizfragen und Antworten im JSON-Format |
| Status | Status der Verarbeitung |
| ErrorMessage | Fehlermeldung bei einer fehlgeschlagenen Analyse |
| ModelName | Verwendetes KI-Modell |

### Beziehungen

```
StudyFolder 1 ─── n StudyFile
StudyFile   1 ─── n AiAnalysis
```

- Ein Lernordner kann mehrere PDF-Dateien enthalten.
- Eine PDF-Datei kann mehrere Analyseergebnisse besitzen.
- Die PDF-Dateien werden lokal gespeichert (`%LOCALAPPDATA%\StudyMate\uploads\`); ihre Metadaten werden in SQLite verwaltet.

**Speicherort der Datenbank:** `%LOCALAPPDATA%\StudyMate\studymate.db`  
**Quelle:** `Data/AppDbContext.cs`, `README.md`

---

*Dieser Bericht basiert ausschließlich auf Informationen aus dem Repository.*
