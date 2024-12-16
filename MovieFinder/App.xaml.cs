using Microsoft.EntityFrameworkCore;
using MovieFinder.Database.Context;

namespace MovieFinder;

/// <summary>
/// Partial application class. 
/// </summary>
public partial class App : Application
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="serviceProvider">The injected service provider.</param>
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }

    #endregion

    #region Methods
    
    /// <summary>
    /// Override for CreateWindow method.
    /// </summary>
    /// <param name="activationState">The activation state.</param>
    /// <returns></returns>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    #endregion
}