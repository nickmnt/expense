namespace ExpenseService.Dtos;

public class ExpenseReadDto
{
    public int Id { get; set; }

    public decimal Amount { get; set; }
    
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CategoryId { get; set; }
}