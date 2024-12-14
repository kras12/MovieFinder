namespace MovieFinder.Database.Entities;

public class WatchedMovieEntity
{
    /// <summary>
    /// The ID of the movie.
    /// </summary>
    public int MovieId { get; init; }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title { get; init; } = "";

    /// <summary>
    /// The vote given by the user. 
    /// </summary>
    public int Vote { get; init; }
}
