namespace MovieFinder.ViewModels.Interfaces;

public interface IWatchedMovieViewModel
{
    /// <summary>
    /// The ID of the movie. 
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// A formatted text to show the user's vote for the movie. 
    /// </summary>
    string UserVoteText { get; }

    /// <summary>
    /// The user's vote for this movie. 
    /// </summary>
    public int Vote {  get; set; }
}
