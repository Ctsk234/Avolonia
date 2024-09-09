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
    public class CreateBookViewModel : ViewModelBase
    {
        private string _bookTitle;
        private string _bookAuthor;
        private BookCategory _bookCategory;

        private String _ErrorMessage = "";

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

        public ReactiveCommand<Unit, Unit> CreateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateBookViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookService = new BookService(appDbContext);
            _bookCategoryService = new BookCategoryService(appDbContext);

            CreateBookCommand = ReactiveCommand.Create(CreateBook);
            BackCommand = ReactiveCommand.Create(NavigateBack);
  

            _bookCategoriesList = _bookCategoryService.GetBooksCategories();
        }

        private async void CreateBook()
        {
            if (string.IsNullOrEmpty(_bookTitle) || string.IsNullOrEmpty(_bookAuthor) || _bookCategory == null)
            {
                ErrorMessage = "Please fill all fields";
                return;
            }

            var book = await _bookService.CreateBook(_bookTitle, _bookAuthor, _bookCategory);

            if (book != null)
            {
                _mainWindowViewModel.ShowBooksView();
            }
            else
            {
                ErrorMessage = "Error creating book";
            }
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowBooksView();
        }
    }
}
