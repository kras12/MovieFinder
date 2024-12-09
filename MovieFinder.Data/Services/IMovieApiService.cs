using MovieFinder.Data.Models;

namespace MovieFinder.Data.Services;

public interface IMovieApiService
{
    /// <summary>
    /// Searches for movies according to the supplied filters.
    /// </summary>
    /// <param name="filter">The search filters.</param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<MovieSearchResult>> SearchMovies(MovieSearchFilter filter);
}