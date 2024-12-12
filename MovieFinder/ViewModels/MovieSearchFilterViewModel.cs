using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Contains filter data for movie searches.
/// </summary>
public partial class MovieSearchFilterViewModel : ObservableObject, IMovieSearchFilterViewModel
{
    #region Fields

    /// <summary>
    /// A collection of all movie categories.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IMovieCategoryViewModel> _movieCategories = [];

    /// <summary>
    /// Filters the movies by category
    /// </summary>
    [ObservableProperty]
    private IMovieCategoryViewModel? _withCategory;

    #endregion
}
