using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieFinder.Data.Configuration;
using MovieFinder.Data.Services;

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

            builder.Services.AddSingleton<IMovieApiService, MovieApiService>();

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
