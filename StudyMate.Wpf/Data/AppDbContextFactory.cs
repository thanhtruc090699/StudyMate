using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudyMate.Wpf.Data;

/// <summary>
/// Factory class for creating AppDbContext instances at design time.
/// Used by Entity Framework Core tools for migrations and database scaffolding.
/// Implements IDesignTimeDbContextFactory to provide database context creation
/// when running EF commands like "dotnet ef migrations add" or "dotnet ef database update".
/// </summary>
public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Creates a new instance of <see cref="AppDbContext"/> with SQLite configuration.
    /// Called by EF Core CLI tools during design-time operations.
    /// </summary>
    /// <param name="args">Command-line arguments passed from EF tools (unused).</param>
    /// <returns>A configured AppDbContext connected to studymate.db SQLite database.</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite(
            "Data Source=studymate.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}
