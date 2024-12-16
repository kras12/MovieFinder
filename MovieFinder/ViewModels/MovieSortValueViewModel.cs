using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Data.Sorting;
using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.ViewModels;

/// <summary>
/// A class that represents a movie sort value used by the API.
/// </summary>
public partial class MovieSortValueViewModel : ObservableObject, IMovieSortValueViewModel
{
    #region Fields

    /// <summary>
    /// Backing field for property <see cref="SortType"/>.
    /// </summary>
    private MovieSortType _sortType;

    #endregion

    #region Properties

    /// <summary>
    /// The human readable description for the sort type. 
    /// </summary>
    public string SortDescription => GetSortDescription(SortType);

    /// <summary>
    /// The type of sorting to perform. 
    /// </summary>
    public MovieSortType SortType
    {
        get => _sortType;

        init
        {
            SetProperty(ref _sortType, value);
            OnPropertyChanged(nameof(SortDescription));
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Gets the human readable sort description from the sort type. 
    /// </summary>
    /// <param name="sortType">The sort type. </param>
    /// <returns><see cref="string"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static string GetSortDescription(MovieSortType sortType)
    {
        switch (sortType)
        {
            case MovieSortType.ByPopularityDesc:
                return "Popularity (DESC)";

            case MovieSortType.ByTitleAsc:
                return "Title (ASC)";

            case MovieSortType.ByTitleDesc:
                return "Title (DESC)";

            case MovieSortType.ByVoteDesc:
                return "Vote (DESC)";

            default:
                throw new InvalidOperationException($"Invalid sort type: {sortType}");
        }
    }

    #endregion
}
