using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder;

/// <summary>
/// The movie details page.
/// </summary>
public partial class MovieDetailsPage : ContentPage
{
    #region Fields

    /// <summary>
    /// The injected <see cref="IMovieDetailsPageViewModel"/>.
    /// </summary>
    private readonly IMovieDetailsPageViewModel _movieDetailsPageViewModel;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor. 
    /// </summary>
    /// <param name="movieDetailsPageViewModel">The injected <see cref="IMovieDetailsPageViewModel"/>.</param>
    public MovieDetailsPage(IMovieDetailsPageViewModel movieDetailsPageViewModel)
    {
        InitializeComponent();
        _movieDetailsPageViewModel = movieDetailsPageViewModel;
        BindingContext = _movieDetailsPageViewModel;
    }

    #endregion
}