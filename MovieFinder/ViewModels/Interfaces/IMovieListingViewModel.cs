using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for movie listing. 
/// </summary>
public interface IMovieListingViewModel
{
    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Movies { get; }

    /// <summary>
    /// Displays the details for a movie.
    /// </summary>
    public IAsyncRelayCommand<IMovieViewModel> GetMovieDetailsCommand { get; }
}