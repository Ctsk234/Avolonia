using BooksManager.Model;
using BooksManager.Persistence;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        private IAppDbContext _appDbContext;
        private User _currentUser;

        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public User CurrentUser
        {
            get => _currentUser;
            set => this.RaiseAndSetIfChanged(ref _currentUser, value);
        }

        public MainWindowViewModel()
        {
            _appDbContext = new AppDbContext();
            CurrentView = new LoginViewModel(this, _appDbContext);
        }

        public void ShowBooksView()
        {
           CurrentView = new BooksViewModel(this, _appDbContext);
        }

        public void ShowCreateBookView()
        {
            CurrentView = new CreateBookViewModel(this, _appDbContext);
        }

        public void ShowUpdateBookView(Book book)
        {
            if (book == null || book?.Id == null) return;

            CurrentView = new UpdateBookViewModel(this, _appDbContext, book);
        }

        public void ShowCreateCheckoutView()
        {
            CurrentView = new CreateCheckoutViewModel(this, _appDbContext);
        }

        public void ShowCheckoutsView()
        {
            CurrentView = new CheckoutsViewModel(this, _appDbContext);
        }

        public void ShowBookCategoriesView()
        {
            CurrentView = new BooksCategoryViewModel(this, _appDbContext);
        }

        public void ShowCreateBookCategoryCategoryView()
        {
            CurrentView = new CreateBookCategoryViewModel(this, _appDbContext);
        }

        public void ShowUpdateBookCategoryView(BookCategory bookCategory)
        {
            if (bookCategory == null || bookCategory?.Id == null) return;

            CurrentView = new UpdateBookCategoryViewModel(this, _appDbContext, bookCategory);
        }
    }
}
