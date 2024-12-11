using AutoMapper;
using MovieFinder.Data.Models;
using MovieFinder.Services;
using MovieFinder.ViewModels;

namespace MovieFinder.Mapping;

/// <summary>
/// The main Auto Mapper profile.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Constructor that creates mapppings. 
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<Movie, IMovieViewModel>()
            .ConvertUsing<IMovieToMovieViewModelConverter>();
    }
}
