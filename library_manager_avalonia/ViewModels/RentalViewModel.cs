using library_manager_avalonia.Models;

namespace library_manager_avalonia.ViewModels
{
    public class RentalViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public int OrderNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string RentalFrom { get; set; }
        public string RentalTo { get; set; }
        public string RentalStatus { get; set; }
        public string BookTitle { get; set; }

        public RentalViewModel()
        {            
        }

        public RentalViewModel(Rental rental, int orderNr)
        {
            Id = rental.Id;
            OrderNr = orderNr;
            FirstName = rental.FirstName;
            LastName = rental.LastName;
            Address = rental.Address;
            PhoneNumber = rental.PhoneNumber;
            RentalFrom = rental.RentalFrom.ToString("yyyy-MM-dd");
            RentalTo = rental.RentalTo.ToString("yyyy-MM-dd");
            RentalStatus = rental.RentalStatus;
            BookTitle = rental.Book is not null ? rental.Book.Title : string.Empty;

        }
    }
}
