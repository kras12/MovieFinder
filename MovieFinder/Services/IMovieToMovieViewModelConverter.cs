using AutoMapper;
using MovieFinder.Data.Models;
using MovieFinder.ViewModels;

namespace MovieFinder.Services;

/// <summary>
/// Interface for a type converter class to convert a <see cref="Movie"/> instance to an <see cref="IMovieViewModel"/> instance.
/// </summary>
public interface IMovieToMovieViewModelConverter : ITypeConverter<Movie, IMovieViewModel>
{

}