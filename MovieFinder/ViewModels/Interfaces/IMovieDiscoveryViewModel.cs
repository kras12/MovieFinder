using CommunityToolkit.Mvvm.Input;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a view model class for the main page. 
/// </summary>
public interface IMovieDiscoveryViewModel
{
    /// <summary>
    /// Closes the movie filters view and refreshes the movie collection. 
    /// </summary>
    public IAsyncRelayCommand ApplyMovieFiltersCommand { get; }

    /// <summary>
    /// Requests the first page using the current search filters. 
    /// </summary>
    public IAsyncRelayCommand GetFirstMovieSearchPageCommand { get; }

    /// <summary>
    /// Requests the last page using the current search filters. 
    /// </summary>
    public IAsyncRelayCommand GetLastMovieSearchPageCommand { get; }

    /// <summary>
    /// Displays the details for a movie.
    /// </summary>
    public IAsyncRelayCommand<IMovieViewModel> GetMovieDetailsCommand { get; }

    /// <summary>
    /// Requests the next page using the current search filters. 
    /// </summary>
    public IAsyncRelayCommand GetNextMovieSearchPageCommand { get; }

    /// <summary>
    /// Requests the previous page using the current search filters. 
    /// </summary>
    public IAsyncRelayCommand GetPreviousMovieSearchPageCommand { get; }

    /// <summary>
    /// Returns true if the movie filters view is open. 
    /// </summary>
    public bool IsMovieFiltersOpen { get; set; }

    /// <summary>
    /// Contains the data for filtering the movie listing.
    /// </summary>
    public IMovieSearchFilterViewModel MovieSearchFilter { get; }

    /// <summary>
    /// Contains the result from a movie search.
    /// </summary>
    public IMovieSearchResultViewModel MovieSearchResult { get; }

    /// <summary>
    /// Opens the movie filters view.
    /// </summary>
    public IRelayCommand OpenMovieFiltersCommand { get; }

    /// <summary>
    /// Refreshes data in the view model.
    /// </summary>
    public void RefreshData();
}
