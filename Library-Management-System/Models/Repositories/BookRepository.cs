using Library_Management_System.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Models.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var books = await _context.Books
            .Include(b => b.BorrowRecords)
            .AsNoTracking()
            .ToListAsync();
        return books;
    }

    public async Task<Book?> GetBookById(int? id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task AddBook(Book book)
    {
        // BookId and IsAvailable are not bound due to [BindNever]
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<string> UpdateBook(int? id ,Book book)
    {
        var existingBook = await GetBookById(id);
        if (existingBook == null)
        {
            return "NotFound";
        }
        // Updating fields that can be edited
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.ISBN = book.ISBN;
        existingBook.PublishedDate = book.PublishedDate;
        await _context.SaveChangesAsync();
        return "Success";
    }

    public async Task DeleteBook(Book book)
    {
        try
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<bool> BookExists(int id)
    {
        return await _context.Books.AnyAsync(e => e.BookId == id);
    }
}