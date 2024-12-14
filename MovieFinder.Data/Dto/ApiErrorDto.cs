using System.Text.Json.Serialization;

namespace MovieFinder.Data.Dto;

/// <summary>
/// Contains API errors.
/// </summary>
public class ApiErrorDto
{
    /// <summary>
    /// The error code.
    /// </summary>
    /// <remarks>Error codes can be found at: https://developer.themoviedb.org/docs/errors</remarks>
    [JsonPropertyName("status_code")]
    public int ErrorCode { get; init; }

    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName("status_message")]
    public string ErrorMessage { get; init; } = "";

    /// <summary>
    /// True if the request was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }
}

