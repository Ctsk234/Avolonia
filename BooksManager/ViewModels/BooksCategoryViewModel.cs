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
    public class BooksCategoryViewModel : ViewModelBase
    {
        private BookCategoryService _bookCategoryService;
        private MainWindowViewModel _mainWindowViewModel;

        private BookCategory _bookCategory;

        public BookCategory BookCategory
        {
            get => _bookCategory;
            set => this.RaiseAndSetIfChanged(ref _bookCategory, value);
        }

        private ObservableCollection<BookCategory> _bookCategoryList;
        public ObservableCollection<BookCategory> BookCategoryList
        {
            get => _bookCategoryList;
            set => this.RaiseAndSetIfChanged(ref _bookCategoryList, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveBookCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateBookCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateBookCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public BooksCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;

            RemoveBookCategoryCommand = ReactiveCommand.Create(RemoveBookCategory);
            CreateBookCategoryCommand = ReactiveCommand.Create(CreateBookCategory);
            UpdateBookCategoryCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowUpdateBookCategoryView(BookCategory));
            BackCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowBooksView());

            _bookCategoryService = new BookCategoryService(appDbContext);
            _bookCategoryList = new ObservableCollection<BookCategory>(_bookCategoryService.GetBooksCategories());
        }

        private async void RemoveBookCategory()
        {
            if (BookCategory == null) return;

            await _bookCategoryService.RemoveBookCategory(BookCategory.Id);
            _bookCategoryList = new ObservableCollection<BookCategory>(_bookCategoryService.GetBooksCategories());
            this.RaisePropertyChanged(nameof(BookCategoryList));
        }

        private void CreateBookCategory()
        {
            _mainWindowViewModel.ShowCreateBookCategoryCategoryView();
        }
    }
}
