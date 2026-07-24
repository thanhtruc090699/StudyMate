using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Data;

/// <summary>
/// Represents the Entity Framework Core database context for StudyMate
/// and provides access to study folders, files, and AI analyses.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<StudyFolder> StudyFolders { get; set; }

    public DbSet<StudyFile> StudyFiles { get; set; }

    public DbSet<AiAnalysis> AiAnalyses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures entity constraints, relationships, and delete behaviours.
    /// </summary>
    /// <param name="modelBuilder">
    /// The builder used to configure the Entity Framework Core model.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudyFolder>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.Property(x => x.UpdatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<StudyFile>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.OriginalFileName)
                .IsRequired();

            entity.Property(x => x.StoredFileName)
                .IsRequired();

            entity.Property(x => x.FilePath)
                .IsRequired();

            entity.Property(x => x.FileExtension)
                .IsRequired();

            entity.Property(x => x.ContentType)
                .IsRequired();

            entity.Property(x => x.FileSizeBytes)
                .IsRequired();

            entity.Property(x => x.UploadedAt)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.Property(x => x.UpdatedAt)
                .IsRequired();

            entity.HasOne(x => x.Folder)
                .WithMany()
                .HasForeignKey(x => x.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AiAnalysis>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.Property(x => x.Summary)
                .IsRequired();

            entity.Property(x => x.StructuredContentJson)
                .IsRequired();

            entity.Property(x => x.QuizJson)
                .IsRequired();

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.ErrorMessage);
            entity.Property(x => x.ModelName);

            entity.HasOne(x => x.StudyFile)
                .WithMany(x => x.AiAnalyses)
                .HasForeignKey(x => x.StudyFileId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}