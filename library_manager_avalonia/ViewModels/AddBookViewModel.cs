using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using library_manager_avalonia.Models;
using System.Linq;

namespace library_manager_avalonia.ViewModels
{
    public class AddBookViewModel : ViewModelBase
    {
        private string _title;
        private int _publicationYear;

        private string _authorSearchText;
        private string _categorySearchText;

        private ObservableCollection<AuthorViewModel> _authors;
        private ObservableCollection<AuthorViewModel> _selectedAuthors;
        private ObservableCollection<AuthorViewModel> _filteredAuthors;
        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<CategoryViewModel> _selectedCategories;
        private ObservableCollection<CategoryViewModel> _filteredCategories;

        public AddBookViewModel()
        {
            PublicationYear = DateTime.Now.Year;

            Authors = new ObservableCollection<AuthorViewModel>();
            SelectedAuthors = new ObservableCollection<AuthorViewModel>();
            FilteredAuthors = new ObservableCollection<AuthorViewModel>();
            Categories = new ObservableCollection<CategoryViewModel>();
            SelectedCategories = new ObservableCollection<CategoryViewModel>();

        }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public int PublicationYear
        {
            get => _publicationYear;
            set => this.RaiseAndSetIfChanged(ref _publicationYear, value);
        }

        public string AuthorSearchText
        {
            get => _authorSearchText;
            set
            {
                this.RaiseAndSetIfChanged(ref _authorSearchText, value);
                FilterAuthors();
            }
        }

        public string CategorySearchText
        {
            get => _categorySearchText;
            set
            {
                this.RaiseAndSetIfChanged(ref _categorySearchText, value);
                FilterCategories();
            }
        }

        public ObservableCollection<AuthorViewModel> Authors
        {
            get => _authors;
            set => this.RaiseAndSetIfChanged(ref _authors, value);
        }

        public ObservableCollection<AuthorViewModel> SelectedAuthors
        {
            get => _selectedAuthors;
            set => this.RaiseAndSetIfChanged(ref _selectedAuthors, value);
        }

        public ObservableCollection<AuthorViewModel> FilteredAuthors
        {
            get => _filteredAuthors;
            set => this.RaiseAndSetIfChanged(ref _filteredAuthors, value);
        }

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public ObservableCollection<CategoryViewModel> SelectedCategories
        {
            get => _selectedCategories;
            set => this.RaiseAndSetIfChanged(ref _selectedCategories, value);
        }

        public ObservableCollection<CategoryViewModel> FilteredCategories
        {
            get => _filteredCategories;
            set => this.RaiseAndSetIfChanged(ref _filteredCategories, value);
        }

        public void LoadAuthors(IEnumerable<Author> authors)
        {
            foreach (var author in authors)
            {
                Authors.Add(new AuthorViewModel(author, 0));
            }

            FilterAuthors();
        }

        public void LoadCategories(IEnumerable<Category> categories) 
        {
            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category, 0));
            }

            FilterCategories();
        }

        private void FilterAuthors()
        {
            if (string.IsNullOrWhiteSpace(AuthorSearchText))
            {
                FilteredAuthors = new ObservableCollection<AuthorViewModel>(Authors);
            }
            else
            {
                FilteredAuthors = new ObservableCollection<AuthorViewModel>(
                    Authors.Where(a => (a.FirstName + a.LastName).Contains(AuthorSearchText, StringComparison.OrdinalIgnoreCase))
                );
            }
        }

        private void FilterCategories()
        {
            if (string.IsNullOrWhiteSpace(CategorySearchText))
            {
                FilteredCategories = new ObservableCollection<CategoryViewModel>(Categories);
            }
            else
            {
                FilteredCategories = new ObservableCollection<CategoryViewModel>(
                    Categories.Where(c => c.Name.Contains(CategorySearchText, StringComparison.OrdinalIgnoreCase))
                );
            }
        }
    }
}
