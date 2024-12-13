using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for a mview model that Contains the search result from a movie search.
/// </summary>
public interface IMovieSearchResultViewModel
{
    /// <summary>
    /// Returns true if there exists next pages with the current search parameters.
    /// </summary>
    bool HaveNextPages { get; }

    /// <summary>
    /// Returns true if there exists previous pages with the current search parameters.
    /// </summary>
    bool HavePreviousPages { get; }

    /// <summary>
    /// The page number for the search result.
    /// </summary>
    int Page { get; init; }

    /// <summary>
    /// Returns a formatted pagination text. 
    /// </summary>
    public string PaginationText { get; }

    /// <summary>
    /// Contains a collection of found movies. 
    /// </summary>
    ObservableCollection<IMovieViewModel> Results { get; init; }

    /// <summary>
    /// The total number of search result pages available for this search.
    /// </summary>
    int TotalPages { get; init; }

    /// <summary>
    /// The total number of movies found for this search. 
    /// </summary>
    int TotalResults { get; init; }
}