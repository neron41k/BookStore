using BooksStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.DataAccess
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) 
            : base(options)
        {
        
        }
        public DbSet<BookEntity> Books { get; set; }
    }
}
