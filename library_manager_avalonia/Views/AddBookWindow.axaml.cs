using library_manager_avalonia.ViewModels;
using library_manager_avalonia.Core.Windows;
using library_manager_avalonia.Database;
using library_manager_avalonia.Models;
using Avalonia.Interactivity;
using library_manager_avalonia.Core.Enums;

namespace library_manager_avalonia;

public partial class AddBookWindow : ReactiveWindowBase<AddBookViewModel>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Category> _categoryRepository;

    public AddBookWindow()
    {
        InitializeComponent();
    }

    public AddBookWindow(AddBookViewModel viewModel, IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<Category> categoryRepository)
    {
        DataContext = viewModel;

        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;

        InitializeComponent();
    }

    private async void OnAddBookButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddBookViewModel viewModel)
        {
            await _bookRepository.AddAsync(new()
            {
                Title = viewModel.Title,
                PublicationYear = viewModel.PublicationYear
            });
        }

        SetResultAndClose(WindowResult.OK);
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        SetResultAndClose(WindowResult.Closed);
    }
}