using BooksStore.DataAccess.Entities;
using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.DataAccess.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDBContext _context;

        public BooksRepository(BookStoreDBContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.ID, b.Title, b.Description, b.Price).Book)
                .ToList();

            return books;
        }
        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                ID = book.ID,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.ID;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Books
               .Where(b => b.ID == id)
               .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, b => title)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Price, b => price));

            return id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books
                .Where(b => b.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
