using System.Text.Json.Serialization;

namespace MovieFinder.Data.Dto;

/// <summary>
/// Contains the search result from a movie search.
/// </summary>
internal class MovieSearchResultDto
{
    #region properties

    /// <summary>
    /// The page number for the search result.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    public List<MovieDto> Results { get; init; } = [];

    /// <summary>
    /// The total number of search result pages available for this search.
    /// </summary>
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    /// <summary>
    /// The total number of movies found for this search. 
    /// </summary>
    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }

    #endregion
}

