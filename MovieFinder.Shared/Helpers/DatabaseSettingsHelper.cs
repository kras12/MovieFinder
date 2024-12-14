namespace MovieFinder.Shared.Helpers;

/// <summary>
/// A helper class that stores the the database connection string.
/// </summary>
public static class DatabaseSettingsHelper
{
    #region Properties

    /// <summary>
    /// The key for the developmment database connection string.
    /// </summary>
    public static string DevDbConnectionString => $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "movies.db")}";

    #endregion
}
