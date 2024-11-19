using Library_Management_System.ViewModels;

namespace Library_Management_System.Models.Repositories;

public interface IBorrowRepository
{
    Task<BorrowRecord?> GetBorrowRecord(int? borrowRecordId);
    Task<Book?> Create(BorrowViewModel model);
    Task<BorrowRecord?> Return(ReturnViewModel model);
    
}