using CategoryService.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CategoryService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("CategoryDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Category>()
            .Key(x => x.Id, type: KeyType.Ascending)
            .CreateAsync();

        var count = await DB.CountAsync<Category>();

        if (count == 0)
        {
            await Seed();
        }
    }

    private static async Task Seed()
    {
        var categories = new List<Category>()
        {
            new Category {Id = 1, Name = "Salary"},
            new Category {Id = 2, Name = "Utilities"},
            new Category {Id = 3, Name = "Rent"}
        };

        await DB.SaveAsync(categories);
    }
}