using Library_Management_System.Models;
using Library_Management_System.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _repository;

    public BookController(IBookRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IActionResult> Index()
    {
        try
        {
            var books = await _repository.GetAllBooks();
            return View(books);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while loading the books.";
            return View("Error");
        }
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id == 0)
        {
            TempData["ErrorMessage"] = "Book ID was not provided.";
            return View("NotFound");
        }
        try
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id}.";
                return View("NotFound");
            }
            return View(book);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while loading the book details.";
            return View("Error");
        }
    }
    
    // GET: Books/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            try
            {
                 await _repository.AddBook(book);
                TempData["SuccessMessage"] = $"Successfully added the book: {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the book.";
                return View(book);
            }
        }
        return View(book);
    }
    
    // GET: Books/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            TempData["ErrorMessage"] = "Book ID was not provided for editing.";
            return View("NotFound");
        }
        try
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for editing.";
                return View("NotFound");
            }
            return View(book);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while loading the book for editing.";
            return View("Error");
        }
    }
    // POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Book book)
    {
        if (id == null || id == 0)
        {
            TempData["ErrorMessage"] = "Book ID was not provided for updating.";
            return View("NotFound");
        }
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _repository.UpdateBook(id , book);
                if (result == "NotFound")
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for updating.";
                    return View("NotFound");
                }
                
                TempData["SuccessMessage"] = $"Successfully updated the book: {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (! await _repository.BookExists(book.BookId))
                {
                    TempData["ErrorMessage"] = $"No book found with ID {book.BookId} during concurrency check.";
                    return View("NotFound");
                }
                else
                {
                    TempData["ErrorMessage"] = "A concurrency error occurred during the update.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the book.";
                return View("Error");
            }
        }
        return View(book);
    }
    
    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            TempData["ErrorMessage"] = "Book ID was not provided for deletion.";
            return View("NotFound");
        }
        try
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                return View("NotFound");
            }
            return View(book);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while loading the book for deletion.";
            return View("Error");
        }
    }
    
    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                return View("NotFound");
            }

            await _repository.DeleteBook(book);
            TempData["SuccessMessage"] = $"Successfully deleted the book: {book.Title}.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while deleting the book.";
            return View("Error");
        }
    }
}