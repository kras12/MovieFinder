using Microsoft.Extensions.Configuration;
using MovieFinder.Data.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MovieFinder.Data.Services;

/// <summary>
/// A service used for searching for movies via the TMDB API. 
/// Link: https://developer.themoviedb.org/docs
/// </summary>
public class MovieApiService : IMovieApiService
{
    #region Constants

    /// <summary>
    /// The API endpoint for movie searches.
    /// </summary>
    private const string MovieSearchUrl = "https://api.themoviedb.org/3/discover/movie";

    #endregion

    #region Fields

    /// <summary>
    /// The API token.
    /// </summary>
    private readonly string _apiToken;

    /// <summary>
    /// The injected HttpClient.
    /// </summary>
    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor. 
    /// </summary>
    /// <param name="httpClient">The injected HttpClient.</param>
    /// <param name="configuration">The injected configuration that contains the API token.</param>
    /// <exception cref="Exception"></exception>
    public MovieApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _apiToken = configuration["ApiToken"] ?? throw new Exception("Failed to find the API token");
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Searches for movies according to the supplied filters.
    /// </summary>
    /// <param name="filter">The search filters.</param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    public async Task<ApiResponse<MovieSearchResult>> SearchMovies(MovieSearchFilter filter)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{MovieSearchUrl}{BuildSearchMovieQueryString(filter)}");
            return await HandleResponse<MovieSearchResult>(response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<MovieSearchResult>()
            {
                ApiError = CreateGeneralApiError()
            };
        }
    }

    /// <summary>
    /// Converts a boolean value to a string.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns><see cref="string"/></returns>
    private string BooleanToString(bool value)
    {
        return value.ToString().ToLower();
    }

    /// <summary>
    /// Builds the query filter string for a movie search. 
    /// </summary>
    /// <param name="filter">The filter to use.</param>
    /// <returns><see cref="string"/></returns>
    private string BuildSearchMovieQueryString(MovieSearchFilter filter)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append("?");
        stringBuilder.Append($"include_video={BooleanToString(filter.IncludeVideo)}");
        stringBuilder.Append($"include_adult={BooleanToString(filter.IncludeAdult)}");
        stringBuilder.Append($"language={filter.Language}");
        stringBuilder.Append($"sort_by={filter.SortBy}");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Creates a deserialization <see cref="ApiError"/>.
    /// </summary>
    /// <returns><see cref="ApiError"/></returns>
    private ApiError CreateDeSerializationApiError()
    {
        return new ApiError(errorCode: 0, "Failed to deserialize response content.");
    }

    /// <summary>
    /// Creates a general <see cref="ApiError"/>.
    /// </summary>
    /// <returns><see cref="ApiError"/></returns>
    private ApiError CreateGeneralApiError()
    {
        return new ApiError(errorCode: 0, "An unexpected error occurred while processing your request.");
    }

    /// <summary>
    /// Handles the response from an API request.
    /// </summary>
    /// <typeparam name="T">The type of the data payload for successful responses.</typeparam>
    /// <param name="response">The response to handle.</param>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response) where T : class
    {
        ApiResponse<T> result = new();

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();

                if (data != null)
                {
                    result.Data = data;
                }
                else
                {
                    result.ApiError = CreateDeSerializationApiError();
                }
            }
            else
            {
                result.ApiError = await response.Content.ReadFromJsonAsync<ApiError>() ?? CreateDeSerializationApiError();
            }
        }
        catch (Exception)
        {
            result.ApiError = CreateGeneralApiError();
        }

        return result;
    }

    #endregion
}
