using BooksManager.Model;
using BooksManager.Persistence;
using BooksManager.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.ViewModels
{
    public class BooksViewModel : ViewModelBase
    {
        private BookService _bookService;
        private MainWindowViewModel _mainWindowViewModel;

        private Book _book;
        private int _sortByCount;
        private string _sortByButtonText = "ID";

        public Book Book
        {
            get => _book;
            set => this.RaiseAndSetIfChanged(ref _book, value);
        }
        public string SortByButtonText
        {
            get => _sortByButtonText;
            set => this.RaiseAndSetIfChanged(ref _sortByButtonText, value);
        }

        private ObservableCollection<Book> _bookList;
        public ObservableCollection<Book> BookList
        {
            get => _bookList;
            set => this.RaiseAndSetIfChanged(ref _bookList, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveBookCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> SortByCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCheckoutsViewCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowBookCategoriesViewCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCalculatorViewCommand { get; }

        public BooksViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;

            RemoveBookCommand = ReactiveCommand.Create(RemoveBook);
            CreateBookCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowCreateBookView());
            UpdateBookCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowUpdateBookView(Book));
            ShowCheckoutsViewCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowCheckoutsView());
            ShowBookCategoriesViewCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowBookCategoriesView());

            _bookService = new BookService(appDbContext);
            _bookList = new ObservableCollection<Book>(_bookService.GetBooks());
        }

        private async void RemoveBook()
        {
            if (Book == null) return;

            await _bookService.RemoveBook(Book.Id);
            _bookList = new ObservableCollection<Book>(_bookService.GetBooks());
            this.RaisePropertyChanged(nameof(BookList));
        }

    }
}
