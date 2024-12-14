using AutoMapper;
using MovieFinder.Data.Dto;
using MovieFinder.Data.Models;

namespace MovieFinder.Data.Mapping;

/// <summary>
/// The data layer Auto Mapper profile.
/// </summary>
public class DataLayerAutoMapperProfile : Profile
{
    #region Constructors
    
    /// <summary>
    /// Constructor that creates mapppings. 
    /// </summary>
    public DataLayerAutoMapperProfile()
    {
        CreateMap<MovieDto, Movie>()
            .ForMember(dest => dest.Categories, opt => opt.ConvertUsing<IDataLayerMovieCategoryIdToMovieCategoryConverter, List<int>>(src => src.CategoryIds));

        CreateMap<MovieSearchResultDto, MovieSearchResult>();
        CreateMap<ApiErrorDto, ApiError>();
    }

    #endregion
}
