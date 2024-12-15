using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieFinder.Database.Entities;

namespace MovieFinder.Database.Configuration;

/// <summary>
/// Configuration class for watched movie entities. 
/// </summary>
internal class WatchedMovieConfiguration : IEntityTypeConfiguration<WatchedMovieEntity>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WatchedMovieEntity> builder)
    {
        builder
            .HasKey(x => x.WatchedMovieId);

        builder
            .HasIndex(x => x.ApiMovieId)
            .IsUnique();
    }
}
