using MovieFinder.Database.Entities;

namespace MovieFinder.Database.Repositories;

/// <summary>
/// Interface for repository class for watched movies.
/// </summary>
public interface IWatchedMoviesRepository
{
    /// <summary>
    /// Adds a movie to the database.
    /// </summary>
    /// <param name="movie">The movie to add.</param>
    /// <returns>The created <see cref="WatchedMovieEntity"/>.</returns>
    Task<WatchedMovieEntity> CreateMovieAsync(WatchedMovieEntity movie);

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="movie">The movie to delete.</param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteMovieAsync(WatchedMovieEntity movie);

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="movieId">The ID of the movie to delete.</param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteMovieAsync(int movieId);

    /// <summary>
    /// Fetches all movies from the database.
    /// </summary>
    /// <returns>A collection of <see cref="WatchedMovieEntity"/>.</returns>
    Task<List<WatchedMovieEntity>> GetAllAsync();

    /// <summary>
    /// Attempts to fetch a movie by ID.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The found <see cref="WatchedMovieEntity"/> if the operation was successful.</returns>
    Task<WatchedMovieEntity?> GetAsync(int id);

    /// <summary>
    /// Checks whether a movie exists in the database.
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <returns>True if the movie exists.</returns>
    Task<bool> MovieExists(string title);

    /// <summary>
    /// Returns the number of movies in the database.
    /// </summary>
    /// <returns>The number of movies in the form of an <see cref="int"/>.</returns>
    Task<int> MoviesCount();

    /// <summary>
    /// Updates a movie in the database.
    /// </summary>
    /// <param name="movie">The movie to update.</param>
    /// <returns>The updated <see cref="WatchedMovieEntity"/>.</returns>
    Task<WatchedMovieEntity> Update(WatchedMovieEntity movie);
}
