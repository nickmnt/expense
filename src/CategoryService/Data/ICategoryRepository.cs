using CategoryService.Models;

namespace CategoryService.Data;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategoryById(string id);
    Task CreateCategory(Category category);
}