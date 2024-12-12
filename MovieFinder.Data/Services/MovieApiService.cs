using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieFinder.Data.Configuration;
using MovieFinder.Data.Dto;
using MovieFinder.Data.Models;
using System.Diagnostics;
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
    /// The API endpoint for movie category listing.
    /// </summary>
    private const string MovieCategoryListUrl = "https://api.themoviedb.org/3/genre/movie/list";

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

    /// <summary>
    /// The injected data mapper.
    /// </summary>
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor. 
    /// </summary>
    /// <param name="httpClient">The injected HttpClient.</param>
    /// <param name="configuration">The injected configuration that contains the API token.</param>
    /// <exception cref="Exception"></exception>
    public MovieApiService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
    {
        _apiToken = configuration[ApiServiceConfigurationKeys.ApiTokenKey] ?? throw new Exception("Failed to find the API token");
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
        this._mapper = mapper;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets all movie categories. 
    /// </summary>
    /// <returns>A collection of <see cref="MovieCategory"/>.</returns>
    public async Task<ApiResponse<MovieCategoryCollection>> GetMovieCategories()
    {
        try
        {
            // TODO - Support different languages?
            var response = await _httpClient.GetAsync($"{MovieCategoryListUrl}?language=en");
            return await HandleResponse<MovieCategoryCollection>(response);
        }
        catch (Exception ex)
        {
            // TODO - Logging
            Debug.WriteLine($"{ex.Message}");
            Debug.WriteLine($"{ex.StackTrace}");
            return CreateGeneralApiErrorResponse<MovieCategoryCollection>();
        }
    }

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
            var resultDto = await HandleResponse<MovieSearchResultDto>(response);
            return ConvertResponse<MovieSearchResultDto, MovieSearchResult>(resultDto);
        }
        catch (Exception ex)
        {
            // TODO - Logging
            Debug.WriteLine($"{ex.Message}");
            Debug.WriteLine($"{ex.StackTrace}");
            return CreateGeneralApiErrorResponse<MovieSearchResult>();
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
        stringBuilder.Append($"&include_adult={BooleanToString(filter.IncludeAdult)}");
        stringBuilder.Append($"&language={filter.Language}");
        stringBuilder.Append($"&sort_by={filter.SortBy}");

        if (filter.WithCategory.HasValue)
        {
            stringBuilder.Append($"&with_genres={filter.WithCategory.Value}");
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Converts the API response from type <typeparamref name="TSource"/> to <typeparamref name="TDestination"/>.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destionation type.</typeparam>
    /// <param name="sourceResponse">The source response to convert.</param>
    /// <returns>A new <see cref="ApiResponse{T}"/>.</returns>
    private ApiResponse<TDestination> ConvertResponse<TSource, TDestination>(ApiResponse<TSource> sourceResponse)
        where TSource : class where TDestination : class
    {
        return new ApiResponse<TDestination>()
        {
            ApiError = sourceResponse.ApiError,
            Data = sourceResponse.Data == null ? null : _mapper.Map<TSource, TDestination>(sourceResponse.Data)
        };
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
    /// Creates a general API response of type T. 
    /// </summary>
    /// <typeparam name="T">The type of the response.</typeparam>
    /// <returns><see cref="ApiResponse{T}"/></returns>
    private ApiResponse<T> CreateGeneralApiErrorResponse<T>() where T: class
    {
        return new ApiResponse<T>()
        {
            ApiError = CreateGeneralApiError()
        };
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
        catch (Exception ex)
        {
            // TODO - Logging
            Debug.WriteLine($"{ex.Message}");
            Debug.WriteLine($"{ex.StackTrace}");
            result.ApiError = CreateGeneralApiError();
        }

        return result;
    }

    #endregion
}
