using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Database.Repositories;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MovieFinder.ViewModels;

/// <summary>
/// Viewmodel for a page that shows movies that the user have watched. 
/// </summary>
public partial class WatchedMoviesPageViewModel : ObservableObject, IWatchedMoviesPageViewModel
{
    #region Fields

    /// <summary>
    /// Injected data mapper. 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Injected watched movies repository. 
    /// </summary>
    private readonly IWatchedMoviesRepository _watchedMoviesRepository;

    /// <summary>
    /// Backing field for property <see cref="Movies"/>.
    /// </summary>    
    private ObservableCollection<IWatchedMovieViewModel> _movies = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="watchedMoviesRepository">Injected watched movies repository. </param>
    /// <param name="mapper">/// <summary>
    /// Injected data mapper. 
    /// </summary></param>
    public WatchedMoviesPageViewModel(IWatchedMoviesRepository watchedMoviesRepository, IMapper mapper)
    {
        _watchedMoviesRepository = watchedMoviesRepository;
        _mapper = mapper;

        LoadWatchedMovies();
    }

    #endregion

    #region Commands

    /// <summary>
    /// Deletes a movie from the database. 
    /// </summary>
    /// <param name="movie">The movie to delete.</param>
    /// <returns></returns>
    [RelayCommand]
    private async Task DeleteMovie(IWatchedMovieViewModel movie)
    {
        try
        {
            if (await _watchedMoviesRepository.MovieExists(movie.WatchedMovieId))
            {
                await _watchedMoviesRepository.DeleteMovieAsync(movie.WatchedMovieId);
                Movies.Remove(movie);
                OnPropertyChanged(nameof(PageTitle));
            }            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }        
    }

    #endregion

    #region Properties

    /// <summary>
    /// A collection of movies that the use have watched. 
    /// </summary>
    public ObservableCollection<IWatchedMovieViewModel> Movies
    {
        get => _movies;

        set
        {
            SetProperty(ref _movies, value);
            OnPropertyChanged(nameof(PageTitle));
        }
    }

    /// <summary>
    /// The title that is shown above the movie list.
    /// </summary>
    public string PageTitle
    {
        get => $"Movie List ({Movies.Count})";
    }

    #endregion

    #region Methods
    
    /// <summary>
    /// Loads watched movies from the database. 
    /// </summary>
    /// <returns></returns>
    private async Task LoadWatchedMovies()
    {
        try
        {
            Movies = new ObservableCollection<IWatchedMovieViewModel>(_mapper.Map<List<IWatchedMovieViewModel>>(await _watchedMoviesRepository.GetAllAsync()));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    #endregion
}
