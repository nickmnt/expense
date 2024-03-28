using ExpenseService.Models;
using ExpenseService.SyncDataServices.Grpc;

namespace ExpenseService.Data;

public class Seed
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<ICategoryDataClient>();

            var categories = grpcClient.ReturnAllCategories();
                
            SeedData(serviceScope.ServiceProvider.GetService<IExpenseRepo>(), categories);
        }
    }

    private static void SeedData(IExpenseRepo repo, IEnumerable<Category> categories)
    {
        Console.Out.WriteLine("Seeding new categories...");

        foreach (var cat in categories)
        {
            if (!repo.ExternalCategoryExists(cat.ExternalId))
            {
                repo.CreateCategory(cat);
            }

            repo.SaveChanges();
        } 
    }
}