using MovieFinder.Data.Models;
using MovieFinder.ViewModels;

namespace MovieFinder.Services;

/// <summary>
/// Interface for a factory class to create <see cref="IMovieViewModel"/> instances. 
/// </summary>
public interface IMovieViewModelFactory
{
    /// <summary>
    /// Creates an <see cref="IMovieViewModel"/> instance.
    /// </summary>
    /// <param name="movie">The movie to base the viewmodel on.</param>
    /// <returns><see cref="IMovieViewModel"/></returns>
    IMovieViewModel Create(Movie movie);
}