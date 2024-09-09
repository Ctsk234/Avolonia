using BooksManager.Model;
using BooksManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Service
{
    public class BookService
    {
        private IAppDbContext _appDbContext;

        public BookService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Book> GetBooks() 
        {
            return _appDbContext.Books;
        }

        public async Task<Book> CreateBook(string title, string author, BookCategory bookCategory)
        {
            var newBook = new Book()
            {
                Title = title,
                Author = author,
                BookCategory = bookCategory,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var result = _appDbContext.Books.Add(newBook);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> RemoveBook(int id)
        {
            var bookToRemove = _appDbContext.Books.First(n => n.Id == id);
            var result = _appDbContext.Books.Remove(bookToRemove);
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }
        public async Task<bool> UpdateBook(Book book)
        {
            var result = _appDbContext.Books.Add(book);
            _appDbContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return result != null;
        }
    }
}
