using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.ViewModels.Interfaces;
using System.Diagnostics;

namespace MovieFinder.ViewModels;

/// <summary>
/// View model class for showing moving details.
/// </summary>
public partial class MovieDetailsPageViewModel : ObservableObject, IMovieDetailsPageViewModel, IQueryAttributable
{
    #region Fields

    /// <summary>
    /// Backing field for property <see cref="Movie"/>.
    /// </summary>
    private IMovieViewModel _movie = default!;

    #endregion

    #region Commands

    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task NavigateBack()
    {
        try
        {
            await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation failed: {ex.Message}");
        }
    }   

    #endregion

    #region Properties

    /// <summary>
    /// The movie to show details for. 
    /// </summary>
    public IMovieViewModel Movie
    {
        get
        {
            return _movie;
        }

        private set 
        {
            SetProperty(ref _movie, value);
        }
            
    }

    #endregion

    #region Methods

    /// <summary>
    /// Applies query attributes to properties.
    /// </summary>
    /// <remarks>For some reason the <see cref="QueryPropertyAttribute"/> does not work, so this method must be used.</remarks>
    /// <param name="queryAttribute">The query attribute.</param>
    /// <exception cref="InvalidOperationException"></exception>
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> queryAttribute)
    {
        if (queryAttribute.ContainsKey(nameof(IMovieDetailsPageViewModel.Movie)) && queryAttribute[nameof(IMovieDetailsPageViewModel.Movie)] is IMovieViewModel movie)
        {
            Movie = movie;
            return;
        }

        throw new InvalidOperationException("Failed to apply query attribute");
    }

    #endregion
}
