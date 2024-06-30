using library_manager_avalonia.Models;

namespace library_manager_avalonia.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNr { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category, int orderNr)
        {
            Id = category.Id;
            Name = category.Name;
            OrderNr = orderNr;
        }
    }
}
