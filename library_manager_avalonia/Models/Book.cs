using System.Collections.Generic;

namespace library_manager_avalonia.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<RentalBook> RentalBooks { get; set; }
    }
}
