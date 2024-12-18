using MovieFinder.Data.Models;

namespace MovieFinder.Data.Services;

/// <summary>
/// Interface for a service used for searching for movies via the TMDB API. 
/// Link: https://developer.themoviedb.org/docs
/// </summary>
public interface IMovieApiService
{
    /// <summary>
    /// The max page number request allowed by the api when searching for movies. 
    /// </summary>
    public int MaxNumberForPageRequest { get; }

    /// <summary>
    /// Gets all movie categories. 
    /// </summary>
    /// <returns>A collection of <see cref="MovieCategory"/>.</returns>
    public Task<ApiResponse<MovieCategoryCollection>> GetMovieCategories();

    /// <summary>
    /// Fetches the images for a movie.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <returns><see cref="MovieImageSearchResult"/></returns>
    public Task<ApiResponse<MovieImageSearchResult>> GetMovieImages(int movieId);

    /// <summary>
    /// Searches for movies according to the supplied filters.
    /// </summary>
    /// <param name="filter">The search filters.</param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    public Task<ApiResponse<MovieSearchResult>> SearchMovies(MovieSearchFilter filter);
}