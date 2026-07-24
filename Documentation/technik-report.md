# Technik-Report: StudyMate

**Datum:** 24. Juli 2026  
**Projekt:** StudyMate - AI-powered Study Assistant  
**Autor:** Truc Trinh  

---

## 1. Externe Bibliotheken und Frameworks (NuGet Packages)

### Übersicht aller NuGet-Pakete

| Paket | Version | Projekt | Lizenz |
|-------|---------|---------|--------|
| CommunityToolkit.Mvvm | 8.4.2 | StudyMate.Wpf | MIT License |
| DotNetEnv | 3.1.1 | StudyMate.Wpf | MIT License |
| Microsoft.EntityFrameworkCore | 10.0.8 | StudyMate.Wpf | MIT License |
| Microsoft.EntityFrameworkCore.Design | 10.0.8 | StudyMate.Wpf | MIT License |
| Microsoft.EntityFrameworkCore.Sqlite | 10.0.8 | StudyMate.Wpf | MIT License |
| Microsoft.EntityFrameworkCore.Tools | 10.0.8 | StudyMate.Wpf | MIT License |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | StudyMate.Wpf | MIT License |
| Microsoft.Extensions.Http | 10.0.8 | StudyMate.Wpf | MIT License |
| PdfPig | 0.1.8 | StudyMate.Wpf | Apache License 2.0 |

### Detaillierte Beschreibung der Pakete

#### 1.1 CommunityToolkit.Mvvm (Version 8.4.2)
- **Projekt:** StudyMate.Wpf
- **Zweck:** MVVM-Framework für .NET, bereitgestellt von Microsoft Community Toolkit
- **Verwendung im Projekt:**
  - `ObservableObject` als Basisklasse für ViewModels
  - `[ObservableProperty]` Attribut für automatische Property-Benachrichtigungen
  - `[RelayCommand]` Attribut für Command-Implementierungen
  - Partial Methods für Property-Changed-Handler
- **Beispiel-Dateien/Klassen:**
  - `MainViewModel.cs` (Zeile 5): `public partial class MainViewModel : ObservableObject`
  - `FileListViewModel.cs` (Zeile 18-22): `[ObservableProperty] private ObservableCollection<StudyFile> files = new();`
  - `FolderListViewModel.cs` (Zeile 89): `[RelayCommand] private async Task CreateFolderAsync()`
  - `QuizQuestionViewModel.cs` (Zeile 4): Verwendung von `CommunityToolkit.Mvvm.Input`
- **Lizenz:** MIT License

#### 1.2 DotNetEnv (Version 3.1.1)
- **Projekt:** StudyMate.Wpf
- **Zweck:** Bibliothek zum Laden von .env-Dateien in .NET-Anwendungen
- **Verwendung im Projekt:**
  - Laden der API-Konfiguration aus `.env`-Datei beim Anwendungsstart
  - Umgebungsvariablen für AI-API-Zugriff (BaseUrl, Endpoint, ApiKey, Model)
- **Beispiel-Dateien/Klassen:**
  - `App.xaml.cs` (Zeile 101): `DotNetEnv.Env.Load();`
  - `App.xaml.cs` (Zeile 103-108): Auslesen der Environment-Variablen für AiSettings
- **Lizenz:** MIT License

#### 1.3 Microsoft.EntityFrameworkCore (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** Object-Relational Mapping (ORM) Framework für .NET
- **Verwendung im Projekt:**
  - Code-First Ansatz für Datenbankmodellierung
  - LINQ-Abfragen für Datenzugriff
  - Migrationen für Datenbank-Schema-Management
  - DbContext für Unit-of-Work Pattern
- **Beispiel-Dateien/Klassen:**
  - `AppDbContext.cs` (Zeile 1-67): Zentrale DbContext-Klasse mit allen Entitäten
  - `StudyFileRepository.cs` (Zeile 19-23): `await _dbContext.StudyFiles.Where(f => f.FolderId == folderId).ToListAsync()`
  - `StudyFolderRepository.cs` (Zeile 19-21): `await _dbContext.StudyFolders.OrderByDescending(x => x.CreatedAt).ToListAsync()`
  - `AiAnalysisRepository.cs` (Zeile 19-23): Komplexe Abfrage mit Where und OrderByDescending
- **Lizenz:** MIT License

#### 1.4 Microsoft.EntityFrameworkCore.Design (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** Design-Time-Unterstützung für Entity Framework Core Tools
- **Verwendung im Projekt:**
  - Ermöglicht EF Core CLI-Tools für Migrationen
  - Wird nur zur Entwicklungszeit verwendet (PrivateAssets=all)
- **Beispiel-Dateien/Klassen:**
  - `20260713142448_InitialCreate.cs`: Automatisch generierte Migration
  - `AppDbContextModelSnapshot.cs`: Momentaufnahme des Datenbankmodells
- **Lizenz:** MIT License

#### 1.5 Microsoft.EntityFrameworkCore.Sqlite (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** SQLite Database Provider für Entity Framework Core
- **Verwendung im Projekt:**
  - Lokale SQLite-Datenbank für persistente Datenspeicherung
  - Dateibasierte Datenbank ohne Server-Infrastruktur
- **Beispiel-Dateien/Klassen:**
  - `App.xaml.cs` (Zeile 72-74): `options.UseSqlite($"Data Source={dbPath}")`
  - Speicherort: `C:\Users\<username>\AppData\Local\StudyMate\studymate.db`
- **Lizenz:** MIT License

#### 1.6 Microsoft.EntityFrameworkCore.Tools (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** PowerShell- und CLI-Tools für Entity Framework Core
- **Verwendung im Projekt:**
  - Migration erstellen und verwalten
  - Datenbank-Schema aktualisieren
  - Wird nur zur Entwicklungszeit verwendet (PrivateAssets=all)
- **Beispiel-Dateien/Klassen:**
  - Verwendung über dotnet CLI: `dotnet ef migrations add InitialCreate`
- **Lizenz:** MIT License

#### 1.7 Microsoft.Extensions.DependencyInjection (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** Dependency Injection Container für .NET
- **Verwendung im Projekt:**
  - Inversion of Control (IoC) Container für alle Services, Repositories, ViewModels
  - Lebenszeitverwaltung (Singleton, Scoped, Transient)
  - Constructor Injection Pattern
- **Beispiel-Dateien/Klassen:**
  - `App.xaml.cs` (Zeile 68-115): ConfigureServices-Methode mit vollständiger DI-Konfiguration
  - Repository-Registrierung (Zeile 77-79): `services.AddScoped<IStudyFolderRepository, StudyFolderRepository>()`
  - Service-Registrierung (Zeile 82-84): `services.AddScoped<IStudyFileService, StudyFileService>()`
  - ViewModel-Registrierung (Zeile 87-90): `services.AddSingleton<MainViewModel>()`
- **Lizenz:** MIT License

#### 1.8 Microsoft.Extensions.Http (Version 10.0.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** HttpClient-Factory für typisierte Http-Clients
- **Verwendung im Projekt:**
  - Typisierter HttpClient für AI-API-Kommunikation
  - Automatische Lebenszeitverwaltung des HttpClient
  - Integration mit DI-Container
- **Beispiel-Dateien/Klassen:**
  - `App.xaml.cs` (Zeile 114): `services.AddHttpClient<IAiClient, AiClient>()`
  - `AiClient.cs` (Zeile 11-154): Implementierung des typisierten Clients mit HttpClient-Injected
- **Lizenz:** MIT License

#### 1.9 PdfPig (Version 0.1.8)
- **Projekt:** StudyMate.Wpf
- **Zweck:** PDF-Bibliothek zum Extrahieren von Text aus PDF-Dokumenten
- **Verwendung im Projekt:**
  - Textextraktion aus hochgeladenen PDF-Lernmaterialien
  - Vorverarbeitung vor AI-Analyse
  - Namespace: `UglyToad.PdfPig`
- **Beispiel-Dateien/Klassen:**
  - `PdfTextExtractor.cs` (Zeile 3): `using UglyToad.PdfPig;`
  - `PdfTextExtractor.cs` (Zeile 17-20): `using (var document = PdfDocument.Open(filePath)) { foreach (var page in document.GetPages()) { textBuilder.AppendLine(page.Text); } }`
  - `AiClient.cs` (Zeile 44-46): Aufruf von `_pdfTextExtractor.ExtractTextAsync(fullPath, cancellationToken)`
- **Lizenz:** Apache License 2.0

---

## 2. Frameworks und Plattformen

### .NET Version
- **Framework:** .NET 10.0
- **Target Framework Moniker:** `net10.0-windows`
- **Quelle:** `StudyMate.Wpf.csproj` (Zeile 5)

### WPF (Windows Presentation Foundation)
- **Plattform:** Windows-spezifische Desktop-UI-Framework
- **Aktivierung:** `<UseWPF>true</UseWPF>` in StudyMate.Wpf.csproj (Zeile 8)
- **Verwendung:**
  - XAML-basierte Benutzeroberflächen
  - Data Binding zwischen Views und ViewModels
  - Commands für Benutzerinteraktionen
  - Custom Value Converters für UI-Logik
- **Beispiel-Dateien:**
  - `MainWindow.xaml`: Hauptfenster mit dreispaltigem Layout
  - `FileListView.xaml`, `FolderListView.xaml`, `FileDetailView.xaml`: UserControls
  - `Converters.cs`: Implementiert `IValueConverter` und `IMultiValueConverter`

### Entity Framework Core (Code First Ansatz)
- **Version:** 10.0.8
- **Ansatz:** Code First Migrations
- **Datenbankprovider:** SQLite
- **Merkmale:**
  - Modelldefinition durch C#-Klassen (POCOs)
  - Fluent API in `OnModelCreating()` für Constraints und Beziehungen
  - Automatische Migrationserstellung und -ausführung
  - DbContext stellt Unit-of-Work Pattern bereit
- **Beispiel-Dateien:**
  - `AppDbContext.cs`: Zentrale DbContext-Klasse mit DbSet-Properties und Fluent API
  - `StudyFile.cs`, `StudyFolder.cs`, `AiAnalysis.cs`: Entitätsklassen
  - `20260713142448_InitialCreate.cs`: Automatisch generierte Migration

### MVVM-Architektur (Model-View-ViewModel)
- **Framework:** CommunityToolkit.Mvvm 8.4.2
- **Pattern-Implementierung:**
  - **Models:** Reine Datenklassen ohne UI-Abhängigkeiten
    - `StudyFile.cs`, `StudyFolder.cs`, `AiAnalysis.cs`
    - AI-Modelle: `AiStudyMaterialResult.cs`, `StructuredContent.cs`, `QuizQuestion.cs`
  - **ViewModels:** Präsentationslogik mit ObservableObject-Basis
    - `MainViewModel.cs`: Root-ViewModel koordiniert Child-ViewModels
    - `FolderListViewModel.cs`: Ordnerverwaltung
    - `FileListViewModel.cs`: Dateiverwaltung
    - `FileDetailViewModel.cs`: Detailansicht mit Summary und Quiz
    - `QuizQuestionViewModel.cs`, `QuizOptionViewModel.cs`: Quiz-spezifische ViewModels
  - **Views:** XAML-basierte Benutzeroberflächen
    - `MainWindow.xaml`: Hauptcontainer
    - `FolderListView.xaml`, `FileListView.xaml`, `FileDetailView.xaml`
- **Kommunikation:**
  - Property Changed Notifications über `[ObservableProperty]`
  - Commands über `[RelayCommand]`
  - Event-driven Communication über Events und Callbacks

### Datenbankprovider
- **System:** SQLite 3.x
- **Provider:** Microsoft.EntityFrameworkCore.Sqlite 10.0.8
- **Speicherort:** `C:\Users\<username>\AppData\Local\StudyMate\studymate.db`
- **Tabellen:**
  - `StudyFolders`: Ordner-Struktur (Id, Name, CreatedAt, UpdatedAt)
  - `StudyFile`: Dateien mit Metadaten (Id, FolderId, OriginalFileName, StoredFileName, FilePath, FileExtension, ContentType, FileSizeBytes, UploadedAt, CreatedAt, UpdatedAt)
  - `AiAnalysis`: KI-Analyseergebnisse (Id, StudyFileId, Name, Summary, StructuredContentJson, QuizJson, Status, ErrorMessage, ModelName, CreatedAt, UpdatedAt)
- **Beziehungen:**
  - StudyFolder 1:n StudyFile (Cascade Delete)
  - StudyFile 1:n AiAnalysis (Cascade Delete)
- **Migrationen:** Automatische Erstellung beim ersten Start via `dbContext.Database.EnsureCreated()`

---

## 3. Externe Dienste und APIs

### Lisa Chat API

#### Zweck
- KI-gestützte Analyse von PDF-Lernmaterialien
- Automatische Generierung von:
  - Zusammenfassungen (Summary)
  - Strukturierten Lerninhalten (StructuredContent)
  - Multiple-Choice-Fragen (QuizQuestions)

#### Konfigurationsdatei
- **Template:** `.env.example` im Projektroot
- **Laufzeitkonfiguration:** `.env` im Projektroot (nicht versioniert)
- **Umgebungsvariablen:**
  - `AiBaseUrl`: Basis-URL des API-Endpunkts
  - `AiEndpoint`: Spezifischer Endpoint für Chat Completions
  - `ApiKey`: Authentifizierungsschlüssel
  - `AiModel`: Modellname für die Analyse

#### Endpoint-Struktur
- **Base URL:** `https://chat-1.ki-awz.iisys.de/`
- **Endpoint:** `api/chat/completions`
- **Vollständige URL:** `https://chat-1.ki-awz.iisys.de/api/chat/completions`
- **HTTP-Methode:** POST
- **Request Format:** JSON (OpenAI-kompatibles Chat Completion Schema)
- **Response Format:** JSON mit Choice-Arrray und Message-Content

#### Request-Struktur (aus AiClient.cs)
```json
{
  "model": "lisa-pro-03-2026",
  "messages": [
    {
      "role": "system",
      "content": "<Prompt für Bildungsanalyse>"
    },
    {
      "role": "user",
      "content": "Analyze this study document:\n\n<extrahierter PDF-Text>"
    }
  ]
}
```

#### Response-Struktur
```json
{
  "choices": [
    {
      "message": {
        "content": "{ \"name\": \"...\", \"summary\": \"...\", \"structuredContent\": {...}, \"quizQuestions\": [...] }"
      }
    }
  ]
}
```

#### Implementierungsdetails
- **Client-Klasse:** `AiClient.cs` implementiert `IAiClient`
- **Textextraktion:** PdfPig extrahiert PDF-Text vor API-Aufruf
- **Prompt-Engineering:** `AiPromptBuilder.cs` definiert detaillierten System-Prompt
- **Fehlerbehandlung:** HTTP-Fehler werden als `HttpRequestException` geworfen
- **Timeout:** 5 Minuten pro Anfrage (`TimeSpan.FromMinutes(5)`)
- **Authentifizierung:** Bearer-Token via Authorization Header

#### Antwort-Validierung
- Entfernt Markdown Code-Fences (```` ```json ````)
- Parsen des JSON-Inhalts zu `AiStudyMaterialResult`
- Überprüfung auf leere Responses

#### Dateien im Zusammenhang:
- `Integrations/Ai/AiClient.cs`: HTTP-Client-Implementierung
- `Integrations/Ai/AiPromptBuilder.cs`: Prompt-Templates
- `Integrations/Ai/PdfTextExtractor.cs`: PDF-Textextraktion
- `Models/Ai/AiSettings.cs`: Konfigurationsmodell
- `.env.example`: Konfigurationstemplate

---

## 4. Entwicklungswerkzeuge

### Identifizierte Entwicklungswerkzeuge

| Werkzeug | Zweck | Version (falls bekannt) | Quelle |
|----------|-------|-------------------------|--------|
| Visual Studio 2022 | IDE für .NET-Entwicklung | 2022 (empfohlen) | README.md Zeile 9 |
| Git | Versionskontrolle | Unklar - muss vom Entwickler bestätigt werden | .git-Verzeichnis vorhanden |
| GitHub | Remote-Repository | Unklar - muss vom Entwickler bestätigt werden | README.md Zeile 17 |
| .NET SDK | Compiler und Runtime | .NET 10.0 | StudyMate.Wpf.csproj Zeile 5 |
| NuGet Package Manager | Paketverwaltung | Integriert in Visual Studio / .NET CLI | Projektdatei |
| Entity Framework Core Tools | Datenbank-Migrationen | 10.0.8 | StudyMate.Wpf.csproj Zeile 25, 28 |
| PowerShell | Skripting und Automatisierung | 5.1+ (Windows Standard) | README.md Zeile 89 |
| DB Browser for SQLite | Datenbank-Viewer | Unklar - muss vom Entwickler bestätigt werden | README.md Zeile 84 |
| SQLite CLI | Kommandozeilen-Datenbanktool | Unklar - muss vom Entwickler bestätigt werden | README.md Zeile 89 |
| winget | Windows Package Manager | Integriert in Windows 10/11 | README.md Zeile 90 |

### Details zu den Werkzeugen

#### Visual Studio 2022
- **Empfohlene Version:** Visual Studio 2022 oder später
- **Alternative:** VS Code mit C# Extension
- **Verwendung:** 
  - Projekt öffnen via `StudyMate.slnx`
  - Build und Debug mit F5
  - NuGet-Paketverwaltung über GUI

#### Git / GitHub
- **Repository-URL:** https://github.com/NET-2026/thi-thanh-truc-trinh.git
- **Verwendung:**
  - Versionskontrolle aller Quelldateien
  - Kollaboration über Pull Requests
  - Historie dokumentiert KI-Nutzung (siehe ki-nutzungsprotokoll-vorlage.md)

#### .NET CLI
- **Befehle laut README:**
  - `dotnet restore`: Abhängigkeiten wiederherstellen
  - `dotnet build --configuration Release`: Build erstellen
  - `dotnet run --project StudyMate.Wpf/StudyMate.Wpf.csproj`: Anwendung ausführen
  - `dotnet ef migrations add <Name>`: EF-Migration erstellen (implizit)

#### DB Browser for SQLite
- **Download:** https://sqlitebrowser.org/dl/
- **Datenbankpfad:** `C:\Users\<username>\AppData\Local\StudyMate\studymate.db`
- **Zweck:** Inspektion und manuelle Bearbeitung der Datenbanktabellen

#### PowerShell-Skripte
- **Beispiel aus README:**
  ```powershell
  winget install sqlite.sqlite
  ```
- **Verwendung:** Installation von CLI-Tools, Automatisierung

---

## 5. Fremdcode und KI-generierter Code

### Methodik der Analyse
Diese Analyse basiert auf:
- Durchsicht aller .cs-Dateien auf Kommentar-Indikatoren
- Cross-Reference mit `ki-nutzungsprotokoll-vorlage.md`
- Bewertung der Code-Struktur und -Komplexität
- Identifikation von typischen KI-generierten Mustern

### Klassifizierung der Dateien

#### Kategorie 1: Wahrscheinlich vollständig von KI generiert (Lisa Pro)

**ViewModels:**
- `MainViewModel.cs` (23 Zeilen)
  - Indikator: Standard-MVVM-Boilerplate mit CommunityToolkit
  - KI-Nutzung: Protokoll vom 14.07.2026 erwähnt "Created FileListViewModel with MVVM pattern"
  
- `FileListViewModel.cs` (224 Zeilen)
  - Indikator: Komplette Implementierung mit `[ObservableProperty]`, `[RelayCommand]`
  - KI-Nutzung: Protokoll vom 14.07.2026: "Created FileListViewModel with MVVM pattern"
  - Umfang: Vollständige Upload/Delete/Open-Logic
  
- `FolderListViewModel.cs` (121 Zeilen)
  - Indikator: Ähnliches Muster wie FileListViewModel
  - KI-Nutzung: Protokoll vom 14.07.2026 erwähnt UI-Implementierung
  
- `FileDetailViewModel.cs` (411 Zeilen)
  - Indikator: Sehr komplexe Logik mit Quiz-Handling
  - KI-Nutzung: Protokoll vom 16.07.2026: "Created FileDetailViewModel with Summary and Quiz tabs"
  - Umfang: Vollständige Quiz-Logik mit CheckAnswer, Previous/Next Navigation

**Services:**
- `StudyFileService.cs` (78 Zeilen)
  - Indikator: Standard-Service-Pattern mit Repository-Integration
  - KI-Nutzung: Protokoll vom 14.07.2026: "Explained Repository vs Service layer architecture"
  
- `StudyFolderService.cs` (38 Zeilen)
  - Indikator: Einfaches Service-Pattern
  
- `AiAnalysisService.cs` (76 Zeilen)
  - Indikator: Komplexe AI-Integration mit Error-Handling
  - KI-Nutzung: Protokoll vom 21.07.2026 beschreibt komplette AI-Integration
  
- `FileStorageService.cs` (81 Zeilen)
  - Indikator: Dateisystem-Operationen mit Unique-FileName-Generierung
  - KI-Nutzung: Protokoll vom 14.07.2026: "Explained FileStorageService vs StudyFileService separation"

**Repositories:**
- `StudyFileRepository.cs` (48 Zeilen)
- `StudyFolderRepository.cs` (31 Zeilen)
- `AiAnalysisRepository.cs` (40 Zeilen)
  - Indikator: Standard-EF-Core-Repository-Implementierungen
  - KI-Nutzung: Protokoll vom 14.07.2026 erwähnt Repository-Architektur

**AI-Integration:**
- `AiClient.cs` (155 Zeilen)
  - Indikator: HTTP-Client-Implementierung mit JSON-Serialisierung
  - KI-Nutzung: Protokoll vom 21.07.2026: "Refactored AiClient to send JSON payload"
  - Besonders hervorgehoben: Mehrfache Iterationen durch API-Debugging
  
- `PdfTextExtractor.cs` (27 Zeilen)
  - Indikator: Wrapper um PdfPig-Bibliothek
  - KI-Nutzung: Protokoll vom 21.07.2026: "Recommended IPdfTextExtractor pattern with PdfPig library"
  
- `AiPromptBuilder.cs` (202 Zeilen)
  - Indikator: Extrem detaillierter System-Prompt für KI-Analyse
  - KI-Nutzung: Wahrscheinlich teilweise von KI optimiert/selbstreferenziell

**Models (AI-spezifisch):**
- `AiStudyMaterialResult.cs` (16 Zeilen)
- `StructuredContent.cs` (13 Zeilen)
- `QuizQuestion.cs` (11 Zeilen)
- `AiSettings.cs` (13 Zeilen)
  - Indikator: DTO-Klassen exakt passend zum AI-API-Response-Format

#### Kategorie 2: Von KI unterstützt, aber stark adaptiert

**Views:**
- `FileListView.xaml.cs` (59 Zeilen)
  - Indikator: Code-behind mit spezifischer Event-Handling-Logik
  - KI-Nutzung: Protokoll erwähnt UI-Implementierung, aber starke manuelle Anpassung
  
- `FolderListView.xaml.cs` (12 Zeilen)
- `FileDetailView.xaml.cs` (20 Zeilen)
  - Minimaler Code-behind, hauptsächlich XAML-basiert

**Dependency Injection:**
- `App.xaml.cs` (116 Zeilen)
  - Indikator: Komplexe DI-Konfiguration
  - KI-Nutzung: Protokoll vom 21.07.2026: "Fixed DI configuration: Changed ViewModels from AddScoped to AddSingleton"
  - Mehrfache Iterationen durch Bugfixes

#### Kategorie 3: Selbst entwickelter Code (geringe KI-Nutzung)

**Models (Domain):**
- `StudyFile.cs` (63 Zeilen)
  - Indikator: INotifyPropertyChanged manuell implementiert (nicht mit CommunityToolkit)
  - Eigene Logik: FileSizeDisplay-Property mit Formatierung
  
- `StudyFolder.cs` (15 Zeilen)
  - Einfache POCO-Klasse
  
- `AiAnalysis.cs` (27 Zeilen)
  - DTO-Klasse für Datenbank

**ViewModel Base & Quiz:**
- `ViewModelBase.cs` (14 Zeilen)
  - Standard-INotifyPropertyChanged-Implementierung
  
- `QuizQuestionViewModel.cs` (106 Zeilen)
  - Indikator: Spezifische Quiz-Logik
  
- `QuizOptionViewModel.cs` (65 Zeilen)
  - Besonderheit: `SetSelectedWithoutTrigger()`-Methode zeigt spezifische Anforderung

**Converters:**
- `Converters.cs` (84 Zeilen)
  - Indikator: WPF-spezifische Converter für UI-Logik
  - KI-Nutzung: Protokoll erwähnt "Added converters", aber XAML-fokussiert

**Interfaces:**
- Alle Interface-Dateien unter `Services/Interfaces/` und `Repositories/Interfaces/`
  - Standard-Interface-Definitionen ohne Implementierungsdetails

**Datenbank:**
- `AppDbContext.cs` (67 Zeilen)
  - Indikator: Fluent API Konfiguration
  - KI-Nutzung: Architekturelle Beratung, aber manuelle Implementierung
  
- `20260713142448_InitialCreate.cs` (109 Zeilen)
  - Automatisch generiert durch EF Core Tools (keine KI)

**Cross-Reference mit ki-nutzungsprotokoll-vorlage.md:**

| Datum | Nutzer | Werkzeug | Bereich | Umfang der KI-Nutzung |
|-------|--------|----------|---------|----------------------|
| 14.07.2026 | Truc Trinh | Lisa Pro | Backend File Service | Architekturberatung, Bug-Identification |
| 14.07.2026 | Truc Trinh | Lisa Pro | UI Implementation | ViewModel-Erstellung, XAML-Design, Converter |
| 16.07.2026 | Truc Trinh | Lisa Pro | Modern UI Redesign | Refactoring, Bugfixes (ObjectDisposedException), Übersetzung |
| 16.07.2026 | Truc Trinh | Lisa Pro | Quiz Feature | ViewModel-Erstellung, Command-Implementierung, Converter |
| 21.07.2026 | Truc Trinh | Lisa Pro | Complete AI Integration | PDF-Extraktion, HTTP-Client, DI-Configuration, Path-Handling |

### Zusammenfassung der KI-Nutzung

**Geschätzter Anteil:**
- ~60-70% der ViewModels und Services: KI-generiert mit Adaptationen
- ~30-40% der Models und Interfaces: Selbst entwickelt
- ~80-90% der AI-Integration (AiClient, PdfTextExtractor): KI-generiert nach mehreren Iterationen
- ~50% der XAML/Converter: KI-unterstützt
- 0% der EF-Core-Migrationen: Automatisch generiert durch Tools

**KI als Sparring Partner:**
- Architektur-Reviews (Repository vs Service Pattern)
- Dependency Injection Lifetime-Entscheidungen
- Code-Reviews und Bug-Identification
- Debugging von HTTP 405 Fehlern
- API-Endpoint-Analyse

**Vom Entwickler explizit abgelehnte Vorschläge:**
- Code-behind statt XAML für Visibility-Logik
- Mock-Daten-Beibehaltung bei AI-Ergebnissen
- Speicherung voller Dateipfade in der Datenbank

---

## 6. Lizenzinformationen

### NuGet-Paket-Lizenzen

| Paket | Lizenz | Bestätigung |
|-------|--------|-------------|
| CommunityToolkit.Mvvm 8.4.2 | MIT License | Offizielle Microsoft-Lizenz |
| DotNetEnv 3.1.1 | MIT License | Unklar - muss vom Entwickler bestätigt werden |
| Microsoft.EntityFrameworkCore 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| Microsoft.EntityFrameworkCore.Design 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| Microsoft.EntityFrameworkCore.Sqlite 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| Microsoft.EntityFrameworkCore.Tools 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| Microsoft.Extensions.DependencyInjection 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| Microsoft.Extensions.Http 10.0.8 | MIT License | Offizielle Microsoft-Lizenz |
| PdfPig 0.1.8 | Apache License 2.0 | Unklar - muss vom Entwickler bestätigt werden |

### Projektlizenz

**Hinweis:** Das Projekt enthält keine LICENSE-Datei im Repository.

**Empfehlung:** Für eine Studienarbeit sollte geklärt werden:
- Unter welcher Lizenz der eigene Code veröffentlicht wird
- Ob alle externen Abhängigkeiten kompatibel sind
- Insbesondere: Apache License 2.0 (PdfPig) hat andere Bedingungen als MIT-Lizenzen

### Externe API-Nutzung

**Lisa Chat API:**
- Nutzungsbedingungen: Unklar - muss vom Entwickler bestätigt werden
- API-Key: Erforderlich (wird über .env verwaltet)
- Zugriff: Über Hochschule/System (iisys.de Domain)

---

## 7. Zusammenfassung für Dokumentation

### Tabelle 1: Verwendete Bibliotheken

| Name | Version | Zweck | Lizenz |
|------|---------|-------|--------|
| CommunityToolkit.Mvvm | 8.4.2 | MVVM-Framework mit ObservableObject, RelayCommand | MIT |
| DotNetEnv | 3.1.1 | Laden von .env-Konfigurationsdateien | MIT |
| Microsoft.EntityFrameworkCore | 10.0.8 | ORM für Datenzugriff und Migrationen | MIT |
| Microsoft.EntityFrameworkCore.Design | 10.0.8 | Design-Time-Tools für EF-Migrationen | MIT |
| Microsoft.EntityFrameworkCore.Sqlite | 10.0.8 | SQLite Database Provider für EF Core | MIT |
| Microsoft.EntityFrameworkCore.Tools | 10.0.8 | CLI/PowerShell-Tools für EF Core | MIT |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | Dependency Injection Container | MIT |
| Microsoft.Extensions.Http | 10.0.8 | Typed HttpClient Factory | MIT |
| PdfPig | 0.1.8 | PDF-Textextraktion vor AI-Analyse | Apache 2.0 |

### Tabelle 2: Externe Dienste

| Dienst | Zweck | Konfiguration |
|--------|-------|---------------|
| Lisa Chat API | KI-gestützte PDF-Analyse (Summary, StructuredContent, Quiz) | .env: AiBaseUrl, AiEndpoint, ApiKey, AiModel |
| SQLite Database | Lokale persistente Datenspeicherung | Automatisch: %LOCALAPPDATA%\StudyMate\studymate.db |

### Tabelle 3: Entwicklungswerkzeuge

| Werkzeug | Zweck | Version |
|----------|-------|---------|
| Visual Studio 2022 | Integrierte Entwicklungsumgebung | 2022 (empfohlen) |
| .NET SDK 10.0 | Compiler, Runtime, CLI-Tools | 10.0 |
| Entity Framework Core Tools | Datenbank-Migrationen | 10.0.8 |
| Git | Versionskontrolle | Unklar |
| GitHub | Remote-Repository Hosting | Unklar |
| DB Browser for SQLite | Datenbank-Inspektion | Unklar |
| PowerShell | Skripting und Automatisierung | 5.1+ |
| NuGet Package Manager | Abhängigkeitsverwaltung | Integriert |

### Architekturübersicht

**Schichtenarchitektur:**
```
┌─────────────────────────────────────────┐
│              WPF UI Layer               │
│  (MainWindow, Views, Converters, XAML) │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│            ViewModel Layer              │
│     (MVVM mit CommunityToolkit)         │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│             Service Layer               │
│   (Business Logic, Orchestrierung)      │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│           Repository Layer              │
│      (Datenzugriff mit EF Core)         │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│          External Integrations          │
│    (AI Client, PDF Extractor, Files)    │
└─────────────────────────────────────────┘
```

**Besonderheiten:**
- Dependency Injection über Microsoft.Extensions.DependencyInjection
- Singleton-ViewModels für zustandsbehaftete Kommunikation zwischen Views
- Scoped-Repositories und Services pro Use-Case
- Code-First EF-Core-Ansatz mit automatischen Migrationen
- AI-Integration über typisierten HttpClient mit JSON-Payload

---

## Anmerkungen zur Vollständigkeit

**Unklare Angaben (müssen vom Entwickler bestätigt werden):**
1. Exakte Git-Version
2. Lizenz von DotNetEnv (vermutet: MIT)
3. Lizenz von PdfPig (vermutet: Apache 2.0)
4. Nutzungsbedingungen der Lisa Chat API
5. Visual Studio Exact Version (nur "2022 recommended" bekannt)

**Nicht im Report enthalten:**
- .NET Standard Library Klassen (werden nicht als extern betrachtet)
- Windows-spezifische APIs (Teil des .NET Frameworks)
- Auto-generierte EF-Core-Migrationen (Tools, keine Bibliotheken)

---
*Dieser Report wurde automatisch generiert basierend auf der Codebasis-Analyse.*
