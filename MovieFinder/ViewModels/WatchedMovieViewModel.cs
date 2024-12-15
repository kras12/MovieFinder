using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.ViewModels;

/// <summary>
/// Viewmodel for a watched movie. 
/// </summary>
public partial class WatchedMovieViewModel : ObservableObject, IWatchedMovieViewModel
{
    #region Fields
    
    /// <summary>
    /// Backing field for property <see cref="MovieId"/>.
    /// </summary>
    private int _movieId;

    /// <summary>
    /// Backing field for property <see cref="Title"/>.
    /// </summary>
    private string _title = "";

    /// <summary>
    /// Backing field for property <see cref="Vote"/>.
    /// </summary>
    private int _vote;    
    
    #endregion

    #region properties

    /// <summary>
    /// The ID of the movie. 
    /// </summary>
    public int MovieId 
    {
        get => _movieId; 
        set => SetProperty(ref _movieId, value);
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
    /// The user's vote for this movie. 
    /// </summary>
    public int Vote 
    {
        get => _vote;
        set => SetProperty(ref _vote, value);
    }

    #endregion
}
