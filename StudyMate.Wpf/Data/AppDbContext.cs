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
    }
}