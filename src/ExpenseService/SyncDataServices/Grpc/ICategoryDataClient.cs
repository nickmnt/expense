using ExpenseService.Models;

namespace ExpenseService.SyncDataServices.Grpc;

public interface ICategoryDataClient
{
    IEnumerable<Category> ReturnAllCategories();
}