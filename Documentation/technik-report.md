# Technik-Report: StudyMate

**Projekt:** StudyMate – AI-powered Study Assistant  
**Datum der Analyse:** 24. Juli 2026  
**Repository:** https://github.com/NET-2026/thi-thanh-truc-trinh.git  

---

## 1. Projektübersicht

StudyMate ist eine WPF-Desktopanwendung zur Unterstützung beim Lernen mit KI-gestützter Analyse von PDF-Lernmaterialien. Die Anwendung ermöglicht:

- Verwaltung von Lernordnern und PDF-Dateien
- Automatische Extraktion von Text aus PDF-Dokumenten
- KI-gestützte Generierung von Zusammenfassungen, Wissensstrukturen und Multiple-Choice-Fragen
- Anzeige und Bearbeitung der analysierten Inhalte in einer modernen Benutzeroberfläche

**Verwendete Programmiersprachen:** C#, XAML  
**Architekturmuster:** MVVM (Model-View-ViewModel), Repository Pattern, Service Layer, Dependency Injection

---

## 2. Technische Plattform

### Laufzeitumgebung

| Merkmal | Wert | Quelle |
|---------|------|--------|
| Target Framework | .NET 10.0 Windows | `StudyMate.Wpf.csproj`, Zeile 5 |
| Typ des Projekts | Windows-Desktopanwendung (WinExe) | `StudyMate.Wpf.csproj`, Zeile 4 |
| UI-Framework | WPF (Windows Presentation Foundation) | `StudyMate.Wpf.csproj`, Zeile 8 |
| Nullable Reference Types | Aktiviert | `StudyMate.Wpf.csproj`, Zeile 6 |
| Implicit Usings | Aktiviert | `StudyMate.Wpf.csproj`, Zeile 7 |

### Systemvoraussetzungen laut Dokumentation

- **Betriebssystem:** Windows 10/11
- **.NET SDK:** Version 10.0 oder später
- **IDE:** Visual Studio 2022 (empfohlen) oder VS Code mit C# Extension
- **SQLite:** Für Datenbankbetrachtung (nicht zwingend für Ausführung erforderlich)

**Hinweis:** Die README.md nennt "Visual Studio 2022" als Empfehlung. Eine spezifische Anforderung an Visual Studio 2026 kann aus dem Repository nicht verifiziert werden.

---

## 3. Architektur

### Schichtenarchitektur

Die Anwendung folgt einer mehrschichtigen Architektur mit klarer Trennung der Verantwortlichkeiten:

```
┌─────────────────────────────────────────┐
│           Präsentationsschicht          │
│    MainWindow, Views (XAML + Code)      │
│         FolderListView, FileListView,   │
│         FileDetailView, Converters      │
└─────────────────────────────────────────┘
                    ↕ (Data Binding)
┌─────────────────────────────────────────┐
│          ViewModel-Schicht              │
│   MainViewModel, FolderListViewModel,   │
│   FileListViewModel, FileDetailViewModel│
│   QuizQuestionViewModel,                │
│   QuizOptionViewModel                   │
└─────────────────────────────────────────┘
                    ↕ (Dependency Injection)
┌─────────────────────────────────────────┐
│            Service-Schicht              │
│   IStudyFolderService, StudyFolderService│
│   IStudyFileService, StudyFileService   │
│   IFileStorageService, FileStorageService│
│   IAiAnalysisService, AiAnalysisService │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│         Repository-Schicht              │
│   IStudyFolderRepository,               │
│   StudyFolderRepository                 │
│   IStudyFileRepository, StudyFileRepository│
│   IAiAnalysisRepository,                │
│   AiAnalysisRepository                  │
└─────────────────────────────────────────┘
                    ↕ (Entity Framework Core)
┌─────────────────────────────────────────┐
│          Datenzugriffsschicht           │
│        AppDbContext, Migrationen        │
│        SQLite-Datenbank                 │
└─────────────────────────────────────────┘
                    ↕
┌─────────────────────────────────────────┐
│         Externe Integrationen           │
│   IAiClient, AiClient (HTTP Client)     │
│   IPdfTextExtractor, PdfTextExtractor   │
│   Lisa Chat API                         │
└─────────────────────────────────────────┘
```

### Komponenten im Detail

#### Modelle (Domain Entities)

- **StudyFolder.cs** – Repräsentiert einen Lernordner mit Eigenschaften: Id, Name, CreatedAt, UpdatedAt
- **StudyFile.cs** – Repräsentiert eine PDF-Datei mit Metadaten: Id, FolderId, OriginalFileName, StoredFileName, FilePath, FileExtension, ContentType, FileSizeBytes, UploadedAt, CreatedAt, UpdatedAt
- **AiAnalysis.cs** – Speichert KI-Analyseergebnisse: Id, StudyFileId, Name, Summary, StructuredContentJson, QuizJson, Status, ErrorMessage, ModelName, CreatedAt, UpdatedAt

#### AI-spezifische Modelle

- **AiStudyMaterialResult.cs** – DTO für KI-Antwortstruktur (Name, Summary, StructuredContent, QuizQuestions)
- **StructuredContent.cs** – Strukturierte Lerninhalte mit Topics und Subtopics
- **QuizQuestion.cs** – Multiple-Choice-Fragen mit Optionen und Erklärungen
- **AiSettings.cs** – Konfiguration für KI-API (BaseUrl, AnalysisEndpoint, ApiKey, ModelName)

#### ViewModels (MVVM mit CommunityToolkit.Mvvm)

- **ViewModelBase.cs** – Basis-Klasse mit INotifyPropertyChanged
- **MainViewModel.cs** – Root-ViewModel, koordiniert untergeordnete ViewModels
- **FolderListViewModel.cs** – Ordnerverwaltung mit CRUD-Operationen
- **FileListViewModel.cs** – Dateiverwaltung mit Upload, Delete, Update
- **FileDetailViewModel.cs** – Detailansicht mit Tabs für Summary und Quiz
- **QuizQuestionViewModel.cs**, **QuizOptionViewModel.cs** – Quiz-spezifische Logik

#### Services (Geschäftslogik)

- **StudyFolderService.cs** – Orchestriert Ordneroperationen
- **StudyFileService.cs** – Orchestriert Dateioperationen
- **FileStorageService.cs** – Verwaltet physische Dateispeicherung
- **AiAnalysisService.cs** – Koordiniert KI-Analyseprozess

#### Repositories (Datenzugriff)

- **StudyFolderRepository.cs** – Datenzugriff für StudyFolder-Entitäten
- **StudyFileRepository.cs** – Datenzugriff für StudyFile-Entitäten
- **AiAnalysisRepository.cs** – Datenzugriff für AiAnalysis-Entitäten

#### Views (XAML-basierte Oberflächen)

- **MainWindow.xaml** – Hauptfenster mit dreispaltigem Layout
- **FolderListView.xaml** – Benutzeroberfläche für Ordnerliste
- **FileListView.xaml** – Benutzeroberfläche für Dateiliste
- **FileDetailView.xaml** – Detailansicht mit Summary und Quiz-Tabs

#### Converters (WPF Value Converter)

- **Converters.cs** – Implementiert:
  - NullToVisibilityConverter
  - CountToVisibilityConverter
  - BoolToVisibilityConverter
  - ResultTextConverter
  - InvertedBoolConverter

---

## 4. Externe Bibliotheken und NuGet-Pakete

Alle externen Bibliotheken werden über die `.csproj`-Datei verwaltet. Nachfolgend die vollständige Liste aller referenzierten Pakete mit versionierter Angabe:

### Übersichtstabelle

| Paketname | Version | Projekt | Zweck | Lizenz |
|-----------|---------|---------|-------|--------|
| CommunityToolkit.Mvvm | 8.4.2 | StudyMate.Wpf | MVVM-Framework | Lizenz muss separat überprüft werden |
| DotNetEnv | 3.1.1 | StudyMate.Wpf | Laden von .env-Dateien | Lizenz muss separat überprüft werden |
| Microsoft.EntityFrameworkCore | 10.0.8 | StudyMate.Wpf | ORM-Framework | Lizenz muss separat überprüft werden |
| Microsoft.EntityFrameworkCore.Design | 10.0.8 | StudyMate.Wpf | Design-Time-Tools für EF | Lizenz muss separat überprüft werden |
| Microsoft.EntityFrameworkCore.Sqlite | 10.0.8 | StudyMate.Wpf | SQLite Database Provider | Lizenz muss separat überprüft werden |
| Microsoft.EntityFrameworkCore.Tools | 10.0.8 | StudyMate.Wpf | CLI/PowerShell-Tools für EF | Lizenz muss separat überprüft werden |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | StudyMate.Wpf | Dependency Injection Container | Lizenz muss separat überprüft werden |
| Microsoft.Extensions.Http | 10.0.8 | StudyMate.Wpf | Typed HttpClient Factory | Lizenz muss separat überprüft werden |
| PdfPig | 0.1.8 | StudyMate.Wpf | PDF-Textextraktion | Lizenz muss separat überprüft werden |

**Quellen:** `StudyMate.Wpf/StudyMate.Wpf.csproj`, Zeilen 17–31

### Detaillierte Beschreibung der Pakete

#### 4.1 CommunityToolkit.Mvvm (Version 8.4.2)

- **Zweck:** MVVM-Framework für .NET-Anwendungen, bereitgestellt vom Microsoft Community Toolkit
- **Verwendung im Projekt:**
  - Basisklasse `ObservableObject` für alle ViewModels
  - Attribut `[ObservableProperty]` zur automatischen Generierung von Property-Benachrichtigungen
  - Attribut `[RelayCommand]` zur Generierung von ICommand-Implementierungen
  - Nutzung von Partial Methods für `OnPropertyChanged`-Handler
- **Repräsentative Verwendungsorte:**
  - `MainViewModel.cs` (Zeile 5): `public partial class MainViewModel : ObservableObject`
  - `FileListViewModel.cs` (Zeile 18–22): `[ObservableProperty] private ObservableCollection<StudyFile> files = new();`
  - `FolderListViewModel.cs` (Zeile 89): `[RelayCommand] private async Task CreateFolderAsync()`
  - `QuizQuestionViewModel.cs` (Namespace `CommunityToolkit.Mvvm.Input`)

#### 4.2 DotNetEnv (Version 3.1.1)

- **Zweck:** Bibliothek zum Laden von Umgebungsvariablen aus `.env`-Dateien in .NET-Anwendungen
- **Verwendung im Projekt:**
  - Laden der KI-API-Konfiguration beim Anwendungsstart
  - Bereitstellung von Umgebungsvariablen für `AiBaseUrl`, `AiEndpoint`, `ApiKey`, `AiModel`
- **Repräsentative Verwendungsorte:**
  - `App.xaml.cs` (Zeile 101): `DotNetEnv.Env.Load();`
  - `App.xaml.cs` (Zeile 103–108): Auslesen der Environment-Variablen für `AiSettings`

#### 4.3 Microsoft.EntityFrameworkCore (Version 10.0.8)

- **Zweck:** Object-Relational Mapping (ORM) Framework für .NET
- **Verwendung im Projekt:**
  - Code-First-Ansatz für Datenbankmodellierung
  - LINQ-basierte Abfragen für Datenzugriffe
  - Definition von Entitätsklassen und Beziehungen
  - Unit-of-Work Pattern via DbContext
- **Repräsentative Verwendungsorte:**
  - `AppDbContext.cs` (Zeile 1–67): Zentrale DbContext-Klasse mit `DbSet<T>`-Properties und Fluent API in `OnModelCreating()`
  - `StudyFileRepository.cs` (Zeile 19–23): `await _dbContext.StudyFiles.Where(f => f.FolderId == folderId).ToListAsync()`
  - `StudyFolderRepository.cs` (Zeile 19–21): `await _dbContext.StudyFolders.OrderByDescending(x => x.CreatedAt).ToListAsync()`
  - `AiAnalysisRepository.cs` (Zeile 19–23): Komplexe Abfrage mit Where und OrderByDescending

#### 4.4 Microsoft.EntityFrameworkCore.Design (Version 10.0.8)

- **Zweck:** Design-Time-Unterstützung für Entity Framework Core Tools
- **Verwendung im Projekt:**
  - Ermöglicht EF Core CLI- und PowerShell-Tools für Migrationen
  - Wird ausschließlich zur Entwicklungszeit verwendet (`<PrivateAssets>all</PrivateAssets>`)
- **Repräsentative Dateien:**
  - Migrationsverzeichnis enthält automatisch generierte Dateien durch diese Tools

#### 4.5 Microsoft.EntityFrameworkCore.Sqlite (Version 10.0.8)

- **Zweck:** SQLite Database Provider für Entity Framework Core
- **Verwendung im Projekt:**
  - Lokale, dateibasierte SQLite-Datenbank ohne Server-Infrastruktur
  - Persistente Speicherung von Ordnern, Dateien und Analyseergebnissen
- **Konfiguration:**
  - `App.xaml.cs` (Zeile 72–74): `options.UseSqlite($"Data Source={dbPath}")`
  - Speicherort: `C:\Users\<Benutzername>\AppData\Local\StudyMate\studymate.db`

#### 4.6 Microsoft.EntityFrameworkCore.Tools (Version 10.0.8)

- **Zweck:** PowerShell- und .NET CLI-Tools für Entity Framework Core
- **Verwendung im Projekt:**
  - Erstellen von Migrationen: `dotnet ef migrations add <Name>`
  - Anwenden von Migrationen: `dotnet ef database update`
  - Verwaltung des Datenbank-Schemas
- **Hinweis:** Wird nur zur Entwicklungszeit verwendet (`<PrivateAssets>all</PrivateAssets>`)

#### 4.7 Microsoft.Extensions.DependencyInjection (Version 10.0.8)

- **Zweck:** Dependency Injection (IoC) Container für .NET-Anwendungen
- **Verwendung im Projekt:**
  - Registrierung aller Services, Repositories, ViewModels und Views
  - Verwaltung von Lebenszyklen (Singleton, Scoped, Transient)
  - Constructor Injection Pattern in allen Komponenten
- **Repräsentative Verwendungsorte:**
  - `App.xaml.cs` (Zeile 68–115): `ConfigureServices(IServiceCollection services)`-Methode
  - Repository-Registrierung (Zeile 77–79): `services.AddScoped<IStudyFolderRepository, StudyFolderRepository>()`
  - Service-Registrierung (Zeile 82–84): `services.AddScoped<IStudyFileService, StudyFileService>()`
  - ViewModel-Registrierung (Zeile 87–90): `services.AddSingleton<MainViewModel>()`

#### 4.8 Microsoft.Extensions.Http (Version 10.0.8)

- **Zweck:** HttpClient-Factory für typisierte HTTP-Clients
- **Verwendung im Projekt:**
  - Typisierter HttpClient für Kommunikation mit der Lisa Chat API
  - Automatische Lebenszeitverwaltung des HttpClient
  - Integration mit Dependency Injection Container
- **Repräsentative Verwendungsorte:**
  - `App.xaml.cs` (Zeile 114): `services.AddHttpClient<IAiClient, AiClient>()`
  - `AiClient.cs` (Zeile 11–154): Implementierung des injizierten `HttpClient`

#### 4.9 PdfPig (Version 0.1.8)

- **Zweck:** Open-Source-Bibliothek zur Extraktion von Text und Metadaten aus PDF-Dokumenten
- **Namespace im Projekt:** `UglyToad.PdfPig`
- **Verwendung im Projekt:**
  - Textextraktion aus hochgeladenen PDF-Lernmaterialien vor der KI-Analyse
  - Implementierung des `IPdfTextExtractor`-Interface
- **Repräsentative Verwendungsorte:**
  - `PdfTextExtractor.cs` (Zeile 3): `using UglyToad.PdfPig;`
  - `PdfTextExtractor.cs` (Zeile 17–20): `using (var document = PdfDocument.Open(filePath)) { foreach (var page in document.GetPages()) { textBuilder.AppendLine(page.Text); } }`
  - `AiClient.cs` (Zeile 44–46): Aufruf von `_pdfTextExtractor.ExtractTextAsync(fullPath, cancellationToken)`

---

## 5. Externe Dienste und APIs

### Lisa Chat API

#### Zweck

Die Lisa Chat API wird verwendet für:

- KI-gestützte Analyse von extrahiertem PDF-Text
- Automatische Generierung von:
  - Zusammenfassungen (Summary)
  - Strukturierten Lerninhalten (StructuredContent mit Topics und Subtopics)
  - Multiple-Choice-Fragen (QuizQuestions mit Optionen und Erklärungen)

#### Konfiguration

**Konfigurationsdateien:**

- Template: `.env.example` (im Repository vorhanden, versioniert)
- Runtime-Konfiguration: `.env` (nicht versioniert, lokal erstellt)

**Erforderliche Umgebungsvariablen (aus `.env.example`):**

| Variable | Beschreibung | Beispielwert |
|----------|--------------|--------------|
| `AiBaseUrl` | Basis-URL der API | `https://chat-1.ki-awz.iisys.de/` |
| `AiEndpoint` | Spezifischer Endpoint für Chat Completions | `api/chat/completions` |
| `ApiKey` | Authentifizierungsschlüssel (Bearer Token) | *(wird nicht angezeigt)* |
| `AiModel` | Modellname für die Analyse | `lisa-pro-03-2026` |

**Quellen:** `.env.example`, `App.xaml.cs` (Zeile 103–108), `Models/Ai/AiSettings.cs`

#### Endpoint-Struktur

- **Vollständige URL:** `https://chat-1.ki-awz.iisys.de/api/chat/completions`
- **HTTP-Methode:** POST
- **Content-Type:** `application/json`
- **Authorization Header:** `Bearer <ApiKey>`
- **Timeout:** 5 Minuten (`TimeSpan.FromMinutes(5)`)

**Quelle:** `Integrations/Ai/AiClient.cs` (Zeile 32–36, 69–73)

#### Request-Format

Die Anwendung sendet ein JSON-Objekt im OpenAI-kompatiblen Chat Completion Schema:

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

**Quellen:** `AiClient.cs` (Zeile 49–68), `AipromptBuilder.cs`

#### Response-Format

Die API antwortet mit einem JSON-Objekt folgender Struktur:

```json
{
  "choices": [
    {
      "message": {
        "content": "{\"name\": \"...\", \"summary\": \"...\", \"structuredContent\": {...}, \"quizQuestions\": [...]}"
      }
    }
  ]
}
```

Der Inhalt der Antwort wird geparst und in ein `AiStudyMaterialResult`-Objekt deserialisiert.

**Quelle:** `AiClient.cs` (Zeile 84–109)

#### Validierung und Fehlerbehandlung

- Überprüfung der API-Konfiguration bei Initialisierung (`ValidateSettings()`)
- Entfernen von Markdown Code-Fences (```` ```json ````) aus der Antwort
- Prüfung auf leere Responses
- Werfen von `HttpRequestException` bei HTTP-Fehlern mit Statuscode und Response-Inhalt
- Protokollierung von HTTP-Status, Response-Länge und Parse-Ergebnissen

**Quellen:** `AiClient.cs` (Zeile 111–134, 136–155), `KI-Nutzungsprotokoll` vom 21.07.2026

#### Dateien im Zusammenhang mit der API-Integration

- `Integrations/Ai/AiClient.cs` – HTTP-Client-Implementierung
- `Integrations/Ai/AipromptBuilder.cs` – Prompt-Templates für System- und User-Messages
- `Integrations/Ai/PdfTextExtractor.cs` – PDF-Textextraktion vor API-Aufruf
- `Integrations/Ai/Interfaces/IAiClient.cs` – Interface-Definition
- `Integrations/Ai/Interfaces/IPdfTextExtractor.cs` – Interface-Definition
- `Models/Ai/AiSettings.cs` – Konfigurationsmodell
- `Services/AiAnalysisService.cs` – Service-Schicht für KI-Analyse

---

## 6. Datenverarbeitung und Persistenz

### Datenbank

**Datenbanksystem:** SQLite 3.x  
**Provider:** Microsoft.EntityFrameworkCore.Sqlite 10.0.8  
**Speicherort:** `C:\Users\<Benutzername>\AppData\Local\StudyMate\studymate.db`

**Quelle:** `App.xaml.cs` (Zeile 72–74), README.md (Abschnitt "Database Location")

### Datenbanktabellen

Die Datenbank umfasst drei Tabellen, definiert in `AppDbContext.cs`:

#### Tabelle: StudyFolders

| Spalte | Typ | Constraints |
|--------|-----|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Name | TEXT | NOT NULL |
| CreatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |
| UpdatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |

**Quelle:** `AppDbContext.cs` (Zeile 17–24)

#### Tabelle: StudyFiles

| Spalte | Typ | Constraints |
|--------|-----|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| FolderId | INTEGER | FOREIGN KEY → StudyFolders(Id), ON DELETE CASCADE |
| OriginalFileName | TEXT | NOT NULL |
| StoredFileName | TEXT | NOT NULL (eindeutiger Dateiname) |
| FilePath | TEXT | NOT NULL (nur Dateiname, kein voller Pfad) |
| FileExtension | TEXT | NOT NULL |
| ContentType | TEXT | NOT NULL |
| FileSizeBytes | INTEGER | NOT NULL |
| UploadedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |
| CreatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |
| UpdatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |

**Quelle:** `AppDbContext.cs` (Zeile 26–44)

#### Tabelle: AiAnalyses

| Spalte | Typ | Constraints |
|--------|-----|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| StudyFileId | INTEGER | FOREIGN KEY → StudyFiles(Id), ON DELETE CASCADE |
| Name | TEXT | NOT NULL |
| Summary | TEXT | OPTIONAL |
| StructuredContentJson | TEXT | OPTIONAL (JSON-Serialisierung) |
| QuizJson | TEXT | OPTIONAL (JSON-Serialisierung) |
| Status | TEXT | NOT NULL (z.B. "Pending", "Completed", "Failed") |
| ErrorMessage | TEXT | OPTIONAL |
| ModelName | TEXT | NOT NULL |
| CreatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |
| UpdatedAt | DATETIME | DEFAULT CURRENT_TIMESTAMP |

**Quelle:** `AppDbContext.cs` (Zeile 46–64)

### Beziehungen

- **StudyFolder → StudyFiles:** 1:n (ein Ordner enthält viele Dateien, Cascade Delete)
- **StudyFile → AiAnalyses:** 1:n (eine Datei kann viele Analysen haben, Cascade Delete)

**Quelle:** `AppDbContext.cs` (Zeile 17–44)

### Migrationsstrategie

**Ansatz:** Code First mit automatischer Migrationserstellung

**Durchgeführte Migrationen:**

- `20260713142448_InitialCreate.cs` – Erstellt alle drei Tabellen
- `20260713142448_InitialCreate.Designer.cs` – Designer-Informationen
- `AppDbContextModelSnapshot.cs` – Momentaufnahme des aktuellen Modells

**Hinweis:** Diese Dateien wurden automatisch durch Entity Framework Core Tools generiert.

**Quelle:** Verzeichnis `StudyMate.Wpf/Migrations/`, `README.md` (Abschnitt "Migrations")

### Automatische Datenbankerstellung

Beim ersten Start der Anwendung wird die Datenbank automatisch erstellt:

```csharp
dbContext.Database.EnsureCreated();
```

**Quelle:** `App.xaml.cs` (Zeile 39)

### Dateispeicherung

Hochgeladene PDF-Dateien werden gespeichert unter:

```
C:\Users\<Benutzername>\AppData\Local\StudyMate\uploads\<StoredFileName>.pdf
```

**Wichtig:** Die Datenbank speichert nur den Dateinamen (`FilePath`), nicht den vollständigen Pfad. Der vollständige Pfad wird zur Laufzeit dynamisch konstruiert using `Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)`.

**Quellen:** `FileStorageService.cs`, `Ki-Nutzungsprotokoll` vom 21.07.2026 ("Fixed file storage path mismatch")

---

## 7. Entwicklungswerkzeuge

### Aus dem Repository verifizierte Werkzeuge

| Werkzeug | Zweck | Version / Angabe | Quelle |
|----------|-------|------------------|--------|
| .NET SDK | Compiler, Runtime, CLI-Tools | .NET 10.0 | `StudyMate.Wpf.csproj`, Zeile 5 |
| NuGet Package Manager | Abhängigkeitsverwaltung | Integriert in .NET SDK / Visual Studio | `StudyMate.Wpf.csproj` |
| Entity Framework Core Tools | Datenbank-Migrationen | 10.0.8 | `StudyMate.Wpf.csproj`, Zeile 25, 28 |
| Git | Versionskontrolle | Nicht spezifiziert | Vorhandenes `.git`-Verzeichnis, `.gitignore` |
| GitHub | Remote-Repository Hosting | Nicht spezifiziert | README.md, Zeile 17 |
| PowerShell | Skripting und Automatisierung | Nicht spezifiziert (Windows Standard) | README.md, Zeile 89 |

### Laut README.md empfohlene Werkzeuge

| Werkzeug | Zweck | Version | Quelle |
|----------|-------|---------|--------|
| Visual Studio 2022 | Integrierte Entwicklungsumgebung | 2022 (empfohlen) | README.md, Zeile 9 |
| VS Code | Alternative IDE | Mit C# Extension | README.md, Zeile 9 |
| DB Browser for SQLite | Datenbank-Inspektion | Nicht spezifiziert | README.md, Zeile 84 |
| sqlite3 CLI | Kommandozeilen-Datenbanktool | Nicht spezifiziert | README.md, Zeile 89 |
| winget | Windows Package Manager | Integriert in Windows 10/11 | README.md, Zeile 90 |

### Build- und Ausführungsprozesse

**Laut README.md dokumentierte Befehle:**

```bash
# Abhängigkeiten wiederherstellen
dotnet restore

# Build erstellen
dotnet build --configuration Release

# Anwendung ausführen
dotnet run --project StudyMate.Wpf/StudyMate.Wpf.csproj
```

**Quelle:** README.md, Zeilen 23–57

### Git-History als Nachverfolgungsinstrument

Das KI-Nutzungsprotokoll verweist explizit auf die Git-History zur Nachverfolgung der Entwicklung:

> "This protocol is maintained as a Markdown file in the group's Git repository. [...] Traceability across versions is provided by the Git history of this file."

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 14–16

---

## 8. Fremdcode

### Definition von Fremdcode

In diesem Bericht bezeichnet **Fremdcode** Quellcode, der aus externen Quellen (z.B. Open-Source-Repositories, Code-Snippets aus dem Internet, Tutorials, Stack Overflow) unverändert oder mit geringfügigen Anpassungen in das Projekt übernommen wurde.

### Analyseergebnis

Eine sorgfältige Prüfung des Repositories ergab Folgendes:

- **Keine LICENSE-Datei** im Repository vorhanden → Keine Hinweise auf kopierten Code mit Lizenzverpflichtungen
- **Keine Copyright-Vermerke** in den Quelldateien festgestellt
- **Keine URLs oder Quellenangaben** zu externem Code in Kommentaren gefunden
- **Alle NuGet-Pakete** werden über offizielle PackageReference-Einträge eingebunden, es wurde kein Code aus diesen Paketen in das Projekt kopiert

**Feststellung:**

> Es wurde kein unverändert übernommener Fremdcode aus externen Quellcode-Repositories oder Internetquellen festgestellt.

Alle im Projekt enthaltenen Quelldateien sind entweder:

- Eigene Entwicklungen des Entwicklungsteams
- Durch KI-Werkzeuge unterstützte Entwürfe (siehe Abschnitt 10)
- Automatisch generierter Code von Entity Framework Core Tools (siehe Abschnitt 9)

### Verwendung externer Bibliotheken

Die Verwendung externer Bibliotheken erfolgt ausschließlich über NuGet-PackageReferenzen. Der Quellcode dieser Bibliotheken bleibt außerhalb des Projekts und wird zur Laufzeit über das .NET-Paketmanagement eingebunden. Dies stellt keine Übernahme von Fremdcode dar, sondern die bestimmungsgemäße Nutzung von Bibliotheken gemäß deren Lizenzbedingungen.

---

## 9. Automatisch generierter Code

### Definition

Automatisch generierter Code bezeichnet Quellcode, der durch Software-Werkzeuge (z.B. ORM-Tools, Code-Generatoren, Designer) ohne manuelle Programmierung erstellt wurde.

### Identifizierte automatisch generierte Dateien

#### Entity Framework Core Migrationen

| Datei | Beschreibung | Generator |
|-------|--------------|-----------|
| `20260713142448_InitialCreate.cs` | Erstellt alle Datenbanktabellen basierend auf dem DbContext-Modell | Entity Framework Core Tools 10.0.8 |
| `20260713142448_InitialCreate.Designer.cs` | Designer-Informationen für die Migration | Entity Framework Core Tools 10.0.8 |
| `AppDbContextModelSnapshot.cs` | Momentaufnahme des aktuellen Datenbankmodells | Entity Framework Core Tools 10.0.8 |

**Quellen:** Verzeichnis `StudyMate.Wpf/Migrations/`, `StudyMate.Wpf.csproj` (Zeilen 20–23, 25–28)

#### Zweck der generierten Dateien

Diese Dateien dienen der versionskontrollierten Datenbank-Schema-Verwaltung im Rahmen des Code-First-Ansatzes von Entity Framework Core. Sie werden automatisch erstellt und sollten nicht manuell bearbeitet werden.

#### Behandlung im Codeumfang

Diese Dateien sind automatisch generiert und werden im Abschnitt 11 gesondert betrachtet.

### Weitere möglicherweise generierte Artefakte

- **XAML-Designer-Dateien** (`*.g.i.cs`, `*.g.cs`) – Werden von WPF zur Kompilierzeit generiert, sind nicht im Repository enthalten
- **AssemblyInfo.cs** – Kann teilweise automatisch generiert sein, enthält aber auch projektspezifische Metadaten

---

## 10. Einsatz von KI-Werkzeugen

### Dokumentierte KI-Nutzung

Die Verwendung von KI-Werkzeugen ist im Dokument `Documentation/ki-nutzungsprotokoll-vorlage.md` ausführlich protokolliert. Nachfolgend eine Zusammenfassung der dokumentierten Aktivitäten, basierend ausschließlich auf diesem Protokoll.

### Verwendetes KI-Werkzeug

- **Tool:** Lisa Pro (LLM Chat Assistant)
- **Zeitraum der Nutzung:** 14.07.2026 – 21.07.2026
- **Nutzer:** Truc Trinh

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 5–8, 29–34

### Tabellarische Übersicht der KI-Unterstützung

| Datum | Bereich | Art der KI-Unterstützung | Prüfung und eigene Anpassung |
|-------|---------|--------------------------|------------------------------|
| 14.07.2026 | Backend File Service | - Erklärung der Repository vs. Service Layer Architektur<br>- Erklärung der Trennung FileStorageService vs. StudyFileService<br>- Identifikation von Bugs in UpdateFileAsync und FileStorageService<br>- Code-Review | - Eigene Implementierung aller Repositories und Services<br>- Behebung der identifizierten Bugs<br>- Test aller Code-Implementierungen vor Commit |
| 14.07.2026 | UI Implementation File Management | - Erstellung FileListViewModel mit MVVM Pattern<br>- Implementierung FileListView XAML<br>- Hinzufügen von ViewConvertern<br>- DI-Container Konfiguration<br>- Behebung von XAML Binding Fehlern | - Entwurf des Zwei-Panel-Layouts (Ordner + Dateien)<br>- Implementierung von Upload/Delete/Update-Funktionen<br>- Integration von Dateidialogen<br>- Test aller CRUD-Operationen |
| 16.07.2026 | Modern UI Redesign | - Redesign FolderListView und FileListView XAML mit modernem Purple Theme (#6C4CF1)<br>- Refactoring MainViewModel zur Koordination<br>- Implementierung der Ordnerauswahl-Kommunikation<br>- Behebung von ObjectDisposedException (DbContext Lifetime)<br>- Behebung von UI State Management Problemen<br>- Übersetzung vietnamesischer Kommentare ins Englische | - Entwurf der card-basierten UI für Dateien mit PDF Icons<br>- Implementierung der Drei-Zustands-Logik (kein Ordner / leerer Ordner / hat Dateien)<br>- Integration von Dateidialogen<br>- End-to-End-Test aller UI-Zustände |
| 16.07.2026 | Quiz Feature | - Erstellung FileDetailViewModel mit Summary und Quiz Tabs<br>- Implementierung QuizQuestionViewModel und QuizOptionViewModel<br>- CheckAnswerCommand mit Validierungslogik<br>- Previous/Next Navigation<br>- Radio Button Command Binding | - Entwurf des Drei-Spalten-Layouts (360px Ordner \| 440px Dateien \| * Details)<br>- Implementierung von TabControl<br>- Erstellung von Mock-Quizdaten (2 ML Fragen)<br>- Farbliches Feedback (grün Korrekt / rot Falsch)<br>- Test des gesamten Quiz-Interaktionsflusses |
| 21.07.2026 | Complete AI Integration | - Identifikation der HTTP 405 Error Ursache (falsches Format)<br>- Empfehlung IPdfTextExtractor Pattern mit PdfPig<br>- Refactoring AiClient für JSON Payload<br>- Implementierung ParseChatResponse()<br>- DI-Konfiguration (Scoped zu Singleton)<br>- Pfad-Problematik gelöst<br>- .env Konfiguration korrigiert<br>- Logging hinzugefügt | - Installation PdfPig NuGet Package<br>- Implementierung PdfTextExtractor<br>- Debugging der HTTP 405 Errors mit PowerShell<br>- Test der PDF-Textextraktion<br>- End-to-End-Test: Upload PDF → Text extrahieren → Lisa API → Datenbank → UI |

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 29–34

### Umgang mit fehlerhaften KI-Antworten

Das Protokoll dokumentiert explizit Fälle, in denen KI-Empfehlungen fehlerhaft waren und korrigiert werden mussten:

| Problem | Ursprüngliche KI-Empfehlung | Identifiziertes Problem | Lösung |
|---------|----------------------------|------------------------|--------|
| Files.Count Binding | `Mode=OneTime` | Verhinderte UI-Updates | Geändert zu `Mode=OneWay` |
| API Request Format | multipart/form-data | Lisa API unterstützt nur JSON | Umstellung auf Textextraktion + JSON Payload |
| Direkter Dateipfad an API | Senden des Dateipfads | HTTP 405 Method Not Allowed | PDF-Text clientseitig extrahieren, dann Text senden |

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 39–42

### Bewusst abgelehnte KI-Vorschläge

Das Entwicklungsteam hat folgende KI-Empfehlungen ausdrücklich abgelehnt:

1. **Code-behind statt XAML für Visibility-Logik**  
   Stattdessen: Reine XAML MultiDataTriggers für bessere Wartbarkeit

2. **Mock-Daten Auto-Trigger bei Dateiauswahl**  
   Stattdessen: Entfernung von `LoadMockData`-Aufrufen, um nur echte KI-Analysen anzuzeigen

3. **Speicherung voller Dateipfade in der Datenbank**  
   Stattdessen: Dynamische Pfadkonstruktion using `Environment.GetFolderPath()`

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 44–47

### KI als Sparring Partner

Neben der konkreten Code-Generierung wurde KI für folgende Aktivitäten eingesetzt:

- Architektur-Reviews (Repository vs. Service Pattern)
- Validierung von Dependency Injection Lifetime-Entscheidungen (Scoped vs. Singleton)
- Code-Review und Bug-Identifikation
- Debugging von HTTP 405 Fehlern durch API-Endpoint-Analyse
- Architektur-Design: Separation von IPdfTextExtractor Interface für Testbarkeit

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 49–54

### Kritische Bugs gefunden durch KI-Zusammenarbeit

1. **Dateipfad-Mismatch:** FileStorageService speichert in `AppData/Local/StudyMate/uploads/`, AiClient suchte in `AppData/Local/Temp/uploads/` – Gelöst durch dynamische Pfadkonstruktion

2. **Database Cache Issue:** SelectedFile Setter rief GenerateAnalysisAsync jedes Mal auf, erstellte doppelte API Calls – Umgestellt auf LoadAnalysisAsync mit Datenbank-Check zuerst

3. **ViewModel Instance Mismatch:** AddScoped verursachte verschiedene Instanzen in verschiedenen Views – Geändert zu AddSingleton für geteilten Zustand

4. **API Endpoint Format:** .env hatte BaseUrl mit /apiSuffix UND Endpoint mit / Präfix导致 malformed URLs – Standardisiert auf BaseUrl ohne Suffix, Endpoint mit vollständigem Pfad

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 56–60

### Zusammenfassung und Bestätigung

Das Protokoll bestätigt abschließend:

> "All adopted content has been reviewed for technical accuracy, adapted as needed, and responsibly integrated into the work."

> "We confirm that the use of AI in this work has been fully and accurately documented to the best of our knowledge. We take responsibility for the technical accuracy, the selection of adopted content, and the entire submitted work."

**Quelle:** `ki-nutzungsprotokoll-vorlage.md`, Zeilen 20, 64–65

### Hinweis zur Zuordnung von Code zu KI-Nutzung

**Eine zuverlässige Zuordnung einzelner Quelldateien zu menschlicher oder KI-generierter Urheberschaft ist allein anhand des Quellcodes nicht möglich.**

Die obige Tabelle basiert ausschließlich auf den im KI-Nutzungsprotokoll dokumentierten Aktivitäten. Welche konkreten Codezeilen von welcher Tätigkeit betroffen sind, lässt sich ohne zusätzliche Dokumentation nicht feststellen.

Alle im Protokoll genannten Bereiche wurden laut Dokumentation vom Entwicklungsteam geprüft, angepasst und getestet.

---

## 11. Codeumfang laut cloc

### Gesamtergebnis

| Kategorie | Anzahl Zeilen |
|-----------|---------------|
| **Total C# und XAML** | **3454** |
| Davon C# Code | 2207 |
| Davon XAML Code | 1247 |
| Kommentarzeilen | 70 |
| Leerzeilen | 975 |

### Aufschlüsselung nach Dateitypen

**Hinweis:** Diese Zahlen beinhalten sowohl manuell geschriebenen Code als auch automatisch generierten Code (z.B. EF Core Migrationen). Eine exakte Trennung der Zeilenzahlen zwischen generierten und nicht-generierten Dateien liegt nicht vor.

### Automatisch generierte Dateien (gesondert betrachtet)

Folgende Dateien sind als automatisch generiert identifiziert und sollten bei der Betrachtung des manuell erstellten Codes separat berücksichtigt werden:

- `Migrations/20260713142448_InitialCreate.cs`
- `Migrations/20260713142448_InitialCreate.Designer.cs`
- `Migrations/AppDbContextModelSnapshot.cs`

**Empfehlung:** Für eine präzise Analyse des manuell geschriebenen Codes sollten diese Dateien separat gezählt und von der Gesamtsumme subtrahiert werden.

---

## 12. Offene oder nicht verifizierbare Angaben

Folgende Angaben konnten aus dem Repository nicht eindeutig verifiziert werden und müssen separat bestätigt werden:

### Nicht verifizierte Angaben

| Angabe | aktueller Stand | Erforderliche Bestätigung |
|--------|-----------------|---------------------------|
| **Lizenzen der NuGet-Pakete** | Nur Paketnamen und Versionen aus `.csproj` bekannt | Alle Lizenzen müssen separat überprüft werden (NuGet.org oder Paket-Metadaten) |
| **Visual Studio Version** | README.md nennt "Visual Studio 2022 (recommended)" | Tatsächlich verwendete Version im Entwicklungsprozess |
| **Git Version** | Git-Repository vorhanden | Installierte Git-Version |
| **GitHub Account / Repository Details** | Remote-URL: https://github.com/NET-2026/thi-thanh-truc-trinh.git | Organisation, Zugriffsberechtigungen, Branch-Strategie |
| **Nutzungsbedingungen Lisa Chat API** | API wird verwendet (.env, AiClient.cs) | Lizenzbedingungen, Rate Limits, Datenschutzbestimmungen der API |
| **DB Browser for SQLite Version** | In README.md empfohlen | Tatsächlich verwendete Version |
| **PowerShell Version** | PowerShell-Skripte in README.md | Konkrete PowerShell-Version (5.1, 7.x) |
| **Externe Code-Größe** | Cloc-Gesamtsumme: 3454 Zeilen | Genau Aufschlüsselung nach Dateien inkl. Exclude der generierten Dateien |

### Lizenzhinweise

Für alle verwendeten NuGet-Pakete gilt:

> **Lizenz muss separat überprüft werden.**

Es wird empfohlen, die offiziellen Lizenzinformationen von NuGet.org oder aus den Paket-Metadaten zu beziehen und in einer separaten Lizenzdatei zu dokumentieren.

### Projekt-Lizenz

Das Repository enthält **keine LICENSE-Datei**. Für eine Studienarbeit sollte geklärt werden:

- Unter welcher Lizenz der eigene Code veröffentlicht wird
- Ob alle externen Abhängigkeiten (insbesondere Apache License 2.0 bei PdfPig) mit der gewählten Lizenz kompatibel sind

---

## 13. Kurzfassung für die Studienarbeitsdokumentation

Die folgenden Tabellen sind für die direkte Übernahme in die Dokumentation der Studienarbeit vorgesehen.

### Tabelle 1: Verwendete externe Bibliotheken und NuGet-Pakete

| Paketname | Version | Zweck | Lizenz |
|-----------|---------|-------|--------|
| CommunityToolkit.Mvvm | 8.4.2 | MVVM-Framework für .NET (ObservableObject, RelayCommand) | Zu überprüfen |
| DotNetEnv | 3.1.1 | Laden von .env-Konfigurationsdateien | Zu überprüfen |
| Microsoft.EntityFrameworkCore | 10.0.8 | ORM für Datenzugriff und Migrationen (Code First) | Zu überprüfen |
| Microsoft.EntityFrameworkCore.Design | 10.0.8 | Design-Time-Tools für EF Core Migrationen | Zu überprüfen |
| Microsoft.EntityFrameworkCore.Sqlite | 10.0.8 | SQLite Database Provider für Entity Framework Core | Zu überprüfen |
| Microsoft.EntityFrameworkCore.Tools | 10.0.8 | CLI/PowerShell-Tools für EF Core | Zu überprüfen |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | Dependency Injection Container (IoC) | Zu überprüfen |
| Microsoft.Extensions.Http | 10.0.8 | Typed HttpClient Factory für API-Kommunikation | Zu überprüfen |
| PdfPig | 0.1.8 | PDF-Textextraktion vor KI-Analyse | Zu überprüfen |

**Hinweis:** Alle Lizenzangaben müssen separat überprüft werden. Quellen: NuGet.org oder Paket-Metadaten.

### Tabelle 2: Externe Dienste und APIs

| Dienst / API | Zweck | Konfiguration |
|--------------|-------|---------------|
| Lisa Chat API | KI-gestützte PDF-Analyse (Summary, StructuredContent, Quiz) | `.env`: AiBaseUrl, AiEndpoint, ApiKey, AiModel |
| SQLite Database | Lokale persistente Datenspeicherung | Automatisch: `%LOCALAPPDATA%\StudyMate\studymate.db` |

### Tabelle 3: Entwicklungswerkzeuge

| Werkzeug | Zweck | Version |
|----------|-------|---------|
| .NET SDK | Compiler, Runtime, CLI-Tools | 10.0 |
| Entity Framework Core Tools | Datenbank-Migrationen | 10.0.8 |
| Visual Studio 2022 | Integrierte Entwicklungsumgebung | 2022 (empfohlen laut README) |
| Git | Versionskontrolle | Nicht spezifiziert |
| GitHub | Remote-Repository Hosting | Nicht spezifiziert |
| NuGet Package Manager | Abhängigkeitsverwaltung | Integriert in .NET SDK |
| PowerShell | Skripting und Automatisierung | Nicht spezifiziert |
| DB Browser for SQLite | Datenbank-Inspektion | Nicht spezifiziert |

### Tabelle 4: KI-Nutzung im Entwicklungsprozess

| Bereich | Art der KI-Unterstützung | Prüfung und eigene Anpassung |
|---------|--------------------------|------------------------------|
| Backend File Service | Architekturerklärung, Bug-Identification, Code-Review | Eigene Implementierung, Bugfixes, Tests |
| UI Implementation | ViewModel-Erstellung, XAML-Design, Converter | Layout-Entwurf, CRUD-Implementierung, Tests |
| Modern UI Redesign | Refactoring, Bugfixes, Übersetzung | Card-UI-Entwurf, Drei-Zustands-Logik, End-to-End-Tests |
| Quiz Feature | ViewModel-Erstellung, Command-Implementierung | Drei-Spalten-Layout, Quiz-UI, Farbdfeedback |
| AI Integration | API-Debugging, PDF-Extraktion, DI-Konfiguration | Bibliotheksinstallation, Debugging, Flow-Tests |

**Datenquelle:** `Documentation/ki-nutzungsprotokoll-vorlage.md` (vollständiges Protokoll liegt bei)

### Architekturübersicht (textuell)

Die Anwendung folgt einer mehrschichtigen Architektur:

1. **Präsentationsschicht:** WPF Views (XAML) mit Data Binding an ViewModels
2. **ViewModel-Schicht:** MVVM mit CommunityToolkit.Mvvm (ObservableObject, RelayCommand)
3. **Service-Schicht:** Geschäftslogik und Orchestrierung
4. **Repository-Schicht:** Datenzugriff mit Entity Framework Core
5. **Datenzugriffsschicht:** SQLite-Datenbank (lokal, dateibasiert)
6. **Externe Integrationen:** Lisa Chat API (HTTP), PDF-Textextraktion (PdfPig)

Kommunikation zwischen den Schichten erfolgt über Dependency Injection (Microsoft.Extensions.DependencyInjection) mit Constructor Injection Pattern.

---

## Verzeichnis der referenzierten Dateien

Alle Aussagen in diesem Bericht basieren auf folgenden Dateien des Repositories:

- `StudyMate.Wpf/StudyMate.Wpf.csproj` – Paketreferenzen, Target Framework
- `StudyMate.slnx` – Solution-Datei
- `README.md` – Systemvoraussetzungen, Setup-Anleitung
- `.env.example` – API-Konfiguration (Template)
- `Documentation/ki-nutzungsprotokoll-vorlage.md` – KI-Nutzungsprotokoll
- `.gitignore` – Git-Ignorierregeln
- `StudyMate.Wpf/App.xaml.cs` – Dependency Injection Konfiguration
- `StudyMate.Wpf/Data/AppDbContext.cs` – DbContext-Definition
- `StudyMate.Wpf/Migrations/` – Automatisch generierte EF-Core-Dateien
- Alle Quelldateien in `StudyMate.Wpf/` (Models, ViewModels, Services, Repositories, Views, Integrations)

---

## Prüfcheckliste für den Entwickler

Vor Einreichung der Studienarbeit müssen folgende Punkte manuell bestätigt werden:

- [ ] **Lizenzen aller NuGet-Pakete überprüft** (insbesondere PdfPig: Apache 2.0 vs. MIT bei anderen)
- [ ] **Eigenes Projekt unter eine Lizenz gestellt** (LICENSE-Datei erstellt)
- [ ] **Tatsächlich verwendete Visual Studio Version dokumentiert**
- [ ] **Git Version dokumentiert**
- [ ] **Nutzungsbedingungen der Lisa Chat API gelesen und akzeptiert**
- [ ] **Datenschutzkonformität der API-Nutzung geprüft** (speichert API Anbieter Daten?)
- [ ] **Cloc-Analyse mit exakter Trennung generierter Dateien durchgeführt**
- [ ] **Alle im KI-Nutzungsprotokoll genannten Aktivitäten auf Vollständigkeit geprüft**
- [ ] **Entscheidung getroffen: Wird der Code unter Open-Source-Lizenz veröffentlicht oder proprietär?**

---

*Dieser Report wurde erstellt basierend auf einer automatisierten Analyse des Repository-Stands vom 24. Juli 2026. Alle Aussagen sind ausschließlich durch repository-interne Dokumente und Quelldateien verifiziert.*
