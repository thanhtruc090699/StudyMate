using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyMate.Wpf.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyFolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FolderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalFileName = table.Column<string>(type: "TEXT", nullable: false),
                    StoredFileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyFile_StudyFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "StudyFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudyFileId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    StructuredContentJson = table.Column<string>(type: "TEXT", nullable: false),
                    QuizJson = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ErrorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    ModelName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiAnalysis_StudyFile_StudyFileId",
                        column: x => x.StudyFileId,
                        principalTable: "StudyFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiAnalysis_StudyFileId",
                table: "AiAnalysis",
                column: "StudyFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyFile_FolderId",
                table: "StudyFile",
                column: "FolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiAnalysis");

            migrationBuilder.DropTable(
                name: "StudyFile");

            migrationBuilder.DropTable(
                name: "StudyFolders");
        }
    }
}
