using CommunityToolkit.Mvvm.Input;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a view model class for showing moving details.
/// </summary>
public interface IMovieDetailsPageViewModel
{
    /// <summary>
    /// The command to go back to the home page. 
    /// </summary>
    IAsyncRelayCommand GoToHomePageCommand { get; }

    /// <summary>
    /// The movie to show details for. 
    /// </summary>
    IMovieViewModel Movie { get; }
}