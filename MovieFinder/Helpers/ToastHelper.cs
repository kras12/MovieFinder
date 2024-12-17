using CommunityToolkit.Maui.Alerts;

namespace MovieFinder.Helpers;

/// <summary>
/// Takes care of creating Toast messages to the user.
/// </summary>
public static class ToastHelper
{
    #region Methods
    
    /// <summary>
    /// Shows a Toast message to the user.
    /// </summary>
    /// <param name="message">The message to show.</param>
    /// <returns></returns>
    public static Task ShowToast(string message)
    {
        return Toast.Make(message).Show();
    }

    #endregion
}
