using System.Collections.Generic;

namespace library_manager_avalonia.Models
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
