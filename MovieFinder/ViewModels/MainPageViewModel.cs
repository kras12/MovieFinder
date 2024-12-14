using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;
using MovieFinder.ViewModels.Interfaces;
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
    /// The category filter option for selecting all categories.
    /// </summary>
    private readonly IMovieCategoryViewModel _allCategory;

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
    /// Backing field for property <see cref="IsMovieFiltersOpen"/>
    /// </summary>
    private bool _isMovieFiltersOpen;

    /// <summary>
    /// Backing field for property <see cref="MovieSearchFilter"/>
    /// </summary>
    private IMovieSearchFilterViewModel _movieSearchFilter;

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
        MovieSearchFilter = movieSearchFilterViewModel;
        MovieSearchResult = movieSearchResult;

        MovieSearchFilter.MovieCategories =
           new ObservableCollection<IMovieCategoryViewModel>(_mapper.Map<List<IMovieCategoryViewModel>>(_movieCategoryCacheService.GetAllCategories()));

        _allCategory = _mapper.Map<IMovieCategoryViewModel>(new MovieCategory() { Id = 0, Name = "All" });
        MovieSearchFilter.MovieCategories.Insert(0, _allCategory);
        MovieSearchFilter.WithCategory = _allCategory;

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
        MovieSearchFilter.Page = 1;
        await SearchMovies();
    }

    /// <summary>
    /// Requests the first page using the current search filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand(CanExecute = nameof(CanNavigateBackwardInMovieSearchResult))]
    private async Task GetFirstMovieSearchPage()
    {
        if (CanNavigateBackwardInMovieSearchResult())
        {
            MovieSearchFilter.Page = 1;
            await SearchMovies();
        }
    }

    /// <summary>
    /// Requests the last page using the current search filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand(CanExecute = nameof(CanNavigateForwardInMovieSearchResult))]
    private async Task GetLastMovieSearchPage()
    {
        if (CanNavigateForwardInMovieSearchResult())
        {
            // TODO - Max
            MovieSearchFilter.Page = Math.Min(MovieSearchResult.TotalPages, _movieApiService.MaxNumberForPageRequest);
            await SearchMovies();
        }
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
    [RelayCommand(CanExecute = nameof(CanNavigateForwardInMovieSearchResult))]
    private async Task GetNextMovieSearchPage()
    {
        if (CanNavigateForwardInMovieSearchResult())
        {
            MovieSearchFilter.Page = Math.Min(MovieSearchResult.Page + 1, _movieApiService.MaxNumberForPageRequest);
            await SearchMovies();
        }
    }

    /// <summary>
    /// Requests the previous page using the current search filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand(CanExecute = nameof(CanNavigateBackwardInMovieSearchResult))]
    private async Task GetPreviousMovieSearchPage()
    {
        if (CanNavigateBackwardInMovieSearchResult())
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
    /// Returns true if the movie filters view is open. 
    /// </summary>
    public bool IsMovieFiltersOpen
    {
        get => _isMovieFiltersOpen;
        set => SetProperty(ref _isMovieFiltersOpen, value);
    }

    /// <summary>
    /// Contains the data for filtering the movie listing.
    /// </summary>
    public IMovieSearchFilterViewModel MovieSearchFilter
    {
        get => _movieSearchFilter;
        set => SetProperty(ref _movieSearchFilter, value);
    }

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
            GetFirstMovieSearchPageCommand.NotifyCanExecuteChanged();
            GetLastMovieSearchPageCommand.NotifyCanExecuteChanged();
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Returns true if it's possible to navigate backwards to another page using the current search parameters.
    /// </summary>
    public bool CanNavigateBackwardInMovieSearchResult()
    {
        return MovieSearchResult.TotalPages > 0 && MovieSearchFilter.Page > 1;
    }

    /// <summary>
    /// Returns true if it's possible to navigate forward to another page using the current search parameters.
    /// </summary>
    public bool CanNavigateForwardInMovieSearchResult()
    {
        return MovieSearchResult.TotalPages > 0
            && MovieSearchResult.Page < Math.Min(MovieSearchResult.TotalPages, _movieApiService.MaxNumberForPageRequest);
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
                Debug.WriteLine($"Error when searching for movies: {response.ApiError?.ErrorMessage ?? ""}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when searching for movies: {ex.Message}");
        }
    }

    #endregion
}
