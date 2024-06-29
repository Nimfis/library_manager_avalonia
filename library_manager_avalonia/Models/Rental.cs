using System.Collections.Generic;

namespace library_manager_avalonia.Models
{
    public class Rental : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<RentalBook> RentalBooks { get; set; }
    }
}
