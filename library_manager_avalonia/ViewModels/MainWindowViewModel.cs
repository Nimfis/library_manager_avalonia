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

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get => _selectedAuthor;
            set => this.RaiseAndSetIfChanged(ref _selectedAuthor, value);
        }

        public ObservableCollection<CategoryViewModel> Categories { get; private set; }
        public ObservableCollection<AuthorViewModel> Authors { get; private set; }

        public MainWindowViewModel()
        {
            Categories = new ObservableCollection<CategoryViewModel>();
            Authors = new ObservableCollection<AuthorViewModel>();
        }

        public void LoadCategories(IEnumerable<Category> categories)
        {
            int orderNr = 1;
            var categoryViewModels = categories.Select(category => new CategoryViewModel(category, orderNr++)).ToList();

            if (Categories != null)
            {
                Categories.Clear();
                foreach (var category in categoryViewModels)
                {
                    Categories.Add(category);
                }
            }
        }

        public void LoadAuthors(IEnumerable<Author> authors)
        {
            int orderNr = 1;
            var authorViewModels = authors.Select(author => new AuthorViewModel(author, orderNr++)).ToList();

            if (Authors != null)
            {
                Authors.Clear();
                foreach (var author in authorViewModels)
                {
                    Authors.Add(author);
                }
            }
        }
    }
}
