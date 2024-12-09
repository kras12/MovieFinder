
namespace MovieFinder.Data.Models;

/// <summary>
/// Contains the search result from a movie search.
/// </summary>
public class MovieSearchResult
{
    #region properties

    /// <summary>
    /// The page number for the search result.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    public List<Movie> Results { get; init; } = [];

    /// <summary>
    /// The total number of search result pages available for this search.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// The total number of movies found for this search. 
    /// </summary>
    public int TotalResults { get; init; }

    #endregion
}

