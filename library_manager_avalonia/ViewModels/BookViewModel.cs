using library_manager_avalonia.Models;
using System.Collections.Generic;

namespace library_manager_avalonia.ViewModels
{
    public class BookViewModel
    {
        public int OrderNr { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<AuthorViewModel> Author { get; set; }

        public BookViewModel()
        {
            Categories = new List<CategoryViewModel>();
            Author = new List<AuthorViewModel>();
        }

        public BookViewModel(Book book, int orderNr)
        { 
            Id = book.Id;
            OrderNr = orderNr;
            Title = book.Title;
            PublicationYear = book.PublicationYear;
        }
    }
}
