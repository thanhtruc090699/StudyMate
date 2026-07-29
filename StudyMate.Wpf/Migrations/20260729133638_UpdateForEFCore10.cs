using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyMate.Wpf.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForEFCore10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiAnalysis_StudyFile_StudyFileId",
                table: "AiAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyFile_StudyFolders_FolderId",
                table: "StudyFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyFile",
                table: "StudyFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AiAnalysis",
                table: "AiAnalysis");

            migrationBuilder.RenameTable(
                name: "StudyFile",
                newName: "StudyFiles");

            migrationBuilder.RenameTable(
                name: "AiAnalysis",
                newName: "AiAnalyses");

            migrationBuilder.RenameIndex(
                name: "IX_StudyFile_FolderId",
                table: "StudyFiles",
                newName: "IX_StudyFiles_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_AiAnalysis_StudyFileId",
                table: "AiAnalyses",
                newName: "IX_AiAnalyses_StudyFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyFiles",
                table: "StudyFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AiAnalyses",
                table: "AiAnalyses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AiAnalyses_StudyFiles_StudyFileId",
                table: "AiAnalyses",
                column: "StudyFileId",
                principalTable: "StudyFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyFiles_StudyFolders_FolderId",
                table: "StudyFiles",
                column: "FolderId",
                principalTable: "StudyFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiAnalyses_StudyFiles_StudyFileId",
                table: "AiAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyFiles_StudyFolders_FolderId",
                table: "StudyFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyFiles",
                table: "StudyFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AiAnalyses",
                table: "AiAnalyses");

            migrationBuilder.RenameTable(
                name: "StudyFiles",
                newName: "StudyFile");

            migrationBuilder.RenameTable(
                name: "AiAnalyses",
                newName: "AiAnalysis");

            migrationBuilder.RenameIndex(
                name: "IX_StudyFiles_FolderId",
                table: "StudyFile",
                newName: "IX_StudyFile_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_AiAnalyses_StudyFileId",
                table: "AiAnalysis",
                newName: "IX_AiAnalysis_StudyFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyFile",
                table: "StudyFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AiAnalysis",
                table: "AiAnalysis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AiAnalysis_StudyFile_StudyFileId",
                table: "AiAnalysis",
                column: "StudyFileId",
                principalTable: "StudyFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyFile_StudyFolders_FolderId",
                table: "StudyFile",
                column: "FolderId",
                principalTable: "StudyFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
