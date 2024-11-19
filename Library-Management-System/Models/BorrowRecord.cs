using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.Models;

// This class defines the BorrowRecord model for tracking book borrowing and return.
// This model links a book to its borrower and records transaction dates.
public class BorrowRecord
{
    [Key]
    public int BorrowRecordId { get; set; } //PK
    
    [Required]
    public int BookId { get; set; } //FK
    
    [Required(ErrorMessage = "Please enter Borrower Name")]
    public string BorrowerName { get; set; }
    
    [Required(ErrorMessage = "Please enter Borrower Email Address")]
    [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
    public string BorrowerEmail { get; set; }
    
    [Required(ErrorMessage = "Please enter Borrower Phone Number")]
    [Phone(ErrorMessage = "Please enter a Valid Phone Number")]
    public string Phone { get; set; }
    
    [BindNever]
    [DataType(DataType.DateTime)]
    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime? ReturnDate { get; set; }
    
    // navigation link back to the Book class.
    [BindNever]
    public Book Book { get; set; }
}