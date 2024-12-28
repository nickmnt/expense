using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(512)]
    public required string ExternalId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
}