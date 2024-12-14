using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder;

/// <summary>
/// A component that lists movies. 
/// </summary>
public partial class MovieListingView : ContentView
{
    #region Fields

    /// <summary>
    /// Bindable property to faciliate binding of a <see cref="IMainPageViewModel"/> object to a <see cref="MovieListingView"/> class instance. 
    /// </summary>
	public static readonly BindableProperty ViewModelProperty =
		BindableProperty.Create(nameof(ViewModel), typeof(IMainPageViewModel), typeof(MovieListingView), default(IMainPageViewModel), propertyChanged: OnViewModelChanged);

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public MovieListingView()
    {
        InitializeComponent();
    }

    #endregion

    #region Properties

    /// <summary>
    /// The view model to use. 
    /// </summary>
    public IMainPageViewModel ViewModel
    {
        get => (IMainPageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    #endregion  

    #region Methods

    /// <summary>
    /// Event handler that sets the new <see cref="IMainPageViewModel"/> as the binding context for a <see cref="MovieListingView"/>.
    /// </summary>
    /// <param name="bindable">The class instance that received a new binding value.</param>
    /// <param name="oldValue">The old viewmodel binding value.</param>
    /// <param name="newValue">The new viewmodel binding value.</param>
    private static void OnViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MovieListingView view && newValue is IMainPageViewModel viewModel)
        {
            view.BindingContext = viewModel;
        }
    }

    #endregion
}