using System.Collections.Generic;

namespace library_manager_avalonia.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
