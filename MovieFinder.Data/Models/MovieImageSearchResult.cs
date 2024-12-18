namespace MovieFinder.Data.Models;

/// <summary>
/// Contains the result from a movie image search. 
/// </summary>
public class MovieImageSearchResult
{
    #region Properties

    /// <summary>
    /// A collection of images from the video. 
    /// </summary>
    public List<MovieImage> Backdrops { get; set; } = [];

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A collection of movie logos.
    /// </summary>
    public List<MovieImage> Logos { get; set; } = [];

    /// <summary>
    /// A collection of movie posters.
    /// </summary>
    public List<MovieImage> Posters { get; set; } = [];

    #endregion
}
