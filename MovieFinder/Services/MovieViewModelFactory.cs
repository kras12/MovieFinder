using MovieFinder.Data.Models;
using MovieFinder.ViewModels;

namespace MovieFinder.Services;

/// <summary>
/// Factory class to create <see cref="IMovieViewModel"/> instances. 
/// </summary>
public class MovieViewModelFactory : IMovieViewModelFactory
{
    #region Methods

    /// <summary>
    /// Creates an <see cref="IMovieViewModel"/> instance.
    /// </summary>
    /// <param name="movie">The movie to base the viewmodel on.</param>
    /// <returns><see cref="IMovieViewModel"/></returns>
    public IMovieViewModel Create(Movie movie)
    {
        return new MovieViewModel(movie);
    }

    #endregion
}
