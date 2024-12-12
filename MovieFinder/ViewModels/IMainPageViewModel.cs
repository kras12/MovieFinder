using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for a view model class for the main page. 
/// </summary>
public interface IMainPageViewModel
{
    /// <summary>
    /// Closes the movie filters view and refreshes the movie collection. 
    /// </summary>
    public IAsyncRelayCommand ApplyMovieFiltersCommand { get; }

    /// <summary>
    /// Returns true if the movie filters view is open. 
    /// </summary>
    public bool IsMovieFiltersOpen { get; set; }

    /// <summary>
    /// A collection of movies fetched from the API. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Movies { get; set; }

    /// <summary>
    /// Contains the data for filtering the movie listing.
    /// </summary>
    public IMovieSearchFilterViewModel MovieSearchFilterViewModel { get; }

    /// <summary>
    /// Opens the movie filters view.
    /// </summary>
    public IRelayCommand OpenMovieFiltersCommand { get; }
}
