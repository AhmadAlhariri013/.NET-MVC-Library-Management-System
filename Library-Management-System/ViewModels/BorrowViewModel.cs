using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.ViewModels;

// This serves as a view model used for borrowing books. It encapsulates the data required when a user borrows a book
// It ensures that necessary information is collected from the user when borrowing a book.
public class BorrowViewModel
{
    [Required]
    public int BookId { get; set; }
    
    [BindNever]
    public string? BookTitle { get; set; }
    
    [Required(ErrorMessage = "Your name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string BorrowerName { get; set; }
    
    [Required(ErrorMessage = "Your email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string BorrowerEmail { get; set; }
    
    [Required(ErrorMessage = "Your Phone Number is required.")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; }
}