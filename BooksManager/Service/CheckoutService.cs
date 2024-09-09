using BooksManager.Model;
using BooksManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Service
{
    public class CheckoutService
    {
        private IAppDbContext _appDbContext;

        public CheckoutService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Checkout> GetCheckouts() 
        {
            return _appDbContext.Checkouts;
        }

        public async Task<Checkout> CreateCheckout(Book book, User user, DateTime from, DateTime to, String phoneNumber)
        {
            var newCheckout = new Checkout()
            {
                Book = book,
                User = user,
                FromDate = from,
                ToDate = to,
                PhoneNumber = phoneNumber,
                CreatedDate = DateTime.Now,
                ReturnedDate = null,
            };

            var result = _appDbContext.Checkouts.Add(newCheckout);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> RemoveCheckout(int id)
        {
            var bookToRemove = _appDbContext.Checkouts.First(n => n.Id == id);
            var result = _appDbContext.Checkouts.Remove(bookToRemove);
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }

        public async Task<bool> UpdateCheckout(Checkout checkout)
        {
            var result = _appDbContext.Checkouts.Add(checkout);
            _appDbContext.Entry(checkout).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }
    }
}
