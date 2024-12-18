using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieFinder.Data.Configuration;
using MovieFinder.Data.Extensions;
using MovieFinder.Data.Services;
using MovieFinder.Database.Context;
using MovieFinder.Database.Repositories;
using MovieFinder.Mapping;
using MovieFinder.Shared.Helpers;
using MovieFinder.ViewModels;
using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder;

/// <summary>
/// The main Maui program class. 
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Creates the Maui app and registers all services.
    /// </summary>
    /// <returns><see cref="MauiApp"/>.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureApiToken(builder.Configuration);

        builder.Services.AddAutoMapper(config => config.AddProfile<AutoMapperProfile>());
        builder.Services.AddSingleton<IMovieApiService, MovieApiService>();
        builder.Services.AddSingleton<IMovieDiscoveryViewModel, MovieDiscoveryViewModel>();
        builder.Services.AddSingleton<HttpClient, HttpClient>();
        builder.Services.AddSingleton<IMovieCategoryCacheService, MovieCategoryCacheService>();
        builder.Services.AddSingleton<IWatchedMoviesRepository, WatchedMoviesRepository>();
        builder.Services.AddTransient<IMovieViewModel, MovieViewModel>();
        builder.Services.AddTransient<IMovieCategoryViewModel, MovieCategoryViewModel>();
        builder.Services.AddTransient<IMovieDetailsPageViewModel, MovieDetailsPageViewModel>();
        builder.Services.AddTransient<IMovieSearchFilterViewModel, MovieSearchFilterViewModel>();
        builder.Services.AddTransient<IMovieSearchResultViewModel, MovieSearchResultViewModel>();
        builder.Services.AddTransient<IWatchedMovieViewModel, WatchedMovieViewModel>();
        builder.Services.AddTransient<IWatchedMoviesPageViewModel, WatchedMoviesPageViewModel>();
        builder.Services.AddTransient<IMovieSortValueViewModel, MovieSortValueViewModel>();
        builder.Services.AddTransient<IMovieImageViewModel,  MovieImageViewModel>();
        builder.Services.AddTransient<IMovieImageSearchResultViewModel, MovieImageSearchResultViewModel>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(DatabaseSettingsHelper.DevDbConnectionString);
        });

        builder.Services.AddDataLayerServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    /// <summary>
    /// Configures the API token.
    /// </summary>
    /// <param name="configurationBuilder"></param>
    /// <exception cref="InvalidOperationException">The configuration builder.</exception>
    private static void ConfigureApiToken(IConfigurationBuilder configurationBuilder)
    {
        string apiKey = SecureStorage.GetAsync(ApiServiceConfigurationKeys.ApiTokenKey).GetAwaiter().GetResult() ?? throw new InvalidOperationException("Failed to find the Jmdb API Key"); ;

        var inMemorySettings = new Dictionary<string, string?>()
            {
                { ApiServiceConfigurationKeys.ApiTokenKey, apiKey }
            };

        configurationBuilder.AddInMemoryCollection(inMemorySettings);
    }
}
