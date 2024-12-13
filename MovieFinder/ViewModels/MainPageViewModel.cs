using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
    public IMovieSearchFilterViewModel _movieSearchFilter;

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
    /// Backing field for property <see cref="Data.Models.MovieSearchResult"/>.
    /// </summary>
    private IMovieSearchResultViewModel _movieSearchResult;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieApiService">The injected movie API service.</param>
    /// <param name="mapper">The injected mapper.</param>
    /// <param name="movieCategoryCacheService">The injected movie category cache service.</param>
    public MainPageViewModel(IMovieApiService movieApiService, IMapper mapper, IMovieCategoryCacheService movieCategoryCacheService, IMovieSearchFilterViewModel movieSearchFilterViewModel, IMovieSearchResultViewModel movieSearchResult)
    {
        _movieApiService = movieApiService;
        _mapper = mapper;
        _movieCategoryCacheService = movieCategoryCacheService;
        _movieSearchFilter = movieSearchFilterViewModel;
        MovieSearchResult = movieSearchResult;

        MovieSearchFilter.MovieCategories =
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
    /// Requests the next page using the current search filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand(CanExecute = nameof(CanGetNextMovieSearchPage))]
    private async Task GetNextMovieSearchPage()
    {
        if (MovieSearchResult.HaveNextPages)
        {
            MovieSearchFilter.Page = MovieSearchResult.Page + 1;
            await SearchMovies();
        }
    }

    /// <summary>
    /// Requests the previous page using the current search filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand(CanExecute = nameof(CanGetPreviousMovieSearchPage))]
    private async Task GetPreviousMovieSearchPage()
    {
        if (MovieSearchResult.HavePreviousPages)
        {
            MovieSearchFilter.Page = MovieSearchResult.Page - 1;
            await SearchMovies();
        }
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

    #region Properties

    /// <summary>
    /// Contains the result from a movie search.
    /// </summary>
    public IMovieSearchResultViewModel MovieSearchResult
    {
        get => _movieSearchResult;

        set
        {
            SetProperty(ref _movieSearchResult, value);
            GetNextMovieSearchPageCommand.NotifyCanExecuteChanged();
            GetPreviousMovieSearchPageCommand.NotifyCanExecuteChanged();
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Checks whether the <see cref="GetNextMovieSearchPageCommand"/> can be executed.
    /// </summary>
    /// <returns>True if the command can be executed.</returns>
    private bool CanGetNextMovieSearchPage()
    {
        return MovieSearchResult.HaveNextPages;
    }

    /// <summary>
    /// Checks whether the <see cref="GetPreviousMovieSearchPageCommand"/> can be executed.
    /// </summary>
    /// <returns>True if the command can be executed.</returns>
    private bool CanGetPreviousMovieSearchPage()
    {
        return MovieSearchResult.HavePreviousPages;
    }

    /// <summary>
    /// Searches for movies that fits the current filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task SearchMovies()
    {
        try
        {
            var response = await _movieApiService.SearchMovies(_mapper.Map<MovieSearchFilter>(MovieSearchFilter));

            if (response.IsSuccess && response.Data != null)
            {
                MovieSearchResult = _mapper.Map<IMovieSearchResultViewModel>(response.Data);
            }
            else
            {
                // TODO - Implement error handling
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when searching for movies: {ex.Message}");
        }
    }

    #endregion
}
