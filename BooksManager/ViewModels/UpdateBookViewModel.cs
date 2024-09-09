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
    public class UpdateBookViewModel : ViewModelBase
    {
        private Book _book;

        private string _bookTitle;
        private string _bookAuthor;
        private BookCategory _bookCategory;

        private String _ErrorMessage;

        private MainWindowViewModel _mainWindowViewModel;
        private BookService _bookService;
        private BookCategoryService _bookCategoryService;

        private IEnumerable<BookCategory> _bookCategoriesList;
        public IEnumerable<BookCategory> BookCategoriesList
        {
            get => _bookCategoriesList;
            set => this.RaiseAndSetIfChanged(ref _bookCategoriesList, value);
        }

        public string BookTitle
        {
            get => _bookTitle;
            set => this.RaiseAndSetIfChanged(ref _bookTitle, value);
        }
        public string BookAuthor
        {
            get => _bookAuthor;
            set => this.RaiseAndSetIfChanged(ref _bookAuthor, value);
        }
        public BookCategory BookCategory
        {
            get => _bookCategory;
            set => this.RaiseAndSetIfChanged(ref _bookCategory, value);
        }
        public String ErrorMessage
        {
            get => _ErrorMessage;
            set => this.RaiseAndSetIfChanged(ref _ErrorMessage, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public UpdateBookViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Book book)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookService = new BookService(appDbContext);
            _bookCategoryService = new BookCategoryService(appDbContext);

            UpdateBookCommand = ReactiveCommand.Create(UpdateBook);
            BackCommand = ReactiveCommand.Create(NavigateBack);

            _bookCategoriesList = _bookCategoryService.GetBooksCategories();

            _book = book;
            _bookTitle = book.Title;
            _bookAuthor = book.Author;
            _bookCategory = book.BookCategory;
        }

        private async void UpdateBook()
        {
            if (string.IsNullOrEmpty(_bookTitle) || string.IsNullOrEmpty(_bookAuthor))
            {
                ErrorMessage = "Title and Author are required!";
                return;
            }

            if (_bookCategory == null)
            {
                ErrorMessage = "Please select a category!";
                return;
            }

            _book.Title = _bookTitle;
            _book.Author = _bookAuthor;
            _book.BookCategory = _bookCategory;
            _book.ModifiedDate = DateTime.Now;

            await _bookService.UpdateBook(_book);
            _mainWindowViewModel.ShowBooksView();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowBooksView();
        }
    }
}
