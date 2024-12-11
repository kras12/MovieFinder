using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieFinder.Data.Configuration;
using MovieFinder.Data.Services;
using MovieFinder.Mapping;
using MovieFinder.Services;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            ConfigureApiToken(builder.Configuration);

            builder.Services.AddAutoMapper(config => config.AddProfile<AutoMapperProfile>());
            builder.Services.AddSingleton<IMovieApiService, MovieApiService>();
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<IMovieViewModelFactory, MovieViewModelFactory>();
            builder.Services.AddSingleton<HttpClient, HttpClient>();
            builder.Services.AddSingleton<IMovieToMovieViewModelConverter, MovieToMovieViewModelConverter>();
            builder.Services.AddTransient<IMovieViewModel, MovieViewModel>();

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
