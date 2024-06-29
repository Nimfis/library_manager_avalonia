using Avalonia.Controls;
using Avalonia.Interactivity;
using library_manager_avalonia.Services;
using library_manager_avalonia.ViewModels;

namespace library_manager_avalonia;

public partial class AddCategoryWindow : Window
{
    private readonly ICategoryService _categoryService;

    public AddCategoryWindow()
    {
        InitializeComponent();
    }

    public AddCategoryWindow(AddCategoryViewModel viewModel, ICategoryService categoryService)
    {
        DataContext = viewModel;

        _categoryService = categoryService;

        InitializeComponent();
    }

    private async void OnAddCategoryButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddCategoryViewModel viewModel)
        {
            await _categoryService.AddCategory(new()
            {
                Name = viewModel.CategoryName
            });
        }

        this.Close();
    }
}