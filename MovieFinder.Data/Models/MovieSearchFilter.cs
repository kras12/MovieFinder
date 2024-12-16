using MovieFinder.Data.Sorting;

namespace MovieFinder.Data.Models;

/// <summary>
/// Contains filter data for movie searches.
/// </summary>
public class MovieSearchFilter
{
    #region Properties

    /// <summary>
    /// The default sort order for the search. 
    /// </summary>
    public MovieSortValue DefaultSortBy { get; } = MovieSortHelper.ByPopularityDescValue;

    /// <summary>
    /// Set to true to include adult movies.
    /// </summary>
    public bool IncludeAdult { get; set; }

    /// <summary>
    /// Set to true to include other types of video material that isn't classed as a movie. 
    /// </summary>
    public bool IncludeVideo { get; set; }

    // TODO - Convert languages to an enum?
    /// <summary>
    /// The language of the movie.
    /// </summary>
    public string Language { get; set; } = "en-US";

    /// <summary>
    /// Sets the page of the search result being requested. 
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The sort order for the search. 
    /// </summary>
    public MovieSortValue? SortBy { get; set; } = null;

    /// <summary>
    /// Filters the movies by category ID.
    /// </summary>
    public int? WithCategory { get; set; }

    #endregion
}
