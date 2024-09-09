using BooksManager.Model;
using BooksManager.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Service
{
    public class BookCategoryService
    {
        private IAppDbContext _appDbContext;

        public BookCategoryService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<BookCategory> GetBooksCategories() 
        {
            return _appDbContext.BookCategories;
        }

        public async Task<BookCategory> CreateBookCategory(string name)
        {
            var bookCategory = new BookCategory
            {
                Name = name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var result = _appDbContext.BookCategories.Add(bookCategory);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> RemoveBookCategory(int id)
        {
            var bookCategory = _appDbContext.BookCategories.FirstOrDefault(x => x.Id == id);

            if (bookCategory == null)
                return false;

            var result = _appDbContext.BookCategories.Remove(bookCategory);
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }

        public async Task<bool> UpdateBookCategory(BookCategory bookCategory)
        {
            var result = _appDbContext.BookCategories.Add(bookCategory);
            _appDbContext.Entry(bookCategory).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }
    }
}
