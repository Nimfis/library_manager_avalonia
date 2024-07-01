using System;

namespace library_manager_avalonia.Models
{
    public class Rental : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset RentalFrom { get; set; }
        public DateTimeOffset RentalTo { get; set; }
        public string RentalStatus { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
