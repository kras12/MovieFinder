using Microsoft.EntityFrameworkCore;
using MovieFinder.Database.Configuration;
using MovieFinder.Database.Entities;

namespace MovieFinder.Database.Context;

/// <summary>
/// A database context for the Movie Finder application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Context options.</param>
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    #endregion

    #region DbSets

    /// <summary>
    /// Database context for watched movies.
    /// </summary>
    public DbSet<WatchedMovieEntity> WatchedMovies { get; set; }

    #endregion

    #region Methods


    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
        }
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new WatchedMovieConfiguration());
    }

    #endregion
}
