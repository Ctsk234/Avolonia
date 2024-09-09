using BooksManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace BooksManager.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=booksManager.db");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}
