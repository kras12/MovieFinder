using CommunityToolkit.Mvvm.ComponentModel;

namespace MovieFinder.ViewModels;

/// <summary>
/// ViewModel class containing information about a movie category.
/// </summary>
public partial class MovieCategoryViewModel : ObservableObject, IMovieCategoryViewModel
{

    #region Fields

    /// <summary>
    /// Backing field for property <see cref="Id"/>.
    /// </summary>
    private int _id;

    /// <summary>
    /// Backing field for property <see cref="Name"/>.
    /// </summary>
    private string _name = "";

    #endregion

    #region Properties

    /// <summary>
    /// The ID of the category.
    /// </summary>
    public int Id
    {
        get => _id;
        init => SetProperty(ref _id, value);
    }

    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name
    {
        get => _name;
        init => SetProperty(ref _name, value);
    }

    #endregion
}
