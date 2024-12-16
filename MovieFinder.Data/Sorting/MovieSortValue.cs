namespace MovieFinder.Data.Sorting;

/// <summary>
/// A class that represents a movie sort value used by the API.
/// </summary>
public class MovieSortValue
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sortType">The type of the sort value.</param>
    /// <param name="apiQueryString">The API query string associated with the sort value.</param>
    public MovieSortValue(MovieSortType sortType, string apiQueryString)
    {
        SortType = sortType;
        ApiQueryString = apiQueryString;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The API query string associated with the sort value.
    /// </summary>
    public string ApiQueryString { get; } = "";

    /// <summary>
    /// The type of the sort value.
    /// </summary>
    public MovieSortType SortType { get; }

    #endregion
}
