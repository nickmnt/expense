using ExpenseService.Models;
using ExpenseService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Data;

public class Seed
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<ICategoryDataClient>();

            var categories = grpcClient.ReturnAllCategories();
                
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),
                serviceScope.ServiceProvider.GetService<IExpenseRepo>(), categories);
        }
    }

    private static void SeedData(AppDbContext context, IExpenseRepo repo, IEnumerable<Category> categories)
    {
        Console.Out.WriteLine("Seeding new categories...");
        
        Console.Out.WriteLine("--> Attempting to apply migrations...");
        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine($"--> Could not run migrations: {ex.Message}");
        }

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