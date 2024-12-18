using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for an image search result. 
/// </summary>
public interface IMovieImageSearchResultViewModel
{
    /// <summary>
    /// A collection of images from the video. 
    /// </summary>
    ObservableCollection<MovieImageViewModel> Backdrops { get; }

    /// <summary>
    /// The ID of the movie.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// A collection of posters to display. 
    /// </summary>
    public ObservableCollection<MovieImageViewModel> ImagesForDisplay { get; }

    /// <summary>
    /// A collection of movie logos.
    /// </summary>
    public ObservableCollection<MovieImageViewModel> Logos { get; }

    /// <summary>
    /// A collection of movie posters.
    /// </summary>
    public ObservableCollection<MovieImageViewModel> Posters { get; }
}
