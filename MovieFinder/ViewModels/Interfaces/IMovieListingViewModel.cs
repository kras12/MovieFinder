using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for movie listing. 
/// </summary>
public interface IMovieListingViewModel
{
    /// <summary>
    /// Marks the movie as watched and adds it to the database.
    /// </summary>
    public IAsyncRelayCommand<IMovieViewModel> CreateWatchedMovieCommand { get; }

    /// <summary>
    /// Unmarks the movie as watched and deletes it from the database.
    /// </summary>
    public IAsyncRelayCommand<IMovieViewModel> DeleteWatchedMovieCommand { get; }

    /// <summary>
    /// Displays the details for a movie.
    /// </summary>
    public IAsyncRelayCommand<IMovieViewModel> GetMovieDetailsCommand { get; }

    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Movies { get; }
}