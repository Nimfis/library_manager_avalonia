using System.Linq;
using System.Collections.Generic;
using library_manager_avalonia.Models;

namespace library_manager_avalonia.ViewModels
{
    public class BookViewModel
    {
        public int OrderNr { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }

        public string AuthorsDisplay
        {
            get
            {
                return string.Join(", ", Authors.Select(x => x.FirstName + " " + x.LastName));
            }
        }
        public string CategoriesDisplay
        {
            get
            {
                return string.Join(", ", Categories.Select(x => x.Name));
            }
        }

        public BookViewModel()
        {
            Categories = new List<CategoryViewModel>();
            Authors = new List<AuthorViewModel>();
        }

        public BookViewModel(Book book, int orderNr)
        { 
            Id = book.Id;
            OrderNr = orderNr;
            Title = book.Title;
            PublicationYear = book.PublicationYear;

            Authors = book.BookAuthors.Select(x => new AuthorViewModel(x.Author, 0));
            Categories = book.BookCategories.Select(x => new CategoryViewModel(x.Category, 0));
        }
    }
}
