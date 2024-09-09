using BooksManager.Model;
using BooksManager.Persistence;
using BooksManager.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.ViewModels
{
    public class CreateCheckoutViewModel : ViewModelBase
    {
        private Book _checkoutBook;
        private String _checkoutPhoneNumber;
        private String _checkoutFrom;
        private String _checkoutTo;

        private String _ErrorMessage;

        private MainWindowViewModel _mainWindowViewModel;
        private CheckoutService _checkoutService;

        public Book CheckoutBook
        {
            get => _checkoutBook;
            set => this.RaiseAndSetIfChanged(ref _checkoutBook, value);
        }

        public String CheckoutPhoneNumber
        {
            get => _checkoutPhoneNumber;
            set => this.RaiseAndSetIfChanged(ref _checkoutPhoneNumber, value);
        }

        public String CheckoutFrom
        {
            get => _checkoutFrom;
            set => this.RaiseAndSetIfChanged(ref _checkoutFrom, value);
        }

        public String CheckoutTo
        {
            get => _checkoutTo;
            set => this.RaiseAndSetIfChanged(ref _checkoutTo, value);
        }

        public String ErrorMessage
        {
            get => _ErrorMessage;
            set => this.RaiseAndSetIfChanged(ref _ErrorMessage, value);
        }

        private IEnumerable<Book> _bookList;
        public IEnumerable<Book> BookList
        {
            get => _bookList;
            set => this.RaiseAndSetIfChanged(ref _bookList, value);
        }

        public ReactiveCommand<Unit, Unit> CreateCheckoutCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateCheckoutViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _checkoutService = new CheckoutService(appDbContext);

            CreateCheckoutCommand = ReactiveCommand.Create(CreateCheckout);
            BackCommand = ReactiveCommand.Create(NavigateBack);

            BookList = (new BookService(appDbContext)).GetBooks();
        }

        private async void CreateCheckout()
        {
            if (CheckoutBook == null)
            {
                ErrorMessage = "Please select a book";
                return;
            }

            if (string.IsNullOrEmpty(_checkoutPhoneNumber))
            {
                ErrorMessage = "Please enter a phone number";
                return;
            }

            if (_checkoutFrom == null)
            {
                ErrorMessage = "Please select a checkout date";
                return;
            }

            if (_checkoutTo == null)
            {
                ErrorMessage = "Please select a return date";
                return;
            }

            if (CheckoutBook.Checkouts != null && CheckoutBook.Checkouts.Any(c => c.ReturnedDate == null))
            {
                ErrorMessage = "Book is already checked out";
                return;
            }

            var from = DateTime.Parse(_checkoutFrom);
            var to = DateTime.Parse(_checkoutTo);

            var book = await _checkoutService.CreateCheckout(CheckoutBook, _mainWindowViewModel.CurrentUser, from, to, _checkoutPhoneNumber);

            if (book != null)
            {
                _mainWindowViewModel.ShowCheckoutsView();
            }
            else
            {
                ErrorMessage = "Error creating book category";
            }
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowCheckoutsView();
        }
    }
}
