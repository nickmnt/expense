using ExpenseService.Data;
using ExpenseService.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Tests.ExpenseService
{
    public class ExpenseRepoTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ExpenseRepo _repo;

        public ExpenseRepoTests()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseTestDb")
                .Options;
            _context = new AppDbContext(options);
            _repo = new ExpenseRepo(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Food", ExternalId = "cat123" },
                new Category { Id = 2, Name = "Travel", ExternalId = "cat456" }
            };
            _context.Categories.AddRange(categories);

            var expenses = new List<Expense>
            {
                new Expense { Id = 1, Description = "Pizza", Amount = 10, CreatedAt = DateTime.UtcNow, CategoryId = 1 },
                new Expense { Id = 2, Description = "Train Ticket", Amount = 50, CreatedAt = DateTime.UtcNow, CategoryId = 2 }
            };
            _context.Expenses.AddRange(expenses);

            _context.SaveChanges();
        }

        [Fact]
        public void GetAllCategories_ShouldReturnAllCategories()
        {
            // Act
            var categories = _repo.GetAllCategories();

            // Assert
            Assert.Equal(2, categories.Count());
        }

        [Fact]
        public void CreateCategory_ShouldAddCategory()
        {
            // Arrange
            var newCategory = new Category { Name = "Health", ExternalId = "cat789" };

            // Act
            _repo.CreateCategory(newCategory);
            _repo.SaveChanges();

            // Assert
            var categories = _repo.GetAllCategories().ToArray();
            Assert.Equal(3, categories.Length);
            Assert.Contains(categories, c => c.Name == "Health");
        }

        [Fact]
        public void CategoryExists_ShouldReturnTrue_IfCategoryExists()
        {
            // Act
            var exists = _repo.CategoryExists(1);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void CategoryExists_ShouldReturnFalse_IfCategoryDoesNotExist()
        {
            // Act
            var exists = _repo.CategoryExists(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public void ExternalCategoryExists_ShouldReturnTrue_IfExternalCategoryExists()
        {
            // Act
            var exists = _repo.ExternalCategoryExists("cat123");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void ExternalCategoryExists_ShouldReturnFalse_IfExternalCategoryDoesNotExist()
        {
            // Act
            var exists = _repo.ExternalCategoryExists("invalidId");

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public void GetExpensesForCategory_ShouldReturnExpenses_ForValidCategory()
        {
            // Act
            var expenses = _repo.GetExpensesForCategory(1).ToArray();

            // Assert
            Assert.Single(expenses);
            Assert.Equal("Pizza", expenses.First().Description);
        }

        [Fact]
        public void GetExpense_ShouldReturnExpense_IfExists()
        {
            // Act
            var expense = _repo.GetExpense(1);

            // Assert
            Assert.NotNull(expense);
            Assert.Equal("Pizza", expense.Description);
        }

        [Fact]
        public void GetExpense_ShouldReturnNull_IfExpenseDoesNotExist()
        {
            // Act
            var expense = _repo.GetExpense(999);

            // Assert
            Assert.Null(expense);
        }

        [Fact]
        public void CreateExpense_ShouldAddExpense()
        {
            // Arrange
            var newExpense = new Expense { Description = "Medicine", Amount = 20, CreatedAt = DateTime.Now };

            // Act
            _repo.CreateExpense(1, newExpense);
            _repo.SaveChanges();

            // Assert
            var expenses = _repo.GetExpensesForCategory(1).ToArray();
            Assert.Equal(2, expenses.Length);
            Assert.Contains(expenses, e => e.Description == "Medicine");
        }
        
        [Fact]
        public void CreateExpense_ShouldAddExpense_WhenCategoryIdIsNull()
        {
            // Arrange
            var newExpense = new Expense { Description = "Uncategorized Expense", Amount = 20, CreatedAt = DateTime.Now };

            // Act
            _repo.CreateExpense(null, newExpense);
            _repo.SaveChanges();

            // Assert
            var expenses = _repo.GetExpensesForCategory(1); 
            Assert.Contains(expenses, e => e.Description == "Uncategorized Expense");
        }
        
        public void Dispose()
        {
            // Cleanup in-memory database
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
