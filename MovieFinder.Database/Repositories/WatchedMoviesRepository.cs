using Microsoft.EntityFrameworkCore;
using MovieFinder.Database.Context;
using MovieFinder.Database.Entities;

namespace MovieFinder.Database.Repositories;

/// <summary>
/// Repository class for watched movies.
/// </summary>
public class WatchedMoviesRepository : IWatchedMoviesRepository
{
    #region Fields

    /// <summary>
    /// Injected database context.
    /// </summary>
    private readonly ApplicationDbContext _context;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">Injected database context.</param>
    public WatchedMoviesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods


    /// <summary>
    /// Adds a movie to the database.
    /// </summary>
    /// <param name="movie">The movie to add.</param>
    /// <returns>The created <see cref="WatchedMovieEntity"/>.</returns>
    public async Task<WatchedMovieEntity> CreateMovieAsync(WatchedMovieEntity movie)
    {
        _context.Add(movie);
        await _context.SaveChangesAsync();
        _context.Entry(movie).State = EntityState.Detached;
        return movie;
    }

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="movie">The movie to delete.</param>
    /// <returns><see cref="Task"/></returns>
    public async Task DeleteMovieAsync(WatchedMovieEntity movie)
    {
        _context.Remove(movie);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="watchedMovieId">The ID of the watched movie to delete.</param>
    /// <returns><see cref="Task"/></returns>
    public Task DeleteMovieAsync(int watchedMovieId)
    {
        return DeleteMovieAsync(new WatchedMovieEntity() { WatchedMovieId = watchedMovieId });
    }

    /// <summary>
    /// Fetches all movies from the database.
    /// </summary>
    /// <returns>A collection of <see cref="WatchedMovieEntity"/>.</returns>
    public async Task<List<WatchedMovieEntity>> GetAllAsync()
    {
        return await _context.WatchedMovies.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Attempts to fetch a movie by the watched movie ID.
    /// </summary>
    /// <param name="watchedMovieId">The ID of the watched movie.</param>
    /// <returns>The found <see cref="WatchedMovieEntity"/> if the operation was successful.</returns>
    public async Task<WatchedMovieEntity?> GetAsync(int watchedMovieId)
    {
        return await _context.WatchedMovies.AsNoTracking().FirstOrDefaultAsync(x => x.WatchedMovieId == watchedMovieId);
    }

    /// <summary>
    /// Attempts to fetch a movie by title.
    /// </summary>
    /// <param name="id">The title of the movie.</param>
    /// <returns>The found <see cref="WatchedMovieEntity"/> if the operation was successful.</returns>
    public async Task<WatchedMovieEntity?> GetAsync(string title)
    {
        return await _context.WatchedMovies.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
    }

    /// <summary>
    /// Checks whether a movie exists in the database.
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <returns>True if the movie exists.</returns>
    public async Task<bool> MovieExists(string title)
    {
        return await _context.WatchedMovies.AnyAsync(x => x.Title == title);
    }

    /// <summary>
    /// Checks whether a movie exists in the database.
    /// </summary>
    /// <param name="watchedMovieId">The ID of the watched movie.</param>
    /// <returns>True if the movie exists.</returns>
    public async Task<bool> MovieExists(int watchedMovieId)
    {
        return await _context.WatchedMovies.AnyAsync(x => x.WatchedMovieId ==  watchedMovieId);
    }

    /// <summary>
    /// Returns the number of movies in the database.
    /// </summary>
    /// <returns>The number of movies in the form of an <see cref="int"/>.</returns>
    public Task<int> MoviesCount()
    {
        return _context.WatchedMovies.CountAsync();
    }

    /// <summary>
    /// Updates a movie in the database.
    /// </summary>
    /// <param name="movie">The movie to update.</param>
    /// <returns>The updated <see cref="WatchedMovieEntity"/>.</returns>
    public async Task<WatchedMovieEntity> Update(WatchedMovieEntity movie)
    {
        _context.Update(movie);
        await _context.SaveChangesAsync();
        _context.Entry(movie).State = EntityState.Detached;
        return movie;
    }

    #endregion
}
