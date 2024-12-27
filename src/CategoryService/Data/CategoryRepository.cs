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

    public async Task CreateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new ArgumentException("Category name is required.");
        }
        
        var existingCategory = await DB.Find<Category>().OneAsync(category.ID);
        
        if (existingCategory != null)
        {
            throw new ArgumentException("Category with the same ID already exists.");
        }

        await category.SaveAsync();
    }
}