using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.Pages;

/// <summary>
/// The page for searching movies with the movie discovery mode. 
/// </summary>
public partial class MovieDiscoveryPage : ContentPage
{
    #region Fields

    /// <summary>
    /// The injected view model. 
    /// </summary>
    private readonly IMovieDiscoveryViewModel _viewModel;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor. 
    /// </summary>
    /// <param name="viewModel">The injected view model. </param>
    public MovieDiscoveryPage(IMovieDiscoveryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
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