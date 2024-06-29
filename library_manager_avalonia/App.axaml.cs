using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using library_manager_avalonia.Database;
using library_manager_avalonia.Services;
using library_manager_avalonia.ViewModels;
using library_manager_avalonia.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace library_manager_avalonia
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.DataContext = new MainWindowViewModel();
                desktop.MainWindow = mainWindow;

                InitializeDatabase();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializeDatabase()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=True;"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddTransient<MainWindow>();
            services.AddTransient<AddBookWindow>();
        }
    }
}