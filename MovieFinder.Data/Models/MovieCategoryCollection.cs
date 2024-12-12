using System.Text.Json.Serialization;

namespace MovieFinder.Data.Models;

/// <summary>
/// Contains a collection of movie categories.
/// </summary>
public class MovieCategoryCollection
{
    #region Properties

    /// <summary>
    /// A collection of movie categories.
    /// </summary>
    [JsonPropertyName("genres")]
    public List<MovieCategory> MovieCategories { get; set; } = [];

    #endregion
}
