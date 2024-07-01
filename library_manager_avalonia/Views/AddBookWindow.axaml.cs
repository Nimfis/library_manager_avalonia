using library_manager_avalonia.ViewModels;
using library_manager_avalonia.Core.Windows;
using library_manager_avalonia.Database;
using library_manager_avalonia.Models;
using Avalonia.Interactivity;
using library_manager_avalonia.Core.Enums;
using library_manager_avalonia.Helpers;
using System.Linq;
using System;

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

        LoadAuthors(viewModel);
        LoadCategories(viewModel);
    }

    private void LoadAuthors(AddBookViewModel viewModel)
    {
        var authors = _authorRepository.GetAll();
        viewModel.LoadAuthors(authors);
    }

    private void LoadCategories(AddBookViewModel viewModel) 
    {
        var categories = _categoryRepository.GetAll();
        viewModel.LoadCategories(categories);
    }

    private async void OnAddBookButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddBookViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Title))
            {
                await MsgBox.ErrorAsync("Błąd", "Nie wpisano tytułu książki", this);
                return;
            }

            if (viewModel.PublicationYear <= 0 || viewModel.PublicationYear > DateTime.Now.Year)
            {
                await MsgBox.ErrorAsync("Błąd", $"Rok publikacji musi być większy od 0 oraz mniejszy niż obecny rok {DateTime.Now.Year}", this);
                return;
            }

            if (!viewModel.SelectedAuthors.Any() || !viewModel.SelectedCategories.Any())
            {
                await MsgBox.ErrorAsync("Błąd", "Nie wybrano żadnego autora lub kategorii", this);
                return;
            }


            var book = new Book()
            {
                Title = viewModel.Title,
                PublicationYear = viewModel.PublicationYear,
                BookCategories = viewModel.SelectedCategories.Select(c => {
                    var category = _categoryRepository.GetById(c.Id);

                    return new BookCategory()
                    {
                        CategoryId = category.Id,
                        Category = category
                    };
                }).ToList(),
                BookAuthors = viewModel.SelectedAuthors.Select(a => {
                    var author = _authorRepository.GetById(a.Id);

                    return new BookAuthor()
                    {
                        Author = author
                    };
                }).ToList()
            };

            await _bookRepository.AddAsync(book);
        }
        else 
        {
            await MsgBox.ErrorAsync("Błąd", "Wystąpił nieoczekiwany błąd podczas tworzenia nowej książki", this);
            return;
        }

        SetResultAndClose(WindowResult.OK);
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        SetResultAndClose(WindowResult.Closed);
    }
}