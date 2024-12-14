using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Contains the search result from a movie search.
/// </summary>
public partial class MovieSearchResultViewModel : ObservableObject, IMovieSearchResultViewModel
{
    #region Fields

    /// <summary>
    /// Backing field for property <see cref="Page"/>.
    /// </summary>
    private int _page = 1;

    /// <summary>
    /// Backing field for property <see cref="Results"/>.
    /// </summary>
    private ObservableCollection<IMovieViewModel> _results = [];

    /// <summary>
    /// Backing field for property <see cref="TotalPages"/>.
    /// </summary>
    private int _totalPages;

    /// <summary>
    /// Backing field for property <see cref="TotalResults"/>.
    /// </summary>
    private int _totalResults;

    #endregion

    #region properties   

    /// <summary>
    /// The page number for the search result.
    /// </summary>
    public int Page
    {
        get => _page;

        init
        {
            SetProperty(ref _page, value);
            OnPropertyChanged(nameof(PaginationText));
        }
    }

    /// <summary>
    /// Returns a formatted pagination text. 
    /// </summary>
    public string PaginationText => $"{Page:N0} of {TotalPages:N0}";

    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    public ObservableCollection<IMovieViewModel> Results
    {
        get => _results;
        init => SetProperty(ref _results, value);
    }

    /// <summary>
    /// The total number of search result pages available for this search.
    /// </summary>
    public int TotalPages
    {
        get => _totalPages;
        init => SetProperty(ref _totalPages, value);
    }

    /// <summary>
    /// The total number of movies found for this search. 
    /// </summary>
    public int TotalResults
    {
        get => _totalResults;
        init => SetProperty(ref _totalResults, value);
    }

    #endregion
}

