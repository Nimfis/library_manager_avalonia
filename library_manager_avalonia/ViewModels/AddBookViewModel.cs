using ReactiveUI;

namespace library_manager_avalonia.ViewModels
{
    public class AddBookViewModel : ViewModelBase
    {
        private string _title;
        private int _publicationYear;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public int PublicationYear
        {
            get => _publicationYear;
            set => this.RaiseAndSetIfChanged(ref _publicationYear, value);
        }
    }
}
