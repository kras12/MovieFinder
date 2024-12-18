using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Data.Helpers;
using MovieFinder.ViewModels.Interfaces;

namespace MovieFinder.ViewModels;

/// <summary>
/// Represents a movie image.
/// </summary>
public partial class MovieImageViewModel : ObservableObject, IMovieImageViewModel
{
    #region Fields

    /// <summary>
    /// Backing field of property <see cref="AspectRatio"/>.
    /// </summary>
    private double _aspectRatio;

    /// <summary>
    /// Backing field of property <see cref="FilePath"/>.
    /// </summary>
    private string _filePath = "";

    /// <summary>
    /// Backing field of property <see cref="Height"/>.
    /// </summary>
    private int _height;

    /// <summary>
    /// Backing field of property <see cref="LanguageCode"/>.
    /// </summary>
    private string? _languageCode;

    /// <summary>
    /// Backing field of property <see cref="VoteAverage"/>.
    /// </summary>
    private double _voteAverage;

    /// <summary>
    /// Backing field of property <see cref="VoteCount"/>.
    /// </summary>
    private int _voteCount;

    /// <summary>
    /// Backing field of property <see cref="Width"/>.
    /// </summary>
    private int _width;

    #endregion

    #region Properties

    /// <summary>
    /// The aspect ratio of the image.
    /// </summary>
    public double AspectRatio
    {
        get => _aspectRatio;
        set => SetProperty(ref _aspectRatio, value);
    }

    /// <summary>
    /// The relative file path of the image.
    /// </summary>
    public string FilePath
    {
        get => MovieImageHelper.ConstructImagePath(_filePath);
        set => SetProperty(ref _filePath, value);
    }

    /// <summary>
    /// The height of the image.
    /// </summary>
    public int Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    /// <summary>
    /// The language code for the image. 
    /// </summary>
    public string? LanguageCode
    {
        get => _languageCode;
        set => SetProperty(ref _languageCode, value);
    }

    /// <summary>
    /// The average vote for the image.
    /// </summary>
    public double VoteAverage
    {
        get => _voteAverage;
        set => SetProperty(ref _voteAverage, value);
    }

    /// <summary>
    /// The number of votes for the image.
    /// </summary>
    public int VoteCount
    {
        get => _voteCount;
        set => SetProperty(ref _voteCount, value);
    }

    /// <summary>
    /// The width of the image. 
    /// </summary>
    public int Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    #endregion
}
