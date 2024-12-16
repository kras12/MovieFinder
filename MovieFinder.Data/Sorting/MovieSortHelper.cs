namespace MovieFinder.Data.Sorting;

/// <summary>
/// Helper class that contains the movie sort values for the API. 
/// </summary>
public static class MovieSortHelper
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    static MovieSortHelper()
    {
        AllSortValues = CreateAllSortValues();
    }

    #endregion

    #region Properties

    /// <summary>
    /// A collection of all movie sort values. 
    /// </summary>
    public static List<MovieSortValue> AllSortValues { get; }

    /// <summary>
    /// The descdending sort by popularity value. 
    /// </summary>
    public static MovieSortValue ByPopularityDescValue { get; private set; }

    /// <summary>
    /// The ascending sort by title value. 
    /// </summary>
    public static MovieSortValue ByTitleAscValue { get; private set; }

    /// <summary>
    /// The descdending sort by title value. 
    /// </summary>
    public static MovieSortValue ByTitleDescValue { get; private set; }

    /// <summary>
    /// The descdending sort by vote value. 
    /// </summary>
    public static MovieSortValue ByVoteDescValue { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Gets a movie sort value by its type. 
    /// </summary>
    /// <param name="sortType">The type of the sort value.</param>
    /// <returns><see cref="MovieSortValue"/></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MovieSortValue GetSortValue(MovieSortType sortType)
    {
        var targetValue = AllSortValues.FirstOrDefault(x => x.SortType == sortType);

        if (targetValue != null)
        {
            return targetValue;
        }

        throw new ArgumentException($"Unsupported sort type was provided as an argument.");
    }

    /// <summary>
    /// Creates all movie sort values. 
    /// </summary>
    /// <returns></returns>
    private static List<MovieSortValue> CreateAllSortValues()
	{
        ByPopularityDescValue = new MovieSortValue(MovieSortType.ByPopularityDesc, "popularity.desc");
        ByVoteDescValue = new MovieSortValue(MovieSortType.ByVoteDesc, "vote_average.desc");
        ByTitleDescValue = new MovieSortValue(MovieSortType.ByTitleDesc, "title.desc");
        ByTitleAscValue = new MovieSortValue(MovieSortType.ByTitleAsc, "title.asc");

        return new List<MovieSortValue>()
        {
            ByPopularityDescValue,
            ByVoteDescValue,
            ByTitleDescValue,
            ByTitleAscValue
        };
    }

    #endregion
}
