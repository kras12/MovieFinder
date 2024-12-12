namespace MovieFinder.Data.Models;

/// <summary>
/// Contains information about a movie category. 
/// </summary>
public class MovieCategory
{
    #region Properties

    /// <summary>
    /// The ID of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name { get; set; } = "";

    #endregion
}
