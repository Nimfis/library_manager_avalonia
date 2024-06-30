using library_manager_avalonia.Models;

namespace library_manager_avalonia.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public int OrderNr { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category, int orderNr)
        {
            Name = category.Name;
            OrderNr = orderNr;
        }
    }
}
