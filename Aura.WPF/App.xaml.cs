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
using Aura.Data.Seed;

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
                services.AddSingleton<TransactionViewModel>();
                services.AddSingleton<ProductManagementViewModel>();

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
        // 1. Start the host
        await _host.StartAsync();

        // 2. RUN DATABASE SETUP AND SEEDING
        using (var scope = _host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<AuraDbContext>();

            // Ensure migrations are applied (creates tables)
            dbContext.Database.Migrate();

            // SEED THE DATA (Fills the tables with initial products)
            await DataSeeder.SeedAsync(dbContext); // <-- NEW LINE

            // The application may pause briefly here as data is written, but it's minimal.
        }

        // 3. Resolve and show the main window
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

