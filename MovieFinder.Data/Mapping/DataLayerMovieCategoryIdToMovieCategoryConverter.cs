using AutoMapper;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;

namespace MovieFinder.Data.Mapping;

/// <summary>
/// A type converter class that converts category IDs of type <see cref="int"/> to <see cref="MovieCategory"/> instances. 
/// It supports both single categories as well as collection of categories. 
/// </summary>
internal class DataLayerMovieCategoryIdToMovieCategoryConverter : IDataLayerMovieCategoryIdToMovieCategoryConverter
{
    #region Fields

    /// <summary>
    /// The injected movie category cache service. 
    /// </summary>
    private readonly IDataLayerMovieCategoryCacheService _movieCategoryCacheService;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieCategoryCacheService">The injected movie category cache service.</param>
    public DataLayerMovieCategoryIdToMovieCategoryConverter(IDataLayerMovieCategoryCacheService movieCategoryCacheService)
    {
        _movieCategoryCacheService = movieCategoryCacheService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Converts a category ID of type <see cref="int"/> to a <see cref="MovieCategory"/> instance.
    /// </summary>
    /// <param name="movieCategoryId">The ID of the category.</param>
    /// <param name="context">The resolution context which is ignored.</param>
    /// <returns><see cref="MovieCategory"/> if the category was found. Throws an exception if not found.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public MovieCategory Convert(int movieCategoryId, ResolutionContext context)
    {
        if (_movieCategoryCacheService.TryGetCategory(movieCategoryId, out var movieCategory))
        {
            return movieCategory;
        }

        throw new InvalidOperationException("Failed to find the movie category");
    }

    /// <summary>
    /// Converts a collection of category IDs of type <see cref="int"/> to a collection of <see cref="MovieCategory"/> instances.
    /// </summary>
    /// <param name="movieCategoryIds">A collection of category IDs to convert.</param>
    /// <param name="context">The resolution context which is ignored.</param>
    /// <returns>A collection of <see cref="MovieCategory"/> if the categories was found. Throws an exception if not found.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<MovieCategory> Convert(List<int> movieCategoryIds, ResolutionContext context)
    {
        List<MovieCategory> result = new();

        foreach (var movieCategoryId in movieCategoryIds)
        {
            result.Add(Convert(movieCategoryId, context));
        }

        return result;
    }

    #endregion
}
