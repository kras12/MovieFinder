using MovieFinder.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace MovieFinder.Data.Services;

/// <summary>
/// Interface for a movie categories cache system.
/// </summary>
public interface IMovieCategoryCacheService
{
    /// <summary>
    /// Attempts to return a matching movie category. 
    /// </summary>
    /// <param name="movieCategoryId">The ID of the movie category to match.</param>
    /// <param name="movieCategory">The matching <see cref="MovieCategory"/> if found.</param>
    /// <returns>True if the operation was successful.</returns>
    bool TryGetCategory(int movieCategoryId, [NotNullWhen(true)] out MovieCategory? movieCategory);
}