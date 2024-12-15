using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.Pages;

/// <summary>
/// A page that shows movies that the user have watched. 
/// </summary>
public partial class WatchedMoviesPage : ContentPage
{	
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The injected view model</param>
    public WatchedMoviesPage(IWatchedMoviesPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}