using MovieFinder.Data.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MovieFinder.Data.Services;

/// <summary>
/// Provides a movie categories cache system that is periodically updated. 
/// </summary>
public class MovieCategoryCacheService : IMovieCategoryCacheService, IDataLayerMovieCategoryCacheService
{
    #region Constants

    /// <summary>
    /// The number of minutes to wait between each refresh of the movie category lookup. 
    /// </summary>
    private const int RefreshMovieCategoriesDelayInMinutes = 5;

    #endregion

    #region Fields

    /// <summary>
    /// The injected movie API service. 
    /// </summary>
    private readonly IMovieApiService _movieApiService;

    /// <summary>
    /// The movie category lookup.
    /// </summary>
    private Dictionary<int, MovieCategory> _categoryLookup = [];

    /// <summary>
    /// A semaphore to controll access to the movie category lookup table. 
    /// </summary>
    private SemaphoreSlim _categoryLookupSemaphore = new SemaphoreSlim(1);

    /// <summary>
    /// The task that initalizes the movie category lookup table. 
    /// </summary>
    private Task _initializeMovieCategoriesTask;

    /// <summary>
    /// The task that is responsible for refreshing the movie category lookup table. 
    /// </summary>
    private Task _periodicMovieCategoriesRefreshTask;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieApiService">The injected movie API service. </param>
    public MovieCategoryCacheService(IMovieApiService movieApiService)
    {
        _movieApiService = movieApiService;
        _initializeMovieCategoriesTask = RefreshCategoriesAsync();
         _periodicMovieCategoriesRefreshTask = StartPeriodicMovieCategoriesRefreshTask();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Attempts to return a matching movie category. 
    /// </summary>
    /// <param name="movieCategoryId">The ID of the movie category to match.</param>
    /// <param name="movieCategory">The matching <see cref="MovieCategory"/> if found.</param>
    /// <returns>True if the operation was successful.</returns>
    public bool TryGetCategory(int movieCategoryId, [NotNullWhen(true)] out MovieCategory? movieCategory)
    {
        try
        {
            _initializeMovieCategoriesTask.GetAwaiter().GetResult();
            _categoryLookupSemaphore.Wait();

            if (_categoryLookup.ContainsKey(movieCategoryId))
            {
                movieCategory = _categoryLookup[movieCategoryId];
                return true;
            }

            movieCategory = null;
            return false;
        }
        finally
        {
            _categoryLookupSemaphore.Release();
        }
    }

    /// <summary>
    /// Refreshes the categories in the lookup table. 
    /// </summary>
    /// <returns></returns>
    private Task RefreshCategoriesAsync()
    {
        return Task.Run(
            async () =>
            {
                var response = await _movieApiService.GetMovieCategories();

                if (response.IsSuccess && response.Data != null)
                {
                    try
                    {
                        await _categoryLookupSemaphore.WaitAsync();
                        _categoryLookup = response.Data.MovieCategories.ToDictionary(x => x.Id, x => x);
                    }
                    finally
                    {
                        _categoryLookupSemaphore.Release();
                    }
                }
            });
    }

    /// <summary>
    /// Creates a task that is responsible for refreshing the movie category lookup table. 
    /// </summary>
    private Task StartPeriodicMovieCategoriesRefreshTask()
    {
        return Task.Run(
            async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(RefreshMovieCategoriesDelayInMinutes));
                        await RefreshCategoriesAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occured while refreshing the movie category lookup: {ex.Message}");
                    }
                }
            });
    }

    #endregion
}
