using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.Pages;

/// <summary>
/// A page that shows movies that the user have watched. 
/// </summary>
public partial class WatchedMoviesPage : ContentPage
{
    #region Fields

    /// <summary>
    /// The injected view model.
    /// </summary>
    private readonly IWatchedMoviesPageViewModel _viewModel;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The injected view model.</param>
    public WatchedMoviesPage(IWatchedMoviesPageViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;        
    }

    #endregion

    #region Methods

    /// <inheritdoc/>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.RefreshData();
    }

    #endregion
}