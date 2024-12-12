using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieFinder.Data.Configuration;
using MovieFinder.Data.Extensions;
using MovieFinder.Data.Services;
using MovieFinder.Mapping;
using MovieFinder.ViewModels;

namespace MovieFinder
{
    public static class MauiProgram
    {
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
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<HttpClient, HttpClient>();
            builder.Services.AddTransient<IMovieViewModel, MovieViewModel>();
            builder.Services.AddTransient<IMovieCategoryViewModel, MovieCategoryViewModel>();

            builder.Services.AddDataLayerServices();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

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
}
