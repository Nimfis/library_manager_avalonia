using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using library_manager_avalonia.Enums;
using library_manager_avalonia.Models;
using library_manager_avalonia.Helpers;
using library_manager_avalonia.Database;
using library_manager_avalonia.ViewModels;
using library_manager_avalonia.Core.Enums;
using library_manager_avalonia.Core.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace library_manager_avalonia.Views
{
    public partial class MainWindow : ReactiveWindowBase<MainWindowViewModel>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Book> _bookRepository;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IServiceProvider serviceProvider, IRepository<Category> categoryRepository, IRepository<Author> authorRepository, IRepository<Book> bookRepository)
        {
            _serviceProvider = serviceProvider;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;

            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }

        private async void TabControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl?.SelectedItem is TabItem selectedTab)
            {
                if (selectedTab.Tag is MainWindowTabs tabType)
                {
                    switch (tabType)
                    {
                        case MainWindowTabs.Books:
                            await RefreshBooks();
                            break;
                        case MainWindowTabs.Categories:
                            await RefreshCategories();
                            break;
                        case MainWindowTabs.Authors:
                            await RefreshAuthors();
                            break;
                        case MainWindowTabs.Rentals:
                            break;
                    }
                }
            }
        }

        private void OnAddBookButtonClick(object? sender, RoutedEventArgs e)
        {
            var addBookWindow = _serviceProvider.GetRequiredService<AddBookWindow>();

            addBookWindow.Closed += async (s, args) =>
            {
                var window = s as AddBookWindow;
                if (window != null && window.Result == WindowResult.OK)
                {
                    var viewModel = window.DataContext as AddBookViewModel;
                    if (viewModel != null)
                    {
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie dodano książkę \"{viewModel.Title}\"", this);
                        await RefreshBooks();
                    }
                }
            };

            addBookWindow.ShowDialog(this);
        }

        private async void OnRemoveBookButtonClick(object? sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null && viewModel.SelectedBook != null)
            {
                var confirm = await MsgBox.ConfirmAsync("Usuwanie książki", $"Czy na pewno chcesz usunąć książkę \"{viewModel.SelectedBook.Title}\"?", this);
                if (confirm)
                {
                    try
                    {
                        _bookRepository.Delete(viewModel.SelectedBook.Id);
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie usunięto książkę \"{viewModel.SelectedBook.Title}\"", this);
                        await RefreshBooks();
                    }
                    catch (Exception ex)
                    {
                        await MsgBox.ErrorAsync("Błąd", "Wystąpił błąd podczas usuwania książki", this);
                    }
                }
            }
            else
            {
                await MsgBox.ErrorAsync("Błąd", "Nie zaznaczono książki do usunięcia", this);
            }
        }

        private void OnAddCategoryButtonClick(object? sender, RoutedEventArgs e)
        {
            var addCategoryWindow = _serviceProvider.GetRequiredService<AddCategoryWindow>();
            addCategoryWindow.Closed += async (s, args) =>
            {
                var window = s as AddCategoryWindow;
                if (window != null && window.Result == WindowResult.OK)
                {
                    var viewModel = window.DataContext as AddCategoryViewModel;
                    if (viewModel != null)
                    {
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie dodano kategorię \"{viewModel.CategoryName}\"", this);
                        await RefreshCategories();
                    }
                }
            };

            addCategoryWindow.ShowDialog(this);
        }

        private async void OnRemoveCategoryButtonClick(object? sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null && viewModel.SelectedCategory != null)
            {
                var confirm = await MsgBox.ConfirmAsync("Usuwanie kategorii", $"Czy na pewno chcesz usunąć kategorię \"{viewModel.SelectedCategory.Name}\"?", this);
                if (confirm)
                {
                    try
                    {
                        _categoryRepository.Delete(viewModel.SelectedCategory.Id);
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie usunięto kategorię \"{viewModel.SelectedCategory.Name}\"", this);
                        await RefreshCategories();
                    }
                    catch (Exception ex)
                    {
                        await MsgBox.ErrorAsync("Błąd", "Wystąpił błąd podczas usuwania kategorii", this);
                    }
                }
            }
            else
            {
                await MsgBox.ErrorAsync("Błąd", "Nie zaznaczono kategorii do usunięcia", this);
            }
        }

        private void OnCategoryCellEditEnded(object? sender, DataGridCellEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    var editedCategory = e.Row.DataContext as CategoryViewModel;
                    if (editedCategory != null)
                    {
                        var viewModel = DataContext as MainWindowViewModel;
                        if (viewModel != null)
                        {
                            _categoryRepository.Update(new Category
                            {
                                Id = editedCategory.Id,
                                Name = editedCategory.Name
                            });
                        }
                    }
                }
            }
        }

        private void OnAddAuthorButtonClick(object? sender, RoutedEventArgs e)
        {
            var addAuthorWindow = _serviceProvider.GetRequiredService<AddAuthorWindow>();
            addAuthorWindow.Closed += async (s, args) =>
            {
                var window = s as AddAuthorWindow;
                if (window != null && window.Result == WindowResult.OK)
                {
                    var viewModel = window.DataContext as AddAuthorViewModel;
                    if (viewModel != null)
                    {
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie dodano autora \"{viewModel.FirstName} {viewModel.LastName}\"", this);
                        await RefreshAuthors();
                    }
                }
            };

            addAuthorWindow.ShowDialog(this);
        }

        private async void OnRemoveAuthorButtonClick(object? sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null && viewModel.SelectedAuthor != null)
            {
                var confirm = await MsgBox.ConfirmAsync("Usuwanie autora", $"Czy na pewno chcesz usunąć autora \"{viewModel.SelectedAuthor.FirstName} {viewModel.SelectedAuthor.LastName}\"?", this);
                if (confirm)
                {
                    try
                    {
                        _authorRepository.Delete(viewModel.SelectedAuthor.Id);
                        await MsgBox.SuccessAsync("Sukces", $"Poprawnie usunięto autora \"{viewModel.SelectedAuthor.FirstName} {viewModel.SelectedAuthor.LastName}\"", this);
                        await RefreshAuthors();
                    }
                    catch (Exception ex)
                    {
                        await MsgBox.ErrorAsync("Błąd", "Wystąpił błąd podczas usuwania autora", this);
                    }
                }
            }
            else
            {
                await MsgBox.ErrorAsync("Błąd", "Nie zaznaczono autora do usunięcia", this);
            }
        }

        private void OnAuthorCellEditEnded(object? sender, DataGridCellEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    var editedAuthor = e.Row.DataContext as AuthorViewModel;
                    if (editedAuthor != null)
                    {
                        var viewModel = DataContext as MainWindowViewModel;
                        if (viewModel != null)
                        {
                            _authorRepository.Update(new Author
                            {
                                Id = editedAuthor.Id,
                                FirstName = editedAuthor.FirstName,
                                LastName = editedAuthor.LastName,
                            });
                        }
                    }
                }
            }
        }

        private async Task RefreshCategories()
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null)
            {
                var categories = await _categoryRepository.GetAllAsync();
                viewModel.LoadCategories(categories);
            }
        }

        private async Task RefreshAuthors()
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null)
            {
                var authors = await _authorRepository.GetAllAsync();
                viewModel.LoadAuthors(authors);
            }
        }

        private async Task RefreshBooks()
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null)
            {
                var books = await _bookRepository.GetAllAsync();
                viewModel.LoadBooks(books);
            }
        }
    }
}