using library_manager_avalonia.Models;

namespace library_manager_avalonia.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public int OrderNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AuthorViewModel()
        {    
        }

        public AuthorViewModel(Author author, int orderNr)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
            OrderNr = orderNr;
        }
    }
}
