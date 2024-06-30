using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using library_manager_avalonia.Models;
using ReactiveUI;

namespace library_manager_avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CategoryViewModel _selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public ObservableCollection<CategoryViewModel> Categories { get; private set; }

        public MainWindowViewModel()
        {
            Categories = new ObservableCollection<CategoryViewModel>();
        }

        public void LoadCategories(IEnumerable<Category> categories)
        {
            int orderNr = 1;
            var categoryViewModels = categories.Select(x => new CategoryViewModel(x, orderNr++)).ToList();

            if (Categories != null)
            {
                Categories.Clear();
                foreach (var category in categoryViewModels)
                {
                    Categories.Add(category);
                }
            }
        }
    }
}
