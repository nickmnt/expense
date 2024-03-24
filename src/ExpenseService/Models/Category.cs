using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ExternalId { get; set; }

    [Required]
    public string Name { get; set; }

    public IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
}