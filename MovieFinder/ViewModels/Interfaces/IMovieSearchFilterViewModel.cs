using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a view model that contains filter data for movie searches.
/// </summary>
public interface IMovieSearchFilterViewModel
{
    /// <summary>
    /// A collection of all movie categories.
    /// </summary>
    public ObservableCollection<IMovieCategoryViewModel> MovieCategories { get; set; }

    /// <summary>
    /// The page of the search result being requested. 
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Filters the movies by category
    /// </summary>
    public IMovieCategoryViewModel? WithCategory { get; set; }
}