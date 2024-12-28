using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Models;

public class Expense
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public decimal Amount { get; set; }
    
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}