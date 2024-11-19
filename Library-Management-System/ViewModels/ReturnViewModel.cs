using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Management_System.ViewModels;

// This class serves as a view model for returning borrowed books.
// It helps display the necessary information during the return process.
public class ReturnViewModel
{
    [Required]
    public int BorrowRecordId { get; set; }
    
    [BindNever]
    public string? BookTitle { get; set; }
    
    [BindNever]
    public string? BorrowerName { get; set; }
    
    [BindNever]
    public DateTime? BorrowDate { get; set; }
}