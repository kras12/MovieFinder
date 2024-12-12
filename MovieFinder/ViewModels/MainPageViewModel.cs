using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Data.Models;
using MovieFinder.Data.Services;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// View model class for the main page. 
/// </summary>
public partial class MainPageViewModel : ObservableObject, IMainPageViewModel
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
    /// A collection of movies fetched from the API. 
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IMovieViewModel> _movies = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="movieApiService">The injected movie API service.</param>
    /// <param name="mapper">The injected mapper.</param>
    public MainPageViewModel(IMovieApiService movieApiService, IMapper mapper)
    {
        _movieApiService = movieApiService;
        _mapper = mapper;
    }

    #endregion

    #region Commands    

    /// <summary>
    /// The command to search movies. 
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [RelayCommand]
    private async Task SearchMovies()
    {
        // TODO - Present filter options in the view. 
        var response = await _movieApiService.SearchMovies(new MovieSearchFilter());

        if (response.IsSuccess && response.Data != null)
        {
            Movies = new ObservableCollection<IMovieViewModel>(_mapper.Map<List<IMovieViewModel>>(response.Data.Results));
        }
        else
        {
            // TODO - Implement error handling
        }
    }

    #endregion
}
