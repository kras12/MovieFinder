using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Contains filter data for movie searches.
/// </summary>
public partial class MovieSearchFilterViewModel : ObservableObject, IMovieSearchFilterViewModel
{
    #region Fields

    /// <summary>
    /// Backing field for property <see cref="MovieCategories"/>.
    /// </summary>
    private ObservableCollection<IMovieCategoryViewModel> _movieCategories = [];

    /// <summary>
    /// Backing field for property <see cref="WithCategory"/>.
    /// </summary>
    private IMovieCategoryViewModel? _withCategory;

    #endregion

    #region Properties

    /// <summary>
    /// A collection of all movie categories.
    /// </summary>
    public ObservableCollection<IMovieCategoryViewModel> MovieCategories
    {
        get => _movieCategories;
        set => SetProperty(ref _movieCategories, value);
    }

    /// <summary>
    /// Sets the page of the search result being requested. 
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Filters the movies by category
    /// </summary>
    public IMovieCategoryViewModel? WithCategory 
    {
        get => _withCategory;
        set => SetProperty(ref _withCategory, value);   
    }

    #endregion
}
