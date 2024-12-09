namespace MovieFinder.Data.Models;

/// <summary>
/// Encapsulates all possible returns from the API. 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T> where T : class
{
    #region Properties

    /// <summary>
    /// Contains the error object on unsuccessful requests.
    /// </summary>
    public ApiError? ApiError { get; set; }

    /// <summary>
    /// Contains the data payload on successful requests.
    /// </summary>    
    public T? Data { get; set; }

    /// <summary>
    /// Returns true if the request was successful. 
    /// </summary>
    public bool IsSuccess => ApiError == null;

    #endregion
}
