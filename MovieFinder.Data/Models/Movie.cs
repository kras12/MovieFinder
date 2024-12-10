using System.Text.Json.Serialization;

namespace MovieFinder.Data.Models;

/// <summary>
/// Contains information about a movie. 
/// </summary>
public class Movie
{
    #region Properties

    /// <summary>
    /// Returns true if it's an adult movie.
    /// </summary>
    public bool Adult { get; init; }

    /// <summary>
    /// Path to an image extracted from the movie. 
    /// </summary>
    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; init; } = "";

    /// <summary>
    /// A collection of genre IDs. 
    /// </summary>
    [JsonPropertyName("genre_ids")]
    public List<int> GenreIds { get; init; } = [];

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The original language of the movie.
    /// </summary>
    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; init; } = "";

    /// <summary>
    /// The original title of the move.
    /// </summary>
    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; init; } = "";

    /// <summary>
    /// The overview of the movie.
    /// </summary>
    public string Overview { get; init; } = "";

    /// <summary>
    /// The popularity of the movie.
    /// </summary>
    public double Popularity { get; init; }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    [JsonPropertyName("poster_path")]
    public string PosterPath { get; init; } = "";

    /// <summary>
    /// The release date for the movie.
    /// </summary>
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; init; } = "";

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title { get; init; } = "";

    /// <summary>
    /// Returns true if it's not a movie, but other types of video material. 
    /// Examples can be compilations, filmed sport events, fitness videos, how-to DVDs, etc. 
    /// </summary>
    public bool Video { get; init; }

    /// <summary>
    /// The average vote for the movie.
    /// </summary>
    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    /// <summary>
    /// The number of votes for the movie. 
    /// </summary>
    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    #endregion
}
