using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Data.Helpers;
using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.ViewModels;

/// <summary>
/// Viewmodel for a watched movie. 
/// </summary>
public partial class WatchedMovieViewModel : ObservableObject, IWatchedMovieViewModel
{
    #region Fields

    /// <summary>
    /// Backing field for property <see cref="ApiMovieId"/>.
    /// </summary>
    private int _apiMovieId;

    /// <summary>
    /// Backing field for property <see cref="PosterPath"/>.
    /// </summary>
    private string? _posterPath = "";

    /// <summary>
    /// Backing field for property <see cref="Title"/>.
    /// </summary>
    private string _title = "";

    /// <summary>
    /// Backing field for property <see cref="Vote"/>.
    /// </summary>
    private int _vote;

    /// <summary>
    /// Backing field for property <see cref="WatchedMovieId"/>.
    /// </summary>
    private int _watchedMovieId;

    #endregion

    #region properties

    /// <summary>
    /// The ID of the API movie. 
    /// </summary>
    public int ApiMovieId
    {
        get => _apiMovieId;
        set => SetProperty(ref _apiMovieId, value);
    }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    public string? PosterPath
    {
        get => _posterPath;
        init => SetProperty(ref _posterPath, value != null ? MovieImageHelper.ConstructImagePath(value) : null);
    }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    /// <summary>
    /// A formatted text to show the user's vote for the movie. 
    /// </summary>
    public string UserVoteText
    {
        get => $"Your vote: {Vote}";
    }

    /// <summary>
    /// The user's vote for this movie. 
    /// </summary>
    public int Vote
    {
        get => _vote;

        set
        {
            SetProperty(ref _vote, value);
            OnPropertyChanged(nameof(UserVoteText));
        }
    }

    /// <summary>
    /// The ID of the watched movie. 
    /// </summary>
    public int WatchedMovieId
    {
        get => _watchedMovieId;
        set => SetProperty(ref _watchedMovieId, value);
    }

    #endregion
}
