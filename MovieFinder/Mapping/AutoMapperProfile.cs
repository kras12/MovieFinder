using AutoMapper;
using MovieFinder.Data.Models;
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
            .ConstructUsingServiceLocator()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath))
            .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.PosterPath))
            .ForMember(dest => dest.OriginalLanguage, opt => opt.MapFrom(src => src.OriginalLanguage))
            .ForMember(dest => dest.OriginalTitle, opt => opt.MapFrom(src => src.OriginalTitle))
            .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.Overview))
            .ForMember(dest => dest.Popularity, opt => opt.MapFrom(src => src.Popularity))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video))
            .ForMember(dest => dest.VoteAverage, opt => opt.MapFrom(src => src.VoteAverage))
            .ForMember(dest => dest.VoteCount, opt => opt.MapFrom(src => src.VoteCount));

        CreateMap<MovieCategory, IMovieCategoryViewModel>()
            .ConstructUsingServiceLocator()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<IMovieSearchFilterViewModel, MovieSearchFilter>()
            .ForMember(dest => dest.WithCategory, opt => opt.MapFrom(src => src.WithCategory != null ? src.WithCategory.Id : (int?)null));
    }
}
