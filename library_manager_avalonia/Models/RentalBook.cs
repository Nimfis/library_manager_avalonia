namespace library_manager_avalonia.Models
{
    public class RentalBook
    {
        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
