# StudyMate

A WPF application for learning support with AI, allowing management of learning materials (PDFs) and automatic generation of summaries, knowledge structures, and multiple-choice questions.

## Documentation & Demo

The full project documentation includes screenshots and a step-by-step walkthrough of the main application flows:

- Folder management
- PDF upload
- AI-generated summaries
- Quiz generation
- Answer validation and explanations

[Open Documentation](Documentation/Documentation.pdf)

## System Requirements

- **.NET 10.0 Windows** (or later)
- **Windows 10/11**
- **Visual Studio 2022** (recommended) or VS Code with C# extension
- **SQLite** (for viewing database, not required to run the app)

## Setup After Pulling the Project

### 1. Clone the repository

```bash
git clone https://github.com/NET-2026/thi-thanh-truc-trinh.git
cd StudyMate
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure AI settings (REQUIRED for first run)

The application requires an `.env` file with API credentials to start:

1. Copy `.env.example` to `.env` in the `StudyMate.Wpf` folder:
   ```bash
   cd StudyMate.Wpf
   copy .env.example .env
   ```

2. Open `.env` and paste your API key:
   ```env
   ApiKey=your-actual-api-key
   AiBaseUrl=https://chat-1.ki-awz.iisys.de/
   AiEndpoint=api/chat/completions
   AiModel=lisa-pro-03-2026
   ```

**Important:** The application will show a configuration error and shut down if `.env` is missing or invalid. Get your API key from https://ki-awz.iisys.de/

### 4. Apply database migrations (REQUIRED for first run)

Before running the app, you MUST apply database migrations to create the SQLite database:

```bash
cd StudyMate.Wpf
dotnet ef database update
```

**Why this is needed:** The project uses EF Core Code First with migrations. When you pull the code fresh, the database doesn't exist yet and must be created from migrations.

**Note:** If you forget this step, the application will show "Database migration failed" error on startup. Just close the app, run the migration command above, then restart.

### 5. Run the application

#### Option 1: Visual Studio (Recommended)
- Open `StudyMate.slnx` in Visual Studio
- Press `F5` to run

#### Option 2: Command line
```bash
cd StudyMate.Wpf
dotnet run
```

#### Option 3: Build and run executable
```bash
cd StudyMate.Wpf
dotnet build --configuration Release
.\bin\Release\net10.0-windows\StudyMate.Wpf.exe
```

## Database

### Database Location

SQLite database is stored at:
```
C:\Users\<username>\AppData\Local\StudyMate\studymate.db
```

### Migrations

The application uses **EF Core Code First with migrations**. 

**On first run (after pulling code):**
1. You MUST manually apply migrations before running the app:
   ```bash
   cd StudyMate.Wpf
   dotnet ef database update
   ```

**On subsequent runs:**
- The application automatically checks and applies any new migrations on startup
- If there are pending model changes, you'll see an error - just run the migration command above

**To add a new migration after making model changes:**
```bash
cd StudyMate.Wpf
dotnet ef migrations add MigrationName
dotnet ef database update
```

**To reset the database completely:**
```powershell
# Delete existing database
Remove-Item "$env:LOCALAPPDATA\StudyMate\studymate.db" -Force

# Re-apply all migrations
dotnet ef database update
```

Database tables:
- `StudyFolders` - Stores learning folders
- `StudyFiles` - Stores learning files (PDFs)
- `AiAnalyses` - Stores AI analysis results

### View Database Data

**Option 1: DB Browser for SQLite** (recommended)
1. Download at: https://sqlitebrowser.org/dl/
2. Open file `C:\Users\<username>\AppData\Local\StudyMate\studymate.db`

**Option 2: Install sqlite3 CLI**
```powershell
winget install sqlite.sqlite
sqlite3 C:\Users\<username>\AppData\Local\StudyMate\studymate.db
```

Then use SQL commands:
```sql
.tables                    -- List all tables
SELECT * FROM StudyFolders; -- View data
.exit                      -- Exit
```

## File Uploads

### File Storage Location

Uploaded PDF files are stored at:
```
C:\Users\<username>\AppData\Local\StudyMate\uploads\
```

**Important:**
- The `uploads/` folder in the project root is **not needed** after pulling
- The application automatically creates the uploads folder in AppData when running
- Upload files are NOT committed to git (already in `.gitignore`)

## Common Errors

### 1. "Configuration Error: .env file not found"
- **Cause:** Missing `.env` file with API credentials
- **Solution:** 
  ```bash
  cd StudyMate.Wpf
  copy .env.example .env
  # Edit .env and add your API key
  ```

### 2. "Database migration failed: An error was generated for warning 'PendingModelChangesWarning'"
- **Cause:** Model has pending changes that need a new migration
- **Solution:**
  ```bash
  cd StudyMate.Wpf
  dotnet ef migrations add FixPendingChanges
  dotnet ef database update
  ```

### 3. "Database migration failed: table already exists"
- **Cause:** Database schema doesn't match current migrations (e.g., old database from previous version)
- **Solution:** Reset the database:
  ```powershell
  Remove-Item "$env:LOCALAPPDATA\StudyMate\studymate.db" -Force
  cd StudyMate.Wpf
  dotnet ef database update
  ```

### 4. "AI BaseUrl is invalid" or AI features not working
- **Cause:** Missing or incorrect AI configuration in `.env`
- **Solution:** Check `.env` file has all required values (ApiKey, AiBaseUrl, AiEndpoint, AiModel)

### 5. "File not found" when opening PDF
- **Cause:** PDF file was deleted from uploads folder
- **Solution:** Re-upload the file

### 6. Database is locked
- **Cause:** Application closed unexpectedly
- **Solution:** Delete `studymate.db-wal` and `studymate.db-shm` files (if they exist) in `%LOCALAPPDATA%\StudyMate\`
