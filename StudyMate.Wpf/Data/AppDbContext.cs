using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Data;

public class AppDbContext : DbContext
{
    public DbSet<StudyFolder> StudyFolders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudyFolder>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.UpdatedAt).IsRequired();

            }
        );

        modelBuilder.Entity<StudyFile>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.OriginalFileName).IsRequired();
                entity.Property(x => x.StoredFileName).IsRequired();
                entity.Property(x => x.FilePath).IsRequired();
                entity.Property(x => x.FileExtension).IsRequired();
                entity.Property(x => x.ContentType).IsRequired();
                entity.Property(x => x.FileSizeBytes).IsRequired();
                entity.Property(x => x.UploadedAt).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.UpdatedAt).IsRequired();

                entity.HasOne(x => x.Folder)
                    .WithMany()
                    .HasForeignKey(x => x.FolderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        modelBuilder.Entity<AiAnalysis>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Summary).IsRequired();
                entity.Property(x => x.StructuredContentJson).IsRequired();
                entity.Property(x => x.QuizJson).IsRequired();
                entity.Property(x => x.Status).IsRequired();
                entity.Property(x => x.ErrorMessage);
                entity.Property(x => x.ModelName);

                entity.HasOne(x => x.StudyFile)
                    .WithMany(x => x.AiAnalyses)
                    .HasForeignKey(x => x.StudyFileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}