using System;
using ReactiveUI;

namespace library_manager_avalonia.ViewModels
{
    public class AddRentalViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _phoneNumber;
        private DateTimeOffset? _rentalFrom;
        private DateTimeOffset? _rentalTo;
        private BookViewModel _selectedBook;

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

        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }

        public DateTimeOffset? RentalFrom
        {
            get => _rentalFrom;
            set => this.RaiseAndSetIfChanged(ref _rentalFrom, value);
        }

        public DateTimeOffset? RentalTo
        {
            get => _rentalTo;
            set => this.RaiseAndSetIfChanged(ref _rentalTo, value);
        }

        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set => this.RaiseAndSetIfChanged(ref _selectedBook, value);
        }

        public AddRentalViewModel()
        {
            _rentalFrom = DateTimeOffset.Now;
            _rentalTo = DateTimeOffset.Now.AddDays(7);
        }
    }
}
