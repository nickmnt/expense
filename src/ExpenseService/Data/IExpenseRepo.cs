using ExpenseService.Models;

namespace ExpenseService.Data;

public interface IExpenseRepo
{
    bool SaveChanges();
    
    // Categories
    IEnumerable<Category> GetAllCategories();
    void CreateCategory(Category category);
    bool CategoryExists(int categoryId);
    bool ExternalCategoryExists(string externalCategoryId);

    // Expenses
    IEnumerable<Expense> GetExpensesForCategory(int categoryId);
    Expense? GetExpense(int expenseId);
    Expense CreateExpense(int? categoryId, Expense expense);
}