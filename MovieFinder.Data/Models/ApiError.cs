namespace MovieFinder.Data.Models;

/// <summary>
/// Contains API errors.
/// </summary>
public class ApiError
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="errorMessage">The error message.</param>
    
    public ApiError(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    #endregion

    /// <summary>
    /// The error code.
    /// </summary>
    /// <remarks>Error codes can be found at: https://developer.themoviedb.org/docs/errors</remarks>
    public int ErrorCode { get; init; }

    /// <summary>
    /// The error message.
    /// </summary>
    public string ErrorMessage { get; init; } = "";
}

