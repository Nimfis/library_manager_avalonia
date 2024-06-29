using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using library_manager_avalonia.Database;
using library_manager_avalonia.ViewModels;
using library_manager_avalonia.Views;

namespace library_manager_avalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };

                InitializeDatabase();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializeDatabase()
        {
            using (var context = new LibraryDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}