using MovieFinder.Data.Models;

namespace MovieFinder.ViewModels;

/// <summary>
/// ViewModel class containing information about a movie.
/// </summary>
public partial class MovieViewModel : IMovieViewModel
{
    #region Constants

    /// <summary>
    /// The base url path for fetching all movie images. 
    /// </summary>
    private const string ImageBasePath = "https://image.tmdb.org/t/p/w300";

    #endregion

    #region Fields

    /// <summary>
    /// The wrapped movie. 
    /// </summary>
    private Movie _movie;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movie">The movie to wrap.</param>
    public MovieViewModel(Movie movie)
    {
        _movie = movie;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns true if it's an adult movie.
    /// </summary>
    public bool Adult => _movie.Adult;

    /// <summary>
    /// Path to an image extracted from the movie.
    /// </summary>
    public string BackdropPath => ConstructImagePath(_movie.BackdropPath);

    /// <summary>
    /// A collection of genre IDs.
    /// </summary>
    public List<int> GenreIds => _movie.GenreIds;

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    public int Id => _movie.Id;

    /// <summary>
    /// The original language of the movie.
    /// </summary>
    public string OriginalLanguage => _movie.OriginalLanguage;

    /// <summary>
    /// The original title of the movie.
    /// </summary>
    public string OriginalTitle => _movie.OriginalTitle;

    /// <summary>
    /// The overview of the movie.
    /// </summary>
    public string Overview => _movie.Overview;

    /// <summary>
    /// The popularity of the movie.
    /// </summary>
    public double Popularity => _movie.Popularity;

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    public string PosterPath => ConstructImagePath(_movie.PosterPath);

    /// <summary>
    /// The release date for the movie.
    /// </summary>
    public string ReleaseDate => _movie.ReleaseDate;

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title => _movie.Title;

    /// <summary>
    /// Returns true if it's not a movie, but other types of video material.
    /// Examples can be compilations, filmed sport events, fitness videos, how-to DVDs, etc.
    /// </summary>
    public bool Video => _movie.Video;

    /// <summary>
    /// The average vote for the movie.
    /// </summary>
    public double VoteAverage => _movie.VoteAverage;

    /// <summary>
    /// The number of votes for the movie.
    /// </summary>
    public int VoteCount => _movie.VoteCount;

    #endregion

    #region Methods

    /// <summary>
    /// Returns the complete URL for a movie image. 
    /// </summary>
    /// <param name="relativeImagePath">The relative image URL path.</param>
    /// <returns><see cref="string"/></returns>
    private string ConstructImagePath(string relativeImagePath)
    {
        return $"{ImageBasePath}/{relativeImagePath.TrimStart(['/'])}";
    }

    #endregion
}
