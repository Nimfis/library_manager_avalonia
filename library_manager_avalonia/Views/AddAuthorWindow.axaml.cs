using Avalonia.Interactivity;
using library_manager_avalonia.Core.Enums;
using library_manager_avalonia.Core.Windows;
using library_manager_avalonia.Database;
using library_manager_avalonia.Models;
using library_manager_avalonia.ViewModels;

namespace library_manager_avalonia;

public partial class AddAuthorWindow : ReactiveWindowBase<AddAuthorViewModel>
{
    private readonly IRepository<Author> _authorRepository;

    public AddAuthorWindow()
    {
        InitializeComponent();
    }

    public AddAuthorWindow(AddAuthorViewModel viewModel, IRepository<Author> authorRepository)
    {
        DataContext = viewModel;

        _authorRepository = authorRepository;

        InitializeComponent();
    }

    private async void OnAddAuthorButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddAuthorViewModel viewModel)
        {
            await _authorRepository.AddAsync(new()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            });
        }

        SetResultAndClose(WindowResult.OK);
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        SetResultAndClose(WindowResult.Closed);
    }
}