using ExpenseService.Models;
using ExpenseService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Data;

public class Seed
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var grpcClient = serviceScope.ServiceProvider.GetService<ICategoryDataClient>();
        var logger = serviceScope.ServiceProvider.GetService<ILogger<Seed>>();

        if (grpcClient != null)
        {
            var categories = grpcClient.ReturnAllCategories();
                
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),
                serviceScope.ServiceProvider.GetService<IExpenseRepo>(), categories);
        }
        else
        {
            logger?.LogCritical("Could not get grpc client for receiving categories.");
            throw new InvalidOperationException("GrpcClient is required to proceed, but was not found.");
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