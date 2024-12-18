using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace MovieFinder.ViewModels
{
    /// <summary>
    /// Represents an image search result. 
    /// </summary>
    public partial class MovieImageSearchResultViewModel : ObservableObject, IMovieImageSearchResultViewModel
    {
        #region Constants

        /// <summary>
        /// The maximum number of posters to display.
        /// </summary> 
        private const int MaxPosters = 10; 

        #endregion

        #region Fields

        /// <summary>
        /// Backing field of property <see cref="Backdrops"/>.
        /// </summary>
        private ObservableCollection<MovieImageViewModel> _backdrops = [];

        /// <summary>
        /// Backing field of property <see cref="Id"/>.
        /// </summary>
        private int _id;

        /// <summary>
        /// Backing field of property <see cref="Logos"/>.
        /// </summary>
        private ObservableCollection<MovieImageViewModel> _logos = [];

        /// <summary>
        /// Backing field of property <see cref="Posters"/>.
        /// </summary>
        private ObservableCollection<MovieImageViewModel> _posters = [];

        #endregion

        #region Properties

        /// <summary>
        /// A collection of images from the video. 
        /// </summary>
        public ObservableCollection<MovieImageViewModel> Backdrops
        {
            get => _backdrops;
            set => SetProperty(ref _backdrops, value);
        }

        /// <summary>
        /// The ID of the movie.
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// A collection of posters to display. 
        /// </summary>
        public ObservableCollection<MovieImageViewModel> ImagesForDisplay => new(Backdrops.Take(MaxPosters));

        /// <summary>
        /// A collection of movie logos.
        /// </summary>
        public ObservableCollection<MovieImageViewModel> Logos
        {
            get => _logos;
            set => SetProperty(ref _logos, value);
        }

        /// <summary>
        /// A collection of movie posters.
        /// </summary>
        public ObservableCollection<MovieImageViewModel> Posters
        {
            get => _posters;

            set
            {
                SetProperty(ref _posters, value);
                OnPropertyChanged(nameof(ImagesForDisplay));
            }
        }

        #endregion
    }
}
