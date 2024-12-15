using MovieFinder.Pages;

namespace MovieFinder;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
        Routing.RegisterRoute(nameof(MovieDiscoveryPage), typeof(MovieDiscoveryPage));
        Routing.RegisterRoute(nameof(WatchedMoviesPage), typeof(WatchedMoviesPage));
    }
}
