using BooksManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace BooksManager.Persistence
{
    public interface IAppDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<BookCategory> BookCategories { get; set; }
        DbSet<Checkout> Checkouts { get; set; }
        Task<int> SaveChangesAsync();
        EntityEntry Entry(object entity);
    }
}
