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
    public class CreateBookCategoryViewModel : ViewModelBase
    {
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

        public ReactiveCommand<Unit, Unit> CreateBookCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateBookCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookCategoryService = new BookCategoryService(appDbContext);

            CreateBookCategoryCommand = ReactiveCommand.Create(CreateBookCategory);
            BackCommand = ReactiveCommand.Create(NavigateBack);
        }

        private async void CreateBookCategory()
        {
            if (string.IsNullOrWhiteSpace(BookCategoryName))
            {
                ErrorMessage = "Book category name is required";
                return;
            }

            var book = await _bookCategoryService.CreateBookCategory(BookCategoryName);

            if (book != null)
            {
                _mainWindowViewModel.ShowBookCategoriesView();
            }
            else
            {
                ErrorMessage = "Error creating book category";
            }
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowBookCategoriesView();
        }
    }
}
