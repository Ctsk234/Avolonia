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
    public class UpdateBookCategoryViewModel : ViewModelBase
    {
        private BookCategory _bookCategory;
        private string _bookCategoryName;

        private String _ErrorMessage = "";

        private MainWindowViewModel _mainWindowViewModel;
        private BookCategoryService _bookCategoryService;

        public string BookCategoryName
        {
            get => _bookCategoryName;
            set => this.RaiseAndSetIfChanged(ref _bookCategoryName, value);
        }
        public String ErrorMessage
        {
            get => _ErrorMessage;
            set => this.RaiseAndSetIfChanged(ref _ErrorMessage, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateBookCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public UpdateBookCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, BookCategory bookCategory)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookCategoryService = new BookCategoryService(appDbContext);

            UpdateBookCategoryCommand = ReactiveCommand.Create(UpdateBookCategory);
            BackCommand = ReactiveCommand.Create(NavigateBack);

            _bookCategory = bookCategory;
            BookCategoryName = _bookCategory.Name;
        }

        private async void UpdateBookCategory()
        {
            if (string.IsNullOrWhiteSpace(BookCategoryName))
            {
                ErrorMessage = "Book category name is required";
                return;
            }

            _bookCategory.Name = BookCategoryName;
            _bookCategory.ModifiedDate = DateTime.Now;

            await _bookCategoryService.UpdateBookCategory(_bookCategory);
            _mainWindowViewModel.ShowBookCategoriesView();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowBookCategoriesView();
        }
    }
}
