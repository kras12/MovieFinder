using MovieFinder.ViewModels;

namespace MovieFinder
{
    public partial class MainPage : ContentPage
    {
        private readonly IMainPageViewModel _viewModel;

        public MainPage(IMainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
    }

}
