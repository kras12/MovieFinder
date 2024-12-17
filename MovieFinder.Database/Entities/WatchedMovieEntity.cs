namespace MovieFinder.Database.Entities;

public class WatchedMovieEntity
{
    /// <summary>
    /// The ID of the API movie.
    /// </summary>
    public int ApiMovieId { get; init; }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    public string PosterPath { get; init; } = "";

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title { get; init; } = "";

    /// <summary>
    /// The vote given by the user. 
    /// </summary>
    public int Vote { get; init; }

    /// <summary>
    /// The ID of the watched movie.
    /// </summary>
    public int WatchedMovieId { get; init; }
}
