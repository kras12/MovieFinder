using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder;

public partial class MovieDiscoveryPage : ContentPage
{
    private readonly IMainPageViewModel _viewModel;

    public MovieDiscoveryPage(IMainPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}