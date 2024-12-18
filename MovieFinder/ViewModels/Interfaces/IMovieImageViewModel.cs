namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a movie image.
/// </summary>
public interface IMovieImageViewModel
{
    #region Properties

    /// <summary>
    /// The aspect ratio of the image.
    /// </summary>
    double AspectRatio { get; set; }

    /// <summary>
    /// The relative file path of the image.
    /// </summary>
    string FilePath { get; set; }

    /// <summary>
    /// The height of the image.
    /// </summary>
    int Height { get; set; }

    /// <summary>
    /// The language code for the image. 
    /// </summary>
    string? LanguageCode { get; set; }

    /// <summary>
    /// The average vote for the image.
    /// </summary>
    double VoteAverage { get; set; }

    /// <summary>
    /// The number of votes for the image.
    /// </summary>
    int VoteCount { get; set; }

    /// <summary>
    /// The width of the image. 
    /// </summary>
    int Width { get; set; }

    #endregion
}
