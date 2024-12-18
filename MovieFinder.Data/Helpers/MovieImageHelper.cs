namespace MovieFinder.Data.Helpers;

/// <summary>
/// Helper class for movie images. 
/// </summary>
public static class MovieImageHelper
{
    #region Constants

    /// <summary>
    /// The base url path for fetching all movie images. 
    /// </summary>
    private const string ImageBasePath = "https://image.tmdb.org/t/p/w300";

    #endregion

    #region Methods

    /// <summary>
    /// Returns the complete URL for a movie image. 
    /// </summary>
    /// <param name="relativeImagePath">The relative image URL path.</param>
    /// <returns><see cref="string"/></returns>
    public static string ConstructImagePath(string relativeImagePath)
    {
        return $"{ImageBasePath}/{relativeImagePath.TrimStart(['/'])}";
    }

    #endregion
}
