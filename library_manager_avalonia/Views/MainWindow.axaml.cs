using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using library_manager_avalonia.Core.Enums;
using library_manager_avalonia.Core.Windows;
using library_manager_avalonia.Database;
using library_manager_avalonia.Helpers;
using library_manager_avalonia.Models;
using library_manager_avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace library_manager_avalonia.Views
{
    public partial class MainWindow : ReactiveWindowBase<MainWindowViewModel>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Category> _categoryRepository;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IServiceProvider serviceProvider, IRepository<Category> categoryRepository)
        {
            _serviceProvider = serviceProvider;
            _categoryRepository = categoryRepository;

            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }

        private void OnAddBookButtonClick(object? sender, RoutedEventArgs e)
        {
            //var addBookWindow = _serviceProvider.GetRequiredService<AddBookWindow>();
            //addBookWindow.Show();
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
                        RefreshCategories();
                    }
                }
            };

            addCategoryWindow.Show();
        }

        private void RefreshCategories()
        {
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel != null)
            {
                var categories = _categoryRepository.GetAll();
                viewModel.LoadCategories(categories);
            }
        }

        private void TabControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl?.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Kategorie")
            {
                RefreshCategories();
            }
        }
    }
}