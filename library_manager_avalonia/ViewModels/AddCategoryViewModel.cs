using ReactiveUI;

namespace library_manager_avalonia.ViewModels
{
    public class AddCategoryViewModel : ViewModelBase
    {
        private string _categoryName;

        public string CategoryName
        {
            get => _categoryName;
            set => this.RaiseAndSetIfChanged(ref _categoryName, value);
        }
    }
}
