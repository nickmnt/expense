using ExpenseService.Models;

namespace ExpenseService.Data;

public class ExpenseRepo : IExpenseRepo
{
    private readonly AppDbContext _context;

    public ExpenseRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        var categories = _context.Categories.ToList();

        return categories;
    }

    public void CreateCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        
        _context.Categories.Add(category);
    }

    public bool CategoryExists(int categoryId)
    {
        return _context
            .Categories
            .Any(c => c.Id == categoryId);
    }

    public bool ExternalCategoryExists(string externalCategoryId)
    {
        return _context.Categories.Any(c => c.ExternalId == externalCategoryId);
    }

    public IEnumerable<Expense> GetExpensesForCategory(int categoryId)
    {
        return _context.Expenses
            .Where(c => c.CategoryId == categoryId)
            .OrderBy(e => e.CreatedAt)
            .ToList();
    }

    public Expense? GetExpense(int expenseId)
    {
        return _context.Expenses
            .FirstOrDefault(e => e.Id == expenseId);
    }

    public void CreateExpense(int? categoryId, Expense expense)
    {
        ArgumentNullException.ThrowIfNull(expense);

        expense.CategoryId = categoryId;
        _context.Expenses.Add(expense);
    }
}