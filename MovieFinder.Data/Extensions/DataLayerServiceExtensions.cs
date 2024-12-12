using MovieFinder.Data.Mapping;
using MovieFinder.Data.Services;

namespace MovieFinder.Data.Extensions;

/// <summary>
/// Extension class that contains a method to register all the services that the data layer needs to function. 
/// </summary>
public static class DataLayerServiceExtensions
{
    #region Methods

    /// <summary>
    /// Registers all the services that the data layer needs to function. 
    /// </summary>
    /// <param name="services">The service collection to use.</param>
    /// <returns>The <see cref="IServiceCollection"/> used.</returns>
    public static IServiceCollection AddDataLayerServices(this IServiceCollection services)
    {
        services.AddTransient<IDataLayerMovieCategoryIdToMovieCategoryConverter, DataLayerMovieCategoryIdToMovieCategoryConverter>();
        services.AddSingleton<IDataLayerMovieCategoryCacheService, MovieCategoryCacheService>();
        services.AddAutoMapper(config => config.AddProfile<DataLayerAutoMapperProfile>());

        return services;
    }

    #endregion
}
