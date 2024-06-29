using Avalonia.Controls;
using Avalonia.Interactivity;
using library_manager_avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Threading.Tasks;

namespace library_manager_avalonia.Views
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
        }

        private void OnAddBookButtonClick(object? sender, RoutedEventArgs e)
        {
            //var addBookWindow = _serviceProvider.GetRequiredService<AddBookWindow>();
            //addBookWindow.Show();
        }

        private async void OnAddCategoryButtonClick(object? sender, RoutedEventArgs e)
        {
            var addCategoryWindow = _serviceProvider.GetRequiredService<AddCategoryWindow>();

            addCategoryWindow.Closed += async (s, args) =>
            {
                var window = s as AddCategoryWindow;
                if (window != null)
                {
                    var viewModel = window.DataContext as AddCategoryViewModel;

                    if (viewModel != null)
                    {
                        await ShowMessageBoxAsync("Category Added", $"Category '{viewModel.CategoryName}' was added successfully.");
                    }
                }
            };

            addCategoryWindow.Show();
        }

        private async Task ShowMessageBoxAsync(string title, string message)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = title,
                ContentMessage = message,
                Icon = MsBox.Avalonia.Enums.Icon.Info,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });

            await messageBoxStandardWindow.ShowAsync();
        }
    }
}