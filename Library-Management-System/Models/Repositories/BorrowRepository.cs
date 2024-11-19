using Library_Management_System.Models.Context;
using Library_Management_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Models.Repositories;

public class BorrowRepository(AppDbContext _context):IBorrowRepository
{
    public async Task<BorrowRecord?> GetBorrowRecord(int? borrowRecordId)
    {
        return await _context.BorrowRecords
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.BorrowRecordId == borrowRecordId);
    }

    public async Task<Book?> Create(BorrowViewModel model)
    {
        var book = await _context.Books.FindAsync(model.BookId);

        if (book != null && !book.IsAvailable)
        {
            var borrowRecord = new BorrowRecord
            {
                BookId = book.BookId,
                BorrowerName = model.BorrowerName,
                BorrowerEmail = model.BorrowerEmail,
                Phone = model.Phone,
                BorrowDate = DateTime.UtcNow
            };
        
            book.IsAvailable = false;
            await _context.BorrowRecords.AddAsync(borrowRecord);
            await _context.SaveChangesAsync();

        }
        
        return book;
    }

    public async Task<BorrowRecord?> Return(ReturnViewModel model)
    {
        var borrowRecord = await GetBorrowRecord(model.BorrowRecordId);
        if (borrowRecord != null && borrowRecord.ReturnDate == null)
        {
            // Update the borrow record
            borrowRecord.ReturnDate = DateTime.UtcNow;
            // Update the book's availability
            borrowRecord.Book.IsAvailable = true;
            await _context.SaveChangesAsync();

        }
        return borrowRecord;
        
    }
}