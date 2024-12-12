namespace MovieFinder.ViewModels;

/// <summary>
/// Interface for a viewModel class containing information about a movie category.
/// </summary>
public interface IMovieCategoryViewModel
{
    /// <summary>
    /// The ID of the category.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name { get; init; }
}