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

| Tabelle | Inhalt |
|---------|--------|
| StudyFolders | Speichert Lernordner und deren Namen. |
| StudyFiles | Speichert Metadaten zu hochgeladenen PDF-Dateien (Dateiname, Größe, Upload-Datum). |
| AiAnalyses | Speichert Zusammenfassungen, Quizfragen und den Status der KI-Analyse. |

**Beziehungen:**

- Ein `StudyFolder` kann mehrere `StudyFiles` enthalten.
- Eine `StudyFile` kann mehrere `AiAnalyses` besitzen.
- Die eigentlichen PDF-Dateien werden lokal im Anwendungsordner gespeichert (`%LOCALAPPDATA%\StudyMate\uploads\`).

**Speicherort der Datenbank:** `%LOCALAPPDATA%\StudyMate\studymate.db`  
**Quelle:** `Data/AppDbContext.cs`, `README.md`

---

*Dieser Bericht basiert ausschließlich auf Informationen aus dem Repository.*
