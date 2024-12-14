using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.Pages;

public partial class MovieDiscoveryPage : ContentPage
{
    private readonly IMovieDiscoveryViewModel _viewModel;

    public MovieDiscoveryPage(IMovieDiscoveryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}