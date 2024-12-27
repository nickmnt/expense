using CategoryService.Models;
using MongoDB.Entities;

namespace CategoryService.Data;

public class CategoryRepository : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        var query = DB.Find<Category>();
        query.Sort(x => x.Ascending(c => c.Name));
        var result = await query.ExecuteAsync();
        return result;
    }

    public Task<Category> GetCategoryById(string id)
    {
        return DB.Find<Category>().OneAsync(id);
    }

    public Task CreateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new ArgumentException("Category name is required.");
        }

        return category.SaveAsync();
    }
}