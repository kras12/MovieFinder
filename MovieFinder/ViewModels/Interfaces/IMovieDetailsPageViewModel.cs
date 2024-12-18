using CommunityToolkit.Mvvm.Input;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a view model class for showing moving details.
/// </summary>
public interface IMovieDetailsPageViewModel
{
    /// <summary>
    /// The movie to show details for. 
    /// </summary>
    public IMovieViewModel Movie { get; }

    /// <summary>
    /// Contains the result from a movie search.
    /// </summary>
    public IMovieImageSearchResultViewModel MovieImages { get; }

    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    public IAsyncRelayCommand NavigateBackCommand { get; }
}