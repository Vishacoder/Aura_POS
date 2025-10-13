using Aura.Core.Interfaces;
using Aura.Core.ViewModels;
using Aura.Data.Repositories;
using Aura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Aura.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        // 1. Create the Host Builder
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // 2. Register Database Context (AuraDbContext)
                // Scoped: Creates one instance per request/unit of work.
                services.AddDbContext<AuraDbContext>(ServiceLifetime.Scoped);

                // 3. Register Repositories and Unit of Work
                // Transient: Creates a new object every time it's requested.
                // We map the interface (IUnitOfWork) to its concrete implementation (UnitOfWork).
                services.AddTransient<IUnitOfWork, UnitOfWork>();

                // 4. Register ViewModels (We will add these next)
                services.AddSingleton<MainWindowViewModel>(); 

                // 5. Register Views
                services.AddSingleton<MainWindow>();

                // 6. Ensure the database exists and applies any pending migrations
                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AuraDbContext>();
                    dbContext.Database.Migrate();
                }

            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        // Resolve the main window from the service provider
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
        base.OnExit(e);
    }
}

