﻿using MovieFinder.Data.Models;

namespace MovieFinder.Data.Services;

/// <summary>
/// Interface for a service used for searching for movies via the TMDB API. 
/// Link: https://developer.themoviedb.org/docs
/// </summary>
public interface IMovieApiService
{
    /// <summary>
    /// Gets all movie categories. 
    /// </summary>
    /// <returns>A collection of <see cref="MovieCategory"/>.</returns>
    public Task<ApiResponse<MovieCategoryCollection>> GetMovieCategories();

    /// <summary>
    /// Searches for movies according to the supplied filters.
    /// </summary>
    /// <param name="filter">The search filters.</param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    Task<ApiResponse<MovieSearchResult>> SearchMovies(MovieSearchFilter filter);
}