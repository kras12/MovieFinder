using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// ViewModel class containing information about a movie.
/// </summary>
public partial class MovieViewModel : ObservableObject, IMovieViewModel
{
    #region Constants

    /// <summary>
    /// The base url path for fetching all movie images. 
    /// </summary>
    private const string ImageBasePath = "https://image.tmdb.org/t/p/w300";

    #endregion

    #region Fields

    /// <summary>
    /// Backing field for property <see cref="Adult"/>.
    /// </summary>
    private bool _adult;

    /// <summary>
    /// Backing field for property <see cref="BackdropPath"/>.
    /// </summary>
    private string? _backdropPath = "";

    /// <summary>
    /// Backing field for property <see cref="Categories"/>.
    /// </summary>
    private ObservableCollection<IMovieCategoryViewModel> _categories = [];

    /// <summary>
    /// Backing field for property <see cref="Id"/>.
    /// </summary>
    private int _id;

    /// <summary>
    /// Backing field for property <see cref="IsWatched"/>.
    /// </summary>
    private bool _iswWatched;

    /// <summary>
    /// Backing field for property <see cref="OriginalLanguage"/>.
    /// </summary>
    private string _originalLanguage = "";

    /// <summary>
    /// Backing field for property <see cref="OriginalTitle"/>.
    /// </summary>
    private string _originalTitle = "";

    /// <summary>
    /// Backing field for property <see cref="Overview"/>.
    /// </summary>
    private string _overview = "";

    /// <summary>
    /// Backing field for property <see cref="Popularity"/>.
    /// </summary>
    private double _popularity;

    /// <summary>
    /// Backing field for property <see cref="PosterPath"/>.
    /// </summary>
    private string? _posterPath = "";

    /// <summary>
    /// Backing field for property <see cref="ReleaseDate"/>.
    /// </summary>
    private string _releaseDate = "";

    /// <summary>
    /// Backing field for property <see cref="Title"/>.
    /// </summary>
    private string _title = "";

    /// <summary>
    /// Backing field for property <see cref="Video"/>.
    /// </summary>
    private bool _video;

    /// <summary>
    /// Backing field for property <see cref="VoteAverage"/>.
    /// </summary>
    private double _voteAverage;

    /// <summary>
    /// Backing field for property <see cref="VoteCount"/>.
    /// </summary>
    private int _voteCount;

    #endregion

    #region Properties

    /// <summary>
    /// Returns true if it's an adult movie.
    /// </summary>
    public bool Adult
    {
        get => _adult;
        init => SetProperty(ref _adult, value);
    }

    /// <summary>
    /// Path to an image extracted from the movie.
    /// </summary>
    public string? BackdropPath
    {
        get
        {
            return _backdropPath;
        }

        init
        {
            SetProperty(ref _backdropPath, value != null ? ConstructImagePath(value) : null);
        }
    }

    /// <summary>
    /// A collection of categories for the move.    
    /// </summary>
    public ObservableCollection<IMovieCategoryViewModel> Categories
    {
        get => _categories;

        init
        {
            SetProperty(ref _categories, value);
            OnPropertyChanged(nameof(CategoriesString));
        }
    }

    /// <summary>
    /// The categories as a comma seperated string.
    /// </summary>
    public string CategoriesString => string.Join(", ", Categories.Select(x => x.Name).Order());

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    public int Id
    {
        get => _id;
        init => SetProperty(ref _id, value);
    }

    /// <summary>
    /// Returns true if the user have watched this movie. 
    /// </summary>
    public bool IsWatched
    {
        get => _iswWatched;
        set => SetProperty(ref _iswWatched, value);
    }

    /// <summary>
    /// The original language of the movie.
    /// </summary>
    public string OriginalLanguage
    {
        get => _originalLanguage;
        init => SetProperty(ref _originalLanguage, value);
    }

    /// <summary>
    /// The original title of the movie.
    /// </summary>
    public string OriginalTitle
    {
        get => _originalTitle;
        init => SetProperty(ref _originalTitle, value);
    }

    /// <summary>
    /// The overview of the movie.
    /// </summary>
    public string Overview
    {
        get => _overview;
        init => SetProperty(ref _overview, value);
    }

    /// <summary>
    /// The popularity of the movie.
    /// </summary>
    public double Popularity
    {
        get => _popularity;
        init => SetProperty(ref _popularity, value);
    }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    public string? PosterPath
    {
        get => _posterPath;
        init => SetProperty(ref _posterPath, value != null ? ConstructImagePath(value) : null);
    }

    /// <summary>
    /// The release date for the movie.
    /// </summary>
    public string ReleaseDate
    {
        get => _releaseDate;
        init => SetProperty(ref _releaseDate, value);
    }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title
    {
        get => _title;
        init => SetProperty(ref _title, value);
    }

    /// <summary>
    /// Returns true if it's not a movie, but other types of video material.
    /// Examples can be compilations, filmed sport events, fitness videos, how-to DVDs, etc.
    /// </summary>
    public bool Video
    {
        get => _video;
        init => SetProperty(ref _video, value);
    }

    /// <summary>
    /// The average vote for the movie.
    /// </summary>
    public double VoteAverage
    {
        get => _voteAverage;
        init => SetProperty(ref _voteAverage, value);
    }

    /// <summary>
    /// The number of votes for the movie.
    /// </summary>
    public int VoteCount
    {
        get => _voteCount;
        init => SetProperty(ref _voteCount, value);
    }

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
