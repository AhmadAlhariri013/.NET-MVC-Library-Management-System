namespace Library_Management_System.Models.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book?> GetBookById(int? id);
    Task AddBook(Book book);
    Task<string> UpdateBook(int? id, Book book);
    Task DeleteBook(Book book);
    Task<bool> BookExists(int id);
}