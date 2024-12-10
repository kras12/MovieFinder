using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for a view model class for the main page. 
/// </summary>
public interface IMainPageViewModel
{
    /// <summary>
    /// A collection of movies fetched from the API. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Movies { get; set; }

    /// <summary>
    /// The command to search movies. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    public IAsyncRelayCommand SearchMoviesCommand { get; }
}
