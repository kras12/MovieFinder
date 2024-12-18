using System.Text.Json.Serialization;

namespace MovieFinder.Data.Models;

/// <summary>
/// Represents a movie image.
/// </summary>
public class MovieImage
{
    #region Properties

    /// <summary>
    /// The aspect ratio of the image.
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public double AspectRatio { get; set; }

    /// <summary>
    /// The height of the image.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The language code for the image. 
    /// </summary>
    [JsonPropertyName("iso_639_1")]
    public string? Iso6391 { get; set; } = "";

    /// <summary>
    /// The relative file path of the image.
    /// </summary>
    [JsonPropertyName("file_path")]
    public string FilePath { get; set; } = "";

    /// <summary>
    /// The average vote for the image.
    /// </summary>
    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    /// <summary>
    /// The number of votes for the image.
    /// </summary>
    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }

    /// <summary>
    /// The width of the image. 
    /// </summary>
    public int Width { get; set; }

    #endregion
}
