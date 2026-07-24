using Microsoft.EntityFrameworkCore;
using StudyMate.Wpf.Models;

namespace StudyMate.Wpf.Data;

/// <summary>
/// Entity Framework Core database context for the StudyMate application.
/// Manages database connections, entity mappings, and data operations for
/// study folders, study files, and AI analyses.
/// 
/// Database Context Role:
/// - Configures connection to SQLite database
/// - Defines entity sets (DbSet) for tables: StudyFolders, StudyFiles, AiAnalyses
/// - Configures entity relationships and constraints in OnModelCreating
/// - Maps navigation properties and cascade delete behaviors
/// - Enforces NOT NULL constraints via IsRequired() calls
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the collection of StudyFolder entities, mapped to the StudyFolders table.
    /// </summary>
    public DbSet<StudyFolder> StudyFolders { get; set; }

    /// <summary>
    /// Gets or sets the collection of StudyFile entities, mapped to the StudyFiles table.
    /// </summary>
    public DbSet<StudyFile> StudyFiles { get; set; }

    /// <summary>
    /// Gets or sets the collection of AiAnalysis entities, mapped to the AiAnalyses table.
    /// </summary>
    public DbSet<AiAnalysis> AiAnalyses { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with specified options.
    /// </summary>
    /// <param name="options">Configuration options for this context (e.g., SQL connection).</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    /// <summary>
    /// Configures entity relationships, constraints, and schema mappings using the Fluent API.
    /// Called by Entity Framework Core during model creation.
    /// </summary>
    /// <param name="modelBuilder">The builder for constructing the EF Core model.</param>
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
