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
    public class CheckoutsViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private CheckoutService _checkoutService;

        private Checkout _checkout;

        public Checkout Checkout
        {
            get => _checkout;
            set => this.RaiseAndSetIfChanged(ref _checkout, value);
        }

        private ObservableCollection<Checkout> _checkoutList;
        public ObservableCollection<Checkout> CheckoutList
        {
            get => _checkoutList;
            set => this.RaiseAndSetIfChanged(ref _checkoutList, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveCheckoutCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateCheckoutCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> ReturnCheckoutCommand { get; }

        public CheckoutsViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;

            RemoveCheckoutCommand = ReactiveCommand.Create(RemoveCheckout);
            CreateCheckoutCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowCreateCheckoutView());
            BackCommand = ReactiveCommand.Create(() => _mainWindowViewModel.ShowBooksView());
            ReturnCheckoutCommand = ReactiveCommand.Create(ReturnCheckout);

            _checkoutService = new CheckoutService(appDbContext);
            _checkoutList = new ObservableCollection<Checkout>(_checkoutService.GetCheckouts());
        }

        private async void RemoveCheckout()
        {
            if (Checkout == null) return;

            await _checkoutService.RemoveCheckout(Checkout.Id);
            _checkoutList = new ObservableCollection<Checkout>(_checkoutService.GetCheckouts());
            this.RaisePropertyChanged(nameof(CheckoutList));
        }

        private async void ReturnCheckout()
        {
            if (Checkout == null) return;

            if (Checkout.ReturnedDate != null)
                return;

            Checkout.ReturnedDate = DateTime.Now;

            await _checkoutService.UpdateCheckout(Checkout);
            _checkoutList = new ObservableCollection<Checkout>(_checkoutService.GetCheckouts());
            this.RaisePropertyChanged(nameof(CheckoutList));
        }
    }
}
