# StudyMate

A WPF application for learning support with AI, allowing management of learning materials (PDFs) and automatic generation of summaries, knowledge structures, and multiple-choice questions.

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

### 3. Configure AI settings (optional)

If you want to use AI features, create a `.env` file in the root directory with:

```env
AiBaseUrl=https://your-ai-api-url.com
AiEndpoint=/api/chat/completions
ApiKey=your-api-key
AiModel=your-model-name
```

**Note:** Without the `.env` file, the application will still run but AI features will not work (will show an error on startup).

### 4. Run the application

#### Option 1: Visual Studio
- Open `StudyMate.slnx` in Visual Studio
- Press `F5` to run

#### Option 2: Command line
```bash
dotnet run --project StudyMate.Wpf/StudyMate.Wpf.csproj
```

#### Option 3: Build and run executable
```bash
dotnet build --configuration Release
.\StudyMate.Wpf\bin\Release\net10.0-windows\StudyMate.Wpf.exe
```

## Database

### Database Location

SQLite database is stored at:
```
C:\Users\<username>\AppData\Local\StudyMate\studymate.db
```

### Migrations

**No manual migration needed!**

When the application starts, it automatically:
- Checks if the database exists
- Creates tables automatically if they don't exist (Code First approach)

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

## Debug Logging

All debug logging has been removed from production build.

If debugging is needed, the application may temporarily log to:
- `C:\Users\<username>\AppData\Local\Temp\ai_debug.log` (when AI errors occur)
- `C:\Users\<username>\AppData\Local\Temp\quiz.log` (during quiz operations)

## Common Errors

### 1. "AI BaseUrl is invalid"
- **Cause:** Missing `.env` file or incorrect AI configuration
- **Solution:** Create `.env` file with correct configuration or skip if not using AI

### 2. "File not found" when opening PDF
- **Cause:** PDF file was deleted from uploads folder
- **Solution:** Re-upload the file

### 3. Database is locked
- **Cause:** Application closed unexpectedly
- **Solution:** Delete `studymate.db-wal` and `studymate.db-shm` files (if they exist)

## Count Lines of Code (for submission)

Use CLOC:
```bash
cloc --include-lang="C#,XAML" --exclude-dir=bin,obj --by-file-by-lang .
```

Download CLOC: https://github.com/AlDanial/cloc/releases

## Submission

Submit via Moodle according to instructions in "Vorgaben Studienarbeit - .NET-Programmierung mit C# - SoSe 2026.pdf"

Deadline: **July 31, 2026, 23:59**
