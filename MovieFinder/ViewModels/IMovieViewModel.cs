
namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for ViewModel containing information about a movie.
/// </summary>
public interface IMovieViewModel
{
    /// <summary>
    /// Returns true if it's an adult movie.
    /// </summary>
    bool Adult { get; }

    /// <summary>
    /// Path to an image extracted from the movie.
    /// </summary>
    string BackdropPath { get; }

    /// <summary>
    /// A collection of genre IDs.
    /// </summary>
    List<int> GenreIds { get; }

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// The original language of the movie.
    /// </summary>
    string OriginalLanguage { get; }

    /// <summary>
    /// The original title of the movie.
    /// </summary>
    string OriginalTitle { get; }

    /// <summary>
    /// The overview of the movie.
    /// </summary>
    string Overview { get; }

    /// <summary>
    /// The popularity of the movie.
    /// </summary>
    double Popularity { get; }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    string PosterPath { get; }

    /// <summary>
    /// The release date for the movie.
    /// </summary>
    string ReleaseDate { get; }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Returns true if it's not a movie, but other types of video material.
    /// Examples can be compilations, filmed sport events, fitness videos, how-to DVDs, etc.
    /// </summary>
    bool Video { get; }

    /// <summary>
    /// The average vote for the movie.
    /// </summary>
    double VoteAverage { get; }

    /// <summary>
    /// The number of votes for the movie.
    /// </summary>
    int VoteCount { get; }
}
