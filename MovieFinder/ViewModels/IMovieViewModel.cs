using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for ViewModel containing information about a movie.
/// </summary>
public interface IMovieViewModel
{
    /// <summary>
    /// Returns true if it's an adult movie.
    /// </summary>
    bool Adult { get; init; }

    /// <summary>
    /// Path to an image extracted from the movie.
    /// </summary>
    string BackdropPath { get; init; }

    /// <summary>
    /// A collection of categories for the move. 
    /// </summary>
    ObservableCollection<IMovieCategoryViewModel> Categories { get; init; }

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    int Id { get; init; }

    /// <summary>
    /// The original language of the movie.
    /// </summary>
    string OriginalLanguage { get; init; }

    /// <summary>
    /// The original title of the movie.
    /// </summary>
    string OriginalTitle { get; init; }

    /// <summary>
    /// The overview of the movie.
    /// </summary>
    string Overview { get; init; }

    /// <summary>
    /// The popularity of the movie.
    /// </summary>
    double Popularity { get; init; }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    string PosterPath { get; init; }

    /// <summary>
    /// The release date for the movie.
    /// </summary>
    string ReleaseDate { get; init; }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    string Title { get; init; }

    /// <summary>
    /// Returns true if it's not a movie, but other types of video material.
    /// Examples can be compilations, filmed sport events, fitness videos, how-to DVDs, etc.
    /// </summary>
    bool Video { get; init; }

    /// <summary>
    /// The average vote for the movie.
    /// </summary>
    double VoteAverage { get; init; }

    /// <summary>
    /// The number of votes for the movie.
    /// </summary>
    int VoteCount { get; init; }
}
