using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.Models;

// This file defines the Book model representing books in the library
public class Book
{
    [BindNever] // used to explicitly prevent a property from being bound by model binding
    public int BookId { get; set; } // Primary key, not bound from user input
    
    [Required(ErrorMessage = "The Title field is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "The Author field is required.")]
    [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
    public string Author { get; set; }
    
    [Required(ErrorMessage = "The ISBN field is required.")]
    [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN must be in the format XXX-XXXXXXXXXX.")]
    public string ISBN { get; set; } // International Standard Book Number. It’s like a special code for every book.
    
    [Required(ErrorMessage = "The Published Date field is required.")]
    [DataType(DataType.Date)] // helps show that this information should be a date.
    [Display(Name = "Published Date")]  // it says "Published Date" instead of just “PublishedDate.” on a form or screen
    public DateTime PublishedDate { get; set; } // when the book was published or came out.
    
    [BindNever]
    [Display(Name = "Available")]
    public bool IsAvailable { get; set; } = true; // Default to available
    
    // Navigation Property
    [BindNever]
    public ICollection<BorrowRecord>? BorrowRecords { get; set; } // This keeps track of all the times people borrowed this book.
}