using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// View model class for the main page. 
/// </summary>
public partial class MainPageViewModel : ObservableObject, IMainPageViewModel
{
    #region Fields

    /// <summary>
    /// Contains the data for filtering the movie listing.
    /// </summary>
    [ObservableProperty]
    public IMovieSearchFilterViewModel _movieSearchFilterViewModel;

    /// <summary>
    /// The injected mapper. 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The injected movie API service. 
    /// </summary>
    private readonly IMovieApiService _movieApiService;

    /// <summary>
    /// The injected movie category cache service. 
    /// </summary>
    private readonly IMovieCategoryCacheService _movieCategoryCacheService;

    /// <summary>
    /// Returns true if the movie filters view is open. 
    /// </summary>
    [ObservableProperty]
    private bool _isMovieFiltersOpen;

    /// <summary>
    /// A collection of movies fetched from the API. 
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IMovieViewModel> _movies = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieApiService">The injected movie API service.</param>
    /// <param name="mapper">The injected mapper.</param>
    /// <param name="movieCategoryCacheService">The injected movie category cache service.</param>
    public MainPageViewModel(IMovieApiService movieApiService, IMapper mapper, IMovieCategoryCacheService movieCategoryCacheService, IMovieSearchFilterViewModel movieSearchFilterViewModel)
    {
        _movieApiService = movieApiService;
        _mapper = mapper;
        _movieCategoryCacheService = movieCategoryCacheService;
        _movieSearchFilterViewModel = movieSearchFilterViewModel;

        MovieSearchFilterViewModel.MovieCategories =
           new ObservableCollection<IMovieCategoryViewModel>(_mapper.Map<List<IMovieCategoryViewModel>>(_movieCategoryCacheService.GetAllCategories()));

        SearchMovies();
    }

    #endregion

    #region Commands    

    /// <summary>
    /// Closes the movie filters view and refreshes the movie collection. 
    /// </summary>
    [RelayCommand]
    private async Task ApplyMovieFilters()
    {
        IsMovieFiltersOpen = false;
        await SearchMovies();
    }

    [RelayCommand]
    private async Task GetMovieDetails(IMovieViewModel movie)
    {
        await Shell.Current.GoToAsync(nameof(MovieDetailsPage), animate: true, new Dictionary<string, object>()
        {
            { nameof(IMovieDetailsPageViewModel.Movie),  movie}
        });
    }


    /// <summary>
    /// Opens the movie filters view.
    /// </summary>
    [RelayCommand]
    private void OpenMovieFilters()
    {
        IsMovieFiltersOpen = true;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Searches for movies that fits the current filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task SearchMovies()
    {
        // TODO - Present filter options in the view. 
        var response = await _movieApiService.SearchMovies(_mapper.Map<MovieSearchFilter>(MovieSearchFilterViewModel));

        if (response.IsSuccess && response.Data != null)
        {
            Movies = new ObservableCollection<IMovieViewModel>(_mapper.Map<List<IMovieViewModel>>(response.Data.Results));
        }
        else
        {
            // TODO - Implement error handling
        }
    }

    #endregion
}
