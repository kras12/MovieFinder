using MovieFinder.Data.Sorting;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a class that represents a movie sort value used by the API.
/// </summary>
public interface IMovieSortValueViewModel
{
    /// <summary>
    /// The human readable description for the sort type. 
    /// </summary>
    public string SortDescription { get; }

    /// <summary>
    /// The type of sorting to perform. 
    /// </summary>
    public MovieSortType SortType { get; init; }
}