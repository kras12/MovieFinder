using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

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
    /// Filters the movies by category
    /// </summary>
    public IMovieCategoryViewModel? WithCategory { get; set; }
}