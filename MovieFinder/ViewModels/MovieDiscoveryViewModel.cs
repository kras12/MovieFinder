using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;
using MovieFinder.Data.Sorting;
using MovieFinder.Database.Entities;
using MovieFinder.Database.Repositories;
using MovieFinder.Helpers;
using MovieFinder.Pages;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MovieFinder.ViewModels;

/// <summary>
/// View model class for the movie discovery page
/// </summary>
public partial class MovieDiscoveryViewModel : ObservableObject, IMovieDiscoveryViewModel, IMovieListingViewModel
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
    /// The injected watched movies repository.
    /// </summary>
    private readonly IWatchedMoviesRepository _watchedMoviesRepository;

    /// <summary>
    /// Backing field for property <see cref="IsBusy"/>.
    /// </summary>
    private bool _isBusy;

    /// <summary>
    /// Backing field for property <see cref="IsMovieFiltersOpen"/>
    /// </summary>
    private bool _isMovieFiltersOpen;

    /// <summary>
    /// Backing field for property <see cref="MovieSearchFilter"/>
    /// </summary>
    private IMovieSearchFilterViewModel _movieSearchFilter;

    /// <summary>
    /// Backing field for property <see cref="MovieSearchResult"/>.
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
    /// <param name="movieSearchFilterViewModel">Contains the data for filtering the movie listing.</param>
    /// <param name="movieSearchResult">Contains the search result from a movie search.</param>
    /// <param name="watchedMoviesRepository">The injected watched movies repository.</param>
    public MovieDiscoveryViewModel(IMovieApiService movieApiService, IMapper mapper, IMovieCategoryCacheService movieCategoryCacheService,
        IMovieSearchFilterViewModel movieSearchFilterViewModel, IMovieSearchResultViewModel movieSearchResult, IWatchedMoviesRepository watchedMoviesRepository)
    {
        _movieApiService = movieApiService;
        _mapper = mapper;
        _movieCategoryCacheService = movieCategoryCacheService;
        MovieSearchFilter = movieSearchFilterViewModel;
        MovieSearchResult = movieSearchResult;
        _watchedMoviesRepository = watchedMoviesRepository;

        // TODO - Move some of these initializations into a factory service? 

        MovieSearchFilter.MovieCategories =
           new ObservableCollection<IMovieCategoryViewModel>(_mapper.Map<List<IMovieCategoryViewModel>>(_movieCategoryCacheService.GetAllCategories()));

        _allCategory = _mapper.Map<IMovieCategoryViewModel>(new MovieCategory() { Id = 0, Name = "All" });
        MovieSearchFilter.MovieCategories.Insert(0, _allCategory);
        MovieSearchFilter.WithCategory = _allCategory;

        MovieSearchFilter.MovieSortValues = new ObservableCollection<IMovieSortValueViewModel>(_mapper.Map<List<IMovieSortValueViewModel>>(MovieSortHelper.AllSortValues));
        MovieSearchFilter.SortBy = MovieSearchFilter.MovieSortValues.FirstOrDefault();

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
    /// Marks the movie as watched and adds it to the database.
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanCreateWatchedMovie))]
    private async Task CreateWatchedMovie(IMovieViewModel movie)
    {
        if (CanCreateWatchedMovie(movie))
        {
            var result = await _watchedMoviesRepository.CreateMovieAsync(_mapper.Map<WatchedMovieEntity>(movie));
            movie.IsWatched = true;
            DeleteWatchedMovieCommand.NotifyCanExecuteChanged();
            await ToastHelper.ShowToast($"{movie.Title} was added to your watched list!");
        }
    }

    /// <summary>
    /// Unmarks the movie as watched and deletes it from the database.
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanDeleteWatchedMovie))]
    private async Task DeleteWatchedMovie(IMovieViewModel movie)
    {
        if (CanDeleteWatchedMovie(movie))
        {
            var targetMovie = await _watchedMoviesRepository.GetAsync(movie.Title);

            if (targetMovie != null)
            {
                await _watchedMoviesRepository.DeleteMovieAsync(targetMovie);
                movie.IsWatched = false;
                CreateWatchedMovieCommand.NotifyCanExecuteChanged();
                await ToastHelper.ShowToast($"{movie.Title} was deleted from your watched list!");
            }
        }
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
    /// Contains a collection of found movies. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Movies => MovieSearchResult.Results;

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
            OnPropertyChanged(nameof(Movies));
        }
    }

    /// <summary>
    /// Returns true while the view model is busy. 
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy; 
        private set => SetProperty(ref _isBusy, value); 
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
    /// Refreshes data in the view model.
    /// </summary>
    public void RefreshData()
    {
        SynchronizeWatchedMoviesWithSearchresult();
    }

    /// <summary>
    /// Returns true if it's possible to create a watched movie post.
    /// </summary>
    /// <param name="movie">The target movie.</param>
    /// <returns>True if the command can be executed.</returns>
    private bool CanCreateWatchedMovie(IMovieViewModel movie)
    {
        return !movie.IsWatched;
    }

    /// <summary>
    /// Returns true if it's possible to delete a watched movie post.
    /// </summary>
    /// <param name="movie">The target movie.</param>
    /// <returns>True if the command can be executed.</returns>
    private bool CanDeleteWatchedMovie(IMovieViewModel movie)
    {
        return movie.IsWatched;
    }

    /// <summary>
    /// Searches for movies that fits the current filters. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task SearchMovies()
    {
        try
        {
            IsBusy = true;
            var response = await _movieApiService.SearchMovies(_mapper.Map<MovieSearchFilter>(MovieSearchFilter));

            if (response.IsSuccess && response.Data != null)
            {
                MovieSearchResult = _mapper.Map<IMovieSearchResultViewModel>(response.Data);
                await SynchronizeWatchedMoviesWithSearchresult();
                
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
        finally 
        {
            IsBusy = false; 
        }
    }

    /// <summary>
    /// Fetches watched movies from the database and synchronizes 
    /// </summary>
    /// <returns></returns>
    private async Task SynchronizeWatchedMoviesWithSearchresult()
    {
        MovieSearchResult.Results.ToList()
            .ForEach(x => x.IsWatched = false);

        var watchedMovies = await _watchedMoviesRepository.GetAllAsync();

        MovieSearchResult.Results
            .Where(x => watchedMovies.Select(x => x.ApiMovieId).Contains(x.Id))
            .ToList()
            .ForEach(x => x.IsWatched = true);

        CreateWatchedMovieCommand.NotifyCanExecuteChanged();
        DeleteWatchedMovieCommand.NotifyCanExecuteChanged();
    }

    #endregion
}
