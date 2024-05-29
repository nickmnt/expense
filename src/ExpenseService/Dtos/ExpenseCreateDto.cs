using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Dtos;

public class ExpenseCreateDto
{
    [Required]
    public decimal Amount { get; set; }
    
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}