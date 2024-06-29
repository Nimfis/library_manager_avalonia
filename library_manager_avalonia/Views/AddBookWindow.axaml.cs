using Avalonia.Controls;
using library_manager_avalonia.Services;

namespace library_manager_avalonia;

public partial class AddBookWindow : Window
{
    private readonly IBookService _bookService;

    public AddBookWindow(IBookService bookService)
    {
        _bookService = bookService;

        InitializeComponent();
    }
}