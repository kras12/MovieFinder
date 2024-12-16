using MovieFinder.Pages;

namespace MovieFinder;

/// <summary>
/// Partial app shell class.
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
        Routing.RegisterRoute(nameof(MovieDiscoveryPage), typeof(MovieDiscoveryPage));
        Routing.RegisterRoute(nameof(WatchedMoviesPage), typeof(WatchedMoviesPage));
    }
}
