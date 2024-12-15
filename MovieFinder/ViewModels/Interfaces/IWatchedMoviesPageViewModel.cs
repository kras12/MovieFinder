using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Inteface for a viewmodel for a page that shows movies that the user have watched. 
/// </summary>
public interface IWatchedMoviesPageViewModel
{
    /// <summary>
    /// Deletes a movie from the database. 
    /// </summary>
    public IAsyncRelayCommand<IWatchedMovieViewModel> DeleteMovieCommand { get; }

    /// <summary>
    /// A collection of movies that the use have watched. 
    /// </summary>
    public ObservableCollection<IWatchedMovieViewModel> Movies { get; set; }

    /// <summary>
    /// The title that is shown above the movie list.
    /// </summary>
    public string PageTitle { get; }
}
