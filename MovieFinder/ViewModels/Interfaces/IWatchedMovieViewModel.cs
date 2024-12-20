﻿namespace MovieFinder.ViewModels.Interfaces;

/// <summary>
/// Interface for a viewmodel for a watched movie. 
/// </summary>
public interface IWatchedMovieViewModel
{
    /// <summary>
    /// The ID of the API movie. 
    /// </summary>
    public int ApiMovieId { get; set; }

    /// <summary>
    /// Path to a poster image for the movie.
    /// </summary>
    string? PosterPath { get; init; }

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
    public int Vote { get; set; }

    /// <summary>
    /// The ID of the watched movie. 
    /// </summary>
    public int WatchedMovieId { get; set; }
}
