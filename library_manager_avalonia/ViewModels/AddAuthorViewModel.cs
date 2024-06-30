using ReactiveUI;

namespace library_manager_avalonia.ViewModels
{
    public class AddAuthorViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }
    }
}
