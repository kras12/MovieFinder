using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MovieFinder.Shared.Helpers;
using MovieFinder.Database.Context;

namespace MovieFinder.Migration.Helpers;

/// <summary>
/// A design time database context needed to support scaffolding and migrations when the database context class resides in a standalone project.
/// </summary>
public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    #region Methods

    /// <summary>
    /// Creates a new instance of a derived context.
    /// </summary>
    /// <param name="args">Arguments provided by the design-time service.</param>
    /// <returns>An instance of <see cref="ApplicationDbContext"/>.</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(DatabaseSettingsHelper.DevDbConnectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    #endregion
}
