using CommunityToolkit.Mvvm.Input;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a view model class for showing moving details.
/// </summary>
public interface IMovieDetailsPageViewModel
{
    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    IAsyncRelayCommand NavigateBackCommand { get; }

    /// <summary>
    /// The movie to show details for. 
    /// </summary>
    IMovieViewModel Movie { get; }
}