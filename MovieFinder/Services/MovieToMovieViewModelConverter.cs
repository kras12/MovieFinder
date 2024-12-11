using AutoMapper;
using MovieFinder.Data.Models;
using MovieFinder.ViewModels;

namespace MovieFinder.Services;

/// <summary>
/// A type converter class to convert a <see cref="Movie"/> instance to an <see cref="IMovieViewModel"/> instance.
/// </summary>
public class MovieToMovieViewModelConverter : ITypeConverter<Movie, IMovieViewModel>, IMovieToMovieViewModelConverter
{
    #region Fields

    /// <summary>
    /// The injected factory class.
    /// </summary>
    private readonly IMovieViewModelFactory _movieViewModelFactory;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieViewModelFactory">The injected factory class.</param>
    public MovieToMovieViewModelConverter(IMovieViewModelFactory movieViewModelFactory)
    {
        _movieViewModelFactory = movieViewModelFactory;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Converts a <see cref="Movie"/> instance to an <see cref="IMovieViewModel"/> instance.
    /// </summary>
    /// <param name="source">The movie to convert.</param>
    /// <param name="destination">The destination view model, which is ignored.</param>
    /// <param name="context">The resolution context, which is ignored.</param>
    /// <returns><see cref="IMovieViewModel"/></returns>
    public IMovieViewModel Convert(Movie source, IMovieViewModel destination, ResolutionContext context)
    {
        return _movieViewModelFactory.Create(source);
    }

    #endregion
}
