using CategoryService.Models;

namespace CategoryService.Data;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategoryById(int id);
    Task CreateCategory(Category category);
}