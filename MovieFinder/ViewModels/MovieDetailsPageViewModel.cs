using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Services;
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
    /// The injected mapper. 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The injected movie API service. 
    /// </summary>
    private readonly IMovieApiService _movieApiService;

    /// <summary>
    /// Backing field for property <see cref="IsBusy"/>.
    /// </summary>
    private bool _isBusy;

    /// <summary>
    /// Backing field for property <see cref="Movie"/>.
    /// </summary>
    private IMovieViewModel _movie = default!;

    /// <summary>
    /// Backing field for property <see cref="MovieImages"/>.
    /// </summary>
    private IMovieImageSearchResultViewModel _movieImages;

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

    #region Constructors

    /// <summary>
    /// Constructor. 
    /// </summary>
    /// <param name="movieApiService">The injected movie API service.</param>
    /// <param name="mapper">The injected mapper.</param>
    public MovieDetailsPageViewModel(IMovieApiService movieApiService, IMapper mapper)
    {
        _movieApiService = movieApiService;
        _mapper = mapper;        
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns true while the view model is busy. 
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        private set => SetProperty(ref _isBusy, value);
    }

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

    /// <summary>
    /// Contains the result from a movie search.
    /// </summary>
    public IMovieImageSearchResultViewModel MovieImages
    {
        get => _movieImages;

        private set
        {
            SetProperty(ref _movieImages, value);
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
            GetMovieImages();
            return;
        }

        throw new InvalidOperationException("Failed to apply query attribute");
    }

    /// <summary>
    /// Fetches all images for the movie. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task GetMovieImages()
    {
        try
        {
            IsBusy = true;
            var response = await _movieApiService.GetMovieImages(Movie.Id);

            if (response.IsSuccess && response.Data != null)
            {
                MovieImages = _mapper.Map<IMovieImageSearchResultViewModel>(response.Data);
            }
            else
            {
                // TODO - Implement error handling
                Debug.WriteLine($"Error when searching for movies: {response.ApiError?.ErrorMessage ?? ""}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when searching for movies: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}
