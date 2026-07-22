# StudyMate

Ứng dụng WPF hỗ trợ học tập với AI, cho phép quản lý tài liệu học tập (PDF) và tự động tạo tóm tắt, cấu trúc kiến thức và câu hỏi trắc nghiệm.

## Yêu cầu hệ thống

- **.NET 10.0 Windows** (hoặc phiên bản mới hơn)
- **Windows 10/11**
- **Visual Studio 2022** (khuyến nghị) hoặc VS Code với C# extension
- **SQLite** (để xem database, không bắt buộc để chạy app)

## Cài đặt khi pull project về

### 1. Clone repository

```bash
git clone https://github.com/NET-2026/thi-thanh-truc-trinh.git
cd StudyMate
```

### 2. Khôi phục dependencies

```bash
dotnet restore
```

### 3. Cấu hình AI settings (tùy chọn)

Nếu muốn sử dụng tính năng AI, tạo file `.env` trong thư mục gốc với nội dung:

```env
AiBaseUrl=https://your-ai-api-url.com
AiEndpoint=/api/chat/completions
ApiKey=your-api-key
AiModel=your-model-name
```

**Lưu ý:** Nếu không có file `.env`, ứng dụng vẫn chạy nhưng tính năng AI sẽ không hoạt động (sẽ báo lỗi khi startup).

### 4. Chạy ứng dụng

#### Cách 1: Visual Studio
- Mở `StudyMate.slnx` trong Visual Studio
- Nhấn `F5` để chạy

#### Cách 2: Command line
```bash
dotnet run --project StudyMate.Wpf/StudyMate.Wpf.csproj
```

#### Cách 3: Build và chạy exe
```bash
dotnet build --configuration Release
.\StudyMate.Wpf\bin\Release\net10.0-windows\StudyMate.Wpf.exe
```

## Database

### Vị trí database

Database SQLite được lưu tại:
```
C:\Users\<username>\AppData\Local\StudyMate\studymate.db
```

### Migration

**Không cần chạy migration thủ công!** 

Khi ứng dụng khởi động, nó sẽ tự động:
- Kiểm tra database có tồn tại chưa
- Tự động tạo các bảng nếu chưa có (Code First approach)

Các bảng trong database:
- `StudyFolders` - Lưu trữ thư mục học tập
- `StudyFiles` - Lưu trữ file học tập (PDF)
- `AiAnalyses` - Lưu trữ kết quả phân tích AI

### Xem dữ liệu database

**Cách 1: DB Browser for SQLite** (khuyến nghị)
1. Tải tại: https://sqlitebrowser.org/dl/
2. Mở file `C:\Users\<username>\AppData\Local\StudyMate\studymate.db`

**Cách 2: Cài đặt sqlite3 CLI**
```powershell
winget install sqlite.sqlite
sqlite3 C:\Users\<username>\AppData\Local\StudyMate\studymate.db
```

Sau đó dùng lệnh SQL:
```sql
.tables                    -- Xem danh sách bảng
SELECT * FROM StudyFolders; -- Xem dữ liệu
.exit                      -- Thoát
```

## File uploads

### Vị trí lưu file

File PDF tải lên được lưu tại:
```
C:\Users\<username>\AppData\Local\StudyMate\uploads\
```

**Lưu ý quan trọng:**
- Folder `uploads/` trong project **không cần thiết** khi pull về
- Ứng dụng tự động tạo folder uploads trong AppData khi chạy
- Không commit file uploads vào git (đã có trong `.gitignore`)

## Debug logging

Đã loại bỏ toàn bộ debug logging trong production build. 

Nếu cần debug, application có thể ghi log tạm thời vào:
- `C:\Users\<username>\AppData\Local\Temp\ai_debug.log` (khi có lỗi AI)
- `C:\Users\<username>\AppData\Local\Temp\quiz.log` (khi làm quiz)

## Lỗi thường gặp

### 1. "AI BaseUrl is invalid"
- **Nguyên nhân:** Thiếu file `.env` hoặc cấu hình AI không đúng
- **Giải pháp:** Tạo file `.env` với cấu hình đúng hoặc bỏ qua nếu không dùng AI

### 2. "File not found" khi mở PDF
- **Nguyên nhân:** File PDF đã bị xóa khỏi folder uploads
- **Giải pháp:** Upload lại file

### 3. Database bị lock
- **Nguyên nhân:** Ứng dụng đóng đột ngột
- **Giải pháp:** Xóa file `studymate.db-wal` và `studymate.db-shm` (nếu có)

## Đếm dòng code (cho submission)

Sử dụng CLOC:
```bash
cloc --include-lang="C#,XAML" --exclude-dir=bin,obj --by-file-by-lang .
```

Download CLOC: https://github.com/AlDanial/cloc/releases

## Submit

Nộp bài qua Moodle theo hướng dẫn trong "Vorgaben Studienarbeit - .NET-Programmierung mit C# - SoSe 2026.pdf"

Deadline: **31. Juli 2026, 23:59 Uhr**
